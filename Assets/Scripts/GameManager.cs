using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public static float GameTime;
    public static int Score;
    //public Image forceBar;
    //public Club club;
    public static bool timeIsRunning = true;
    public static float timeTaken = 0;
    public TMP_Text timeText;
    public TextMeshProUGUI scoreText;
    public GameObject startScreen;
    //public GameObject level;
    public GameObject gameOver;
    public GameObject InventoryUI;
    public GameObject timer;
    public GameObject score;
    public Inventory inventory;
    private Vector3 timerStartPosition;
    private Vector3 scoreStartPosition;
    public int playerStrokes = 0;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject timeTextTMP = GameObject.Find("Time");
        if (timeTextTMP != null)
        {
            timeText = timeTextTMP.GetComponent<TextMeshProUGUI>();
            //gameOverTimeText = timeTextTMP.GetComponent<TextMeshProUGUI>();

        }


        //level.SetActive(false);
        gameOver.SetActive(false);
        InventoryUI.SetActive(false);
        //startScreen.SetActive(false);
        Time.timeScale = 0;
        timer.GetComponent<TextMeshProUGUI>().enabled = false;
        score.GetComponent<TextMeshProUGUI>().enabled = false;
        timerStartPosition = timer.transform.position;
        scoreStartPosition = score.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeTaken >= 0)
            {
                timeTaken += Time.deltaTime;
                DisplayTime(timeTaken);
            }
        }
        scoreText.text = "Strokes: " + playerStrokes;
        if(Input.GetKeyDown(KeyCode.R)) {LastLevel(); }
    }
    void DisplayTime(float timeDisplay)
    {
        timeDisplay++;
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timeText.text = string.Format("Time: "+"{0:00}:{1:00}", minutes, seconds);
    }
    public void Reset()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        timer.GetComponent<TextMeshProUGUI>().enabled = true;
        score.GetComponent<TextMeshProUGUI>().enabled = true;
        timer.transform.position = timerStartPosition;
        score.transform.position = scoreStartPosition;
        playerStrokes = 0;
        timeTaken = 0f;
        InventoryUI.SetActive(false);
        InventoryUI.SetActive(true);
        gameOver.SetActive(false);
        startScreen.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LastLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 0;
        startScreen.SetActive(false);
        gameOver.SetActive(true);
        InventoryUI.SetActive(true);
        //timer.transform.position = new Vector3(650, 350,0);
        //score.transform.position = new Vector3(600, 300, 0);
    }
    public void StartTime()
    {
        Time.timeScale = 1;
        timeTaken = 0f;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        timer.GetComponent<TextMeshProUGUI>().enabled = true;
        score.GetComponent<TextMeshProUGUI>().enabled = true;
        timer.transform.position = timerStartPosition;
        score.transform.position = scoreStartPosition;
        InventoryUI.SetActive(true);
        gameOver.SetActive(false);
        startScreen.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 0;
        startScreen.SetActive(true);
        InventoryUI.SetActive(false);
        gameOver.SetActive(false);
    }
    public void AddScore(int strokes)
    {
        playerStrokes += strokes;
    }

}
