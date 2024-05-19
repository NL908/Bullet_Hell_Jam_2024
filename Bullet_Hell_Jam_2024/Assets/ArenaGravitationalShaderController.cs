using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGravitationalShaderController : MonoBehaviour
{
    private Material material;
    private void Awake()
    {
        SpriteRenderer _sr = GetComponent<SpriteRenderer>();
        material = _sr.material;
    }

    private void LateUpdate()
    {
        material.SetVector("_FocalPoint", (Vector2)Player.instance.transform.position);
    }
}
