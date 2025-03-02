using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    PlayerInputHandler inputHandler;
    PlayerLocomotion playerLocomotion;

    Rigidbody2D _rb;
    bool invulnerable = false;
    

    // Stats
    [SerializeField] protected int life;
    [SerializeField] protected int maxLife;
    // flash effect after taking damage
    [SerializeField] float flashDuration = 2.0f;    // Total duration of the flash effect
    [SerializeField] float flashDelay = 0.1f;       // How quickly the sprite flashes on and off
    [SerializeField] GameObject screenClearWave;
    [SerializeField] protected ParticleSystem _deathParticle;
    [SerializeField] protected Color mainColor;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer dottedLineSprite;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gather player input 
        inputHandler.TickInput();
    }

    private void FixedUpdate()
    {
        UpdateLocomotion();
    }

    private void UpdateLocomotion()
    {
        playerLocomotion.HandleMovement(inputHandler.movementDirection);
        playerLocomotion.HandleAimDirection(inputHandler.aimDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            // Draw aim direction with red line
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, inputHandler.aimDirection * 3f);

            // Draw move direction with blue line
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, inputHandler.movementDirection * 3f);
        }
    }

    [ContextMenu("OnHit")]
    public void OnHit(Vector2 damageDirection)
    {
        if (invulnerable || !GameMaster.instance.isGameStarted) return;
        life = Mathf.Clamp(life - 1, 0, maxLife);

        try
        {
            CanvasScript.instance.UpdateLife(life);
        } catch { Debug.LogWarning("Canvas not loaded"); }
        if (life <= 0)
        {
            OnDeath(damageDirection);
        }
        else
        {
            AudioManager.instance.PlaySound("PlayerDamage");
            Instantiate(screenClearWave, transform.position, Quaternion.identity);
            invulnerable = true;
            StartCoroutine(FlashEffect());
        }
    }

    private void OnDeath(Vector2 damageDirection)
    {
        AudioManager.instance.PlaySound("PlayerDeath");
        playerSprite.enabled = false;
        dottedLineSprite.enabled = false;
        // So player cannot be moved anymore
        inputHandler.enabled = false;
        // remove collider
        GetComponent<Collider2D>().enabled = false;
        // burst some neat particles
        for (int i = 0; i < 5; i++)
        {
            ParticleSystem deathParticle = Instantiate(this._deathParticle, transform.position, Quaternion.LookRotation(Vector3.forward, damageDirection));
            Debug.Log(deathParticle);
            ParticleSystem.MainModule main = deathParticle.main;
            main.startColor = mainColor;
        }
        // Trigger gameover when player is ded
        GameMaster.instance.GameOver();
    }

    IEnumerator FlashEffect()
    {
        float elapsedTime = 0f;
        bool spriteActive = true;

        while (elapsedTime < flashDuration)
        {
            // Toggle visibility
            playerSprite.enabled = spriteActive;
            spriteActive = !spriteActive;

            // Wait for a bit before toggling visibility again
            yield return new WaitForSeconds(flashDelay);
            elapsedTime += flashDelay;
        }

        // Ensure sprite is visible after flashing ends
        // Also turn off invulnerbility
        playerSprite.enabled = true;
        invulnerable = false;
    }

}
