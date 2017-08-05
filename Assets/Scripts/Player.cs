using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float timeScale;
    Rigidbody2D rigid;
    [SerializeField] float musclePower;
    [SerializeField] float jump;
    [SerializeField]
    float timeScaleMod = 10f;
    public float speedUpImpulse;
    public float slowDownImpulse;
    Animator anim;
    public SpriteRenderer sprite;

    bool jumpAvailable = true;
    Vector2[] platforms;

    Vector3 velocity;
    public float friction;

    public Vector3 center;
    public bool isInField;

    public void ApplyFriction(float value)
    {
        friction = value;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        this.timeScale = 1f;
        velocity = Vector3.zero;

        anim = GetComponentInChildren<Animator>();
        var f = new List<Vector2>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Platform"))
            f.Add(g.transform.position);
        platforms = f.ToArray();
    }

    private void FixedUpdate()
    {
        move(Time.fixedDeltaTime);
        timeScale = 1f / (1f + rigid.velocity.sqrMagnitude*rigid.velocity.sqrMagnitude*timeScaleMod);
        anim.SetFloat("Velocity", rigid.velocity.sqrMagnitude);

        jumpAvailable = false;
        foreach (Vector2 g in platforms)
            if (Vector2.Distance(g, transform.position) < 6f)
            {
                jumpAvailable = true;
                break;
            }
    }

    public void SpeedUp()
    {
        GameObject.FindObjectOfType<GameManager>().timeSinceStart += 1f;
    }

    public void SlowDown()
    {
        GameObject.FindObjectOfType<GameManager>().timeSinceStart -= 1f;
    }

    public void Kill()
    {
        Destroy(gameObject);
        GameObject.FindObjectOfType<GameManager>().state = GameState.LevelEnd;
        GameObject.FindObjectOfType<GameManager>().manageGameState();
    }
   

    void move(float time)
    {
        anim.SetBool("Run", false);
        Vector3 upstand = center - transform.position;
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Run", true);
            sprite.flipX = false;
            rigid.AddForce(new Vector2(-upstand.y, upstand.x) * time * musclePower);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Run", true);
            sprite.flipX = true;
            rigid.AddForce(new Vector2(upstand.y, -upstand.x) * time * musclePower);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpAvailable)
        {
            anim.SetBool("Run", false);
            rigid.AddForce(transform.up * jump, ForceMode2D.Impulse);
        }

        //apply gravity
        if (!isInField)
        {
            rigid.AddForce(upstand);
        }
        isInField = false;
    }
    

    private void OnGUI()
    {
        GUILayout.TextArea((velocity).ToString());
    }
}
