using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenericBuilder
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length <= 0)
				return;

			foreach (string arg in args)
			{
				try
				{
					Console.WriteLine("Generating from template '{0}'...", arg);
					GenerateClasses(arg);
				}
				catch (Exception ex)
				{
					Console.WriteLine("There was an error generating classes from the template '{0}'. Error: {1}", arg, ex.Message);
					continue;
				}
			}

			Console.WriteLine("Press any key to exit...");
			Console.Read();
		}

		private static void GenerateClasses(string templatePath)
		{
			string[] inFile = Array.Empty<string>();
			try
			{
				inFile = File.ReadAllLines(templatePath);
				if (inFile.Length == 0)
					throw new ArgumentException("The input file is empty.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("There was an error opening the file '{0}'. Error: {1}", templatePath, ex.Message);
				return;
			}
			
			int numParams = 0;
			var match = Regex.Match(inFile[0], @"^[#]\d+$");
			if (match.Success)
			{
				numParams = int.Parse(match.Value.Substring(1));
				inFile = inFile.Skip(1).ToArray();
			}
			else
			{
				bool valid = false;
				string input = string.Empty;

				do
				{
					Console.Write("Enter the number of generic parameters to generate (greater than 0): ");
					input = Console.ReadLine();
					valid = int.TryParse(input, out numParams);
				} while (numParams <= 0 || !valid);
			}

			StringBuilder initialBuilder = new StringBuilder();
			foreach (var line in inFile)
				initialBuilder.AppendLine(line);

			string template = initialBuilder.ToString();
			
			bool doParams = template.IndexOf("<#>") >= 0;
			bool doArgs = template.IndexOf("#args") >= 0;
			if (!doParams && !doArgs)
			{
				Console.WriteLine("Skipping '{0}' because no identifiers were located.", templatePath);
				return;
			}

			StringBuilder templateBuilder = new StringBuilder(template);
			StringBuilder outputBuilder = new StringBuilder();

			for (int i = 0; i < numParams; i++)
			{
				if (doParams)
				{
					StringBuilder paramsBuilder = new StringBuilder("<");
					for (int j = 0; j < i + 1; j++)
					{
						paramsBuilder.Append("T" + (j + 1));

						if (j < i)
							paramsBuilder.Append(", ");
						else
							paramsBuilder.Append(">");
					}
					
					templateBuilder.Replace("<#>", paramsBuilder.ToString());
				}

				if (doArgs)
				{
					StringBuilder argsBuilder = new StringBuilder();
					for (int j = 0; j < i + 1; j++)
					{
						argsBuilder
							.Append("T" + (j + 1))
							.Append(" arg" + (j + 1));

						if (j < i)
							argsBuilder.Append(", ");
					}

					templateBuilder.Replace("#args", argsBuilder.ToString());
				}

				outputBuilder
					.Append(templateBuilder.ToString())
					.AppendLine();

				templateBuilder = new StringBuilder(template);
			}

			StringBuilder outputPath = new
				StringBuilder(Path.GetDirectoryName(templatePath))
				.Append('\\')
				.Append(Path.GetFileNameWithoutExtension(templatePath))
				.Append("_output")
				.Append(Path.GetExtension(templatePath));

			Console.WriteLine("Saving generated file at '{0}'", outputPath.ToString());
			File.WriteAllText(outputPath.ToString(), outputBuilder.ToString());
		}
	}
}
