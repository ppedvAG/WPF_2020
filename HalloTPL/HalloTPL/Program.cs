using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo TPL ***");

            //Parallel.Invoke(Zähle, () => Zähle(), Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 100_000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);
                throw new ExecutionEngineException();
                Console.WriteLine("T1 fertig");
            });

            t1.ContinueWith(t => { Console.WriteLine("T1 Continue"); });
            t1.ContinueWith(t => { Console.WriteLine("T1 OK"); },TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.ContinueWith(t => { Console.WriteLine($"T1 ERROR {t.Exception.InnerException.Message}"); },TaskContinuationOptions.OnlyOnFaulted);

            Task<long> t2 = new Task<long>(() => 
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(1200);
                Console.WriteLine("T2 fertig");
                return 456789345678;
            });

            t1.Start();
            t2.Start();

            Console.WriteLine($"T2 Result: {t2.Result}"); // ---> t2.Wait();



            var hd = new HalloDelegate();
            hd.deleMitPara.Invoke("Hallo");

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
