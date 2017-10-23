using UnityEngine;

public static class PlayerClass
{
    // UI //
    public static float inventorySize = 100;
    public static float CurrentInventory = 0;
    public static int EnergyMax = 100;
    public static float EnergyDrain = 1F;
    public static float EnergyGain = 100F;
    public static float credits = 300;
    public static float stones = 0;
    public static float sciencePoints = 0;
    public static float CurrentEnergy = EnergyMax;
    public static float TemperatureMultipier = 1;
    public static int SprintMultiplier = 1;

    //public static float Credits = 0;

    public static bool Inbounds;
    public static float fallDmgDrain = 0;
    public static float flashLightDrain = 0;
    public static float jetPackDrain = 0;
    public static bool WarmSuitUpgrade = false;
    public static bool WarmSuit = false;

    // BASE //

    // Items Found //
    public static bool laserDrillBuilt = false;

    // Built/Repaired //
    public static bool duringDay;   // = true when daylight, else false
    public static bool duringNight; // = true when nighttime, else false

    // Drills //
    public static int amountDrills = 0;
    public static float globalTickRate = 1;
    public static int drillTierMax = 3;


    // Cargo Drones //
    public static int amountCargoDrones = 0;

    // Costs //
    public static float UpgradeCostPick = 10;
    public static float UpgradeCostInventory = 50;
    public static float UpgradeCostOxygen = 50;
    public static float UpgradeCostSpeed = 1;
    public static float UpgradeCostLaserDrill = 50;


    // Tool stats //
    public static float stonesPerHitLaser = 1f;
    public static float stonesPerHitPick = 10f;
    public static int CurrentRankPick = 1;
    public static int CurrentRankInventory = 1;
    public static int CurrentRankOxygen = 1;
    public static int CurrentRankLaserDrill = 1;
    public static float mineLength = 2f;
    public static float mineRate = 1f;
    public static int currentSelected = 0;

    // Menu //
    public static int currentMenu = 1;
    public static bool building = false;
    public static int menuActive = 0;
    public static bool blackHoleSelected = false;
    public static bool laserSelected = true;
    public static bool usingForge = false;

    // Notification //
    public static bool displayNotification;
    public static string notificationText;
    public static float textValue;

    public static float shakeAmount = 0.01f;

    public static bool inVehicle = false;
    public static bool activeMenu = false;

    public static int forgeEfficency = 1;

    public static string formatValue(float value)
    {
        
        if (value >= 1000000000000000)
        {
            return (value / 1000000000000000).ToString("F2") + "Q";
        }
        else if (value >= 1000000000000)
        {
            return  (value / 1000000000000).ToString("F2") + "T";
        }
        else if (value >= 1000000000)
        {
            return  (value / 1000000000).ToString("F2") + "B";
        }
        else if (value >= 1000000)
        {
            return (value / 1000000).ToString("F2") + "M";
        }
        else if (value >= 10000)
        {
            return (value / 1000).ToString("F1") + "K";
        }
        else if (value >= 1000)
        {
            return (value / 1000).ToString("F2") + "K";
        }
        else if (value >= 0)
        {
            return Mathf.Floor(value).ToString();
        }
        return "Value could not be reformated";
    }
}
