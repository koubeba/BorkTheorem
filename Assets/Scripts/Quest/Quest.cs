using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest:MonoBehaviour
{
    GameManager gameManager;
    public bool objective; //Is the objective completed?
    public float score; //how much in scale from 0 to 100 is the quest completed?
    public float scoreDelta;
    public Cutscene scene;
    
    void Awake()
    {
        objective = false;
    }
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        setParameters();
    }

    protected virtual void setParameters()
    {
        this.scoreDelta = 0.1f;
    }

    public void countScore()
    {
        this.score += scoreDelta;
        if (score == 1) this.objective = true;
    }

    void Update()
    {
        //TODO: coroutine?
        //countScore();
        if (objective) this.FinishQuest();
    }
    void FinishQuest()
    {
        gameManager.state = GameState.LevelEnd;
        gameManager.manageGameState();
    }
}





