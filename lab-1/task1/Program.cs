using System;
using System.Runtime.InteropServices;

namespace lab_1
{
    class Program
    {
        [DllImport(@"C:\Users\Иван\RiderProjects\lab-1\task1\CPPInterop.dll",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double AddTwoElements(double fristElement, double secondElement);

        static void Main()
        {
            double a = 100, b = 200.5;
            Console.WriteLine(AddTwoElements(a, b));
        }
    }   
}
