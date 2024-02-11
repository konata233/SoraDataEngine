using SoraDataEngine.Commons.Condition;
using System.Diagnostics;
using Test.Tests;
using Tests;

Stopwatch stopwatch = Stopwatch.StartNew();
MainTest mainTest = new MainTest();
mainTest.CycleEventTest();
mainTest.ScheduledEventTest();
mainTest.Run();
Thread.Sleep(1000);
mainTest.Stop();
stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());