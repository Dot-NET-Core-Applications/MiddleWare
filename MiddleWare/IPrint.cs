using System;

namespace MiddleWare
{
    public interface IPrint
    {
        void Print();
    }

    public class Printer : IPrint
    {
        public void Print()
        {
            Console.WriteLine("Printing...");
        }
    }
}