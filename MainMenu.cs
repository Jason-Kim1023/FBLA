using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  //Sets up the main menu quit function.  Allows that on click of the exit button it will quit the game.
    public void QuitGame()
    {
        Application.Quit(); 
    }
        
}
