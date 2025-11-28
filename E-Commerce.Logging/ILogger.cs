namespace E_Commerce.Logging
{
    public interface IAppLogger<T> where T : class
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void Debug(string message);
    }
}