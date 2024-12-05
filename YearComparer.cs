
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise_3
{
    public class YearComparer : IComparer<ComputerTech>
    {
        public int Compare(ComputerTech x, ComputerTech y)
        {
            if (x == null || y == null) return 0;
            return x.Year.CompareTo(y.Year);
        }
    }
}