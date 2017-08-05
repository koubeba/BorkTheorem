using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour {

    public Vector3 dir;
    public float speed;
    float existanceTime = 0;

    void FixedUpdate()
    {
        transform.position += dir * speed * Time.fixedDeltaTime;
        existanceTime += Time.fixedDeltaTime;
        if (existanceTime > 10f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")
        {
            col.GetComponent<Player>().Kill();
            Destroy(gameObject);
        }
    }
}
