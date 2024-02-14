using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Runtime;
using System.Diagnostics;
using Test.Tests;
using Tests;

MainTest mainTest = new MainTest();
//mainTest.CycleEventTest();
//mainTest.ScheduledEventTest();
//mainTest.MessengerTest();
//mainTest.AsynchronousTaskBenchmarkTest();
Stopwatch stopwatch = Stopwatch.StartNew();
mainTest.Run();
RuntimeCore.Scheduler?.FlipClock();
mainTest.CacheTest(100);
mainTest.Stop();
stopwatch.Stop();
//Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());