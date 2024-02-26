using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool isUnlocked;
    public bool isCompleted;
    public List<GameObject> neighbors;


    private void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;
        if (gameObject.name == "1" && PlayerPrefs.GetInt("Level" + gameObject.name) != 2)
        {
            PlayerPrefs.SetInt("Level" + gameObject.name, 1);
        }
        CompleteLevel();
    }

    private void Start()
    {
        UpdateLevelStatus();
        UpdateLevelButton();
    }

    private void UpdateLevelStatus()
    {
        if (PlayerPrefs.GetInt("Level" + gameObject.name) == 2) // 2 means completed
        {
            isUnlocked = true;
            isCompleted = true;
        }
        else if (PlayerPrefs.GetInt("Level" + gameObject.name) == 1) // 1 means only unlocked
        {
            isUnlocked = true;
            isCompleted = false;
        }
        else // 0 means locked
        {
            isUnlocked = false;
            isCompleted = false;
        }
    }
    
    private void UpdateLevelButton()
    {
        if (isUnlocked)
        {
            
            if (isCompleted)
            {
                GetComponent<Button>().interactable = false;
                GetComponent<Image>().color = new Color(0f, 0f, 1f, 0.5f);
            }
            else
            {
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
        else
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0f);
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CompleteLevel()
    {
        if (Convert.ToInt32(gameObject.name) == PlayerPrefs.GetInt("Last"))
        {
            if (neighbors.Count == 0)
            {
                PlayerPrefs.SetInt("Fire", 1);
                SceneManager.LoadScene("RealmSelect");
                return;
            }
            
            int firstNeighbor = Convert.ToInt32(neighbors[0].name);
            for (int i = 1; i < firstNeighbor; i++)
            {
                if (PlayerPrefs.GetInt("Level" + i) != 2)
                {
                    PlayerPrefs.SetInt("Level" + i, 0);
                }
            }
            foreach (GameObject neighbor in neighbors)
            {
                PlayerPrefs.SetInt("Level" + neighbor.name, 1);
            }
        }
    }
}