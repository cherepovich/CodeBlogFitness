using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeBlogFithess.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBlogFithess.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SetNewUserDataTest()
        {
            // Arrange
            var userName = Guid.NewGuid().ToString();
            var gender = "man";
            var birthdate = DateTime.Now.AddYears(-18);
            var weight = 90;
            var height = 190;
            var controller = new UserController(userName);

            // Act
            controller.SetNewUserData(gender, birthdate, weight, height);
            var controller2 = new UserController(userName);

            // Assert
            Assert.AreEqual(controller.CurrentUser.Name, controller2.CurrentUser.Name);
        }

        [TestMethod()]
        public void SaveTest()
        {
            // AAA - "правило 3х А"
            // Arrange - объявление. Задаем ожидаемые данные на входе и выходе.
            var userName = Guid.NewGuid().ToString(); // Guid формирует случайный 128 битный идентификатор.

            // Act - действия, когда мы вызываем что-то.
            // Пользователь будет сгенерирован с уникальным именем, значит создан новый, значит в конструкторе
            // UserController вызовется метод Save
            var controller = new UserController(userName);

            // Assert - сравниваем что получилось и что ожидалось.
            // Класс Assert содержит набор проверок (утверждений).
            Assert.AreEqual(userName, controller.CurrentUser.Name);
        }
    }
}