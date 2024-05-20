using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGravitationalShaderController : MonoBehaviour
{
    [SerializeField]
    private float magnification;
    private Material material;

    private void Start()
    {
        SpriteRenderer _sr = GetComponent<SpriteRenderer>();
        material = _sr.material;
        material.SetFloat("_Magnification", magnification);
    }

    private void LateUpdate()
    {
        material.SetVector("_FocalPoint", (Vector2)Player.instance.transform.position);
    }
}
