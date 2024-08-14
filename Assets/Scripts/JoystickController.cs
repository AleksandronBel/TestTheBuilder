using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Image _joystickBackground;
    [SerializeField] Image _joystickCenter;
    Vector2 _inputVector;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        _inputVector = Vector2.zero;
        _joystickCenter.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickBackground.rectTransform, 
                ped.position, 
                ped.pressEventCamera, 
                out position))
        {
            position.x = (position.x / _joystickBackground.rectTransform.sizeDelta.x);
            position.y = (position.y / _joystickBackground.rectTransform.sizeDelta.x);

            _inputVector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
            _inputVector = (_inputVector.magnitude >1.0f)? _inputVector.normalized : _inputVector;

            _joystickCenter.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.x / 2));
        }
    }

    public float Horizontal()
    {
        if (_inputVector.x != 0)
            return _inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (_inputVector.y != 0)
            return _inputVector.y;
        else
            return Input.GetAxis("Vertical");
    }
}
