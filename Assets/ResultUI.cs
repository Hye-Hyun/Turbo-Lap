using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeText;
    [SerializeField] private TextMeshProUGUI topSpeedText;
    [SerializeField] private TextMeshProUGUI collisionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);

        finalTimeText.text = FormatTime(finalTime);

        float topSpeed = PlayerPrefs.GetFloat("TopSpeed", 0f);

        topSpeedText.text = $"{topSpeed:F1} km/h";

        int collisionCount = PlayerPrefs.GetInt("CollisionCount", 0);

        collisionText.text = collisionCount.ToString();
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        float seconds = time % 60;

        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
}
