using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Project3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient client = new TelegramBotClient(string.Empty);

            //Starting the bot
            try
            {
                client = new TelegramBotClient(GetToken());
            }
            catch (ArgumentException ex)
            {
                Debug.ExceptionLog(ex, "Where is the bot token?");
                return;
            }

            client.StartReceiving(Update, Error);
            BotStatus.startTime = DateTime.Now;
            Console.WriteLine("Bot started");

            while (Console.ReadLine() != "stop")
            {
                continue;
            }
        }

        private static string GetToken()
        {
            //Made for privacy
            //Maybe i'll change txt file to something else, if needed.

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string botClientToken = string.Empty;

            //Get parent directory where token.txt is located
            try
            {
                while (!currentDir.EndsWith("Project3"))
                    currentDir = Directory.GetParent(currentDir).FullName;
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.ExceptionLog(ex, "Directory not found!");
                return null;
            }

            //Get token.txt
            try
            {
                using (StreamReader streamReader = new StreamReader(currentDir + "\\token.txt"))
                    botClientToken = streamReader.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                Debug.ExceptionLog(ex, "Token not found!");
                return null;
            }

            return botClientToken;
        }

        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message == null)
                return;

            if (message.Text.StartsWith('/'))
                Command.DoCommand(client, message);

            else
            {
                Debug.Log(message);
                await client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
        }

        async static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Debug.ExceptionLog(exception, "SOMETHING WRONG: " + exception.Message);
        }
    }
}
