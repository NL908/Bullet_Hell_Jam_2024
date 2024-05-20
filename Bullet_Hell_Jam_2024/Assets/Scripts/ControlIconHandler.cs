using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ControlIconHandler : MonoBehaviour
{
    private bool gamepadButtonPressed;
    private void Update()
    {
        Gamepad currGamepad = Gamepad.current;
        if (currGamepad != null && !gamepadButtonPressed)
        {
            gamepadButtonPressed = currGamepad.allControls.Any(x => (x is ButtonControl button && x.IsPressed() && !x.synthetic) ||
                (x is StickControl stick && stick.ReadValue().magnitude > 0.05f));
        }
        else if (gamepadButtonPressed)
        {
            if (Input.anyKey)
            {
                gamepadButtonPressed = false;
            }
        }
    }
}
