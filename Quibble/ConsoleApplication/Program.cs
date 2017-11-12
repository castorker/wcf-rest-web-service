using ConsoleApplication.DataServices;
using ConsoleApplication.Transports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        private static QuibbleDataService _service;

        static void Main(string[] args)
        {
            _service = new QuibbleDataService();
            string input = "";
            while (input.ToUpper() != "Q")
            {
                Console.WriteLine("---------------");
                Console.WriteLine("L) List quibbles");
                Console.WriteLine("#) Query specific quibble");
                Console.WriteLine("N) Enter a new quibble");
                Console.WriteLine("Q) Quit");
                Console.Write("Please enter a command: ");
                input = Console.ReadLine().ToUpper();
                int index = 0;
                if (input == "L")
                {
                    List();
                }
                else if (input == "N")
                {
                    Enter();
                }
                else if (Int32.TryParse(input, out index))
                {
                    Query(index);
                }
            }
        }

        static void List()
        {
            var quibbles = _service.GetAll();
            foreach (var quibble in quibbles)
            {
                Console.WriteLine($"{quibble.Id}) {quibble.Text}");
            }
            Console.WriteLine("---------------");
        }

        private static void Enter()
        {
            Console.WriteLine("---------------");
            Console.Write("Category of quibble? ");
            var category = Console.ReadLine();
            Console.Write("Text? ");
            var text = Console.ReadLine();
            var quibble = new Quibble
            {
                Category = category,
                Text = text
            };
            _service.Create(quibble);
        }

        private static void Query(int index)
        {
            var quibble = _service.GetById(index);
            Console.WriteLine($"{quibble.Id}) {quibble.Category}:");
            Console.WriteLine($"{quibble.Text}");
            Console.WriteLine("---------------");
            Console.WriteLine("E) Edit quibble");
            Console.WriteLine("D) Delete quibble");
            Console.WriteLine("C) Continue");
            Console.Write("Please enter a command: ");
            var input = Console.ReadLine().ToUpper();
            if (input == "E")
            {
                Edit(index);
            }
            else if (input == "D")
            {
                Delete(index);
            }
        }

        private static void Edit(int index)
        {
            Console.WriteLine("---------------");
            Console.Write("Category of quibble? ");
            var category = Console.ReadLine();
            Console.Write("Text? ");
            var text = Console.ReadLine();
            var quibble = new Quibble
            {
                Id = index,
                Category = category,
                Text = text
            };
            _service.Update(quibble);
        }

        private static void Delete(int index)
        {
            Console.WriteLine("---------------");
            Console.Write("Are you sure you want to delete this quibble? (Y/N) ");
            var input = Console.ReadLine().ToUpper();
            if (input == "Y")
            {
                _service.Delete(index);
            }
        }
    }
}
