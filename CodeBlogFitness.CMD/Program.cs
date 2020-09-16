using CodeBlogFithess.BL.Controller;
using System;

namespace CodeBlogFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет от приложения CodeBlogFitness!");
            Console.Write("Введите имя пользователя:");
            var name = Console.ReadLine();

            var userController = new UserController(name);

            // Если пользователь новый, то устанавливаем остальные свойства кроме Name
            if (userController.IsNewUser)
            {
                Console.Write("Введите пол:");
                var gender = Console.ReadLine();
                var birthDate = ParseDateTime();
                var weight = ParseDouble("вес");
                var height = ParseDouble("рост");

                // Непосредственно тут устанавливаем остальные свойства.
                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);
            Console.ReadLine();
        }

        /// <summary>
        /// Получение данных о дне рождения пользователя в цикле, с учетом проверки на правильность.
        /// </summary>
        /// <returns>Дата рождения в правильном формате.</returns>
        private static DateTime ParseDateTime()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Введите дату рождения в формате dd.MM.yyyy: ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный формат даты рождения");
                }
            }
            return birthDate;
        }

        /// <summary>
        /// Считывание параметра double с учетом проверки правильности ввода и перезапроса
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
                    Console.WriteLine($"Неверный формат {name}");
                }
            }
        }
    }
}
