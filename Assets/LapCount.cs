using TMPro;
using UnityEngine;

public class LapCount : MonoBehaviour
{
    [SerializeField] private TMP_Text lapText;

    // Update is called once per frame
    void Update()
    {
        int displayLap = Mathf.Min(
            RaceManager.Instance.currentLap + 1,
            RaceManager.Instance.targetLap
        );

        lapText.text =
            $"LAP {displayLap} / {RaceManager.Instance.targetLap}";
    }
}
