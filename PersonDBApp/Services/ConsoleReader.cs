using PersonDBApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp.Services
{
    internal class ConsoleReader
    {
        //Поля для хранения вводимой информации
        string? fullName;
        DateTime birthDate;
        string? gender;

        public Person GetPerson()
        {
            //Пользователю требуется ввести полное имя
            Console.WriteLine("Введите ФИО:");
            fullName = Console.ReadLine();

            //Пользователю требуется ввести дату рождения
            Console.WriteLine("Введите дату рождения:");
            birthDate = DateTime.Parse(Console.ReadLine());

            //Пользователю требуется ввести пол
            Console.WriteLine("Введите пол:");
            gender = Console.ReadLine();

            return new Person { FullName = fullName, BirthDate = birthDate, Gender = gender };
        }
    }
}
