public class RoomProperty
{
    public const string Owner = "Owner";
    public const string Password = "Password";

    public const string RedScore = "RedScore";
    public const string BlueScore = "BlueScore";

    public const string StartTime = "StartTime";
    public const string GameTime = "GameTime";

    static public string [] GetProperties()
    {
        string[] properties =
        {
            Owner, Password
        };

        return properties;
    }
}
