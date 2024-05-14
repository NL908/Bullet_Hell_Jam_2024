using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public int selectedWeaponIndex = 0;
    PlayerInputHandler inputHandler;
    [SerializeField] ProjectileEmitter[] emitters;

    float weaponTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileEmitter currentEmitter = emitters[selectedWeaponIndex];
        weaponTimer -= Time.deltaTime;
        if (inputHandler.fireButton) {
            if (weaponTimer <= 0) {
                currentEmitter.EmitProjectile();
                weaponTimer = currentEmitter.emitInterval;
            }
        }
    }

    public void SelectNextWeapon()
    {

    }

    public void SelectPrevWeapon()
    {

    }
}
