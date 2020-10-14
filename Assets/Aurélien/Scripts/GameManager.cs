using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public Text gameOverText;
    public Text victoryText;
    public Text jumpText;
    public Image prefMenu;
    public bool timeMove = false;
    public float jumpCounter = 0;

    //Audio parameters
    [SerializeField] private AudioSource victory_s;
    [SerializeField] private AudioSource restart_s;

    // Start is called before the first frame update
    void Start()
    {
        
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
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }

    public void Victory()
    {
        victory_s.Play();
        
        victoryText.gameObject.SetActive(true);
        gameOver = true;
    }

    private void Restart()
    {
        restart_s.PlayOneShot(restart_s.clip, 1.0F);
        gameOverText.gameObject.SetActive(false);
        victoryText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("LevelSelect");
        gameOver = false;
    }

    public void DisplayPreferences()
    {
        /*if (prefMenu == null)
        {
            Debug.LogError("prefMenu has not been assigned.", this);
        }*/
        prefMenu.gameObject.SetActive(true);
        //Debug.Log("wtf");
    }
}
