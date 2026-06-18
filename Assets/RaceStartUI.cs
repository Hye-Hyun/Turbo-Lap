using UnityEngine;
using System.Collections;

public class RaceStartUI : MonoBehaviour
{
    [SerializeField] private GameObject readyUI;
    [SerializeField] private GameObject count3UI;
    [SerializeField] private GameObject count2UI;
    [SerializeField] private GameObject count1UI;
    [SerializeField] private GameObject startUI;
    [SerializeField] private RaceTimer raceTimer;

    public static bool raceStarted = false; //차량 움직임 방지 

    private IEnumerator Start()
    {
        readyUI.SetActive(false);
        count3UI.SetActive(false);
        count2UI.SetActive(false);
        count1UI.SetActive(false);
        startUI.SetActive(false);

        readyUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        readyUI.SetActive(false);

        count3UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        count3UI.SetActive(false);

        count2UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        count2UI.SetActive(false);

        count1UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        count1UI.SetActive(false);

        startUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        startUI.SetActive(false);

        raceStarted = true; //카운트다운 이후 차량 움직임
        raceTimer.StartTimer(); //카운트다운 이후 타이머 동작

        Debug.Log("Race Start!");
    }
}
