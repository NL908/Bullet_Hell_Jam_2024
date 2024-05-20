using System;
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
        UpdateWeaponSelectionUI();
    }

    // Update is called once per frame
    void Update()
    {
        ProjectileEmitter currentEmitter = emitters[selectedWeaponIndex];
        weaponTimer -= Time.deltaTime;
        if (inputHandler.fireButton) {
            if (weaponTimer <= 0) {
                // Play corresponding fire sound
                switch (selectedWeaponIndex) {
                    case 0:
                        // Rifle
                        AudioManager.instance.PlaySound("RifleShoot");
                        break;
                    case 1:
                        // Sniper
                        AudioManager.instance.PlaySound("SniperShoot");
                        break;
                    case 2:
                        // Rocket
                        AudioManager.instance.PlaySound("RocketShoot");
                        break;
                }
                // Fire current selected weapon
                currentEmitter.EmitProjectile();
                weaponTimer = currentEmitter.emitInterval;
                if (EnemyGenerationManager.instance.isActive)
                {
                    EnemyGenerationManager.instance.UpdateWeaponFire(selectedWeaponIndex);
                    EnemyGenerationManager.instance.UpdateHeatFire(selectedWeaponIndex, currentEmitter.emitInterval);
                }
            }
        }
    }

    #region Select Weapon Methods
    public void SelectNextWeapon()
    {
        selectedWeaponIndex += 1;
        if (selectedWeaponIndex >= emitters.Length) {
            selectedWeaponIndex = 0;
        }
        UpdateWeaponSelectionUI();
    }

    public void SelectPrevWeapon()
    {
        selectedWeaponIndex -= 1;
        if (selectedWeaponIndex < 0) {
            selectedWeaponIndex = emitters.Length - 1;
        }
        UpdateWeaponSelectionUI();
    }

    public void SelectWeapon1()
    {
        selectedWeaponIndex = 0;
        UpdateWeaponSelectionUI();
    }

    public void SelectWeapon2()
    {
        selectedWeaponIndex = 1;
        UpdateWeaponSelectionUI();
    }

    public void SelectWeapon3()
    {
        selectedWeaponIndex = 2;
        UpdateWeaponSelectionUI();
    }
    #endregion

    private void UpdateWeaponSelectionUI()
    {
        try
        {
            CanvasScript.instance.UpdateSelectedWeapon(selectedWeaponIndex);
        }
        catch (Exception e) { Debug.LogError("Select Weapon UI update error with: " + e.Message); }
    }
}
