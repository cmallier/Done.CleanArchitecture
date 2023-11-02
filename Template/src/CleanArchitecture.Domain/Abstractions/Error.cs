namespace CleanArchitecture.Domain.Abstractions;

public record Error( string Code, string Name )
{
    public static Error None = new( string.Empty, string.Empty );

    public static Error NullValue = new( "Error.NullValue", "Null value was provided" );

    public static Error NotFound = new( "Error.NotFound", "Item was not found" );

    public static Error NotCreated = new( "Error.NotCreated", "Item was not created" );

    public static Error NotUpdated = new( "Error.NotCreated", "Item was not created" );
}
