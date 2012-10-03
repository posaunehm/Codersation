using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Juice
    {
        public const string NAME_Coke = "コーラ";
        public const string NAME_RedBull = "レッドブル";
        public const string NAME_Water = "水";

        public string Name { get; private set; }

        public Juice(string name)
        {
            this.Name = name;
        }
    }
}
