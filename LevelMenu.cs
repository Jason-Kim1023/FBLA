using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelMenu : MonoBehaviour
 
{
    
    //All of the things below are all the same.  They all allow the Unity Program to make it so that if a button is clicked, it will load a scene depending on what I set it to.
    //Basically, I have to just go into Unity and click on a button. Then on the "On Click" area it allows the public void that I set up to be chosen.  Thus, if clicked it will load a scene
    //with any of the following names that are in quotations.
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void PlayLevel6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void PlayLevel1FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel1");
    }
    public void PlayLevel2FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel2");
    }
    public void PlayLevel3FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel3");
    }
    public void PlayLevel4FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel4");
    }
    public void PlayLevel5FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel5");
    }
    public void PlayLevel6FromtheMainMenu()
    {
        SceneManager.LoadScene("LearningLevel6");
    }
    public void PlayBossLevel()
    {
        SceneManager.LoadScene("Boss LVL");
    }
    public void PlayBossLevelfromtheMainMenu()
    {
        SceneManager.LoadScene("BossLevelLearning");
    }

    public void BossLevelPracticeGame()
    {
        SceneManager.LoadScene("MultiplayerPractice");
    }

}



