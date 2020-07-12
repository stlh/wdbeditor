using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Net.Zxnn.Dnd.Core
{
    public static class DiceBox
    {
        private static Dictionary<string, List<Dice>> _cache = new Dictionary<string, List<Dice>>();
        public static Regex regex = new Regex(@"^\d+d\d+$");

        public static int Roll(string str = "1d6")
        {
            if (!regex.IsMatch(str))
            {
                throw new ArgumentException(nameof(str) + " is invalid");
            }

            List<Dice> diceList;
            if (!_cache.ContainsKey(str))
            {
                string[] strs = str.Split('d');

                diceList = new List<Dice>();
                if (int.TryParse(strs[0], out int count) && int.TryParse(strs[1], out int faces))
                {
                    for (int i = 0; i < count; i++)
                    {
                        diceList.Add(new Dice(faces));
                    }
                }
            }
            else
            {
                _cache.TryGetValue(str, out diceList);
            }

            int point = 0;

            foreach (Dice dice in diceList)
            {
                point += dice.Roll();
            }

            return point;
        }
    }
}