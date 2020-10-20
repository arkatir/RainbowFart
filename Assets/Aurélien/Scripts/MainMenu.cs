using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //How To play screen
    public GameObject howToImage = null;

    //Button management;
    public GameObject playButton = null; 
    public GameObject howToButton = null;
    public GameObject creditsButton = null;
    public GameObject okButton = null;
    public GameObject crossButton = null;
    public GameObject quitButton = null;

    //Button Text update
    /*public int buttonOffsetX = 3, buttonOffsetY = 12;
    Vector3 pos;*/

    // Start is called before the first frame update
    void Start()
    {
        //back.GetComponent<Button>().onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayHowTo()
    {
        howToImage.SetActive(true);
        okButton.SetActive(true);
    }

    public void HideHowTo()
    {
        howToImage.SetActive(false);
        okButton.SetActive(false);
    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }


    /*//Button text updates/animations
    public void UpdateButtonPressedText(Text text) 
    {
        pos = text.transform.position;
        text.transform.position = new Vector3(pos.x + (float)buttonOffsetX, pos.y - (float)buttonOffsetY, pos.z); 
    }

    public void UpdateButtonReleasedText(Text text)
    {
        pos = text.transform.position;
        text.transform.position = new Vector3(pos.x - (float)buttonOffsetX, pos.y + (float)buttonOffsetY, pos.z);
    }*/

}
