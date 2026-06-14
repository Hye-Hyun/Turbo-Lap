using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceManager.Instance.passedCheckpoint = true;

            Debug.Log("Checkpoint Passed!");
        }
    }
}
