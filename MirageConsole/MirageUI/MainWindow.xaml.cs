using System;
using System.Collections.Generic;
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

		public MainWindow()
		{
			InitializeComponent();
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
			string firstFileData = PythonRunner.RunWithArgument(sourcePath);
			string secondFileData = PythonRunner.RunWithArgument(targetPath);

			float[][] parsedFirstFileData = (JsonConvert.DeserializeObject(firstFileData) as JArray).ToObject<float[][]>();
			float[][] parsedSecondFileData = (JsonConvert.DeserializeObject(secondFileData) as JArray).ToObject<float[][]>();

			Score.Text = DataComparer.CompareFileData(parsedFirstFileData, parsedSecondFileData);
		}
	}
}
