using System;
using System.Collections.Generic;
using LeafS.DataTypes;

namespace LeafS
{
    public class GlobalValues
    {
        public static GlobalValues Instance;

        public static Dictionary<string, LeafsValue> Items = new Dictionary<string, LeafsValue>
        {
            {"Pi", new LeafsValue("number", (float) Math.PI)}
        };
    }
}