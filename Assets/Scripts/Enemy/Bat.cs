using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Enemy
{
    public class Bat : Enemy
    {
        [SerializeField] private VoidEvent spawnedCreature;

        private void Start()
        {
            spawnedCreature.Raise();
        }

        public override void SetPath(Path value)
        {
            base.SetPath(value);
            SetIndex(path.GetPathLength() - 1);
        }
    }
}