/*
    Purpose: Holds persistent game data across scene loads.
    Static class, since no GameObject needed, it lives in memory.
    We use this to track stats during ONLY the current play session because of main menu logic
*/
public static class SaveData
{
    public static int trueDay = 1;
    public static int dayIncludingFillerDays = 1;
    public static int AIAngerMeter = 0;
    public static int captchaPoints = 0;
    public static int captchaCurrentDay = 0;
    public static bool hasExistingSave = false;
    public static float masterVolume = 1f;
}