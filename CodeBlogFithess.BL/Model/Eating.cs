using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBlogFithess.BL.Model
{
    /// <summary>
    /// Прием пищи пользователем.
    /// </summary>
    [Serializable]
    public class Eating
    {
        /// <summary>
        /// Пользователь, который ел.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Время приема пищи.
        /// </summary>
        public DateTime Moment { get; }

        /// <summary>
        /// Список и вес продуктов, которые всего съел пользователь.
        /// </summary>
        public Dictionary<Food, double> Foods { get; }

        public Eating(User user)
        {
            User = user ?? throw new ArgumentNullException("Пользователь не может быть пустым.", nameof(user));
            Moment = DateTime.UtcNow;
            Foods = new Dictionary<Food, double>();
        }

        /// <summary>
        /// Добавление (поедание) продукта.
        /// </summary>
        /// <param name="food"></param>
        /// <param name="weight"></param>
        public void Add(Food food, double weight)
        {
            var product = Foods.Keys.FirstOrDefault(f => f.Name.Equals(food.Name));

            // Если такого продукта еще не было (не нашли в предыдущей строке)
            if (product == null)
            {
                Foods.Add(food, weight); // Добавляем новый продукт с весом
            }
            else
            // Нашли продукт
            {
                Foods[product] += weight; // Увеличиваем общий вес найденного продукта
            }
        }
    }
}
