using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    private CanvasGroup cg;

    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        cg.alpha = 0.4f + Mathf.PingPong(Time.time * 0.5f, 0.6f);
    }
}