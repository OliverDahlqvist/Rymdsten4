using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerStatistics
{
    public int SceneID;
    public float PositionX, PositionY, PositionZ;

    public float inventory;
    public float Material;
}

[Serializable]
public class SavedDestuctrableRocks
{
    public float amountStones;
    public float startScale;
}

[Serializable]
public class SavedList
{
    public int SceneID;
    public List<SavedDestuctrableRocks> SavedRocks;
    

    public SavedList(int newSceneID)
    {
        this.SceneID = newSceneID;
        this.SavedRocks = new List<SavedDestuctrableRocks>();   
    }
}



