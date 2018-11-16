using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using MirageBusiness;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;

namespace MirageUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string sourcePath;
		private string targetPath;
		private BackgroundWorker bkWorker  = new BackgroundWorker();

		public MainWindow()
		{
			InitializeComponent();
			bkWorker.DoWork += BkWorkerOnDoWork;
			bkWorker.RunWorkerCompleted += BkWorkerOnRunWorkerCompleted;
		}

		

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			sourcePath = GetFileName();
			sourceFile.Text = Path.GetFileName(sourcePath);
		}

		private string GetFileName()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Wave files (*.wav)|*.wav";
			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			return string.Empty;
		}

		private void btn2OpenFile_Click(object sender, RoutedEventArgs e)
		{
			targetPath = GetFileName();
			targetFile.Text = Path.GetFileName(targetPath);
		}

		private void scoreCalculation_Click(object sender, RoutedEventArgs e)
		{
			Score.Text = "0";
			sourceNotes.Text = "Calculating...";
			targetNotes.Text = "Calculating...";
			
			bkWorker.RunWorkerAsync();
		}

		private void BkWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
		{
			var tuple = args.Result as Tuple<string, string>;
			ProcessData(tuple.Item1, tuple.Item2);

		}

		private void BkWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			string firstFileData = PythonRunner.RunWithArgument(sourcePath);
			string secondFileData = PythonRunner.RunWithArgument(targetPath);

			doWorkEventArgs.Result = new Tuple<string, string>(firstFileData, secondFileData);
		}

		private void ProcessData(string firstFileData, string secondFileData)
		{

			float[][] parsedFirstFileData = (JsonConvert.DeserializeObject(firstFileData) as JArray).ToObject<float[][]>();
			float[][] parsedSecondFileData = (JsonConvert.DeserializeObject(secondFileData) as JArray).ToObject<float[][]>();

			string source, target;

			Score.Text = DataComparer.CompareFileData(parsedFirstFileData, parsedSecondFileData, out source, out target);

			var sb = new StringBuilder();

			var sourceList = source.Split(' ').ToList();
			sourceList.ForEach(s => sb.Append(s.PadRight(2)));

			sourceNotes.Text = sb.ToString();

			sb.Clear();

			var targetList = target.Split(' ').ToList();
			targetList.ForEach(s => sb.Append(s.PadRight(2)));

			targetNotes.Text = sb.ToString();
		}
	}
}
