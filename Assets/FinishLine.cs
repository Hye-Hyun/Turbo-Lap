using UnityEngine;

public class FinishLine: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        if (RaceManager.Instance.passedCheckpoint)
        {
            Debug.Log("Race Finished!");

            Time.timeScale = 0f;
        }
    }
}
