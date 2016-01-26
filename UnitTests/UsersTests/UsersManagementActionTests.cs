using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyBlog.Controllers;
using MyBlog.Helpers;
using MyBlog.Models.Users;

namespace UnitTests.UsersTests
{
    [TestClass]
    public class UsersManagementActionTests
    {
        [TestMethod]
        public void UsersManagement_ViewNotNull()
        {
            //Avarage
            var mockIUsersManager = new Mock<IUsersManager>();
           // mockIUsersManager.Setup(x => x.GetAllUsers())
            //    .Returns(new List<User>());
            const string expected = "UsersManagement";

            var controller = new UsersController(mockIUsersManager.Object);

            //Act
            var result = controller.UsersManagement() as ViewResult;
     
            //Assert
            Assert.IsNotNull(result.Model, "we don't forget create model using 'new Model();'"); 
            Assert.AreEqual(expected, result.ViewName);
        }


        [TestMethod]
        public void UsersManagement_ModelIsCorrect()
        {
            //Avarage
            var i = 0;
            var countUsers = 2;
            var totalPages = 4;
            var currentPage = 2;

            var mockIUsersManager = new Mock<IUsersManager>();
            mockIUsersManager.Setup(x => x.GetUsers(It.IsAny<string>(), It.IsAny<int>(), out i))
                .Returns(new List<User>()
                {
                    new User(),
                    new User(),
                });

            var controller = new UsersController(mockIUsersManager.Object);

            //Act
            var result = controller.UsersManagement(currentPage) as ViewResult;

            var model = (UserModel)result.Model;
            model.PageInfo = PageInfoHelper.SetPageInfo(totalPages, currentPage);

            //Assert
            Assert.AreEqual(countUsers, model.ListUsers.AllUsers.Count);
            Assert.AreEqual(totalPages, model.PageInfo.TotalPages);
            Assert.AreEqual(currentPage , model.PageInfo.CurrentPage);
        }

    }
}
