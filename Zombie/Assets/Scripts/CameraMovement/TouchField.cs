using UnityEngine;
using UnityEngine.EventSystems;


public class TouchField : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Camera cam;
    CameraMovement camMov;

    void Start()
    {
        camMov = cam.GetComponent<CameraMovement>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        camMov.Pressed = true;
        camMov.PointerId = eventData.pointerId;
        camMov.PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        camMov.Pressed = false;
    }
}
