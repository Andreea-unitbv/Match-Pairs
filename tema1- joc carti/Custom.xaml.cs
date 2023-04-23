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

namespace tema1__joc_carti
{
    /// <summary>
    /// Interaction logic for Custom.xaml
    /// </summary>
    public partial class Custom : Window
    {
        public int Rows { get; set; } = 5;
        public int Cols { get; set; } = 5;
        public Custom()
        {
            InitializeComponent();
            ComboBoxItem item = new ComboBoxItem();

            for (int i = 1; i < 6; i++)
                rowSelector.Items.Add(item.Content = i);

            for (int i = 1; i < 6; i++)
                columnSelector.Items.Add(item.Content = i);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rows = (int)rowSelector.SelectedItem;
            Cols = (int)columnSelector.SelectedItem;
            this.Hide();
        }
    }
}
