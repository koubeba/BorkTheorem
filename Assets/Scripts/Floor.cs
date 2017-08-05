using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public float Friction;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("floor entered");
        if(col.tag=="Player")
        {
            col.GetComponent<Player>().ApplyFriction(Friction);
        }
    }
}
