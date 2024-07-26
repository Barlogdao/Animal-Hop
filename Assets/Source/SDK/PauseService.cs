using UnityEngine;

public class PauseService
{
    public void Pause()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        AudioListener.pause = false;

        Time.timeScale = 1f;
    }
}