using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EscortQuest : Quest
{
    Pickable[] escortItems;
    protected override void setParameters()
    {
        escortItems = GameObject.FindObjectsOfType<Pickable>().Where(n => n.tag == "EscortQuest").ToArray();
        scoreDelta = 1f / escortItems.Length;
    }
}
