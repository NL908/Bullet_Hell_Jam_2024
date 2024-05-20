using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds", menuName = "ScriptableObjects/SoundsScriptableObject", order = 1)]
public class SoundsScriptableObject : ScriptableObject
{
    [SerializeField] public Sound[] sounds;
}
