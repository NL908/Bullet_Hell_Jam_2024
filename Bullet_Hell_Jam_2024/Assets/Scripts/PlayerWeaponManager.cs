using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public int selectedWeaponIndex = 0;
    PlayerInputHandler inputHandler;
    [SerializeField] ProjectileEmitter[] emitters;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileEmitter currentEmitter = emitters[selectedWeaponIndex];
        currentEmitter.isActive = inputHandler.fireButton;
    }

    public void SelectNextWeapon()
    {

    }

    public void SelectPrevWeapon()
    {

    }
}
