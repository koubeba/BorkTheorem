using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public enum GameState { InGame, Menu, Pause, LevelEnd, Cutscene };

public class GameManager : MonoBehaviour {
    
    public Quest activeQuest; //we choose random active quest
    static int sceneIndex = 0;
    GameManager instance;
    Player player;
    public float timeSinceStart;
    public GameState state;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Canvas[] UIElements;

    [SerializeField] Text textilizer;
    [SerializeField]
    Image img;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(instance);
        this.state = GameState.Cutscene;

        timeSinceStart = 0f;
        player = GameObject.FindObjectOfType<Player>();
        UIElements = canvas.GetComponentsInChildren<Canvas>().Where(n=>n.transform.parent == canvas.transform).ToArray();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        manageGameState();
    }

    private void Update()
    {
        if (state == GameState.Cutscene)
        {
            Debug.Log("cutsc");
            img = canvas.GetComponentInChildren<Image>();
            textilizer.text = activeQuest.scene.text.ToString();
            if (img.tag=="Character") img.sprite = activeQuest.scene.character;
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("s press");
                state = GameState.InGame;
                manageGameState();
            }
        }
        else if (state == GameState.InGame)
        {
            timeSinceStart += Time.deltaTime * player.timeScale;
            textilizer.text = timeSinceStart.ToString("0.00");
            if (Input.GetKeyDown(KeyCode.P))
            {
                state = GameState.Pause;
                manageGameState();
            }
            else if (timeSinceStart >= 10f)
            {
                state = GameState.LevelEnd;
                manageGameState();
            }
        }
        else if (state == GameState.Pause)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                state = GameState.InGame;
                manageGameState();
            }
        }
        else if (state == GameState.LevelEnd)
        {
            textilizer.text = "your time is over\nyour score: " + activeQuest.score + "\npress space to continue...";
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeSinceStart = 0f;
                state = GameState.Cutscene;
                manageGameState();
                if (sceneIndex + 1 < SceneManager.sceneCount) SceneManager.LoadScene(sceneIndex + 1);
            }
        } 
    }

    private void OnLevelWasLoaded(int level)
    {
        manageGameState();
    }

    public void manageGameState()
    {
        DisableAllExcept(state.ToString());
        textilizer = UIElements.Where(n => n.name == state.ToString()).First().GetComponentInChildren<Text>();
        switch (this.state)
        {
            case GameState.InGame:
                Time.timeScale = 1;
                //textilizer = UIElements.Where(n => n.name == state.ToString()).First().GetComponentInChildren<Text>();
                break;
            case GameState.LevelEnd:
                Time.timeScale = 0;
                break;
            case GameState.Menu:
                Time.timeScale = 0;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.Cutscene:
                Time.timeScale = 0;
                break;
        }
        
    }

    void DisableAllExcept(string name)
    {
        foreach (Canvas c in UIElements)
        {
            if (c.name != name) c.gameObject.SetActive(false);
            else c.gameObject.SetActive(true);
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
        sceneIndex = index;
    }

}
