using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //3 sound paths
    public AudioClip slowMusic;
    public AudioClip mediumMusic;
    public AudioClip fastMusic;
    AudioSource source;

    Player player;
    [SerializeField]
    float minimalSoundTime = 1f;
    float timer=2f;


	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > minimalSoundTime)
        {
            timer = 0;
            if (player.timeScale > 0.8f)
            {
                //play slow music
                source.PlayOneShot(slowMusic, 1f);
            }
            else if (player.timeScale > 0.1f)
            {
                //play medium music
                source.PlayOneShot(mediumMusic, 1f);
            }
            else
            {
                //play fast music
                source.PlayOneShot(fastMusic, 1f);
            }
        }
	}
}
