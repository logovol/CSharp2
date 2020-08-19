using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для EditWindow2.xaml
    /// </summary>
    public partial class EditWindow2 : Window
    {
        public DataRow resultRow { get; set; }
        public EditWindow2(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = resultRow["Name"].ToString();            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Name"] = nameTextBox.Text;            
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
