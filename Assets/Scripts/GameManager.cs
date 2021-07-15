using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    
    public readonly int PLAYER_LAYER = 8;
    public readonly int ENEMY_LAYER = 9;
    public int coin;
    public int life = 5;

    public PoolManager poolManager { get; private set; }
    public GameObject pausePanel;
    public Text HighScore = null;
    public Text coinText = null;
    public Text LifeText = null;
    [SerializeField]
    private GameObject bomb = null;
    [SerializeField]
    private GameObject skyenemy = null;

    [SerializeField]
    private float time = 0;
    [SerializeField]
    private float maxTime = 2;
    [SerializeField]
    private List<GameObject> enemies;
    public long highscore = 0;
    private Playermove playermove;
    public bool isskill = false;
    public bool isbossspawn = false;
    void Awake()
    {
        playermove = FindObjectOfType<Playermove>();
        
       
    }
    void Start()
    {
        
        UpdateLife();
        UpdateScore();
        UpdateHighScore();
        coin = 0;
        poolManager = FindObjectOfType<PoolManager>();
        MinPosition = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        MaxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        
    }


    void Update()
    {
            time += Time.deltaTime;
            if (time > maxTime)
            {
                int check = Random.Range(0, 2);
                if (check == 0)
                {
                    Instantiate(bomb, new Vector2(10, Random.Range(-4.0f, 4.0f)), Quaternion.identity);
                Instantiate(skyenemy, new Vector2(Random.Range(-8.0f, 8.0f), 5.5f), Quaternion.identity);
                }
                else
                {
                    int type = Random.Range(0, 4);
                    Instantiate(enemies[type], new Vector2(10, Random.Range(-4.0f, 4.0f)), Quaternion.identity);
                }
                time = 0;
            }

    }


    public void UpdateScore()
    {

        coinText.text = string.Format("Score : {0}", coin);
        PlayerPrefs.SetInt("Score", coin);
        if (coin > highscore)
        {
            highscore = coin;
            PlayerPrefs.SetInt("BEST", (int)highscore);
            UpdateHighScore();
        }
    }
    public void UpdateHighScore()
    {
        highscore= PlayerPrefs.GetInt("BEST", 0);
        HighScore.text = string.Format("BEST : {0}", highscore);
    }
    public void UpdateLife()
    {
        LifeText.text = string.Format("Life : {0}", life);
    }
    public void PauseAction()
    {
        SoundManager.instance.UiSound();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeAction()
    {
        SoundManager.instance.UiSound();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void MainMenuAction()
    {
        SoundManager.instance.UiSound();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
    
    public void OnClickSkill()
    {

        isskill = true;
        playermove.Razershot();
    }
    
}
