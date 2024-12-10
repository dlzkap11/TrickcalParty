using Unity.VisualScripting;
using UnityEngine;

public class GameManager
{
    public bool isLive;



    public void Pause()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public void Fast()
    {
        Time.timeScale = 2;
    }
}
