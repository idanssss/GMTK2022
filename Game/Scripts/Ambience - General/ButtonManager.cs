using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();

    public void Resume()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
