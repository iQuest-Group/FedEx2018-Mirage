using System;
using System.Linq;
using System.IO;
using MirageBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MirageConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Usage: MirageConsole fistFile secondFile");
				return;
			}

			string firstFile = args[0];
			string secondFile = args[1];

			if (!File.Exists(firstFile))
			{
				Console.WriteLine("Cannot find first file");
				return;
			}

			if (!File.Exists(secondFile))
			{
				Console.WriteLine("Cannot find second file");
				return;
			}

			string firstFileData = PythonRunner.RunWithArgument(firstFile);
			string secondFileData = PythonRunner.RunWithArgument(secondFile);

			float[][] parsedFirstFileData = (JsonConvert.DeserializeObject(firstFileData) as JArray).ToObject<float[][]>();
			float[][] parsedSecondFileData = (JsonConvert.DeserializeObject(secondFileData) as JArray).ToObject<float[][]>();

			string result = DataComparer.CompareFileData(parsedFirstFileData, parsedSecondFileData);

			Console.WriteLine("The result is: {0}", result);
			Console.ReadLine();
		}
	}
}
