﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Actor.Base;
using Procurios.Public;

namespace Actor.Server
{
    public static class NetDataActorSerializer
    {
        public static SerialObject DeSerialize(Stream inputStream)
        {
            CheckArg.Stream(inputStream);
            inputStream.Seek(0, SeekOrigin.Begin);
            NetDataContractSerializer dcs = new NetDataContractSerializer()
            {
                SurrogateSelector = new ActorSurrogatorSelector(),
                Binder = new ActorBinder()
            };
            return (SerialObject)dcs.ReadObject(inputStream);
        }

        public static void Serialize(SerialObject so, Stream outputStream)
        {
            NetDataContractSerializer dcs = new NetDataContractSerializer()
            {
                SurrogateSelector = new ActorSurrogatorSelector(),
                Binder = new ActorBinder()
            };
            dcs.Serialize(outputStream, so);
        }
    }
}
