using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Project3
{
    /// <summary>
    /// Class for writing needed information to the console.
    /// </summary>
    public static class Debug
    {
        //Default message
        public static void Log(Message message)
        {
            DateTime dateTime = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[{dateTime}] {message.Chat.FirstName} | {message.Text}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Command use
        public static void CommandLog(Message message, string commandName)
        {
            DateTime dateTime = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{dateTime}] {message.Chat.FirstName} used {commandName}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ExceptionLog(Exception e, string message)
        {
            DateTime dateTime = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{dateTime}] Error! | {message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
