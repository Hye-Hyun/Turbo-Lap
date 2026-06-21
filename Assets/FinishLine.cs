using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishLine: MonoBehaviour
{
    [SerializeField] private RaceStartUI raceStartUI;
    [SerializeField] private RaceTimer raceTimer;

    [SerializeField] private CarController carController;

    [SerializeField] private FadeManager fadeManager;

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

                float finalTime = raceTimer.ElapsedTime;

                PlayerPrefs.SetFloat("FinalTime", raceTimer.ElapsedTime);

                float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

                if(finalTime < bestTime)
                {
                    PlayerPrefs.SetFloat("BestTime", finalTime);
                }

                PlayerPrefs.SetFloat(
                    "TopSpeed",
                    RaceManager.Instance.maxSpeed);

                PlayerPrefs.SetInt(
                    "CollisionCount",
                    RaceManager.Instance.collisionCount);

                StartCoroutine(FinishSequence());
            }
        }
    }

    private IEnumerator FinishSequence()
    {
        raceTimer.StopTimer();

        carController.FinishRace();

        raceStartUI.StopRaceAudio();

        yield return raceStartUI.ShowFinishUI();

        yield return fadeManager.FadeOut();

        SceneManager.LoadScene("Result Scene");
    }
}
