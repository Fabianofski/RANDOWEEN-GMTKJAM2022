using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    [SerializeField] private GameObject dummyEnemy;
    public bool started;
    
    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            enemies.Add(dummyEnemy);
        }
        enemySpawner.AddEnemies(enemies);
    }

    public void StartNextWave()
    {
        enemySpawner.StartSpawningEnemies(1, .5f);
    }
}
