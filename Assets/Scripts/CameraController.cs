using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    Player player;
    bool isFacedLeft = false; 
    public float distToMove;
    bool move = false;
    Vector3 destPos;

    [SerializeField]
    float baseDist;
    [SerializeField]
    float scalarDist;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
	}
	/// <summary>
    ///n
    /// </summary>
    void FixedUpdate () {
        /*if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y)) > distToMove)
            move = true;
        if(move)
        {
            destPos = player.transform.position + new Vector3(0, 0, -10f);
            this.transform.position = Vector3.Lerp(this.transform.position, destPos, Time.fixedDeltaTime*3f);
            if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y)) < 1f)
                move = false;
        }*/
        
            Vector3 upDir = player.transform.position - player.center;
        Vector3 forwardDir=new Vector3(0,0,10f);

        this.transform.rotation = Quaternion.LookRotation(forwardDir, upDir);
        Camera.main.orthographicSize = upDir.sqrMagnitude / scalarDist + baseDist;
      
        //Debug.Log("faced left" + isFacedLeft);
        //player.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, player.transform.rotation.y,transform.rotation.z));
        
                
    }
}
