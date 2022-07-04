using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FishingBotFoffosEdition
{
    public class MousePosUpdateThread
    {

    }

    public class ThreadWork
    {
        public static void DoWork()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Working thread...");
                Thread.Sleep(100);
            }
        }
    }
    //class ThreadTest
    //{
    //    public static void Main()
    //    {
    //        Thread thread1 = new Thread(ThreadWork.DoWork);
    //        thread1.Start();
    //        for (int i = 0; i < 3; i++)
    //        {
    //            Console.WriteLine("In main.");
    //            Thread.Sleep(100);
    //        }
    //    }
    //}
}
