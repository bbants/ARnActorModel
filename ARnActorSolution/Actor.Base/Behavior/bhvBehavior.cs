﻿/*****************************************************************************
		               ARnActor Actor Model Library .Net
     
	 Copyright (C) {2015}  {ARn/SyndARn} 
 
 
     This program is free software; you can redistribute it and/or modify 
     it under the terms of the GNU General Public License as published by 
     the Free Software Foundation; either version 2 of the License, or 
     (at your option) any later version. 
 
 
     This program is distributed in the hope that it will be useful, 
     but WITHOUT ANY WARRANTY; without even the implied warranty of 
     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
     GNU General Public License for more details. 
 
 
     You should have received a copy of the GNU General Public License along 
     with this program; if not, write to the Free Software Foundation, Inc., 
     51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA. 
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actor.Base
{
    /// <summary>
    /// Behaviors holds many behaviors.
    /// Behaviors are actor brain.
    /// Behaviors when null in an actor means this actor is dead (it can't change anymore his own behavior from this point)
    /// </summary>
    public class Behaviors
    {
        private List<IBehavior> fList = new List<IBehavior>();

        public actActor LinkedActor { get; private set; }

        public Behaviors()
        {
        }

        public void LinkToActor(actActor anActor)
        {
            LinkedActor = anActor;
        }

        public void AddBehavior(IBehavior aBehavior)
        {
            if (aBehavior != null)
            {
                aBehavior.LinkBehaviors(this);
                fList.Add(aBehavior);
            }
        }
        public void RemoveBehavior(IBehavior aBehavior)
        {
            aBehavior.LinkBehaviors(null);
            fList.Remove(aBehavior);
        }
        public IEnumerable<IBehavior> GetBehaviors()
        {
            return fList ;
        }
        public bool NotEmpty
        {
            get { return fList.Count > 0; }
        }
    }

    /// <summary>
    /// bhvBehavior
    /// A behavior is describe with two properties : Pattern and Apply.
    /// At message reception, it's tested against each Pattern and if it succeeded, 
    /// the Apply is invoke with this message as parameter.
    /// Patterns order can be relevant in this process.
    /// Type is the acq of the message to be send to this behavior
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class bhvBehavior<T> : IBehavior<T>, IBehavior 
    {
        public Func<T,Boolean> Pattern { get; protected set; }
        public Action<T> Apply { get; protected set; }
        private Behaviors fLinkedBehaviors;

        public Behaviors LinkedTo()
        {
            return fLinkedBehaviors;
        }

        public void LinkBehaviors(Behaviors someBehaviors)
        {
            fLinkedBehaviors = someBehaviors;
        }

        public bhvBehavior(Func<T, Boolean> aPattern, Action<T> anApply)
        {
            Pattern = aPattern;
            Apply = anApply;
        }
        
        public bhvBehavior()
        {
        }
        
        public void SendMessageTo(Object aData,IActor Target)
        {
            this.LinkedTo().LinkedActor.SendMessageTo(aData,Target);
        }
    
        public bhvBehavior(Action<T> anApply)
        {
            Pattern = t => t is T;
            Apply = anApply;
        }

        public Boolean StandardPattern(Object aT)
        {
            if (Pattern == null)
                return false;
            if (aT is T)
                return Pattern((T)aT);
            else
                return false;
        }

        public void StandardApply(Object aT)
        {
            if (Apply != null)
            {
                Apply((T)aT) ;
            }
        }
    }
}
    