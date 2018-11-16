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
using Path = System.IO.Path;

namespace MirageUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			sourceFile.Text = Path.GetFileName(GetFileName());
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
			targetFile.Text = Path.GetFileName(GetFileName());
		}

		private void scoreCalculation_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
