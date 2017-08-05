using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableEnumum { przyspieszacz, spowalniacz, zabijacz, nic};

public class Pickable : MonoBehaviour {
    [SerializeField]
    PickableEnumum type;
    public GameObject pickUpParticles;

    void CleanUp()
    {
        Debug.Log("kleen");
        Destroy(this.gameObject);
        Instantiate(pickUpParticles, transform.position, Quaternion.identity);
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Player")
        {
            Player player = col.GetComponent<Player>();
            switch(type)
            {
                case PickableEnumum.przyspieszacz:
                    player.SpeedUp();
                    Debug.Log("speeding up");
                    break;
                case PickableEnumum.spowalniacz:
                    player.SlowDown();
                    Debug.Log("slowing down");
                    break;
                case PickableEnumum.zabijacz:
                    player.Kill();
                    Debug.Log("killing");
                    break;
            }
            switch(this.tag)
            {
                case "PickupQuest":
                    GameObject.FindObjectOfType<PickupQuest>().countScore();
                    break;
                case "DestroyQuest":
                    GameObject.FindObjectOfType<DestroyQuest>().countScore();
                    break;
                case "EscortQuest":
                    GameObject.FindObjectOfType<EscortQuest>().countScore();
                    break;
            }
            CleanUp();
        }
    }
}
