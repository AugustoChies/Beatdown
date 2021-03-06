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
    private void Update()
    {
        if (isLoadButton)
        {
            if (!Inventory.Instance.GameInitialized)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                                GetComponent<Button>().interactable = true;

            }
        }
    }

    public void NewGame()
    {
        Inventory.Instance.SetGender(chooseMale);
        Inventory.Instance.ResetData();
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
