using UnityEngine;

public class FinishLine: MonoBehaviour
{
    [SerializeField] private RaceStartUI raceStartUI;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        if (RaceManager.Instance.passedCheckpoint)
        {
            RaceManager.Instance.currentLap++;

            Debug.Log($"Lap {RaceManager.Instance.currentLap} completed!");

            RaceManager.Instance.passedCheckpoint = false;

            if (RaceManager.Instance.currentLap >= RaceManager.Instance.targetLap)
            {
                Debug.Log("Race Finished!");

                raceStartUI.StopRaceAudio(); //레이스 종료 시 엔진음 정지

                Time.timeScale = 0f;
            }
        }
    }
}
