using System.Windows;
using System.Data;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для EditWindow1.xaml
    /// </summary>
    public partial class EditWindow1 : Window
    {
        public DataRow resultRow { get; set; }
        public EditWindow1(DataRow dataRow)
        {
            InitializeComponent();
            resultRow = dataRow;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameTextBox.Text = resultRow["Name"].ToString();
            ageTextBox.Text = resultRow["Age"].ToString();
            salaryTextBox.Text = resultRow["Salary"].ToString();
            departmentTextBox.Text = resultRow["Department"].ToString();
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            resultRow["Name"] = nameTextBox.Text;
            resultRow["Age"] = ageTextBox.Text;
            resultRow["Salary"] = salaryTextBox.Text;
            resultRow["Department"] = departmentTextBox.Text;
            DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
