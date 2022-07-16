using System;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Building
{
    public class TeslaCoil : MonoBehaviour, Building
    {
        [SerializeField] private GameObject lastUpgrade;
        [SerializeField] private GameObject nextUpgrade;
        [SerializeField] private LineRenderer[] beamLineRenderers;

        [SerializeField] private float radius;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask enemyLayer;

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
            Shoot(Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer));
        }

        private void Shoot(Collider2D[] closestEnemies)
        {
            closestEnemies = closestEnemies.OrderBy(
                (e) => Vector3.Distance(e.transform.position, transform.position)).ToArray();
            for (int i = 0; i < beamLineRenderers.Length; i++)
            {
                try
                {
                    Vector3 enemyPos = transform.InverseTransformPoint(closestEnemies[i].transform.position);
                    beamLineRenderers[i].SetPosition(1, enemyPos);
                    closestEnemies[i].GetComponent<Enemy.Enemy>().DealDamage(damage * Time.deltaTime);
                }
                catch (Exception)
                {
                    beamLineRenderers[i].SetPosition(1, Vector3.zero);
                }
                
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}