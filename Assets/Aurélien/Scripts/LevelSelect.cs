using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] int numberLevels = 1;
    [SerializeField] int numberStars = 3;
    //public Image back;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberLevels; i++)
        {
            GameObject level = GameObject.Find("Level " + (i+1));
            for (int j = 0; j < numberStars; j++)
            {
                GameObject star = level.transform.GetChild(j).gameObject;
                GameObject starGrey = level.transform.GetChild(j + numberStars).gameObject;
                if (PlayerPrefs.GetInt("Star" + (i+1) + (j+1)) == 1)
                {
                    star.SetActive(true);
                    starGrey.SetActive(false);
                }
            }  
        }

        //back.GetComponent<Button>().onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
