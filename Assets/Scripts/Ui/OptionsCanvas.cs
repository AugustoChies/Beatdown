using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsCanvas : MonoBehaviour
{
    public void Save()
    {
        PlayerDataManager.Instance.Save();
        gameObject.SetActive(false);
    }
    
    public void Load()
    {
        PlayerDataManager.Instance.LoadGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    
    public void Quit()
    {
        print("You can't quit while in editor");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
