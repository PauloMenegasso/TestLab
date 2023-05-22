namespace PersonCRUD.Services;

public interface IloggerService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(Exception ex);
}

public class LoggerService : IloggerService
{
    public void LogError(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    public void LogInformation(string message)
    {
        Console.WriteLine($"Info: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"Warning: {message}");
    }
}