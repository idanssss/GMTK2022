using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject GOMenu;
    void Update()
    {
        GOMenu.SetActive(GameObject.Find("Player") == null);
    }
}
