using System.Collections.Generic;
using System.Web.Mvc;
using BLL;
using BLL.Users;
using DAL;
using DAL.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlog.Controllers;
using MyBlog.Models.Users;
using  Moq;

namespace UnitTests.UsersTests
{
    [TestClass]
    public class EditUsersActionTests
    {
        [TestMethod]
        public void EditUser_TryEditUser()
        {
            var mockUsersManager = new Mock<IUsersManager>();
            var controller = new UsersController(mockUsersManager.Object);
            var selectUserModel = new SelectUserModel();

            //Act
            controller.EditUser(selectUserModel, null);

            //Assert
            mockUsersManager.Verify(x => x.EditUser(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
