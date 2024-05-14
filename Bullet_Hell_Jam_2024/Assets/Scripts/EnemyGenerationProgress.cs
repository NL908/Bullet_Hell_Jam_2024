using UnityEngine;

[System.Serializable]
public class EnemyGenerationProgress
{
    // Progress variables
    //[HideInInspector]
    public float currentProgress;

    [SerializeField]
    private float maxProgress = 10;

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int enemySpawnNumber = 1;

    public EnemyGenerationProgress(float maxProgress)
    {
        this.maxProgress = maxProgress;
        currentProgress = 0;
    }

    // Method to update progress
    // Should be called in the generation manager. E.g. If generate passively, amount = Time.deltatime.
    public void UpdateProgress(float amount)
    {
        currentProgress = Mathf.Clamp(currentProgress + amount, 0f, maxProgress);
    }

    // Method to check if progress is complete
    public bool IsProgressComplete()
    {
        return currentProgress >= maxProgress;
    }

    // Return the percenrage of the progress. 0 means it's empty and 1 means it should generate now.
    public float GetProgressPercentage()
    {
        return maxProgress / currentProgress;
    }

    // return a tuple with enemy GameObject and the number of enemy spawn with it
    public (GameObject, int) GetEnemyPrefab()
    {
        return (enemyPrefab, enemySpawnNumber);
    }
}
