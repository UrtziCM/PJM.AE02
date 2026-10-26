using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEventDispatcher : MonoBehaviour
{
    InputAction mainTouch;
    Action<InputAction.CallbackContext> tapMethod;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mainTouch = InputSystem.actions.FindAction("MainTouch");
        tapMethod = (InputAction.CallbackContext cc) => Tapped(cc);
        mainTouch.performed += tapMethod;

    }
    private void Tapped(InputAction.CallbackContext context)
    {
        EventBus.Call(new ScreenTappedData(context.ReadValue<Vector2>())); // en esta línea se me ha ido 1 hora
    }

}
