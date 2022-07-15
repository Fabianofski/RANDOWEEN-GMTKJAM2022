using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour 
    {

        private Path path;
        [SerializeField] protected float speed = 1f;
        [SerializeField] private IntVariable playerHealth;

        private int index;
        private bool reachedEnd;


        public void SetPath(Path value)
        {
            path = value;
        }
        
        private void Update()
        {
            MoveAlongPath();
        }

        private void MoveAlongPath()
        {
            if (reachedEnd)
            {
                playerHealth.Subtract(10);
                Destroy(gameObject);
                return;
            }

            reachedEnd = path.ReachedEnd(index);

            index = path.UpdateIndex(transform.position, index);
            transform.position = path.MoveTowardsNextPoint(transform.position, index, speed / 500);
        }
    }
}