using System;

namespace CodeBlogFithess.BL.Model
{
    /// <summary>
    /// Класс - описание справочника активностей.
    /// </summary>
    /// 
    [Serializable]
    public class Activity
    {
        /// <summary>
        /// Название вида активности.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Сколько расходуется калорий в единицу времени.
        /// </summary>
        public double CaloriesPerMinute { get; }

        public Activity(string name, double caloriesPerMinute)
        {
            //TODO: Проверка
            Name = name;
            CaloriesPerMinute = caloriesPerMinute;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
