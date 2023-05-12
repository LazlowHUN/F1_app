using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace I2CQ73_HFT_2022231.WpfClient
{
	/// <summary>
	/// Interaction logic for CarEditor.xaml
	/// </summary>
	public partial class CarEditor : Window
	{
		public CarEditor()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ChooseWindow choose = new ChooseWindow();
			choose.Show();
			this.Close();
		}
	}
}
