using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Building;
using TMPro;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] private BoolVariable gameOver;
    [SerializeField] private BoolVariable rerollAvailable;

    [Header("Enemies")]
    [SerializeField] private IntVariable ghostAmount;    
    [SerializeField] private IntVariable jackAmount;    
    [SerializeField] private IntVariable zombieAmount;    
    [SerializeField] private IntVariable plagueAmount;    
    
    [Header("Upgrades")]
    [SerializeField] private IntVariable upgradeAmount;    
    [SerializeField] private IntVariable downgradeAmount;    
    
    [Header("Buildings")]
    [SerializeField] private IntVariable cannonAmount;    
    [SerializeField] private IntVariable coilAmount;    
    [SerializeField] private IntVariable mortarAmount;

    [SerializeField] private int wave;
    [SerializeField] private IntVariable waveVariable;

    
    [SerializeField] private List<Placeable> buildingInstances;
    [SerializeField] private GameObjectEvent addToShop;
    
    private void Start()
    {
        RollNewWave(true);
    }

    public void RollNewWave(bool roll)
    {
        if (roll == false) return;
        rerollAvailable.Value = true;
        ReRollEnemies();
        ReRollBuildings();
        ReRollUpgrades();

        waveVariable.Value = wave;
        wave++;
    }

    public void ReRollEnemies()
    {
        ghostAmount.Value = Random.Range(0, Balancer.GetMaxGhostAmount(wave));
        jackAmount.Value = Random.Range(0, Balancer.GetMaxJackAmount(wave));
        zombieAmount.Value = Random.Range(0, Balancer.GetMaxZombieAmount(wave));
        plagueAmount.Value = Random.Range(0, Balancer.GetMaxPlagueAmount(wave));

        if (ghostAmount.Value + jackAmount.Value + zombieAmount.Value + plagueAmount.Value == 0) ghostAmount.Value = 1;
    }

    public void ReRollBuildings()
    {
        cannonAmount.Value = Random.Range(0, Balancer.GetMaxCannonAmount(wave));
        coilAmount.Value = Random.Range(0, Balancer.GetMaxCoilAmount(wave));
        mortarAmount.Value = Random.Range(0, Balancer.GetMaxMortarAmount(wave));

        if (cannonAmount.Value + coilAmount.Value + mortarAmount.Value == 0) cannonAmount.Value = 1;
    }

    public void ReRollUpgrades()
    {
        upgradeAmount.Value = Random.Range(0, Balancer.GetMaxUpgradeAmount(wave));
        downgradeAmount.Value = Random.Range(0, Balancer.GetMaxDowngradeAmount(wave));
    }

    public void ApplyWave()
    {
        Shop shop = FindObjectOfType<Shop>();
        foreach (Placeable placeable in buildingInstances)
            for (int i = 0; i < placeable.amount.Value; i++)
            {
                shop.SetSprite(placeable.sprite);
                addToShop.Raise(placeable.instance);
            }
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
        public Sprite sprite;
    }
}
