using CodeBlogFithess.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBlogFithess.BL.Controller
{
    /// <summary>
    /// Реализация контроллера пользователя.
    /// Действия: сохранение, считывание и т.д.
    /// </summary>
    public class UserController
    {
        public List<User> Users { get; } // Использование List не безопасно т.к. можно получить доступ к элементам даже, если типа private
        public User CurrentUser { get; } // Активный пользователь

        /* true - если будет создан новый пользователь
         * false - пользователь считан из файла */
        public bool IsNewUser { get; } = false; // конструкция для указания значения по умолчанию = false
        public UserController(string userName)
        {
            #region Проверки
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или содержать только пробел", nameof(userName));
            }
            #endregion

            Users = GetUsersData();

            // Ищем пользователя с именем = имени параметра userName. Если не нашли, получаем null.
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            /* Если не удалось найти пользователя, то нужно его установить.
             * ПОДРОБНО: Если удалось найти пользователя, то получили все его параметры.
             * Если не удалось, то создаем нового пользователя с указанием только имени (через отдельную перегрузку конструктора) */
            if (CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser); // Добавляем пользователя в список пользователей.
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Загрузка (десириализация) пользователей из файла либо создание новых пользователей.
        /// </summary>
        /// <returns> Пользователь приложения. </returns>
        private List<User> GetUsersData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                /* Если в файле считается тип являющийся списком пользователей, то вернем его.
                Если же нет, то создаем новый пустой список пользователей и возвращаем его.*/
                if (formatter.Deserialize(fs) is List<User> users)
                {
                    return users;
                }
                else
                {
                    return new List<User>();
                }
            }
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            #region TODO: Сделать проверку
            #endregion

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }

        /// <summary>
        /// Сохранение (сериализация) списка пользователей.
        /// </summary>
        public void Save()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
    }
}
