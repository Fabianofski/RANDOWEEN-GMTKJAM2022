using UnityEditor.Experimental.GraphView;

public static class Balancer
{

    #region Enemies
    public static int GetMaxGhostAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 3;
            case 2: return 5;
            case 3: return 7;
            case 4: return 8;
            case 5: return 9;
            case 6: return 10;
            case 7: return 11;
            case 8: return 12;
            case 9: return 13;
            default: return 15;
        }
    }
    
    public static int GetMaxJackAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 2;
            case 4: return 4;
            case 5: return 6;
            case 6: return 8;
            case 7: return 10;
            case 8: return 11;
            case 9: return 12;
            default: return 14;
        }
    }
    
    public static int GetMaxZombieAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 1;
            case 4: return 1;
            case 5: return 2;
            case 6: return 2;
            case 7: return 3;
            case 8: return 3;
            case 9: return 4;
            default: return 6;
        }
    }
    
    public static int GetMaxPlagueAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 0;
            case 4: return 1;
            case 5: return 2;
            case 6: return 3;
            case 7: return 5;
            case 8: return 7;
            case 9: return 8;
            default: return 10;
        }
    }
    #endregion
    
    #region Buildings
    public static int GetMaxCannonAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 2;
            case 2: return 2;
            case 3: return 4;
            default: return 4;
        }
    }
    
    public static int GetMaxCoilAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 1;
            case 3: return 2;
            case 4: return 2;
            case 9: return 2;
            default: return 3;
        }
    }
    
    public static int GetMaxMortarAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 0;
            case 4: return 0;
            case 5: return 1;
            case 6: return 2;
            case 7: return 2;
            case 8: return 2;
            case 9: return 2;
            default: return 3;
        }
    }
    #endregion
    
    public static int GetMaxUpgradeAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 2;
            case 4: return 3;
            case 5: return 3;
            case 6: return 4;
            case 7: return 4;
            default: return 5;
        }
    }
    
    public static int GetMaxDowngradeAmount(int wave)
    {
        switch (wave)
        {
            case 1: return 0;
            case 2: return 0;
            case 3: return 0;
            case 4: return 2;
            case 5: return 3;
            case 6: return 3;
            default: return 4;
        }
    }
    
}
