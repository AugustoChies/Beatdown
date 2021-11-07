using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{ 
    public void NewGame()
    {
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
