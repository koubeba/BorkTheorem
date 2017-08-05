using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestroyQuest : Quest
{
    Pickable[] destroyItems;
    protected override void setParameters()
    {
        destroyItems = GameObject.FindObjectsOfType<Pickable>().Where(n => n.tag == "DestroyQuest").ToArray();
        scoreDelta = 1f / destroyItems.Length;
    }
}
