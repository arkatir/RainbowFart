using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] int numberLevels = 1;
    [SerializeField] int numberStars = 3;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }
}
