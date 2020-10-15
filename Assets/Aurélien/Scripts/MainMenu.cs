using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public Image back;
    public GameObject howTo;
    public GameObject okButton;

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
        howTo.SetActive(true);
        okButton.SetActive(true);
    }

    public void HideHowTo()
    {
        howTo.SetActive(false);
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
}
