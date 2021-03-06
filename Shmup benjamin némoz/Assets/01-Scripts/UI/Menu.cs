using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] int playSceneIndex = 1;

    private void Start()
    {
        Cursor.visible = false;
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(playSceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
