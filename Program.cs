using System;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Threading;

namespace Bot
{
	public class Program
	{
		private static ITelegramBotClient BotClient;

		public static void Main(string[] args)
		{
			try
			{
				using (StreamReader sr = new StreamReader("chatId.txt", System.Text.Encoding.Default))
				{

				}
			}
			catch(Exception ex)
			{
				using (StreamWriter sw = new StreamWriter("chatId.txt", true, System.Text.Encoding.Default))
				{

				}
			}

			BotClient = new TelegramBotClient("Your Token") { Timeout = TimeSpan.FromSeconds(10) };
			var me = BotClient.GetMeAsync().Result;
			BotClient.OnMessage += Bot_OnMessage;
			BotClient.StartReceiving();
			Console.ReadKey();
		}

		public async static void Bot_OnMessage(object sender, MessageEventArgs e)
		{
			readText(e);

			var text = e?.Message?.Text;
			{
				if(text == null)
				{
					return;
				}
				switch(text)
				{
					case "/start":
						await BotClient.SendTextMessageAsync(
							chatId: e.Message.Chat.Id,
							text: $"Вас приветствует информационный бот. Данный бот будет время от времени присылать вам" +
	   $" какую-то небольшую информацию! Всего хорошего))))0",
							Telegram.Bot.Types.Enums.ParseMode.Default,
							false,
							false,
							0
							).ConfigureAwait(false);
						break;
					case "/help":
						//
						break;
				}
			}
		}

		public async static void notifyMethod(string text)
		{
			using (StreamReader sr = new StreamReader("chatId.txt", System.Text.Encoding.Default))
			{
				string line;
				while ((line = await sr.ReadLineAsync()) != null)
				{
					try
					{
						await BotClient.SendTextMessageAsync(
							chatId: line,
							text: $"Уведомление по запросу: {text}",
							Telegram.Bot.Types.Enums.ParseMode.Default,
							false,
							false,
							0
							).ConfigureAwait(false);
					}
					catch(Exception ex)
					{
						//Console.WriteLine("Возможно пользователь остановил бота!");
					}
					Thread.Sleep(10);
				}
			}
		}

		private static void readText(MessageEventArgs e1)
		{
			var flg = false;
			List<long> temp = new List<long>();
			using (StreamReader sr = new StreamReader("chatId.txt", System.Text.Encoding.Default))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					temp.Add(Convert.ToInt64(line));
				}
				
				foreach(var i in temp)
				{
					if(i == e1.Message.Chat.Id)
					{
						flg = true;
						break;
					}
				}
			}
			if (!flg)
			{
				writeText(e1);
			}
		}

		private static void writeText(MessageEventArgs e1)
		{
			using (StreamWriter sw = new StreamWriter("chatId.txt", true, System.Text.Encoding.Default))
			{
				sw.WriteLine(e1.Message.Chat.Id);
			}
		}
	}
}