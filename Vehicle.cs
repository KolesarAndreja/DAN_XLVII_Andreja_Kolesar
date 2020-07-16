using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVII_AndrejaKolesar
{
    enum Direction
    {
        South,
        North
    }

    class Vehicle
    {
        internal int order { get; set; }
        internal Direction direction
        {
            get
            {
                int num = Program.rnd.Next(0, 2);
                return (Direction)num;
            }
        }

        public Vehicle() { }
        public Vehicle(int ord)
        {
            order = ord;
        }
    }
}
