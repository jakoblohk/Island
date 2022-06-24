using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // entering our game when pressing PLAY 
    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }

    // exit the game
    public void QuitGame()
    {
        // checking if quit-function works (otherwise not visible in unity editor)
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
