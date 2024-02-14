using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public void OnClickNextLevelButton()
    {
        GameManager.Instance.GoToNextLevel();
        gameObject.SetActive(false);
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
