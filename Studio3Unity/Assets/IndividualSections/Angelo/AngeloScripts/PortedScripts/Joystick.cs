using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector3 Input { set; get; }
    public float Xposition;
    public float Yposition;
    public Image TheJoyStick;
    public Image JoystickLimiter;
    public float JoyStickMovementLimiter;

    public void Start()
    {
        JoystickLimiter = GetComponent<Image>();
        TheJoyStick = transform.GetChild(0).GetComponent<Image>();
        //Sets Input to Zero Initially
        Input = Vector3.zero;
    }

    public virtual void OnPointerDown(PointerEventData DATA)
    {
        OnDrag(DATA);
    }

    public virtual void OnPointerUp(PointerEventData DATA)
    {
        Input = Vector3.zero;
        TheJoyStick.rectTransform.anchoredPosition = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData DATA)
    {
        Vector2 JoyStickPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickLimiter.rectTransform, DATA.position, DATA.pressEventCamera, out JoyStickPosition))
        {
            JoyStickPosition.x = (JoyStickPosition.x / JoystickLimiter.rectTransform.sizeDelta.x);
            JoyStickPosition.y = (JoyStickPosition.y / JoystickLimiter.rectTransform.sizeDelta.y);

            Yposition = (JoystickLimiter.rectTransform.pivot.y == 1) ? JoyStickPosition.y * 2 + 1 : JoyStickPosition.y * 2 - 1;
            Xposition = (JoystickLimiter.rectTransform.pivot.x == 1) ? JoyStickPosition.x * 2 + 1 : JoyStickPosition.x * 2 - 1;

            Input = new Vector3(Xposition, 0, Yposition);

            Input = (Input.magnitude > 1) ? Input.normalized : Input;

            TheJoyStick.rectTransform.anchoredPosition = new Vector3(Input.x * (JoystickLimiter.rectTransform.sizeDelta.x / JoyStickMovementLimiter), Input.z * (JoystickLimiter.rectTransform.sizeDelta.y / JoyStickMovementLimiter));
        }
    }

    public void Update()
    {
        //Debug.Log(Xposition);
        //Debug.Log(Yposition);
        Debug.Log(Input);
    }
}