public class RoomProperty
{
    public const string Owner = "Owner";
    public const string Password = "Password";

    public const string RedScore = "RedScore";
    public const string BlueScore = "BlueScore";

    public const string CountdownTimerLimit = "CountdownTimerLimit";
    public const string ScoreLimit = "ScoreLimit";

    public const string StartTime = "StartTime";
    public const string CurrentCountdownTimer = "CurrentCountdownTimer";

    public const string GameState = "GameState";
    public const string Night = "Night";

    static public string [] GetPublicProperties()
    {
        string[] properties =
        {
            Owner, Password
        };

        return properties;
    }
}
