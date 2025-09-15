using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject healthPowerUpPrefab;
    public GameObject shieldPowerUpPrefab;
    public GameObject fireRatePowerUpPrefab;


    public float timeBetweenWaves = 5f;

    private int currentWave = 1;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            GameManager.instance.UpdateWaveText(currentWave);

            int enemyCount = currentWave + 2;

            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }

            

            // ✅ 30% chance to spawn a power-up
            if (Random.value < 0.3f)
            {
                SpawnPowerUp();
            }

            currentWave++;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * 5f;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnPowerUp()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * 5f;

        GameObject powerUpToSpawn = null;

        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                powerUpToSpawn = healthPowerUpPrefab;
                break;
            case 1:
                powerUpToSpawn = shieldPowerUpPrefab;
                break;
            case 2:
                powerUpToSpawn = fireRatePowerUpPrefab;
                break;
        }

        if (powerUpToSpawn != null)
        {
            Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
        }
    }

 
}
