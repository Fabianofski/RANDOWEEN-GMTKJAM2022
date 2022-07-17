using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Enemy
{
    using UnityEngine;
    public class EnemySpawner : MonoBehaviour
    {
        private Queue<GameObject> enemyQueue;
        [SerializeField] private Path path;
        private float spawnRate;
        private float fluctuation;
        private float counter;
        private bool waveIsStarted;

        private void Awake()
        {
            enemyQueue = new Queue<GameObject>();
        }

        public bool QueueIsEmpty()
        {
            return enemyQueue.Count == 0;
        }

        public int QueueLength()
        {
            return enemyQueue.Count;
        }
        
        public void AddEnemies(List<GameObject> enemies)
        {
            foreach (GameObject enemy in enemies)
                enemyQueue.Enqueue(enemy);
        }

        private void Update()
        {
            counter -= Time.deltaTime;

            if (counter > 0 || !waveIsStarted) return;
            SpawnNextEnemy();
            counter = spawnRate + Random.Range(-fluctuation, fluctuation);
        }

        public void SpawnNextEnemy()
        {
            if (enemyQueue.Count == 0) return;
            
            GameObject go = Instantiate(enemyQueue.Dequeue(), path.GetStartingPosition(), Quaternion.identity);
            go.GetComponent<Enemy>().SetPath(path);
        }

        public void StartSpawningEnemies(float rate, float fluct)
        {
            spawnRate = rate;
            fluctuation = fluct;
            waveIsStarted = true;
            
            counter = spawnRate + Random.Range(-fluctuation, fluctuation);
        }

        public void StopSpawningEnemies()
        {
            waveIsStarted = false;
            enemyQueue.Clear();
        }
    }
}