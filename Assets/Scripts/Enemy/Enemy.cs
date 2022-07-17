using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour 
    {

        protected Path path;
        [SerializeField] protected float speed = 1f;
        [SerializeField] private IntVariable playerHealth;
        [SerializeField] private float health;
        [SerializeField] private GameObject healthBar;
        [SerializeField] private VoidEvent enemyDied;
        private UIHealth uiHealth;
        
        [Header("Sprites")] 
        private Vector2 moveDirection;
        private SpriteRenderer spriteRenderer;
        [SerializeField] private bool useAnimator;
        [SerializeField] private Sprite lookUp;
        [SerializeField] private Sprite lookDown;
        [SerializeField] private Sprite lookRight;
        [SerializeField] private Sprite lookLeft;

        private int index;
        private bool reachedEnd;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            GameObject bar = Instantiate(healthBar, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, transform);
            uiHealth = bar.GetComponentInChildren<UIHealth>();
            uiHealth.SetMaxHealth(Mathf.RoundToInt(health));
        }

        public virtual void SetPath(Path value)
        {
            path = value;
        }

        protected void SetIndex(int i)
        {
            index = i;
        }
        
        public void DealDamage(float damage)
        {
            health -= damage;
            uiHealth.SetHealth(Mathf.RoundToInt(health));
            if (health <= 0)
            {
                enemyDied.Raise();
                Destroy(gameObject);
            }
        }
        
        protected virtual void Update()
        {
            if (path == null) return;
            
            MoveAlongPath();
            if(!useAnimator)
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