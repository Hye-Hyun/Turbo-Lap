using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public bool passedCheckpoint = false;

    public int currentLap = 0;
    public int targetLap = 2;

    private void Awake()
    {
        Instance = this;
    }
}
