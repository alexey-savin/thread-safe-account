using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeAccount
{
    class Account
    {
        private object _locker = new object();

        private int _balance = 0;
        private Random _rand = new Random();

        public Account(int initial)
        {
            _balance = initial;
        }

        private int Withdraw(int amount)
        {
            if (_balance < 0)
            {
                throw new InvalidOperationException("Negative balance detected");
            }

            lock (_locker)
            {
                if (_balance >= amount)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}");

                    Console.WriteLine($"Balance before Withdraw {_balance}");
                    Console.WriteLine($"Amount to Withdraw -{amount}");

                    _balance = _balance - amount;

                    Console.WriteLine($"Balance after Withdraw {_balance}");

                    return amount;
                }
                else
                {
                    return 0; // transaction rejected
                }
            }
        }

        public void DoTransactions()
        {
            for (int i = 0; i < 100; i++)
            {
                Withdraw(_rand.Next(1, 100));
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} exited");
        }
    }
}
