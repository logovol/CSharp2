using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab5
{
    /// <summary>
    /// Логика взаимодействия для AddDepWindow.xaml
    /// </summary>
    public partial class AddDepWindow : Window
    {
        Department dep;
        public List<Department> listD { get; set; }
        public ObservableCollection<Department> itemsD { get; set; }

        public AddDepWindow()
        {
            InitializeComponent();
        }

        private void Button_ClickSave(object sender, RoutedEventArgs e)
        {
           if (tbName.Text != String.Empty)
            {
                dep = new Department();
                dep.Id = int.Parse(tbID.Text);
                dep.Name = tbName.Text;
                listD.Add(dep);
                itemsD.Add(dep);
                Close();
            }
            else
                MessageBox.Show("Укажите наименование");
        }
    }
}
