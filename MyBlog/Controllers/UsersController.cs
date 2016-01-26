using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using BLL;
using BLL.Users;
using Email;
using Imgs;
using Logs;
using AdminAppSettings;
using MyBlog.Helpers;
using MyBlog.Models.Users;


namespace MyBlog.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersManager _usersManager;
        public event EventHandler<EventUserId> ResizeImgCompleted; 

        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        public ActionResult UsersManagement(int currentPage=1)
        {
            var userModel = new UserModel();
          
            int countPages;

            userModel.ListUsers.AllUsers = _usersManager.GetUsers(userModel.PageInfo.Find, currentPage, out countPages);

            userModel.PageInfo = PageInfoHelper.SetPageInfo(countPages, currentPage);

            return View("UsersManagement", userModel);
        }

        [HttpPost]
        public ActionResult FindUser(PageInfo pageInfo)
        {           
            int countPages;
            var userModel = new UserModel();

            if (pageInfo.CurrentPage == 0)
            {
                pageInfo.CurrentPage = 1;
            }
             
            userModel.ListUsers.AllUsers = _usersManager.GetUsers(
                pageInfo.Find, pageInfo.CurrentPage, out countPages);

            userModel.PageInfo = PageInfoHelper.SetPageInfo(countPages, pageInfo.CurrentPage);

            return PartialView("FoundUsers", userModel);
        }

        [HttpPost]
        public ActionResult AddUser(SelectUserModel selectUserModel)
        {
            if (!ModelState.IsValid) return PartialView();

            _usersManager.AddUser(
                selectUserModel.FirstName,
                selectUserModel.LastName,
                selectUserModel.Email);

            int countPages;
            var userModel = new UserModel {PageInfo = {CurrentPage = 1}};

            userModel.ListUsers.AllUsers = _usersManager.GetUsers(userModel.PageInfo.Find, userModel.PageInfo.CurrentPage, out countPages);

            userModel.PageInfo = PageInfoHelper.SetPageInfo(countPages);

            return PartialView("FoundUsers", userModel);
        }

       [HttpPost]
        public ActionResult DeleteUser(int idParam)
        {
            _usersManager.DeleteUser(idParam);

            return Json(new { id = idParam, ok = true, message="User was deleted"});         
        }

        [HttpPost]
        public ActionResult EditUserQuery(int idParam)
        {
            var imgsManager = new ImgsManager();

            var user = _usersManager.EditUserQuery(idParam);

            var srcImgUser = imgsManager.GetUserImg(idParam);

            return Json(new
            {
                id = idParam,
                firstName = user.FirstName,
                lastName = user.LastName, 
                eMail = user.Email,
                srcImg = srcImgUser,
                ok = true
            });
        }

        [HttpPost]
        public ActionResult EditUser(SelectUserModel selectUserModel, string find, int currentPage = 1)
        {
            if (!ModelState.IsValid) return PartialView();

            selectUserModel.FirstName = selectUserModel.FirstName.ToLower();

            _usersManager.EditUser(
                selectUserModel.ID, selectUserModel.FirstName,
                selectUserModel.LastName, selectUserModel.Email);

            

            int countPages;
            var userModel = new UserModel
            {
                ListUsers =
                {
                    AllUsers = _usersManager.GetUsers(find, currentPage, out countPages)
                },
                PageInfo = PageInfoHelper.SetPageInfo(countPages, currentPage)
            };

            return PartialView("FoundUsers", userModel);
        }

        
        public ActionResult Upload(int idParam)
        {
            var userModel = new SelectUserModel {ID = idParam};

            return View(userModel);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int id)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = file.FileName;

                var lastDot = fileName.LastIndexOf('.');
                var extensionName = fileName.Substring(lastDot + 1).ToLower();

                fileName = id + "." + extensionName;   
          
                var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
            
                file.SaveAs(path);

                ResizeImgCompleted += UsersController_ResizeImgCompleted;
                ResizeImgProcess(path, id);

            }
                   
             return RedirectToAction("UsersManagement");
        }

        public class EventUserId : EventArgs
        {
            public int UserId { get; set; }

            public EventUserId(int id)
            {
                UserId = id;
            }
        }

        static void UsersController_ResizeImgCompleted(object sender, EventUserId even)
        {
            var appSettings = new AppSettingsManager();
            var logsType = appSettings.DetectLogsTypes();

            var logs = new LogsManager(logsType, even.UserId);
            logs.WriteLogs();
        }

        private void ResizeImgProcess(string pathImg, int id)
        {
            Task.Run(() =>
            {
                var imgsManager = new ImgsManager();
                imgsManager.ResizeImg(pathImg);

                if (ResizeImgCompleted != null) { ResizeImgCompleted(this, new EventUserId(id)); }
            });
        }
    }
}