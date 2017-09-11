using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{

    public GameObject rockPrefab;

    void Start()
    {
        GlobalControl.Instance.InitializeSceneList();

        if (GlobalControl.Instance.IsSceneBeingLoaded || GlobalControl.Instance.IsSceneBeingTransitioned)
        {
            SavedList localList = GlobalControl.Instance.GetListForScene();

            if (localList != null)
            {
                print("Saved amountStones count: " + localList.SavedRocks.Count);

                for (int i = 0; i < localList.SavedRocks.Count; i++)
                {
                    GameObject rock = (GameObject)Instantiate(rockPrefab);
                    rock.GetComponent<Stone>().startScale = localList.SavedRocks[i].startScale;                    
                }

            }
            else
                print("Local List was null!");
        }
    }
}

