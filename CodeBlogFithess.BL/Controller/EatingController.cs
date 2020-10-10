using CodeBlogFithess.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBlogFithess.BL.Controller
{
    /// <summary>
    /// Контроллер еды.
    /// Реализация действий:
    /// 1. добавление еды в справочник продуктов
    /// 2. добавление еды в базу съеденного
    /// 3. извлечение еды из справочника продуктов
    /// </summary>
    public class EatingController : ControllerBase
    {
        private const string FOODS_FILE_NAME = "foods.dat";
        private const string EATINGS_FILE_NAME = "eatings.dat";
        private readonly User user; // * readonly - поле может только считываться, устанавливается только при объявлении или в конструкторе.

        /// <summary>
        /// Справочник продуктов.
        /// </summary>
        public List<Food> Foods { get; }

        /// <summary>
        /// Прием пищи.
        /// </summary>
        public Eating Eating { get; }

        /// <summary>
        /// Конструирование контроллера еды.
        /// </summary>
        public EatingController(User user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым.", nameof(user));
            Foods = GetAllFoods();
            Eating = GetEating();
        }

        /// <summary>
        /// Добавление продукта в справочник продуктов, добавление в базу съеденного. 
        /// </summary>
        /// <param name="food">Параметр - продукт.</param>
        /// <param name="weight">Параметр - вес продукта.</param>
        public void Add(Food food, double weight)
        {
            var product = Foods.SingleOrDefault(f => f.Name == food.Name);
            if (product == null) // Новый продукт.
            {
                Foods.Add(food); // Пополняем справочник продуктов.
                Eating.Add(food, weight); // Пополняем базу съеденного.
                Save();
            }
            else // Такой продукт уже когда-то ели.
            {
                Eating.Add(product, weight);
                Save();
            }

        }

        /// <summary>
        /// Загрузка из файла справочника продуктов либо создание нового списка. 
        /// </summary>
        /// <returns>Список продуктов.</returns>
        private List<Food> GetAllFoods()
        {
            return Load<List<Food>>(FOODS_FILE_NAME) ?? new List<Food>();
        }

        /// <summary>
        /// Загрузка из файла конкретного приема пищи либо создание нового приема пищи.
        /// </summary>
        /// <returns>Конкретный прием пищи.</returns>
        private Eating GetEating()
        {
            return Load<Eating>(EATINGS_FILE_NAME) ?? new Eating(user);
        }

        /// <summary>
        /// Сохранение справочника продуктов и базы съеденного.
        /// </summary>
        private void Save()
        {
            Save(FOODS_FILE_NAME, Foods);
            Save(EATINGS_FILE_NAME, Eating);
        }
    }
}
