using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarController carController;
    [SerializeField] private float steeringValue;

    public void OnPointerDown(PointerEventData eventData)
    {
        carController.SetSteering(steeringValue);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        carController.SetSteering(0f);
    }
}
