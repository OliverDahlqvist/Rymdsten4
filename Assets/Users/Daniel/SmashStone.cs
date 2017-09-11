using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmashStone : MonoBehaviour
{
        public void Start()
        {
            GlobalControl.SaveEvent += SaveFunction;
        }

        public void OnDestroy()
        {
            GlobalControl.SaveEvent -= SaveFunction;
        }

        public void SaveFunction(object sender, EventArgs args)
        {
            GameObject rocks = GameObject.Find("rock_01");
            SavedDestuctrableRocks rock = new SavedDestuctrableRocks();
            rock.startScale = rocks.GetComponent<Stone>().startScale;
            GlobalControl.Instance.GetListForScene().SavedRocks.Add(rock);
         }
    }

