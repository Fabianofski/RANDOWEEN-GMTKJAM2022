using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] private BoolVariable gameOver;
    [SerializeField] private BoolVariable rerollAvailable;
    [SerializeField] private List<RandomObjects> buildings;
    [SerializeField] private List<RandomObjects> enemies;
    [SerializeField] private List<RandomObjects> upDownGrades;
    
    
    
    [SerializeField] private IntVariable upgrade;
    [SerializeField] private IntVariable downgrade;

    [SerializeField] private float percentage;

    [SerializeField] private List<Placeable> buildingInstances;
    [SerializeField] private GameObjectEvent addToShop;
    
    private void Start()
    {
        RollNewWave();
    }

    public void RollNewWave()
    {
        rerollAvailable.Value = true;
        ReRollEnemies();
        ReRollBuildings();
        ReRollUpgrades();
    }

    public void ReRollEnemies()
    {
        foreach (RandomObjects randomObject in enemies)
            randomObject.amount.Value = GetRandomNumberOfProgression(randomObject.maxAmount.Value);
    }

    public void ReRollBuildings()
    {
        foreach (RandomObjects randomObject in buildings)
            randomObject.amount.Value = GetRandomNumberOfProgression(randomObject.maxAmount.Value);
        
    }

    public void ReRollUpgrades()
    {
        foreach (RandomObjects randomObject in upDownGrades)
            randomObject.amount.Value = GetRandomNumberOfProgression(randomObject.maxAmount.Value);
    }

    private int GetRandomNumberOfProgression(int maxAmount)
    {
        return Random.Range(0, Mathf.RoundToInt(maxAmount * percentage));
    }

    public void ApplyWave()
    {
        foreach (Placeable placeable in buildingInstances)
            for (int i = 0; i < placeable.amount.Value; i++)
                addToShop.Raise(placeable.instance);
    }
    
    public void HealthChanged(int newHealth)
    {
        if (newHealth <= 0) gameOver.Value = true;
    }
    
    
    [Serializable]
    public struct RandomObjects
    {
        public IntVariable amount;
        public IntVariable maxAmount;
    }

    [Serializable]
    public struct Placeable
    {
        public IntVariable amount;
        public GameObject instance;
    }
}
