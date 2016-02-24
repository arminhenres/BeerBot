using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Coordinate
    {
        private decimal x;

        private decimal y;

        private decimal z;

        private decimal l1;

        private decimal l2;

        private string nameSet;

        public Coordinate(string name, string coordinates)
        {
            coordinates = coordinates.Trim().Trim('\\').Trim('r').TrimEnd('n');
            char[] sep = new char[1];
            sep[0] = ',';
            var splitted = coordinates.Split(sep);

            this.x = Decimal.Parse(splitted[0].Replace('.', ','));
            this.y = Decimal.Parse(splitted[1].Replace('.', ','));
            this.z = Decimal.Parse(splitted[2].Replace('.', ','));
            this.l1 = Decimal.Parse(splitted[3].Replace('.', ','));
            this.l2 = Decimal.Parse(splitted[4].Replace('.', ','));
            nameSet = name;
        }

        public Coordinate(string x, string y, string z, string l1, string l2, string name)
        {
            this.x = Decimal.Parse(x.Replace('.', ','));
            this.y = Decimal.Parse(y.Replace('.', ','));
            this.z = Decimal.Parse(z.Replace('.', ','));
            this.l1 = Decimal.Parse(l1.Replace('.', ','));
            this.l2 = Decimal.Parse(l2.Replace('.', ','));
            nameSet = name;
        }

        public string Name
        {
            get
            {
                return nameSet;
            }

        }

        public decimal X
        {
            get
            {
                return x;
            }
        }

        public decimal Y
        {
            get
            {
                return y;
            }
        }

        public decimal Z
        {
            get
            {
                return z;
            }
        }

        public decimal L1
        {
            get
            {
                return l1;
            }
        }

        public decimal L2
        {
            get
            {
                return l2;
            }
        }

        public override string ToString()
        {
            string stringX = x.ToString().Replace(",", ".");
            string stringY = y.ToString().Replace(",", ".");
            string stringZ = z.ToString().Replace(",", ".");
            string stringL1 = l1.ToString().Replace(",", ".");
            string stringL2 = l2.ToString().Replace(",", ".");

            string returnString = string.Format("{0},{1},{2},{3},{4}", stringX, stringY, stringZ, stringL1, stringL2);
            return returnString;
        }
    }
}
