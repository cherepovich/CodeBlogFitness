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
    /// Пользовательский контроллер.
    /// Реализация действий: загрузка, сохранение списка пользователей, а так же 
    /// инициализация всех свойств текущего пользователя с сохранением.
    /// </summary>
    public class UserController : ControllerBase // Наследование чтобы сократить код и избавиться от дублирования кода
    {
        private const string USERS_FILE_NAME = "users.dat";

        /// <summary>
        /// Список всех пользователей приложения.
        /// </summary>
        public List<User> Users { get; } // Использование List не безопасно т.к. можно получить доступ к элементам даже, если типа private.

        /// <summary>
        /// Активный пользователь (которого загружаем или создаем).
        /// </summary>
        public User CurrentUser { get; }

        /// <summary>
        /// Флаг: true - если будет создан новый пользователь, false - пользователь считан из файла.
        /// </summary>
        public bool IsNewUser { get; } = false; // * Конструкция для указания значения по умолчанию = false.

        /// <summary>
        /// Конструирование контроллера.
        /// </summary>
        /// <param name="userName">Параметр - имя пользователя, которое вводят в форму
        /// на приглашение "введите имя пользователя".
        /// По этому имени будет осуществлен поиск пользователя по базе, либо создан новый пользователь.</param>
        public UserController(string userName)
        {
            #region Проверки параметра userName
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым или содержать только пробел", nameof(userName));
            }
            #endregion

            Users = GetUsersData();

            // В списке пользователей ищем пользователя с именем == имени параметра userName.
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            // Если не удалось найти пользователя (получаем null), то создаем нового пользователя с указанием
            // только имени (через отдельную перегрузку конструктора).
            if (CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser); // Добавляем пользователя в список пользователей.
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Загрузка (десериализация) пользователей из файла либо создание нового списка пользователей.
        /// </summary>
        /// <returns>Список пользователей (загруженный или созданный - пустой).</returns>
        private List<User> GetUsersData()
        {
            // * Возвращает значение своего операнда слева, если его значение не равно null.
            return Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
        }

        /// <summary>
        /// В случае, когда создается новый пользователь, метод используется для инициализации 
        /// остальных параметров (кроме имени) в CurrentUser и сохранения нового пользователя.
        /// <param name="genderName"></param>
        /// <param name="birthDate"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            #region TODO: Сделать проверку параметров
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
