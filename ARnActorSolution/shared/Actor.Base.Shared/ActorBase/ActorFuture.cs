﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actor.Base
{

    public class Future<T> : BaseActor, IFuture<T>
    {
        public Future()
        {
        }

        public async Task<object> GetResultAsync()
        {
            return await ResultAsync();
        }

        public T Result() => (T)Receive(t => t is T).Result;

        public T Result(int timeOutMS) => (T)Receive(t => t is T, timeOutMS).Result;

        public async Task<T> ResultAsync()
        {
            return (T)await Receive(t => t is T);
        }

        public async Task<T> ResultAsync(int timeOutMS)
        {
            return (T)await Receive(t => t is T, timeOutMS);
        }
    }

    public class Future<T1,T2> : BaseActor, IFuture<T1, T2>
    {
        public Future()
        {
        }

        public async Task<object> GetResultAsync()
        {
            return await ResultAsync();
        }

        public Tuple<T1,T2> Result() => (Tuple<T1, T2>)Receive(t => t is Tuple<T1, T2>).Result;

        public Tuple<T1, T2> Result(int timeOutMS) => (Tuple<T1, T2>)Receive(t => t is Tuple<T1, T2>, timeOutMS).Result;

        public async Task<Tuple<T1, T2>> ResultAsync()
        {
            return (Tuple < T1, T2 >) await Receive(t => t is Tuple<T1, T2>);
        }

        public async Task<Tuple<T1, T2>> ResultAsync(int timeOutMS)
        {
            return (Tuple<T1, T2>)await Receive(t => t is Tuple<T1, T2>,timeOutMS);
        }
    }

    public class Future<T1, T2, T3> : BaseActor, IFuture<T1, T2, T3>
    {
        public Future()
        {
        }

        public async Task<object> GetResultAsync()
        {
            return await ResultAsync();
        }

        public Tuple<T1, T2, T3> Result() => (Tuple<T1, T2, T3>)Receive(t => t is Tuple<T1, T2, T3>).Result;

        public Tuple<T1, T2, T3> Result(int timeOutMS) => (Tuple<T1, T2, T3>)Receive(t => t is Tuple<T1, T2, T3>, timeOutMS).Result;

        public async Task<Tuple<T1, T2, T3>> ResultAsync()
        {
            return (Tuple<T1, T2, T3>)await Receive(t => t is Tuple<T1, T2, T3>);
        }

        public async Task<Tuple<T1, T2, T3>> ResultAsync(int timeOutMS)
        {
            return (Tuple<T1, T2, T3>)await Receive(t => t is Tuple<T1, T2, T3>, timeOutMS);
        }
    }

}