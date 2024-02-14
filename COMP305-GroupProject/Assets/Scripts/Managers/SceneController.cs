using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);

        //Temp audio thing to play again the main menu bgm.
        if (scene == "MainScreen") 
        {
            SoundManager.Instance.PlayMusic("Title");
        }
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
