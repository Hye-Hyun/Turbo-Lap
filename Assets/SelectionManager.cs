using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [Header("Map")]
    public TextMeshProUGUI mapNameText;
    public Image mapPreview;

    [Header("Car")]
    public TextMeshProUGUI carNameText;
    public Image carPreview;

    public Sprite[] mapSprites;
    public Sprite[] carSprites;

    string[] mapNames =
    {
        "Night City Circuit",
        "Urban Expressway",
        "Metro Circuit",
        "Skyline Loop",
    };

    string[] carNames =
    {
        "Phantom",
        "Blue Vortex",
        "Crimson X",
        "Thunder R"
    };

    int mapIndex;
    int carIndex;

    void Start()
    {
        UpdateUI();
    }

    public void NextMap()
    {
        mapIndex = (mapIndex + 1) % mapNames.Length;
        UpdateUI();
    }

    public void PrevMap()
    {
        mapIndex = (mapIndex - 1 + mapNames.Length) % mapNames.Length;
        UpdateUI();
    }

    public void NextCar()
    {
        carIndex = (carIndex + 1) % carNames.Length;
        UpdateUI();
    }

    public void PrevCar()
    {
        carIndex = (carIndex - 1 + carNames.Length) % carNames.Length;
        UpdateUI();
    }

    void UpdateUI()
    {
        mapNameText.text = mapNames[mapIndex];
        carNameText.text = carNames[carIndex];

        mapPreview.sprite = mapSprites[mapIndex];
        carPreview.sprite = carSprites[carIndex];

    }
}