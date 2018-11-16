using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MirageBusiness
{
	public class DataComparer
	{
		private static Dictionary<int, string> notes = new Dictionary<int, string>
		{
			{0, "C"},
			{1, "C#"},
			{2, "D"},
			{3, "D#"},
			{4, "E"},
			{5, "F"},
			{6, "F#"},
			{7, "G"},
			{8, "G#"},
			{9, "A"},
			{10, "A#"},
			{11, "B"}
		};

		public static string CompareFileData(float[][] firstFileData, float[][] secondFileData, out string source, out string target)
		{
			string firstFileNotes = ExtractNotes(firstFileData);
			string secondFileNotes = ExtractNotes(secondFileData);

			source = firstFileNotes;
			target = secondFileNotes;

			int score = LevenshteinDistance.Compute(firstFileNotes, secondFileNotes);

			int maxScore = new[] { firstFileNotes.Length, secondFileNotes.Length }.Max();

			return (((maxScore - score) * 100.00) / maxScore).ToString("###.##", CultureInfo.CurrentCulture);
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
