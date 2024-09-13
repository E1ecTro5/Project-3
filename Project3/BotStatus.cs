using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class BotStatus
    {
        public static DateTime startTime;

        public static string Status()
        {
            return $"Bot is working:\n{(DateTime.Now - startTime).Hours}:{(DateTime.Now - startTime).Minutes}:{(DateTime.Now - startTime).Seconds}";
        }
    }
}
