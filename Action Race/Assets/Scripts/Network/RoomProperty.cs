public class RoomProperty
{
    public const string Owner = "Owner";
    public const string Password = "Password";

    public const string RedScore = "RedScore";
    public const string BlueScore = "BlueScore";

    public const string TimeLimit = "TimeLimit";
    public const string ScoreLimit = "ScoreLimit";

    public const string StartTime = "StartTime";
    public const string GameState = "GameState";

    static public string [] GetPublicProperties()
    {
        string[] properties =
        {
            Owner, Password
        };

        return properties;
    }
}
