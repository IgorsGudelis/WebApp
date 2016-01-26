namespace DAL.ImgsServices
{
    public interface IImgsManager
    {
        string GetUserImg(int id);
        void ResizeImg(string path);
    }
}
