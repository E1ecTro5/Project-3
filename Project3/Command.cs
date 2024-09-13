using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Project3
{
    public class Command
    {
        public static void DoCommand(ITelegramBotClient client, Message message)
        {
            string command = message.Text.Split(' ')[0];
            Debug.CommandLog(message, command);

            switch (command)
            {
                case "/weather":
                    client.SendTextMessageAsync(message.Chat.Id, Weather.GetCurrentWeather(message.Text.Replace("/weather ", string.Empty)));
                    break;
                case "/status":
                    client.SendTextMessageAsync(message.Chat.Id, BotStatus.Status());
                    break;
                default:
                    Debug.ErrorLog("Unknown command!");
                    client.SendTextMessageAsync(message.Chat.Id, "Unknown command!");
                    break;
            }
        }
    }
}
