using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public bool passedCheckpoint = false;

    private void Awake()
    {
        Instance = this;
    }
}
