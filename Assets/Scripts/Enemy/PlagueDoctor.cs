using Unity.Mathematics;
using UnityEngine;

namespace Enemy
{
    public class PlagueDoctor : Enemy
    {

        [SerializeField] private GameObject bat;
        
        protected override void Update()
        {
            base.Update();
        }

        public override void PerformSpecialAttack()
        {
            base.PerformSpecialAttack();
            GameObject newBat = Instantiate(bat, transform.position, Quaternion.identity);
            newBat.GetComponent<Enemy>().SetPath(path);
        }
    }
}