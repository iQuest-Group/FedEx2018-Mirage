using System;

namespace MirageConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = "python.exe";
			proc.StartInfo.Arguments = "chroma_compare.py -s \"audio\\simple_piano.wav\"";
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start();
			string output = proc.StandardOutput.ReadToEnd();
			Console.Write(output);
			string errors = proc.StandardError.ReadToEnd();
		}
	}
}
