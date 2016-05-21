﻿using Actor.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestActor
{
    public class TestLauncherActor : ActionActor
    {
        public TestLauncherActor()
            : base()
        {
        }

        public void Finish()
        {
            SendMessage(true);
        }

        public bool Wait()
        {
            return Wait(10000);
        }

        public bool Wait(int ms)
        {
            var val = Receive(t => t is bool);
            var inTime = val.Wait(ms);
            return inTime && (bool)val.Result;
        }

        public static void Test(Action action)
        {
            var launcher = new TestLauncherActor();
            launcher.SendAction(
                () =>
                {
                    action();
                    launcher.Finish();
                });
            Assert.IsTrue(launcher.Wait());
        }
    }
}
