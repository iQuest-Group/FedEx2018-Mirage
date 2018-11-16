namespace MirageBusiness
{
	public class PythonRunner
	{
		public static string RunWithArgument(string argument)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = "python.exe";
			proc.StartInfo.Arguments = $"chroma_compare.py -s \"{argument}\"";
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.CreateNoWindow = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start();

			return proc.StandardOutput.ReadToEnd();
		}
	}
}
