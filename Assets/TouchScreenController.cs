using UnityEngine;
using UnityEngine.InputSystem;
public class TouchScreenController : MonoBehaviour
{
    TouchControls touchControls;
    private void Awake()
    {
        touchControls = new TouchControls();
    }
    private void OnEnable()
    {
        touchControls.Enable();
    }
    void OnDisable()
    {
        touchControls.Disable();
    }
    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touch press started");
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touch press ended");
    }
}
