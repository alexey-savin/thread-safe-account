using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            Account acc = new Account(1000);

            int threadsCount = 2;
            Thread[] threads = new Thread[threadsCount];

            for (int i = 0; i < threadsCount; i++)
            {
                Thread t = new Thread(new ThreadStart(acc.DoTransactions));
                threads[i] = t;
            }

            for (int i = 0; i < threadsCount; i++)
            {
                threads[i].Start();
            }

            Console.ReadKey();
        }
    }
}
