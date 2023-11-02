namespace CleanArchitecture.Presentation.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = null!;

    public int MaxRetryCount { get; set; } = 3;

    public int CommandTimeout { get; set; } = 30;

    public bool EnableDetailedErrors { get; set; } = false;

    public bool EnableSensitiveDataLogging { get; set; } = false;
}
