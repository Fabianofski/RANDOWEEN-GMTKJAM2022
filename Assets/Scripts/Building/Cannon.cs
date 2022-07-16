using System;
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

        public void Downgrade()
        {
            if (lastUpgrade == null) return;
            lastUpgrade.SetActive(true);
            gameObject.SetActive(false);
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
            Collider2D closestEnemy = Physics2D.OverlapCircle(transform.position, radius, enemyLayer);
            if (closestEnemy == null) return false;

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
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}