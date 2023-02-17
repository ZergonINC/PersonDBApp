using PersonDBApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp.Querys
{
    public class InsertingData
    {
        //Запрос на добавление данных
        public void Insert(Person person)
        {
            using (ApplicationContext db = new())
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }
    }
}
