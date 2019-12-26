using System;
using System.Threading;

namespace Reqest
{
	class Program
	{
		public delegate void Worker();
		private static Thread worker;

		static void Main(string[] args)
		{
			worker = new Thread(new ThreadStart(Work));
			worker.Start();

			static void Work() => Bot.Program.Main(new string[1]);

			while (true)
			{
				Console.WriteLine("Напишите текст который хотите отправить всем пользователям!\n");
				var text = Console.ReadLine();
				Bot.Program.notifyMethod(text);
				Console.WriteLine("\nТекст отправлен!");
				Console.WriteLine("-----------------------------------------------------------------");
			}
		}
	}
}
