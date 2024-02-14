using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Runtime;
using System.Diagnostics;
using Test.Tests;
using Tests;

MainTest mainTest = new MainTest();
//mainTest.CycleEventTest();
//mainTest.ScheduledEventTest();
//mainTest.MessengerTest();
mainTest.AsynchronousTaskBenchmarkTest();
Stopwatch stopwatch = Stopwatch.StartNew();
mainTest.Run();
Thread.Sleep(1000);
mainTest.Stop();
stopwatch.Stop();
Console.WriteLine(mainTest.count);
Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());