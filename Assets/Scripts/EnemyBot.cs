using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class EnemyBot : MonoBehaviour {
    Player player;
    bool isTriggered = false;
    public GameObject[] bulletPrefab;
    Stopwatch sw;
    
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
	}
	
	void FixedUpdate () {
		if(isTriggered)
        {
            if(sw.Elapsed.Seconds>3)
            {
                Shoot();
                sw.Reset();
                sw.Start();
            }

            //transform.LookAt(player.transform);
            Vector3 dir = (player.transform.position - transform.position);
            transform.position += dir.normalized * Time.fixedDeltaTime;
            if (dir.sqrMagnitude < 1f)
                player.Kill();
        }
        else
        {
            //wander around

        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isTriggered = true;
            sw=Stopwatch.StartNew();
        }
    }

    void Shoot()
    {
        System.Random rand = new System.Random();
        Instantiate(bulletPrefab[rand.Next(bulletPrefab.Length)], this.transform.position, Quaternion.identity).GetComponent<EnemyMissile>().dir = player.transform.position - transform.position;
    }
}
