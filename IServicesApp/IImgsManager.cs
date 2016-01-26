namespace IServicesApp
{
    public interface IImgsManager
    {
        string GetUserImg(int id);
        void ResizeImg(string path);
    }
}
