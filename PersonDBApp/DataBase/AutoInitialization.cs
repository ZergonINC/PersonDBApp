
namespace PersonDBApp.DataBase
{
    public class AutoInitialization
    {
        List<Person> persons = new();

        enum Gender
        {
            Male,
            Female
        }

        Random random = new Random();

        List<string> lastNames = new();

        public void Initiate()
        {
            DateTime startDate = new(1980, 1, 1);
            //Генерируем нужно кол-во фамилий
            for (int i = 0; lastNames.Count != 100; i++)
            {
                var name = Faker.Name.Last();
                if (name.StartsWith('F'))
                    lastNames.Add(name);
            }
            //Добавляем сначала 100 требуемых
            for (int i = 0; i < 100; i++)
            {
                string firstName = Faker.Name.First();
                string lastName = lastNames[i];
                string middleName = Faker.Name.Middle();

                string fullName = string.Format("{0} {1} {2}", firstName, lastName, middleName);
                DateTime birthDate = startDate.AddDays(random.Next(366)).AddYears(random.Next(30));
                string gender = "Male";

                Person pers = new() { FullName = fullName, BirthDate = birthDate, Gender = gender };

                persons.Add(pers);
            }
            //сохраняем
            using (ApplicationContext db = new())
            {
                db.AddRangeAsync(persons);
                db.SaveChanges();
            };

            //Добавляем оставшийся миллион человек
            for (int i = 0; i < 200; i++)
            {
                persons.Clear();

                for (int j = 0; j < 5000; j++)
                {
                    string fullName = Faker.Name.FullName();
                    DateTime birthDate = startDate.AddDays(random.Next(366)).AddYears(random.Next(30));
                    string gender = Faker.Enum.Random<Gender>().ToString();

                    Person pers = new() { FullName = fullName, BirthDate = birthDate, Gender = gender };

                    persons.Add(pers);
                }
                //Сохраням блоками для повышения произовдительности
                using (ApplicationContext db = new())
                {
                    db.AddRangeAsync(persons);
                    db.SaveChanges();
                };
            }
        }
    }
}
