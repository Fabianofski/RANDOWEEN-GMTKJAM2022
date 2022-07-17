using UnityEngine;

namespace Enemy
{
    public class Bat : Enemy
    {
        public override void SetPath(Path value)
        {
            base.SetPath(value);
            SetIndex(path.GetPathLength() - 1);
        }
    }
}