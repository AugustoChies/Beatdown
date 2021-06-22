using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool CanPause = true;
    public bool Paused = false;

    [SerializeField]
    private Canvas mycanvas = null;

    [SerializeField]
    private ActiveMusicList music = null;


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (!CanPause) return;
            if(Paused)
            {
                Time.timeScale = 1;
                Paused = false;
                mycanvas.enabled = false;
                music.ResumeAll();
            }
            else
            {
                Time.timeScale = 0;
                Paused = true;
                mycanvas.enabled = true;
                music.PauseAll();
            }
        }
    }
}
