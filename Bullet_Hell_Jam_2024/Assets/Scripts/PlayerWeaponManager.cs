using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public static PlayerWeaponManager instance;

    public int selectedWeaponIndex = 0;
    PlayerInputHandler inputHandler;
    [SerializeField] ProjectileEmitter[] emitters;

    float weaponTimer = 0f;

    private void Awake()
    {
        instance = this;
    }

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
                // Fire current selected weapon
                currentEmitter.EmitProjectile();
                weaponTimer = currentEmitter.emitInterval;
                if (EnemyGenerationManager.instance.isActive)
                {
                    EnemyGenerationManager.instance.UpdateWeaponFire(selectedWeaponIndex);
                }
            }
        }
    }

    public void SelectNextWeapon()
    {
        selectedWeaponIndex += 1;
        if (selectedWeaponIndex >= emitters.Length) {
            selectedWeaponIndex = 0;
        }
    }

    public void SelectPrevWeapon()
    {
        selectedWeaponIndex -= 1;
        if (selectedWeaponIndex < 0) {
            selectedWeaponIndex = emitters.Length - 1;
        }
    }
}
