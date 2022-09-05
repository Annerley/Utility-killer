using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        
        class Util
        {
            public static int timeAllowed = 1;
            public static int frequencyCheck = 1;
            public static string proc = "notepad++";
        }


        public static void KillUtil(string proc)
        {
            while (true)
            {
                Thread.Sleep(Util.frequencyCheck * 60 * 1000);
                foreach (Process process in Process.GetProcessesByName(proc))
                {
                    Console.WriteLine("Living time: " + ((DateTime.Now - process.StartTime).Minutes));
                    if (((DateTime.Now - process.StartTime).Minutes) >= Util.timeAllowed)
                    {
                        Console.WriteLine("I killed " + process.ProcessName);
                        process.Kill();
                        return;
                    }
                    //Console.WriteLine("Time is less than reference");
                }
            }

        }

        public static void CheckButtons()
        {
            ConsoleKeyInfo keyInfo;
            do { keyInfo = Console.ReadKey(true); }
            while (keyInfo.Key != ConsoleKey.Q);
            
        }

        static void Main(string[] args)
        {

            if (args.LongLength > 0)
            {
                Util.proc = args[0];
                Util.timeAllowed = Int32.Parse(args[1]);
                Util.frequencyCheck = Int32.Parse(args[2]);
            }


            Thread killingThread = new Thread(() => KillUtil(Util.proc));
            Thread buttonThread = new Thread(() => CheckButtons());

            killingThread.IsBackground = true;
            buttonThread.IsBackground = true;
            killingThread.Start();
            buttonThread.Start();


            Console.WriteLine("Press Q to quit");
            do
            {
                
            }
            while (buttonThread.IsAlive == true && killingThread.IsAlive == true);
            
            



        }

        
    }
}
