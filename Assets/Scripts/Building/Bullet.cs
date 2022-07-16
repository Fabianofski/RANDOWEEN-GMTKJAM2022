using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Building
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float damage;

        private void Awake()
        {
            Destroy(gameObject, 10);
        }

        private void Update()
        {
            transform.position += (transform.up * (speed * Time.deltaTime));
        }

        public void SetDamage(float dmg)
        {
            damage = dmg;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer != 6) return;
            
            col.GetComponent<Enemy.Enemy>().DealDamage(damage);
            Destroy(gameObject);
        }
    }
}