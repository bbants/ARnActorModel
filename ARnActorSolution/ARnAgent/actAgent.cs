﻿using Actor.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARnAgent
{
    public enum ActorAtom {Kill} ;
    public enum AgentAtom {Stop} ;

    public class actAgent : actActor, IAgent
    {
        protected string AgentName;
        private bhvBehavior<DeltaTimeMessage> bhvReact;

        public actAgent()
            : base()
        {
            Become(new bhvBehavior<string>(DoInitState));
        }

        public void Start(string anAgentName)
        {
            SendMessage(anAgentName);
        }


        private void DoInitState(string msg)
        {
            InitState(msg);
        }

        public virtual void InitState(string anAgentName)
        {
            AgentName = anAgentName;
            bhvReact = new bhvBehavior<DeltaTimeMessage>(DoReact) ;
            var bhvObserve = new bhvBehavior<Tuple<IActor,DeltaTime>>(DoObserve);
            var bhvKill = new bhvBehavior<ActorAtom>(DoKill);
            var bhvStop = new bhvBehavior<AgentAtom>(DoStop);
            var bhvMany = new Behaviors() ;
            bhvMany.AddBehavior(bhvReact) ;
            bhvMany.AddBehavior(bhvObserve) ;
            bhvMany.AddBehavior(bhvKill) ;
            actSendByName<IAgent>.SendByName(this, "AgentDirectory");
            BecomeMany(bhvMany);
        }

        private void DoStop(AgentAtom msg)
        {
            RemoveBehavior(bhvReact);
        }

        private void DoReact(DeltaTimeMessage Dtm)
        {
            React(Dtm);
        }

        public virtual void React(DeltaTimeMessage Dtm)
        {

        }

        public virtual string GetObserveMessage(long dt)
        {
            return string.Empty;
        }

        private void DoObserve(Tuple<IActor, DeltaTime> msg)
        {
            string s = GetObserveMessage(msg.Item2.asDt);
            if (! string.IsNullOrEmpty(s))
              msg.Item1.SendMessage(string.Format("{0} {1} {2}", msg.Item2.asDt, AgentName, s)); 
        }


        private void DoKill(ActorAtom msg)
        {
            Become(null);
        }

        public virtual void Kill()
        {

        }


    }
}