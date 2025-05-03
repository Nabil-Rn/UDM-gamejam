using System.Collections.Generic;

public static class GameData
{
    // public static bool undercover = false;
    // public static bool talkedToBartender = false;
    // public static bool heardSailorRumor = false;
    private static Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public static void SetFlag(string key, bool value)
    {
        flags[key] = value;
    }

    public static bool GetFlag(string key)
    {
        return flags.ContainsKey(key) && flags[key];
    }

    public static bool HasFlag(string key)
    {
        return flags.ContainsKey(key);
    }

    public static void ClearAll()
    {
        flags.Clear();
    }
}
