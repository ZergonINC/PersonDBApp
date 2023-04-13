using PersonDBApp.DataBase;
using System.Diagnostics;

namespace PersonDBApp.Querys
{
    public class SelectionData
    {
        //Запрос возврашающий ФИО и дату рождения
        public List<Tuple<string?, DateTime>> SelectFullNameDate()
        {
            using (ApplicationContext db = new())
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
            using (ApplicationContext db = new())
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

        //Запрос возвращаюлюдей людей с Фамилией начинающуюся на Ф, мужского пола
        public List<Tuple<string?, DateTime, string?>> SelectLastNameMale(out long time)
        {
            var sw = Stopwatch.StartNew();
            using (ApplicationContext db = new())
            {
                var result = db.Persons?.Select(p => new { p.FullName,p.BirthDate, p.Gender })
                    .Where(p => p.FullName.Contains(" F") && p.Gender.Contains("Male"))
                    .AsEnumerable()
                    .Select(p => new Tuple<string?, DateTime, string?>(p.FullName,p.BirthDate, p.Gender))
                    .ToList();

                sw.Stop();
                time = sw.ElapsedMilliseconds;

                return result;
            }
        }
    }
}
