using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {
    GameManager manager;
    private void Awake()
    {
        manager = GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") collision.GetComponent<Player>().Kill();
    }
}
