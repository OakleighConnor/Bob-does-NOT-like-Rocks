using UnityEngine;
using UnityEngine.InputSystem;
public class SwipeDetection : MonoBehaviour
{
    public static SwipeDetection instance;

    public delegate void Swipe(Vector2 direction);

    public event Swipe swipePerformed;

    [SerializeField] InputAction position, press;

    [SerializeField] float swipeResistence = 100;

    Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();
        instance = this;
    }

    void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;

        Vector2 direction = Vector2.zero;

        
        if(Mathf.Abs(delta.x) > swipeResistence)
        {
            direction.x = delta.x;
        }

        if (Mathf.Abs(delta.y) > swipeResistence)
        {
            direction.y = delta.y;
        }

        if(direction != Vector2.zero & swipePerformed != null)
        {
            swipePerformed(direction);
        }

    }
}
