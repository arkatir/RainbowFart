using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    private UIManager uiScript;

    public bool gameOver = false;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public Text jumpText;
    public Image pauseMenu;
    public bool timeMove = false;
    public float jumpCounter = 0;
    public int levelNumber;
    public Image[] stars;

    //Audio parameters
    [SerializeField] private AudioSource victory_s = null;
    [SerializeField] private AudioSource restart_s = null;

    // Start is called before the first frame update
    void Start()
    {
        uiScript = canvas.GetComponent<UIManager>();

        for (int i = 0; i < 3; i++)
        {
            string starName = "Star" + levelNumber + (i+1);
            if(PlayerPrefs.GetInt(starName) == 1)
            {
                DisplayStar(i);
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
        victory_s.Play();
        
        uiScript.DisplayScreen(victoryScreen);

        gameOver = true;
    }

    public void Restart()
    {
        restart_s.PlayOneShot(restart_s.clip, 1.0F);

        uiScript.HideScreen(victoryScreen);
        uiScript.HideScreen(gameOverScreen);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("LevelSelect");
        gameOver = false;
    }

    public void DisplayStar(int i)
    {
        stars[i].gameObject.SetActive(true);
        stars[i + 3].gameObject.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
