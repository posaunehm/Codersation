using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachine
    {
        public VendingMachine()
        {
            TotalAmount = 0;
        }

        public int TotalAmount { get; private set; }
    }
}
