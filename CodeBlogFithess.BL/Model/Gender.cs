using System;

namespace CodeBlogFithess.BL.Model
{
    /// <summary>
    /// Пол
    /// </summary>
    [Serializable]
    public class Gender
    {
        /// <summary>
        /// Название пола.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Конструирование пола.
        /// </summary>
        /// <param name="name">Параметр - название пола.</param>
        public Gender(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя пола не может быть пустым или null", nameof(name));
            }

            Name = name;
        }

        // Сами переопределим стандартное поведение метода ToString
        public override string ToString()
        {
            return Name;
        }
    }
}
