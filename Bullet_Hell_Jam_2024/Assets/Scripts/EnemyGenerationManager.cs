using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerationManager : MonoBehaviour
{
    public static EnemyGenerationManager instance;

    [SerializeField]
    public EnemyGenerationProgressGroup[] progressGroups;

    // Passive generation rate. Unit in per second
    public float passiveGenerationRate = 1f;

    private void Update()
    {
        UpdatePassiveGeneration();
    }

    private void UpdatePassiveGeneration()
    {
        float deltaTime = Time.deltaTime;
        foreach(EnemyGenerationProgressGroup group in progressGroups)
        {
            group.UpdateProgress(passiveGenerationRate * deltaTime);
        }
    }

    // Update every progress in a progress group with the index
    // Called this when fire weapon?
    public void UpdateWeaponFire(int index)
    {
        progressGroups[index].UpdateProgress(progressGroups[index].fireProgressRate);
    }
}
