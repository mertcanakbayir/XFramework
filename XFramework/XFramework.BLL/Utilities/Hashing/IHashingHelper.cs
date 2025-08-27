namespace XFramework.BLL.Utilities.Hashing
{
    public interface IHashingHelper
    {
        string HashPassword(string plainedPassword);
        bool VerifyPassword(string hashedPassword, string plainedPassword);

    }
}
