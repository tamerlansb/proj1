using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class Employee
    {
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }

        public string email { get; set;  }
        public string number { get; set;}
        public SetOfPositions position { set; get; }
        public Subdivisions division { set; get; }
        public string head { set; get; }

        public Employee(string surname = "", string name = "", string patronymic = "")
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
        public override string ToString()
        {
            return surname + " " + name + " " + patronymic;
        }
        public int CompareTo(Employee obj)
        {
            if (obj == null) return 1;

            return head.CompareTo(obj.head);
        }
        public int Compare(Employee First, Employee Second)
        {
            if (First == null && Second == null)
            {
                return 0;
            }
            else if (First == null)
            {
                return -1;
            }
            else if (Second == null)
            {
                return 1;
            }
            else
            {
                return First.head.CompareTo(Second.head);
            }
        }
    }
}
