using System;

namespace CodeBlogFithess.BL.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]
    public class User
    {
        #region Свойства класса
        public string Name { get; } // Публичное свойство - "правильно обернутые глобальные переменные"
        public Gender Gender { get; set; } // Пол человека. Не даем возможности изменять
        public DateTime BirthDate { get; set; }
        public Double Weight { get; set; }
        public Double Height { get; set; }
        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } } // Упрощенное вычисление года (в некот. случ. м.б. неккоректно)
        #endregion

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="name">параметр - имя пользователя</param>
        /// <param name="gender"></param>
        /// <param name="birthDate"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        public User(string name, Gender gender, DateTime birthDate, Double weight, Double height)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(name));
            }

            if (gender == null)
            {
                throw new ArgumentNullException("Пол пользователя не может быть null", nameof(gender));
            }

            if (birthDate < DateTime.Parse("01.01.1900") || birthDate >= DateTime.Now)
            {
                throw new ArgumentException("Невозможная дата рождения", nameof(birthDate));
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Неверный вес", nameof(weight));
            }

            if (height <= 0)
            {
                throw new ArgumentException("Неверный рост", nameof(height));
            }
            #endregion

            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Weight = weight;
            Height = height;
        }

        /* Версия конструктора для создания пользователя только по имени, для использования в CurrentUser (процесс входа в систему).
         * Начали делать "Используем обращение к базовому конструктору" но пока не стали */
        public User(string name)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(name));
            }
            #endregion

            Name = name;
        }

        public override string ToString()
        {
            return Name + " " + Age;
        }
    }
}