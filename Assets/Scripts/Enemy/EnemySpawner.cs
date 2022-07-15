using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Enemy
{
    using UnityEngine;
    public class EnemySpawner : MonoBehaviour
    {
        private Queue<GameObject> enemiesThisWave;
        [SerializeField] private GameObject dummyEnemy;
        [SerializeField] private Path path;

        private void Awake()
        {
            enemiesThisWave = new Queue<GameObject>();

            List<GameObject> enemies = new List<GameObject>();
            for (int i = 0; i < 10; i++)
            {
                enemies.Add(dummyEnemy);
            }

            AddEnemies(enemies);
        }

        public void AddEnemies(List<GameObject> enemies)
        {
            foreach (GameObject enemy in enemies)
                enemiesThisWave.Enqueue(enemy);
        }

        public void SpawnNextEnemy()
        {
            if (enemiesThisWave.Count == 0) return;
            
            GameObject go = Instantiate(enemiesThisWave.Dequeue(), path.GetStartingPosition(), Quaternion.identity);
            go.GetComponent<Enemy>().SetPath(path);
        }

        public void OnSpawn()
        {
            SpawnNextEnemy();
        }
        
    }
}