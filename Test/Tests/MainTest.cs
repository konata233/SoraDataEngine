﻿using SoraDataEngine.Runtime;
using SoraDataEngine.Runtime.Timeline;
using SoraDataEngine.Commons.Event;
using SoraDataEngine.Commons.Effects;
using SoraDataEngine.Commons.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoraDataEngine.Runtime.Binding;

namespace Test.Tests
{
    internal class MainTest
    {
        private RuntimeCore _runtimeCore;
        public MainTest() 
        {
            _runtimeCore = new RuntimeCore(new TimerClock(1));
        }

        public void CycleEventTest()
        {
            Console.WriteLine(RuntimeCore.Instance == null);
            RuntimeCore.EventManager?.RegistEvent(
                new CycleEvent(new TrueCondition(), new List<IEffect>
                {
                    new Effect(new TrueCondition(), new List<Action<ulong>>
                    {
                        (ulong time) => Console.WriteLine("CycleEventTest, time: " + time.ToString())
                    })
                }, 0, 1000000, 1)
            );
        }

        public void ScheduledEventTest()
        {
            RuntimeCore.EventManager?.RegistEvent(
                new ScheduledEvent(new TrueCondition(),
                new List<IEffect>
                {
                    new Effect(
                        new TrueCondition(),
                        new List<Action<ulong>>
                        {
                            new Action<ulong>(
                            (ulong time) => Console.WriteLine("ScheduledEventTest, time:" + time.ToString())
                            )
                        })
                }
            , 10, 12));
        }

        public void MessengerTest()
        {
            Console.WriteLine(RuntimeCore.Messenger == null);
            RuntimeCore.Messenger?.RegistListener(new Listener("test.lis", new Action<ulong, MessageCapsule>(
                (ulong time, MessageCapsule capsule) =>
                {
                    var msg = (string)capsule.Message[0];
                    var sender = capsule.Sender;
                    Console.WriteLine("message recv: " + msg + ";sender: " + sender.ToString());
                }
                )));

            RuntimeCore.Messenger?.SendMessage(new MessageCapsule(this, "test.lis", "helloworld"));
        }

        public void Run()
        {
            _runtimeCore.Start();
        }

        public void Stop()
        {
            _runtimeCore.Dispose();
        }
    }
}
