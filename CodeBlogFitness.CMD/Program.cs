using CodeBlogFithess.BL.Controller;
using CodeBlogFithess.BL.Model;
using System;
using System.Globalization;
using System.Resources;

namespace CodeBlogFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем культуру.
            //var culture = CultureInfo.CreateSpecificCulture("ru");
            var culture = CultureInfo.CurrentCulture;

            // Создаем менеджер ресурсов.
            var resourceManager = new ResourceManager("CodeBlogFitness.CMD.Languages.Messages", typeof(Program).Assembly);

            // Console.WriteLine(Languages.Messages.Hello);
            Console.WriteLine(resourceManager.GetString("Hello", culture));
            Console.Write(resourceManager.GetString("EnterName", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);
            var exerciseController = new ExerciseController(userController.CurrentUser);

            // Если пользователь новый, то устанавливаем остальные свойства кроме Name
            if (userController.IsNewUser)
            {
                Console.Write("Введите пол:");
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime("дата рождения");
                var weight = ParseDouble("вес");
                var height = ParseDouble("рост");

                // Непосредственно тут устанавливаем остальные свойства.
                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            // Вывод на консоль данных о текущем пользователе.
            Console.WriteLine(userController.CurrentUser);

            while(true)
                { 
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("Е - ввести прием пищи");
                Console.WriteLine("А - ввести упражнение");
                Console.WriteLine("Q - выход");
                var key = Console.ReadKey();
                Console.WriteLine();
                switch(key.Key)
                {
                    case ConsoleKey.E:
                        var foods = EnterEating();
                        eatingController.Add(foods.Food, foods.Weight);

                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key} - {item.Value}");
                        }
                        break;

                    case ConsoleKey.A:
                        var exe = EnterExercise();
                        exerciseController.Add(exe.Activity, exe.Begin, exe.End);
                        foreach (var item in exerciseController.Exercises)
                        {
                            Console.WriteLine($"\t{item.Activity} с {item.Start.ToShortTimeString()} до { item.Finish.ToShortTimeString()}");
                        }
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
                Console.ReadLine();
            }
        }

        private static (DateTime Begin, DateTime End, Activity Activity) EnterExercise()
        {
            Console.WriteLine("Введите название упражнения:");
            var name = Console.ReadLine();

            var energy = ParseDouble("расход энергии в минуту");

            var begin = ParseDateTime("начало упражнения");
            var end = ParseDateTime("окончание упражнения");
            var activity = new Activity(name, energy);

            return (begin, end, activity);
        }

        /// <summary>
        /// Ввод описания продукта: имя, калории, БЖУ, вес.
        /// </summary>
        /// <returns></returns>
        private static (Food Food, double Weight) EnterEating()
        {
            Console.WriteLine("Введите имя продукта: ");
            var food = Console.ReadLine();
            var caloryes = ParseDouble("калорийность");
            var prots = ParseDouble("белки");
            var fats = ParseDouble("жиры");
            var carbs = ParseDouble("углеводы");
            var weight = ParseDouble("вес порции");
            var product = new Food(food, caloryes, prots, fats, carbs);

            // Использование кортежей - "набор значений анонимного типа"
            return (Food: product, Weight: weight);
        }

        /// <summary>
        /// Получение данных о дне рождения пользователя в цикле, с учетом проверки на правильность.
        /// </summary>
        /// <returns>Дата рождения в правильном формате.</returns>
        private static DateTime ParseDateTime(string value)
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write($"Введите {value} в формате dd.MM.yyyy: ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {value}");
                }
            }
            return birthDate;
        }

        /// <summary>
        /// Считывание параметра double с учетом проверки правильности ввода и перезапроса.
        /// </summary>
        /// <param name="name">Название параметра, который считываем.</param>
        /// <returns>Проверенное значение в double - формате.</returns>
        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name}: ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"Неверный формат поля {name}");
                }
            }
        }
    }
}
