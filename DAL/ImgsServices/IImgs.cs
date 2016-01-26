namespace DAL.ImgsServices
{
    public interface IImgs
    {
        string GetUserImg(int id);
        void ResizeImg(string path);
    }
}
