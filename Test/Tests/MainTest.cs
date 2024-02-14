using SoraDataEngine.Runtime;
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
using SoraDataEngine.Commons.Scopes;
using SoraDataEngine.Commons.Attributes;
using Attribute = SoraDataEngine.Commons.Attributes.Attribute;
using System.Diagnostics;

namespace Test.Tests
{
    internal class MainTest
    {
        private RuntimeCore _runtimeCore;
        public int count = 0;
        public MainTest() 
        {
            _runtimeCore = new RuntimeCore(new SoraDataEngine.Runtime.Loader.AsmLoaderConfig("D:\\.go\\", "*.ignore"));
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

        public void AsynchronousTaskBenchmarkTest()
        {
            for (int i = 0; i < 100000;  i++)
            {
                RuntimeCore.EventManager?.RegistEvent(
                new CycleEvent(new Condition
                {
                    IsSatisfied = () => Random.Shared.Next(0, 100) > 50
                },
                new List<IEffect>
                {
                    new Effect(
                        new TrueCondition(),
                        new List<Action<ulong>>
                        {
                            new Action<ulong>(
                            (ulong time) => count++
                            )
                        })
                }
                , 0, 100000, 1));
            }
        }

        public void CacheTest(int t)
        {
            long s1, s2;
            s1 = 0;
            s2 = 0;
            for (int i = 0;i < t;i++)
            {
                var ret = _SingleCacheTest();
                s1 += ret.Item1;
                s2 += ret.Item2;
            }
            Console.WriteLine("Tests:" + t.ToString() + "; t1: " +  (s1/t).ToString() + "; t2: " + (s2/t).ToString());
        }

        private (long, long) _SingleCacheTest()
        {
            var ch = RuntimeCore.ScopeManager?.GetRootScope().AddChild(new Scope("test", string.Empty, RuntimeCore.ScopeManager.GetRootScope(), RuntimeCore.ScopeManager.GetRootScope()));
            for (int i = 0; i < 100000; i++)
            {
                ch.AddAttribute(new Attribute("attr" + i.ToString(), 1));
            }
            Stopwatch sw = new Stopwatch();
            string name = ch.FullName + ".attr"+Random.Shared.Next(0, 100000-1);
            sw.Start();
            for (int i = 0; i < 100000 ; i++)
            {
                _ = RuntimeCore.ScopeManager?.GetAttributeByFullName(name, cache: false);
            }
            sw.Stop();
            long t1 = sw.ElapsedMilliseconds;
            //Console.WriteLine(sw.ElapsedMilliseconds.ToString());
            sw.Restart();
            for (int i = 0;i < 100000; i++)
            {
                _ = RuntimeCore.ScopeManager?.GetAttributeByFullName(name, cache: true);
            }
            sw.Stop();
            long t2 = sw.ElapsedMilliseconds;
            //Console.WriteLine(sw.ElapsedMilliseconds.ToString());
            RuntimeCore.ScopeManager?.RemoveScopeByFullName(ch.FullName);
            return (t1, t2);
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
