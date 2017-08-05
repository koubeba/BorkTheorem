using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickupQuest : Quest
{
    public Pickable[] pickupItems;

    //just for testing
    public PickupQuest()
    {

    }

    protected override void setParameters()
    {
        pickupItems = GameObject.FindObjectsOfType<Pickable>().Where(n => n.tag == "PickupQuest").ToArray();
        scoreDelta = 1f / pickupItems.Length;
    }
}

