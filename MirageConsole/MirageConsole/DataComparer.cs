using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MirageConsole
{
	class DataComparer
	{
		private static Dictionary<int, string> notes = new Dictionary<int, string>
		{
			{0, "DO"},
			{1, "DO#"},
			{2, "RE"},
			{3, "RE#"},
			{4, "MI"},
			{5, "FA"},
			{6, "FA#"},
			{7, "SOL"},
			{8, "SOL#"},
			{9, "LA"},
			{10, "LA#"},
			{11, "SI"}
		};

		public static string CompareFileData(float[][] firstFileData, float[][] secondFileData)
		{
			string firstFileNotes = ExtractNotes(firstFileData);
			string secondFileNotes = ExtractNotes(secondFileData);

			int score = LevenshteinDistance.Compute(firstFileNotes, secondFileNotes);

			return score.ToString();
		}

		private static string ExtractNotes(float[][] fileData)
		{
			StringBuilder output = new StringBuilder();

			int nrColoane = fileData[0].Length;
			string prevNote = string.Empty;
			float prevSum = 0;
			for (int i = 0; i < nrColoane; i = i + 2)
			{
				var toProcess = fileData.Select(d => d[i]).ToArray();
				int maxIndex = Array.IndexOf(toProcess, toProcess.Max());
				var sum = toProcess.Sum() - 1;
				var note = notes[maxIndex];
				if (!note.Equals(prevNote) || (sum - prevSum) > 0.9)
				{
					prevNote = note;
					output.Append($"{note} ");
				}
				prevSum = sum;
			}

			return output.ToString();
		}
	}
}
