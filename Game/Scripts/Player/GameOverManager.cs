using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject GOMenu;
    public Text txt;
    void Update()
    {
        GOMenu.SetActive(GameObject.Find("Player") == null);
        if(!(GameObject.Find("Player") == null))
        {
            if(GameObject.Find("Player").GetComponent<PlayerInput>().Score > PlayerPrefs.GetInt("Score", 0))
            {
                PlayerPrefs.SetInt("Score", GameObject.Find("Player").GetComponent<PlayerInput>().Score);
            }
        }
        txt.text = "High Score : " + PlayerPrefs.GetInt("Score", 0);
    }
}
