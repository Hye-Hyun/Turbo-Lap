using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public bool passedCheckpoint = false;

    public int currentLap = 0;
    public int targetLap = 2;

    public float finalTime;
    public float bestLapTime;
    public float maxSpeed;
    public int collisionCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
