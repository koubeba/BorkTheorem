using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {
    [SerializeField] Vector3 center;
   // Vector3 force;
    [SerializeField] float str;

    private void Start()
    {
       //orce = center - transform.position;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.tag=="Player")
        {
            collision.GetComponent<Player>().isInField = true;
            collision.GetComponent<Rigidbody2D>().AddForce(-transform.up*Time.fixedDeltaTime*str);
            collision.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, 1f), transform.up);
        }
    }
}
