using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int Levelload = 1;
    public GameMaster gm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Savescore();
            gm.Inputtext.text = ("Press Attack to enter");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.GetComponent<Player>().attacking)
            {
                Savescore();
                SceneManager.LoadScene(Levelload);
            }
            if (Input.GetKey(KeyCode.R))
                Restart();        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gm.Inputtext.text = ("");
        }
    }

    private void Savescore()
    {
        PlayerPrefs.SetInt("points", gm.points);
        if (PlayerPrefs.GetInt("highscore") < gm.points)
            PlayerPrefs.SetInt("highscore", gm.points);
    }
    private void Restart()
    {
        PlayerPrefs.SetInt("points", 0);
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("gem", 0);
    }

}
