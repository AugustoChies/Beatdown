using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public bool isLoadButton;
    public bool chooseMale;
    private void Start()
    {
        if (isLoadButton)
        {
            if (!Inventory.Instance.GameInitialized)
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }

    public void NewGame()
    {
        Inventory.Instance.ResetData();
        Inventory.Instance.SetGender(chooseMale);
        SceneManager.LoadScene("WorldMap");
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("WorldMap");
    }
    
    public void QuitGame()
    {
        Application.Quit();   
    }
}
