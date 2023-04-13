using PersonDBApp.DataBase;
using PersonDBApp.Querys;
using PersonDBApp.Services;

ConsoleReader reader = new();
InsertingData inserting = new();
CreatureTable creature = new();
Deletion deletion = new();
SelectionData selection = new();
AutoInitialization autoInitialization = new();

//Переменная для хранения команд
string command = string.Empty;
bool work = true;

while (work)
{
    //Ожидание ввода команды
    Console.Write(">");
    command = Console.ReadLine();

    //Кейсы команд
    switch (command.ToLower())
    {
        //Сoздание таблицы
        case "create":
        case "persondbapp 1":
            creature.Create();
            break;

        //Добавление данных в БД       
        case "insert":
        case "persondbapp 2":
            var person = reader.GetPerson();
            inserting.Insert(person);
            break;

        //Фильтрация по фио и дате рождения + Все данные и возраст
        case "persondbapp 3":
            var resultNameDate = selection.SelectFullNameDate();
            Console.WriteLine("ФИО  Дата рождения");
            ConsoleWriter<Tuple<string?, DateTime>> writerNameDate = new(resultNameDate);
            Console.WriteLine("ФИО  Дата рождения  Пол  Возраст");
            var resultAll = selection.SelectAll();
            ConsoleWriter<Tuple<string?,DateTime, string?, int?>> writerAll = new(resultAll);
            break;

        //Генерация миллиона значений
        case "auto":
        case "persondbapp 4":
            autoInitialization.Initiate();
            break;

        case "persondbapp 5":
            var result = selection.SelectLastNameMale(out long time);
            ConsoleWriter<Tuple<string?,DateTime, string?>> writerNameMale = new(result);
            Console.WriteLine("Время выполнения:{0} мс.",time);
            break;


        //Удаление данных в БД
        case "delete":
            deletion.Delete();
            break;
        // Удаление данных в БД
        case "exit":
            work= false;
            break;

        //Информация о доступных командах
        case "помощь":
        case "h":
        case "help":
            Console.WriteLine("Доступные команды:\n" +
                "Создать таблицу - create или PersonDBApp 1\n" +
                "Добавление в базу - insert или persondbapp 2\n" +
                "ФИО и дата рождения(Фильтрованные) + Все данные с возрастом - persondbapp 3\n" +
                "Автозаполнение данных - auto или persondbapp 4\n" +
                "Мужской пол с фамилией на F + время выполнения - persondbapp 5\n" +
                "Удаление базы - delete\n" +
                "Выход - exit\n");
            break;
        //Обработка неверных команд
        default:
            Console.WriteLine($"Команда {command} некорректна!\nДля помощи введите комманду: help или помощь");
            break;
    }
}