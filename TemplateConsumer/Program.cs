using System;

namespace TemplateConsumer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var worker = new RestWorker();
            worker.Start();

            Console.ReadLine();
        }
    }
}