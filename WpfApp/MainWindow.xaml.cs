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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeObservableCollection EmployeesList = new EmployeeObservableCollection();
        private Employee employee = new Employee();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeesList.CollectionChanged += EmployeesList.CollectionChangedHandeler;
            employee = new Employee();
            this.DataContext = EmployeesList;
            EmpoyeesListBox.ItemsSource = EmployeesList;
            listBox.ItemsSource = EmployeesList;
            DataTemplate template = this.TryFindResource("Listbox_datatempelate") as DataTemplate;
            if (template != null)
                EmpoyeesListBox.ItemTemplate = template;
            template = this.TryFindResource("heirar") as DataTemplate;
            if (template != null)
                listBox.ItemTemplate = template;
            positionAdd.ItemsSource =  Enum.GetValues(typeof(SetOfPositions));
            divisionAdd.ItemsSource = Enum.GetValues(typeof(Subdivisions));
        }

        private void request(object sender, RoutedEventArgs e)
        {
            if (EmployeesList.changed)
            {
                MessageBoxResult rez = MessageBox.Show("Внесены изменения. Сохранить ? ", "", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)
                    SaveFile_Click(sender, e);
            }
        }
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            request(sender, e);
            EmployeesList = new EmployeeObservableCollection();
            EmployeesList.CollectionChanged += EmployeesList.CollectionChangedHandeler;
            DataContext = EmployeesList;
            EmpoyeesListBox.ItemsSource = EmployeesList ;
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
                filename = dlg.FileName;
            else
                filename = null;

            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binF = new BinaryFormatter();
                EmployeeObservableCollection temp = binF.Deserialize(fileStream) as EmployeeObservableCollection;
                for (int i = 0; i < temp.Count;++i)
                {
                    EmployeesList.Add_Employee(temp[i]);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (fileStream != null)
                    fileStream.Close();
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }


        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            string filename;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
                filename = dlg.FileName;
            else
                filename = null;

            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binF = new BinaryFormatter();
                binF.Serialize(fileStream, EmployeesList);
               EmployeesList.changed = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (fileStream != null)
                    fileStream.Close();
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        private void AddDefault_Click(object sender, RoutedEventArgs e)
        {
            EmployeesList.AddDefault();
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            EmployeesList.Remove_EmployeeAt(EmpoyeesListBox.SelectedIndex);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (surnameAdd.Text == "")
            {
                MessageBox.Show("  Введите фамилию");
                return;
            }
            if (nameAdd.Text=="")
            {
                MessageBox.Show("  Введите имя");
                return;
            }
            if (patronymicAdd.Text == "")
            {
                MessageBox.Show("  Введите отчество");
                return;
            }
            if (headAdd.Text == "")
            {
                MessageBox.Show("  Введите ФИО рукводителя");
                return;
            }
            if ((Subdivisions)divisionAdd.SelectedValue == Subdivisions.DeveloperDivision)
            {
                if (!((SetOfPositions)positionAdd.SelectedValue == SetOfPositions.HeadOfDevelopment || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.SeniorDeveloper || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.JuniorDeveloper))
                {
                    MessageBox.Show(" Неверно выбрана должность");
                    return;
                }
             }
            if ((Subdivisions)divisionAdd.SelectedValue == Subdivisions.DivisionBusinessAnalysys)
            {
                if (!((SetOfPositions)positionAdd.SelectedValue == SetOfPositions.HeadOfAnalyst || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.JuniorAnalyst || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.SeniorAnalyst))
                {
                    MessageBox.Show(" Неверно выбрана должность");
                    return;
                }

            }
            if ((Subdivisions)divisionAdd.SelectedValue == Subdivisions.ProjectManagementDivision)
            {
                if (!((SetOfPositions)positionAdd.SelectedValue == SetOfPositions.HeadOfManagemant || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.SeniorManager || (SetOfPositions)positionAdd.SelectedValue == SetOfPositions.JuniorManager))
                {
                    MessageBox.Show(" Неверно выбрана должность");
                    return;
                }
            }
            employee.name = nameAdd.Text;
            employee.surname = surnameAdd.Text;
            employee.patronymic = patronymicAdd.Text;
            employee.head = headAdd.Text;
            employee.number = numberAdd.Text;
            employee.email = emailAdd.Text;
            employee.division = (Subdivisions)divisionAdd.SelectedValue;
            employee.position = (SetOfPositions)positionAdd.SelectedValue;
            EmployeesList.Add(employee);
            employee = new Employee();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            request(sender, new RoutedEventArgs());
        }
    }
}
