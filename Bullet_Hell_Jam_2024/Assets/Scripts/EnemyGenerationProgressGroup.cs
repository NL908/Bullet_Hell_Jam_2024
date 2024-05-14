using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGenerationProgressGroup
{
    [SerializeField]
    private EnemyGenerationProgress easyProgress;

    [SerializeField]
    private EnemyGenerationProgress mediumProgress;

    [SerializeField]
    private EnemyGenerationProgress hardProgress;

    [SerializeField]
    public float fireProgressRate;

    // Method to update progress for all progress within the group 
    public void UpdateProgress(float amount)
    {
        easyProgress.UpdateProgress(amount);
        mediumProgress.UpdateProgress(amount);
        hardProgress.UpdateProgress(amount);
    }

    // Method to check if any progress has complete
    // Return a list of enemy prefabs for complete progresses
    public List<(GameObject, float)> CheckIsProgressComplete()
    {
        List<(GameObject, float)> enemyGameObjects = new List<(GameObject, float)>();
        
        if (easyProgress.IsProgressComplete())
        {
            easyProgress.currentProgress = 0;
            enemyGameObjects.Add(easyProgress.GetEnemyPrefab());
        }
        if (mediumProgress.IsProgressComplete())
        {
            mediumProgress.currentProgress = 0;
            enemyGameObjects.Add(mediumProgress.GetEnemyPrefab());
        }
        if (hardProgress.IsProgressComplete())
        {
            hardProgress.currentProgress = 0;
            enemyGameObjects.Add(hardProgress.GetEnemyPrefab());
        }

        return enemyGameObjects;
    }

    public float[] GetGenerationPercentage()
    {
        float[] percentageList = {
            easyProgress.GetProgressPercentage(),
            easyProgress.GetProgressPercentage(),
            easyProgress.GetProgressPercentage()
        };
        return percentageList;
    }
}