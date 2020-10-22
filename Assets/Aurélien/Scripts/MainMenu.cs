using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Menu screens
    //public GameObject howToScreen = null;
    //public GameObject creditsScreen = null;

    //UI Script
    public GameObject canvas;
    private UIManager uiScript;


    void Awake()
    {
        uiScript = canvas.GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //back.GetComponent<Button>().onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ResetParameters(GameObject screen)
    {
        PlayerPrefs.DeleteAll();
        uiScript.HideScreen(screen);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
