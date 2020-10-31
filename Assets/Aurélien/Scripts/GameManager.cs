using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    private UIManager uiScript;

    public bool gameOver = false;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject uiBar;
    public Text jumpText;
    public Image pauseMenu;
    public bool timeMove = false;
    public float jumpCounter = 0;
    public int levelNumber;
    public Image[] stars;

    //Audio parameters
    //[SerializeField] private AudioSource victory_s = null;
    //[SerializeField] private AudioSource restart_s = null;

    // Start is called before the first frame update
    void Start()
    {
        uiScript = canvas.GetComponent<UIManager>();

        levelNumber = Int32.Parse(string.Join(string.Empty, Regex.Matches(SceneManager.GetActiveScene().name, @"\d+").OfType<Match>().Select(m => m.Value)));

        for (int i = 0; i < 3; i++)
        {
            string starName = "Star" + levelNumber + (i+1);
            if(PlayerPrefs.GetInt(starName) == 1)
            {
                DisplayStar(2*i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Restart
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            Restart();
        }

        jumpText.text = "Sauts : " + jumpCounter;
    }

    public void GameOver()
    {
        uiScript.DisplayScreen(gameOverScreen); 
        gameOver = true;
    }

    public void Victory()
    {
        AudioManager.instance.Play("Sound - Success");
        //victory_s.Play();

        uiScript.HideScreen(uiBar);
        uiScript.DisplayScreen(victoryScreen);

        //Display of victory stars
        for (int i = 0; i < 3; i++)
        {
            string starName = "Star" + levelNumber + (i + 1);
            if (PlayerPrefs.GetInt(starName) == 1)
            {
                DisplayStar(stars.Length / 2 + 2 * i);
            }
        }

        gameOver = true;
    }

    public void Restart()
    {
        //restart_s.PlayOneShot(restart_s.clip, 1.0F);
        AudioManager.instance.Play("Sound - Restart");

        uiScript.HideScreen(victoryScreen);
        uiScript.HideScreen(gameOverScreen);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("LevelSelect");
        gameOver = false;
    }

    public void DisplayStar(int i)
    {
        stars[i].gameObject.SetActive(false);
        stars[i + 1].gameObject.SetActive(true);

    }

    public void Quit()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
