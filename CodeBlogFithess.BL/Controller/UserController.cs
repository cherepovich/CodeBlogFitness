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
    public class UserController : ControllerBase // Наследование чтобы сократить код и избавиться от дублирования кода
    {
        private const string USERS_FILE_NAME = "users.dat";
        public List<User> Users { get; } // Использование List не безопасно т.к. можно получить доступ к элементам даже, если типа private.
        public User CurrentUser { get; } // Активный пользователь (которого ищем или создаем).

        /* Флаг: true - если будет создан новый пользователь
         * false - пользователь считан из файла */
        public bool IsNewUser { get; } = false; // конструкция для указания значения по умолчанию = false

        /// <summary>
        /// Конструирование контроллера.
        /// </summary>
        /// <param name="userName">Параметр - имя пользователя, которое вводят на форму.</param>
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
        /// Загрузка (десеарилизация) пользователей из файла либо создание списка новых пользователей.
        /// </summary>
        /// <returns> Пользователь приложения. </returns>
        private List<User> GetUsersData()
        {
            // Возвращает значение своего операнда слева, если его значение не равно null
            return Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
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
        /// Сохранение (сериализация) текущего списка пользователей.
        /// </summary>
        public void Save()
        {
            Save(USERS_FILE_NAME, Users);
        }
    }
}
