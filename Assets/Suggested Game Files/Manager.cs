using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;



    public Image p1ChoiceImage;
    public Image p2ChoiceImage;

    public string[] choices;
    public Sprite rock, paper, scissors, oGChoiceSprite;

    private int score1 = 0;
    private int score2 = 0;
    private int drawCount = 0;
    public int winCondition = 5;

    public TextMeshProUGUI score1Text;

    public TextMeshProUGUI score2Text;
    public TextMeshProUGUI drawCountText;
    public TextMeshProUGUI result;

    private string p1Choice;
    private string p2Choice;

    public GameObject p2Group;
    public GameObject p1Group;
    public GameObject nextRoundButton;
    public GameObject p1ChoiceObject;
    public GameObject p2ChoiceObject;
    public GameObject resetButton;

    private bool isP1Turn = true;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
        p2Group.SetActive(false);
        nextRoundButton.SetActive(false);
        p2ChoiceObject.SetActive(false);
        resetButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { P1RoundWin(1); }
        if (Input.GetKeyDown(KeyCode.Q)) { P2RoundWin(1); }
    }

    public void PlayRound()
    {
        if (p1Choice == null || p2Choice == null) return;

        switch (p2Choice)
        {
            case "rock":
                switch (p1Choice)
                {
                    case "rock":
                        result.text = "Draw";
                        DrawCount(1);
                        break;
                    case "paper":
                        result.text = "P1 Wins";
                        P1RoundWin(1);
                        break;
                    case "scissors":
                        result.text = "P2 Wins";
                        P2RoundWin(1);
                        break;
                }
                p2ChoiceImage.sprite = rock;
                break;

            case "paper":
                switch (p1Choice)
                {
                    case "rock":
                        result.text = "P2 Wins";
                        P2RoundWin(1);
                        break;
                    case "paper":
                        result.text = "Draw";
                        DrawCount(1);
                        break;
                    case "scissors":
                        result.text = "P1 Wins";
                        P1RoundWin(1);
                        break;
                }
                p2ChoiceImage.sprite = paper;
                break;

            case "scissors":
                switch (p1Choice)
                {
                    case "rock":
                        result.text = "P1 Wins";
                        P1RoundWin(1);
                        break;
                    case "paper":
                        result.text = "P2 Wins";
                        P2RoundWin(1);
                        break;
                    case "scissors":
                        result.text = "Draw";
                        DrawCount(1);
                        break;
                }
                p2ChoiceImage.sprite = scissors;
                break;
        }

        p1ChoiceImage.sprite = p1Choice == "rock" ? rock : p1Choice == "paper" ? paper : scissors;

        WinCondition();
    }

    public void P1RoundWin(int points)
    {
        score1 += points;
        UpdateScoreUI();
        nextRoundButton.gameObject.SetActive(true);
    }

    public void P2RoundWin(int points)
    {
        score2 += points;
        UpdateScoreUI();
        nextRoundButton.gameObject.SetActive(true);
    }
    public void DrawCount(int count)
    {
        drawCount += count;
        UpdateScoreUI();
        nextRoundButton.gameObject.SetActive(true);
    }

    private void UpdateScoreUI()
    {
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
        drawCountText.text = drawCount.ToString();
    }

    public void OnChoiceSelected(string choice)
    {
        if (isP1Turn)
        {
            p1Choice = choice;
            isP1Turn = false;
            p2Group.SetActive(true);
            p1Group.SetActive(false);
            p1ChoiceObject.SetActive(false);
            p2ChoiceObject.SetActive(true);

        }
        else
        {
            p2Choice = choice;
            PlayRound();
            isP1Turn = true;
            p2Group.SetActive(false);
            p1ChoiceObject.SetActive(true);
        }

    }

    public void WinCondition()
    {
        if (score1 >= winCondition)
        {
            result.text = "Player 1 Wins the Game!";
            nextRoundButton.SetActive(false);
            resetButton.SetActive(true);
        }
        else if (score2 >= winCondition)
        {
            result.text = "Player 2 Wins the Game!";
            nextRoundButton.SetActive(false);
            resetButton.SetActive(true);
        }
    }
    public void NextRound()
    {
        p2Group.SetActive(false);
        p1Group.SetActive(true);
        nextRoundButton.SetActive(false);
        result.text = "";
        p2ChoiceImage.sprite = oGChoiceSprite;
        p1ChoiceImage.sprite = oGChoiceSprite;
        p1ChoiceObject.SetActive(true);
        p2ChoiceObject.SetActive(false);

    }
    public void ResetGame()
    {
        score1 = 0;
        score2 = 0;
        drawCount = 0;
        p1Choice = null;
        p2Choice = null;
        UpdateScoreUI();
        p2Group.gameObject.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
}
