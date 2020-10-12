using CodeBlogFithess.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBlogFithess.BL.Controller
{
    public class ExerciseController : ControllerBase
    {
        private const string EXERCISES_FILE_NAME = "exercises.dat";
        private const string ACTIVITIES_FILE_NAME = "activities.dat";
        private readonly User user;

        public List<Exercise> Exercises { get; }
        public List<Activity> Activities { get; }
        

        public ExerciseController(User user)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            Exercises = GetAllExercises();
            Activities = GetAllActivities();
        }

        /// <summary>
        /// Загрузка из файла списка всех возможных активностей либо создание нового списка.
        /// </summary>
        /// <returns></returns>
        private List<Activity> GetAllActivities()
        {
            return Load<List<Activity>>(ACTIVITIES_FILE_NAME) ?? new List<Activity>();
        }

        /// <summary>
        /// Загрузка из файла всех сделанных упражнений либо создание нового списка.
        /// </summary>
        /// <returns></returns>
        private List<Exercise> GetAllExercises()
        {
            return Load<List<Exercise>>(EXERCISES_FILE_NAME) ?? new List<Exercise>();
        }

        /// <summary>
        /// Добавление активности.
        /// </summary>
        /// <param name="activityName"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public void Add(Activity activity, DateTime begin, DateTime end)
        {
            var act = Activities.SingleOrDefault(a => a.Name == activity.Name);
            if (act == null) // Если не нашли активность.
            {
                Activities.Add(activity); // Добавляем активность.
                
                var exercise = new Exercise(begin, end, activity, user);
                Exercises.Add(exercise); // Добавляем упражнение.
            }
            else // иначе нашли активность act.
            {
                var exercise = new Exercise(begin, end, act, user);
                Exercises.Add(exercise); // Добавляем упражнение.
            }
            Save();
        }
     
        private void Save()
        {
            Save(EXERCISES_FILE_NAME, Exercises);
            Save(ACTIVITIES_FILE_NAME, Activities);
        }
    }
}
