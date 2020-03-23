public class RoomProperty
{
    public const string Owner = "Owner";
    public const string Password = "Password";
    public const string TeamsNumber = "TN";
    public const string TeamSize = "TS";
    public const string Mode = "M1";
    public const string Map = "M2";

    static public string [] GetProperties()
    {
        string[] properties =
        {
            Owner, Password
        };

        return properties;
    }
}
