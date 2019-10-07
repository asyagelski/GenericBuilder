using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sharprompt;

namespace GenericBuilder
{
	public class Program
	{
		public static string Identifier
		{
			get => Properties.Settings.Default.Identifier;
			set => Properties.Settings.Default.Identifier = value;
		}

		public static string ArgumentName
		{
			get => Properties.Settings.Default.ArgumentName;
			set => Properties.Settings.Default.ArgumentName = value;
		}

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				ShowOptions();

				Console.WriteLine("Press any key to exit...");
				return;
			}

			foreach (string arg in args)
			{
				try
				{
					GenerateClasses(arg);
				}
				catch (Exception ex)
				{
					Console.WriteLine("There was an error generating classes from the template '{0}'. Error: {1}", arg, ex.Message);
					continue;
				}
			}
		}

		private static void GenerateClasses(string templatePath)
		{
			string template = File.ReadAllText(templatePath);
			List<int> indexes = new List<int>();
			int previousIndex = template.IndexOf("<#>");
		}

		private static void ShowOptions()
		{
			string choice = Prompt.Select("Select a value to change", new string[] { "Argument Name", "Identifier" });
			if (string.IsNullOrEmpty(choice))
			{
				Console.WriteLine("Nothing selected.");
				return;
			}

			switch (choice)
			{
				case "Argument Name":
					{
						Console.Write("Set the argument name (Current: {0}): ", ArgumentName);
						ArgumentName = Console.ReadLine();
						break;
					}
				case "Identifier":
					{
						Console.Write("Set the identifier (Current: {0}): ", Identifier);
						Identifier = Console.ReadLine();
						break;
					}
			}

			Properties.Settings.Default.Save();
		}
	}
}
