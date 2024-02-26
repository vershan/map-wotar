using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RealmScript : MonoBehaviour
{
    public bool isCompleted;
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;
        UpdateLevelButton();
    }

    public void LoadRealm(string realmName)
    {
        SceneManager.LoadScene(realmName);
    }
    
    private void UpdateLevelButton()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            isCompleted = true;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
