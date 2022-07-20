using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Pathfinder pathfinder = new Pathfinder(new ConsoleLogWriter());

            pathfinder.Find();

            pathfinder = new Pathfinder(new FileLogWriter());

            pathfinder.Find();
        }
    }

    interface ILogger
    {
        void Find();
    }

    class Pathfinder : ILogger
    {
        private ILogger _logger;

        public Pathfinder(ILogger logger) => _logger = logger;

        public void Find() => _logger.Find();
    }

    class FileLogWriter : ILogger
    {
        public void Find()
        {
            Console.WriteLine("Я пишу в файл");
        }
    }

    class ConsoleLogWriter : ILogger
    {
        public void Find()
        {
            Console.WriteLine("Я пишу в консоль");
        }
    }

    class FileLogWriterWeekly : ILogger
    {
        public void Find()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine("Я пишу в файл по пятницам");
        }
    }

    class ConsoleLogWriterWeekly : ILogger
    {
        public void Find()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine("Я пишу в консоль по пятницам");
        }
    }

    class SecureLogWriter : ILogger
    {
        public void Find()
        {
            Console.WriteLine("Я пишу в консоль");

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine("Я пишу в консоль по пятницам");
        }
    }
}