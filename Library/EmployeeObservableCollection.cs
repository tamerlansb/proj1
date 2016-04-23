using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Library
{
    [Serializable]
    public class EmployeeObservableCollection : ObservableCollection<Employee>
    {
        public bool changed
        {
            set;
            get;
        }
        public EmployeeObservableCollection()
        {
            changed = false;
        }
        public void Add_Employee(Employee item)
        {
            if (!base.Contains(item)) base.Add(item);
        }
        public void CollectionChangedHandeler(object sender, EventArgs e)
        {
            changed = true;
        }
        public void Remove_EmployeeAt(int index)
        {
            if (base.Count!=0 && index>=0)
               base.RemoveItem(index);
            ///change = true;
        }
        public void AddDefault()
        {
            Employee temp = new Employee("Иванов", "Иван", "Иванович");
            temp.division = Subdivisions.DeveloperDivision;
            temp.position = SetOfPositions.JuniorDeveloper;
            temp.email = "ivan@webconsultants.ru";
            temp.number = "+79987654321";
            temp.head = "Алексеев А.В.";
            base.Add(temp);
            temp = new Employee("Петров", "Петр", "Петрович");
            temp.division = Subdivisions.DeveloperDivision;
            temp.position = SetOfPositions.SeniorDeveloper;
            temp.email = "petr@webconsultants.ru";
            temp.number = "+7999123456";
            temp.head = "Алексеев А.В.";
            base.Add(temp);
        }
     
    }
}
