using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCanvas : MonoBehaviour
{
    private Inventory _inventory = null;
    private GameObject _currentMinigame = null;
    private void Start()
    {
        _inventory = Inventory.Instance;
    }

    public void ShowMinigame(GameObject game)
    {
        _currentMinigame = Instantiate(game, this.transform);
        _currentMinigame.GetComponent<Minigame>().parentCanvas = this;
    }

    public void HideMinigame()
    {
        if(_currentMinigame != null)
        {
            Destroy(_currentMinigame);
        }
    }
}
