using System;
using System.Collections.Generic;
using leafs_lang.DataTypes;

namespace leafs_lang
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