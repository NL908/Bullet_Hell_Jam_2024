using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ControlIconHandler : MonoBehaviour
{
    [SerializeField] GameObject[] kmIcons;
    [SerializeField] GameObject[] controllerIcons;
    public bool gamepadButtonPressed;
    private void Update()
    {
        Gamepad currGamepad = Gamepad.current;
        if (currGamepad != null && !gamepadButtonPressed)
        {

            if (currGamepad.allControls.Any(x => (x is ButtonControl button && x.IsPressed() && !x.synthetic) ||
                (x is StickControl stick && stick.ReadValue().magnitude > 0.05f)))
            {
                gamepadButtonPressed = true;
                for(int i = 0; i < controllerIcons.Length; i++)
                {
                    kmIcons[i].SetActive(false);
                    controllerIcons[i].SetActive(true);
                }
            }
        }
        else if (gamepadButtonPressed)
        {
            if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
            {
                gamepadButtonPressed = false;
                for (int i = 0; i < controllerIcons.Length; i++)
                {
                    kmIcons[i].SetActive(true);
                    controllerIcons[i].SetActive(false);
                }
            }
        }
    }


}
