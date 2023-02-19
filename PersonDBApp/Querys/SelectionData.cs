using Microsoft.EntityFrameworkCore;
using PersonDBApp.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonDBApp.Querys
{
    public class SelectionData
    {
        //Запрос возврашающий ФИО и дату рождения
        public List<Tuple<string?, DateTime>> SelectFullNameDate()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Persons?.Select(p => new { p.FullName, p.BirthDate })
                    .Distinct()
                    .OrderBy(p => p.FullName)
                    .AsEnumerable()
                    .Select(p => new Tuple<string?, DateTime>(p.FullName, p.BirthDate.Date))
                    .ToList();

                return result;
            }
        }

        //Запрос возвращающий Все данныйе из таблицы вместе с возрастом
        public List<Tuple<string?, DateTime, string?, int?>> SelectAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = (from p in db.Persons
                                  //получим разницу в годах с даты рождения
                              let years = DateTime.Now.Year - p.BirthDate.Year
                              //получим дату дня рождения в этом году
                              let birthdayThisYear = p.BirthDate.AddYears(years)
                              select new
                              {
                                  p.FullName,
                                  p.BirthDate,
                                  p.Gender,
                                  //если день рождения еще не прошел в этом году,годы - 1
                                  Age = birthdayThisYear > DateTime.Now ? years - 1 : years
                              })
                             .AsEnumerable()
                             .Select(p => new Tuple<string?, DateTime, string?, int?>(p.FullName, p.BirthDate.Date, p.Gender, p.Age))
                             .ToList();

                return result;
            }
        }

        //Запрос возвращаюлюдей людей с Фамилией начинающуюся на Ф, мужского пола(Неоптимизированный)
        public List<Tuple<string?, DateTime, string?>> SelectLastNameMale(out long time)
        {
            var sw = Stopwatch.StartNew();
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Persons?.Select(p => new { p.FullName,p.BirthDate, p.Gender })
                    .Where(p => p.FullName.Contains(" F") && p.Gender.Contains("Male"))//Запрос вернет также все результаты начинающие на пробел и F. Я перебрал множество вариантов, regex не оптимально(будет перебирать очень долго), а большинство запросов не могут быть переведены linq(Я все еще ищу нужный). Единственный выход использовать LIKE для sql и писать свою функцию substring. Но оптимальным вариантом было бы перепроектировать базу данных(Учесть это в самом начале) с раздельными столбцами ФИО и соединять либо внешними ключами в другой таблице или когда нужно программно. Что сильно упростило не только такие запросы
                    .AsEnumerable()
                    .Select(p => new Tuple<string?, DateTime, string?>(p.FullName,p.BirthDate, p.Gender))
                    .ToList();//Это один из замых отимальных способов, быстрее только убрать анонимные функции и преобразование в кортеж, но тогда будет проблематично вернуть получившие значение не написав и прям в Main (1200 мс в среднем для обработки миллиона значенией)

                sw.Stop();
                time = sw.ElapsedMilliseconds;

                return result;
            }
        }
    }
}
