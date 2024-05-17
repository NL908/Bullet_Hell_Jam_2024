using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayerProjectile : Projectile
{
    [SerializeField] float rotateSpeed = 360f;    // Rotation speed (degrees per second)
    [SerializeField] float trackDuration = 5f; // The duration of the projectile tracking player (second)

    private Transform target;
    private bool isTracking;
    // Update is called once per frame

    protected override void Start()
    {
        base.Start();
        isTracking = true;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StopTracking(trackDuration));
    }

    void FixedUpdate()
    {
        if (target != null && isTracking)
        {
            Vector2 direction = (Vector2)target.position - _rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rb.angularVelocity = -rotateAmount * rotateSpeed;
        }
        else _rb.angularVelocity = 0f;
        _rb.velocity = transform.up * speed;
    }

    // Turns off tracking after a set amount of time
    IEnumerator StopTracking(float duration)
    {
        yield return new WaitForSeconds(duration);
        isTracking = false;
    }
}
