using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Project3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Starting the bot
            var client = new TelegramBotClient(GetToken());
            client.StartReceiving(Update, Error);

            Console.ReadLine();
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
            }

            return botClientToken;
        }

        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            //just for testing
            if (message != null)
            {
                Debug.Log(message);
                await client.SendTextMessageAsync(message.Chat.Id ,message.Text);
            }
        }

        async static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            //?
        }
    }
}
