using System;
using System.Collections.Generic;
using Enemy;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    [SerializeField] private BoolVariable lastEnemyDied;
    [SerializeField] private List<GameManager.Placeable> enemies;
    private int enemiesToBeKilled;

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void EnemyDied()
    {
        enemiesToBeKilled--;
        lastEnemyDied.Value = enemySpawner.QueueIsEmpty() && enemiesToBeKilled <= 0;
        
        if(lastEnemyDied.Value) enemySpawner.StopSpawningEnemies();
    }
    
    public void StartNextWave()
    {
        List<GameObject> added = new List<GameObject>();
        foreach (GameManager.Placeable placeable in enemies)
            for (int i = 0; i < placeable.amount.Value; i++)
                added.Add(placeable.instance);
        Shuffle(added);
        enemySpawner.AddEnemies(added);
        
        lastEnemyDied.Value = false;
        enemiesToBeKilled = enemySpawner.QueueLength();
        enemySpawner.StartSpawningEnemies(3, .5f);
    }
    
    public void Shuffle<T>(IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = random.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
