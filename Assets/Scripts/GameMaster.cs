using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int points = 0;
    public int highscore = 0;
    public int gem = 0;

    public Text pointtext;
    public Text Hightext;
    public Text gemtext;
    public Text Inputtext;

    // Use this for initialization
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        Hightext.text = ("High Score: " + highscore);
        gem = PlayerPrefs.GetInt("gem");

        if (PlayerPrefs.HasKey("points"))
        {
            Scene ActiveScreen = SceneManager.GetActiveScene();
            if (ActiveScreen.buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("points");
                points = 0;
            }
            else
            {
                points = PlayerPrefs.GetInt("points");
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        pointtext.text = ("Score: " + points);
        gemtext.text = ("Gem: " + gem);
    }
}
