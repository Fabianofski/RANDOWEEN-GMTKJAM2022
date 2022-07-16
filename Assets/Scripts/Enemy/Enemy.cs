using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour 
    {

        private Path path;
        [SerializeField] protected float speed = 1f;
        [SerializeField] private IntVariable playerHealth;
        [SerializeField] private float health;
        [SerializeField] private GameObject healthBar;
        private UIHealth uiHealth;
        
        [Header("Sprites")] 
        private Vector2 moveDirection;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite lookUp;
        [SerializeField] private Sprite lookDown;
        [SerializeField] private Sprite lookRight;
        [SerializeField] private Sprite lookLeft;

        private int index;
        private bool reachedEnd;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GameObject bar = Instantiate(healthBar, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, transform);
            uiHealth = bar.GetComponentInChildren<UIHealth>();
            uiHealth.SetMaxHealth(Mathf.RoundToInt(health));
        }

        public void SetPath(Path value)
        {
            path = value;
        }

        public void DealDamage(float damage)
        {
            health -= damage;
            uiHealth.SetHealth(Mathf.RoundToInt(health));
            if(health <= 0) Destroy(gameObject);
        }
        
        protected virtual void Update()
        {
            if (path == null) return;
            
            MoveAlongPath();
            DisplayCorrectSprite();
        }

        private void MoveAlongPath()
        {
            if (EnemyReachedEnd()) return;

            Vector2 position = transform.position;
            index = path.UpdateIndex(position, index);
            transform.position = path.MoveTowardsNextPoint(position, index, speed);
        }

        private bool EnemyReachedEnd()
        {
            if (reachedEnd)
            {
                playerHealth.Subtract(10);
                Destroy(gameObject);
                return true;
            }
            reachedEnd = path.ReachedEnd(index);
            return false;
        }

        private void DisplayCorrectSprite()
        {
            moveDirection = path.GetNextPosition(index) - (Vector2) transform.position;

            if (Math.Abs(moveDirection.x) > Math.Abs(moveDirection.y))
                spriteRenderer.sprite = moveDirection.x > 0 ? lookRight : lookLeft;
            else
                spriteRenderer.sprite = moveDirection.y > 0 ? lookUp : lookDown;
        }
    }
}