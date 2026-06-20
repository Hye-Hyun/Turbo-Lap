using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine: MonoBehaviour
{
    [SerializeField] private RaceStartUI raceStartUI;
    [SerializeField] private RaceTimer raceTimer;

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

                PlayerPrefs.SetFloat("FinalTime", raceTimer.ElapsedTime);

                PlayerPrefs.SetFloat(
                    "TopSpeed",
                    RaceManager.Instance.maxSpeed);

                PlayerPrefs.SetInt(
                    "CollisionCount",
                    RaceManager.Instance.collisionCount);

                raceStartUI.StopRaceAudio(); //레이스 종료 시 엔진음 정지

                //Time.timeScale = 0f;

                SceneManager.LoadScene("Result Scene");
            }
        }
    }
}
