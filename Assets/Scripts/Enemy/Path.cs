using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class Path : MonoBehaviour {

        private Transform[] pointList;
        [SerializeField] private float tolerance;

        private void Awake()
        {
            GetPathPoints();
        }

        public Vector2 GetStartingPosition()
        {
            return pointList[0].position;
        }
        
        public int GetPathLength()
        {
            return pointList.Length;
        }
    
        public Vector2 GetNextPosition(int index)
        {
            if (index > pointList.Length - 1) return Vector2.one;
            return pointList[index].position;
        }
    
        private void GetPathPoints()
        {
            var transforms = new HashSet<Transform>(GetComponentsInChildren<Transform>());
            transforms.Remove(transform);
            pointList = transforms.ToArray();
        }

        public Vector3 MoveTowardsNextPoint(Vector3 currentPos, int index, float speed)
        {
            if (index > pointList.Length - 1) return currentPos;
            return Vector3.MoveTowards(currentPos, pointList[index].position, speed * Time.deltaTime);
        }

        public int UpdateIndex(Vector2 currentPos, int index)
        {
            if (index > pointList.Length - 1) return index;

            Vector2 target = pointList[index].position;
            float diff = Math.Abs(target.x - currentPos.x) + Math.Abs(target.y - currentPos.y);
            if (diff < tolerance) return index + 1;
        
            return index;
        }

        public bool ReachedEnd(int index)
        {
            return index > pointList.Length - 1;
        }

        private void OnDrawGizmos()
        {
            GetPathPoints();
            Gizmos.color = Color.green;
            for (int i = 0; i < pointList.Length - 1; i++)
            {
                Gizmos.DrawLine(pointList[i].position, pointList[i + 1].position);
            }
        }
    }
}
