using SoraDataEngine.Commons.Condition;
using System.Diagnostics;
using Tests;

A a = new A();
Stopwatch stopwatch = Stopwatch.StartNew();
for (int i = 0; i < 100000; i++)
    ConditionTest.Test();
stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());

class A
{
    public static A Instance { get; set; }
    public A()
    {
        Instance = this;
    }
}