using System;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Building
{
    public class Cannon : MonoBehaviour, Building
    {
        [SerializeField] private GameObject cannonGO;
        [SerializeField] private Transform[] bulletOutputs;
        [SerializeField] private GameObject bullet;
        [SerializeField] private float radius;
        [SerializeField] private float damage;
        [SerializeField] private float fireRate;
        [SerializeField] private LayerMask enemyLayer;
        

        private float counter;
        private float shootDir;

        [SerializeField] private GameObject lastUpgrade;
        [SerializeField] private GameObject nextUpgrade;

        public void Upgrade()
        {
            if (nextUpgrade == null) return;
            nextUpgrade.SetActive(true);
            gameObject.SetActive(false);
        }
        public bool IsUpgradeable()
        {
            return nextUpgrade != null;
        }
        public void Downgrade()
        {
            if (lastUpgrade == null)
            {
                Destroy(transform.parent.gameObject);
                return;
            }
            lastUpgrade.SetActive(true);
            gameObject.SetActive(false);
        }
        public bool IsDowngradeable()
        {
            return true;
        }

        public void Enable()
        {
            this.enabled = true;
        }
        public void Disable()
        {
            this.enabled = false;
        }

        private void Update()
        {
            if (!TargetEnemy()) return;

            counter -= Time.deltaTime;
            if(counter > 0) return;
            Shoot();
            counter = fireRate;
        }

        private bool TargetEnemy()
        {
            Collider2D[] closestEnemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

            if (closestEnemies.Length == 0) return false;
            closestEnemies = closestEnemies.OrderBy((e) => Vector3.Distance(e.transform.position, transform.position)).ToArray();
            Collider2D closestEnemy = closestEnemies[0];

            Vector2 dir = (transform.position - closestEnemy.transform.position).normalized;
            shootDir = math.atan2(dir.x, -dir.y) * 180 / math.PI;
            cannonGO.transform.rotation = Quaternion.Euler(0, 0, shootDir);
            return true;
        }

        private void Shoot()
        {
            foreach (Transform output in bulletOutputs)
            {
                GameObject go = Instantiate(bullet, output.position, output.rotation);
                go.GetComponent<Bullet>().SetDamage(damage);
            }
        }
        
        public int GetLevel()
        {
            if (lastUpgrade == null) return 1;
            else if (nextUpgrade == null) return 3;
            else return 2;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}