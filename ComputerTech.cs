
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Practise_3
{
    public class ComputerTech : IComparable<ComputerTech>, ICloneable
    {
        // Автоматические свойства
        public string Manufacturer { get; set; }

        private int year;
        public int Year
        {
            get { return year; }
            set { year = ((value > 1900 && value < 2025) ? value : 0); } // Проверка года
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = (value > 0 ? value : 0); } // Проверка цены
        }

        public ComputerTech()
        {
            Manufacturer = "";
            Year = 0;
            Price = 0;
        }

        public ComputerTech(string manufacturer, int year, decimal price)
        {
            Manufacturer = manufacturer;
            Year = year;
            Price = price;
        }

        // Метод для вывода информации
        virtual public void Print()
        {
            Console.WriteLine("Manufacturer: " + Manufacturer);
            Console.WriteLine("Price: " + Price + " $");
            Console.WriteLine("Year: " + Year);
        }

        // Реализация IComparable для сравнения по цене
        public int CompareTo(ComputerTech other)
        {
            if (other == null) return 1;
            return this.Price.CompareTo(other.Price);
        }

        // Реализация ICloneable для глубокого копирования
        public object Clone()
        {
            return new ComputerTech(Manufacturer, Year, Price);
        }
    }
}
