using System;
using System.Drawing;
using System.IO;
using IServicesApp;

namespace Imgs
{
    public class ImgsManager : IImgsManager
    {
        public string GetUserImg(int id)
        {
            var pathToImg = "/Files/" + id + ".jpg";
            var root = AppDomain.CurrentDomain.BaseDirectory;

            ResizeImg(root + "/Files/noImg.jpg");

            return File.Exists(root + pathToImg) ? pathToImg : "/Files/noImg.jpg";
        }

        public void ResizeImg(string pathParam)
        {
            int width = 108;
            int height = 110;

            var oldImg = Image.FromFile(pathParam);
            var newImg = new Bitmap(oldImg, width, height);

            oldImg.Dispose();
            File.Delete(pathParam);
            newImg.Save(pathParam);
        }
    }
}
