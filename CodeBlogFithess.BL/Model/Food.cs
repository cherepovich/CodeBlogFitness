using System;

namespace CodeBlogFithess.BL.Model
{
    /// <summary>
    /// Описывает продукт питания.
    /// </summary>
    [Serializable]
    public class Food
    {
        /// <summary>
        /// Наименование продукта
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Калорийность в 1 грамме продукта
        /// </summary>
        public double Caloryes { get; }

        /// <summary>
        /// Белки в 1 грамме продукта
        /// </summary>
        public double Proteins { get; }

        /// <summary>
        /// Жиры в 1 грамме продукта
        /// </summary>
        public double Fats { get; }

        /// <summary>
        /// Углеводы в 1 грамме продукта
        /// </summary>
        public double Carbohydrates { get; }

        /// <summary>
        /// Конструирование по имени
        /// </summary>
        /// <param name="name"></param>
        public Food(string name) : this(name, 0, 0, 0, 0) { }

        /// <summary>
        /// Полная версия конструктора.
        /// </summary>
        /// <param name="name">Параметр - имя продукта</param>
        /// <param name="caloryes">Параметр - содержаний калорий в 100 г. продукта</param>
        /// <param name="proteins">Параметр - содержание белков в 100 г. продукта</param>
        /// <param name="fats">Параметр - содержание жиров в 100 г. продукта</param>
        /// <param name="carbohydrates">Параметр - содержание углеводов в 100 г. продукта</param>
        public Food(string name, double caloryes, double proteins, double fats, double carbohydrates) 
        {
            // TODO: проверка
            Name = name;
            Caloryes = caloryes / 100.0;
            Proteins = proteins / 100.0;
            Fats = fats / 100.0;
            Carbohydrates = carbohydrates / 100.0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
