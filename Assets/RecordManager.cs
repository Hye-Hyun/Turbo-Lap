using UnityEngine;

public static class RecordManager
{
    private const string BestTimeKey = "BestTime";

    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);
    }

    public static bool SaveRecord(float currentTime)
    {
        float bestTime = GetBestTime();

        if (currentTime < bestTime)
        {
            PlayerPrefs.SetFloat(BestTimeKey, currentTime);
            PlayerPrefs.Save();
            return true; // ½Å±â·Ï
        }

        return false;
    }
}