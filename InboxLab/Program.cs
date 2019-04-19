using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using InboxLab.Actors;

namespace InboxLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 本來想要實驗用 Inbox 接收多個 Ask 的結果，但看來是不行，改用 Task 來完成。

            var sys = ActorSystem.Create("sys");
            var hello1 = sys.ActorOf(Props.Create(() => new HelloActor()), "hello1");
            var hello2 = sys.ActorOf(Props.Create(() => new HelloActor()), "hello2");
            var hello3 = sys.ActorOf(Props.Create(() => new HelloActor()), "hello3");

            var locking = new object();
            var results = string.Empty;

            // 同步（此種寫法，每個任務完成後自行 Handle 結果，會有 Thread Safe 的問題，需要鎖。）
            Task.WaitAll(
                hello1.Ask("test").ContinueWith(task =>
                    {
                        Thread.Sleep(1000);

                        lock (locking)
                        {
                            results += task.Result;
                        }

                        Console.WriteLine(task.Result);
                    }),
                hello2.Ask("test").ContinueWith(task =>
                    {
                        Thread.Sleep(1000);

                        lock (locking)
                        {
                            results += task.Result;
                        }

                        Console.WriteLine(task.Result);
                    }),
                hello3.Ask("test").ContinueWith(task =>
                    {
                        Thread.Sleep(1000);

                        lock (locking)
                        {
                            results += task.Result;
                        }

                        Console.WriteLine(task.Result);
                    }));

            Console.WriteLine();
            Console.WriteLine(results);
            Console.WriteLine();

            // 非同步（此種寫法會等到任務全部完成，才一起處理結果，不需要鎖。）
            Task.WhenAll(
                    hello1.Ask("test"),
                    hello2.Ask("test", TimeSpan.FromMilliseconds(1500)),
                    hello3.Ask("test", TimeSpan.FromMilliseconds(1500)))
                .ContinueWith(
                    task =>
                        {
                            results = string.Empty;

                            foreach (var result in task.Result)
                            {
                                HandleResult(result, ref results);
                            }
                        })
                .Wait();

            Console.WriteLine();
            Console.WriteLine(results);
            Console.WriteLine("end.");
            Console.ReadKey();
        }

        private static void HandleResult(object result, ref string results)
        {
            Thread.Sleep(1000);

            results += result;

            Console.WriteLine(result);
        }
    }

    public static class TaskExtension
    {
        public static Task ContinueSynchronouslyWith<TResult>(
            this Task<TResult> me,
            Action<Task<TResult>> continuationAction)
        {
            return me.ContinueWith(continuationAction, TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}