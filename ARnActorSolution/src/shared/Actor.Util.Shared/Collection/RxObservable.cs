﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Base;

namespace Actor.Util
{

    public class RXObservable<T> : BaseActor, IObservable<T>
    {
        private List<IObserver<T>> observers;
        public RXObservable()
        {
            observers = new List<IObserver<T>>();
            var bhv1 = new Behavior<IObserver<T>>(DoSubscribe);
            var bhv2 = new Behavior<T>(DoTrack);

            Become(bhv1);
            AddBehavior(bhv2);
        }

        private void DoSubscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            IDisposable dispo = new Unsubscriber(observers, observer);
            this.SendMessage(this, dispo);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            Task<object> res = Receive(t => { return t is IMessageParam<IActor, IDisposable>; });
            SendMessage(observer);
            var resi = res.Result as IMessageParam<IActor, IDisposable>;
            return resi.Item2;
        }

        public void Track(T loc)
        {
            SendMessage(loc);
        }

        private void DoTrack(T loc)
        {
            foreach (var observer in observers)
            {
                if (loc == null)
                    observer.OnError(new ActorException());
                else
                    observer.OnNext(loc);
            }
        }

        private void DoEndTransmission(T observer)
        {
            foreach (var item in observers.ToArray())
                if (observers.Contains(item))
                    item.OnCompleted();

            observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}