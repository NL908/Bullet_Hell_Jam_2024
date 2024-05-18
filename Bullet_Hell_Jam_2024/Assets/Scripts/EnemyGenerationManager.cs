using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerationManager : MonoBehaviour
{
    public static EnemyGenerationManager instance;

    public bool isActive = false;

    [SerializeField]
    private float enemySpawnBufferDistance;
    [SerializeField]
    private float enemySpawnRadius;
    [SerializeField]
    private float enemyGroupSpawnDelay = 0.1f;

    [SerializeField]
    public EnemyGenerationProgressGroup[] progressGroups;

    // Passive generation rate. Unit in per second
    public float passiveGenerationRate = 1f;
    // Additional generation rate for the EnemyGenerationProgressGroup corresponding to the current selected weapon
    public float selectedGenerationRate = 1f;

    private Vector2 _arenaSize;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Check enemy spawn per 0.1 second. This saves about 0.0005 seconds, very efficient!
        // Totally can comment out this and use the one in Update()
        //InvokeRepeating("CheckEnemySpawns", 0, 0.1f);
        _arenaSize = GameMaster.instance.arenaSize;
    }

    private void Update()
    {
        if (isActive)
        {
            UpdatePassiveGeneration();
            UpdateSelectedWeaponGeneration();
            CheckEnemySpawns();
        }
    }

    #region Generation Progress Update methods
    private void UpdatePassiveGeneration()
    {
        float deltaTime = Time.deltaTime;
        foreach(EnemyGenerationProgressGroup group in progressGroups)
        {
            group.UpdateProgress(passiveGenerationRate * deltaTime);
        }
    }

    private void UpdateSelectedWeaponGeneration()
    {
        float deltaTime = Time.deltaTime;
        int currSelectedWeaponIndex = PlayerWeaponManager.instance.selectedWeaponIndex;
        progressGroups[currSelectedWeaponIndex].UpdateProgress(selectedGenerationRate * deltaTime);
    }

    // Update every progress in a progress group with the index
    // Called this when fire weapon?
    public void UpdateWeaponFire(int index)
    {
        if (isActive)
            progressGroups[index].UpdateProgress(progressGroups[index].fireProgressRate);
    }
    #endregion

    public float[][] GetAllGenerationPercentage()
    {
        float[][] percentageLists;
        percentageLists = new float[progressGroups.Length][];
        for (int i = 0; i < progressGroups.Length; i++)
        {
            percentageLists[i] = progressGroups[i].GetGenerationPercentage();
        }
        return percentageLists;
    }

    // Check all GenerationProgress in progressGroups and spawn enemies that has a full progression
    private void CheckEnemySpawns()
    {
        // Get a List of enemies that need to be spawned
        List<(GameObject, int)> enemyGameObjects = new List<(GameObject, int)>();
        foreach(EnemyGenerationProgressGroup progressGroup in progressGroups)
        {
            enemyGameObjects = enemyGameObjects.Concat(progressGroup.CheckIsProgressComplete()).ToList();
        }
        // Call SpawnEnemies for each enemy prefab and number of enemies to spawn in a group
        foreach ((GameObject enemy, int num) in enemyGameObjects)
        {
            StartCoroutine(SpawnEnemies(enemy, num));
        }
    }

    /* Spawn a group of enemies with a small interview between each enemy spawn in the group
       For each (enemy GameObject, enemy spawn number) in the list:
             - Random from 0 to 2pi as the spawning direction 
             - Get the position that is outside of the arena
             - Initialize them closely around the spawning point given a radius
    */
    private IEnumerator SpawnEnemies(GameObject enemyPrefab, int num)
    {
        // A random point on the edge of the arena
        Vector2 groupSpawnPoint = Utils.RandomPonitOnRectEdge(_arenaSize.x, _arenaSize.y);
        groupSpawnPoint += (groupSpawnPoint.normalized * (enemySpawnBufferDistance + enemySpawnRadius));

        // Spawn enemies and wait between each spawn
        for (int i = 0; i < num; i ++)
        {
            Vector2 spawnPoint = groupSpawnPoint + Random.insideUnitCircle * enemySpawnRadius;
            // TODO: For debugging purposes, spawm them all at groupSpawmPoint for now
            //Vector2 spawnPoint = groupSpawnPoint;
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, transform);
            // TODO: Change this with a SerializeField variable
            yield return new WaitForSeconds(enemyGroupSpawnDelay);
        }
    }
}
