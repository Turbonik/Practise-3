using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            short choise, count, key, index;
            string word, completion, manufacturer;
            double try_value, price;
            int e_find, year;

            Console.WriteLine("Заполнение необобщенной коллекции случайными числами.");
            Random random = new Random();
            ArrayList list = new ArrayList();
            for (short i = 1; i < 6; i++)
            {
                list.Add(random.Next(1, 1000));
            }

            Console.WriteLine("Введите слово, которое небходимо вставить в список: ");
            word = Console.ReadLine();
            list.Add(word);
            ArrayListPrint(list);

            Console.WriteLine("Введите номер элемента, который необходимо удалить (1-5 - номера чисел, 6 - номер слова): ");
            short.TryParse(Console.ReadLine(), out choise);
            if (choise > 0 && choise < 7)
            {
                list.RemoveAt(choise - 1);
            }
            ArrayListPrint(list);


            Stack<double> items = new Stack<double>();
            Console.WriteLine("Заполнение коллекции stack элементами double. Вводите элементы, введите 'stop' для прекращения заполнения:");
            completion = Console.ReadLine();
            while (completion != "stop")
            {
                if (double.TryParse(completion, out try_value) == false)
                    Console.WriteLine("Неверное значение.");
                else
                    items.Push(try_value);
                completion = Console.ReadLine();
            }
            foreach (var item in items)
                Console.WriteLine(item);

            Console.WriteLine("Удаление элементов из стека. Введите количество элементов, которые нужно удалить:");
            key = 0;
            while (key != 1)
            {
                short.TryParse(Console.ReadLine(), out count);
                if (count <= items.Count && count >= 0)
                {
                    while (count != 0)
                    {
                        items.Pop();
                        count--;
                    }
                    key = 1;
                }
                else
                {
                    Console.WriteLine("Введено неверное значение.");
                }
            }
            foreach (var item in items)
                Console.WriteLine(item);


            Console.WriteLine("Создание коллекции LinkedList и заполнение из коллекции stack.");
            LinkedList<double> doubles = new LinkedList<double>(items);
            foreach (var item in doubles)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Введите значение которое нужно найти в коллекции LinkedList, будет выведен индекс этого элемента.");
            index = 0;
            int.TryParse(Console.ReadLine(), out e_find);
            key = 0;
            foreach (var item in doubles)
            {
                if (item == e_find)
                {
                    Console.WriteLine("Индекс элемента:");
                    Console.WriteLine(index);
                    key = 1;
                }
                index++;

            }
            if (key != 1)
            {
                Console.WriteLine("Такого значение в списке нет.");
            }

            Console.WriteLine("Создание класса ComputerTech с реализацией интерфейсов IComparable, IComparer и ICloneable. Введите количество необходимых реализаций класса ComputerTech:");
            key = 0;
            Stack<ComputerTech> comps = new Stack<ComputerTech>();
            while (key != 1)
            {
                short.TryParse(Console.ReadLine(), out count);
                if (count >= 0)
                {
                    while (count != 0)
                    {
                        ComputerTech newDevice = new ComputerTech();
                        newDevice = InicializeDevice();
                        comps.Push(newDevice);
                        count--;
                    }
                    key = 1;
                }
                else
                {
                    Console.WriteLine("Введено неверное значение.");
                }
            }

            if (comps.Count != 0)
            {
                Console.WriteLine("Исходный список компьютеров: ");
                ListClassPrint(comps);

                Console.WriteLine("Сортировка компьютеров по цене с помощью IComparable.");
                var sortedCompsPrice = new List<ComputerTech>(comps);
                sortedCompsPrice.Sort();
                comps.Clear();
                comps = new Stack<ComputerTech>(sortedCompsPrice);
                ListClassPrint(comps);

                Console.WriteLine("Сортировка компьютеров по году с помощью ICompare");
                var sortedCompsYear = new List<ComputerTech>(comps);
                sortedCompsYear.Sort(new YearComparer());
                comps.Clear();
                comps = new Stack<ComputerTech>(sortedCompsYear);
                ListClassPrint(comps);


                Console.WriteLine("Клонирование первого элемента в stack с помощтю ICloneable.");
                var clonedComp = comps.Peek().Clone() as ComputerTech;
                clonedComp.Print();
            }

            Console.WriteLine("Создание ObservableCollection.");
            var computers = new ObservableCollection<ComputerTech>();
            computers.CollectionChanged += Computers_CollectionChanged;

            Console.WriteLine("Введите количество необходимых реализаций класса ComputerTech:");
            count = short.Parse(Console.ReadLine());
            key = 0;
            while (key != 1)
            {
                if (count >= 0)
                {
                    while (count != 0)
                    {
                        ComputerTech newDevice = new ComputerTech();
                        newDevice = InicializeDevice();
                        computers.Add(newDevice);
                        count--;
                    }
                    key = 1;
                }
                else
                {
                    Console.WriteLine("Введено неверное значение.");
                }
            }

            Console.WriteLine("Введите номер элемента, который необходимо удалить.");
            short.TryParse(Console.ReadLine(), out choise);
            if (choise > 0 && choise <= computers.Count)
            {
                computers.RemoveAt(choise - 1);
            }
            else
            {
                Console.WriteLine("Неверное значение.");
            }

        }

        private static void Computers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (ComputerTech tech in e.NewItems)
                {
                    Console.WriteLine("Добавлено:");
                    tech.Print();
                }

            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (ComputerTech tech in e.OldItems)
                {
                    Console.WriteLine("Удалено:");
                    tech.Print();
                }
            }
        }

        public static ComputerTech InicializeDevice()
        {
            string manufacturer;
            int year;
            decimal price;
            Console.WriteLine("Введите производителя: ");
            manufacturer = Console.ReadLine();
            Console.WriteLine("Введите год: ");
            year = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите цену: ");
            price = decimal.Parse(Console.ReadLine());
            ComputerTech newDevice = new ComputerTech(manufacturer, year, price);
            return newDevice;
        }

        public static void ListClassPrint(Stack<ComputerTech> computerTech)
        {
            foreach (ComputerTech tech in computerTech)
            {
                tech.Print();
            }
        }

        public static void ArrayListPrint(ArrayList list)
        {
            Console.WriteLine("ArrayList:");
            for (short i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
}


