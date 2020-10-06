using CodeBlogFithess.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeBlogFithess.BL.Controller
{
    /* ! Абстрактный класс - от которого нельзя (и незачем) непосредственно создать экземпляр
     * Например: создаем только сотрудников банка или клиентов банка, а от класса Human - человек
     * непосредственно не создаем объекты*/
    public abstract class ControllerBase
    {
        /// <summary>
        /// Универсальный метод сохранения структуры данных в файл.
        /// Нужен чтобы убрать дублирование кода.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="item"></param>
        // ! protected значит метод доступен в текущем классе и в производных классах.
        protected void Save(string fileName, object item)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }

        /// <summary>
        /// Универсальный метод загрузки из файла структуры данных.
        /// Используется "универсальный" метод.
        /// Задумано чтобы убрать дублирование кода.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Имя файла из которого читаем данные.</param>
        /// <returns></returns>
        protected T Load<T>(string fileName)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                /* Если в файле считается тип являющийся типом = Т, то вернем его.
                Если же нет, то создаем новый пустой Т и возвращаем его.*/
                if (fs.Length > 0 && formatter.Deserialize(fs) is T items)
                {
                    return items;
                }
                else
                {
                    return default(T); // default "создает специальное неинициализированное значение забитое нулями"
                }
            }
        }
    }
}
