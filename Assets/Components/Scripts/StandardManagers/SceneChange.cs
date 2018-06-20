using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        GameManager.GM.FadingInToScene();
    }

    public void ChangeScene(string name)
    {
        GameManager.GM.FadingOutOfScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
