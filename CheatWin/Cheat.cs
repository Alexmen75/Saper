using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CheatWin
{
    class Cheat
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (args[0] == "1")
                {
                    AlternatePathOfExecution();
                }
                //add other options here and below              
            }
            else
            {
                NormalPathOfExectution();
            }
        }


        private static void NormalPathOfExectution()
        {
           // Console.WriteLine("Doing something here");
            //need one of these for each additional console window
            Process p = Process.Start(@"C:\Users\alexe\source\repos\сапер\CheatWin\bin\Debug\CheatWin.exe", "1");
            p.Kill();
            Console.ReadLine();

        }
        private static void AlternatePathOfExecution()
        {
            Console.WriteLine("Write something different on other Console");
            Console.ReadLine();
        }
    }
}
