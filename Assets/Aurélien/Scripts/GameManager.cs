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
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
    }

    public void Victory()
    {
        victoryText.gameObject.SetActive(true);
        gameOver = true;
    }

    private void Restart()
    {
        gameOverText.gameObject.SetActive(false);
        victoryText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
    }
}
