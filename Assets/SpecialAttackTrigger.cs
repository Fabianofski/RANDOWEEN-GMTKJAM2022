using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTrigger : MonoBehaviour
{
    private Enemy.Enemy enemy;

    private void Awake()
    {
        enemy = transform.parent.GetComponent<Enemy.Enemy>();
    }

    public void DoSpecial()
    {
        enemy.PerformSpecialAttack();
    }
    
}
