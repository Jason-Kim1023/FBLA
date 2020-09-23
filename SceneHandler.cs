using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    //Inputs everything that is being used in every scene.  It is the SceneManager so it is going to be managing everything that is in the scene.  Everything below are UI elements in the game.
    GameObject mc;
    GameObject scroll;
    DialougeFactory dialougeFactory = new DialougeFactory();
    // Dialogue
    GameObject panel;
    GameObject dialougeWordsGameObject;
    GameObject dialougeNameOfNPC;
    GameObject dialougePanelGameObject;
    GameObject canvasGameObject;
    GameObject camFollowingMC;
    GameObject dialougeContinueButtonGameObject;
    Button dialougeContinueButton;
    Text dialougeText;
    Text dialougeNameOfNPCText;
    Dialouge dialouge = null;

    Canvas canvas;

    string currentInteractableDialogeObject;
    private int totalInteractablesFound = 0;

    GameObject monsterCollidedWithGameObject;
    Monster currentMonster;
    bool monsterDialoge = false;

    public List<string> allInactiveInteractables;
    public List<string> allInactiveMonsters;

    // Start is called before the first frame update
    void Start()
    {
        //bringing in the necessary elements for the dialogue feature that will be created
        mc = GameObject.Find("MC");
        scroll = GameObject.Find("Scroll");
        panel = GameObject.Find("Panel");
        //This is all setting up the dialogue that will take place in the canvas on the panal... Specifically the words that will appear.. Everything below is setting up the dialogue feature
        //and making it so that the dialogue will have the specific elements.
        dialougeWordsGameObject = GameObject.Find("Words");
        dialougePanelGameObject = GameObject.Find("Panel");
        dialougeContinueButtonGameObject = GameObject.Find("DialougeButton");

        dialougeNameOfNPC = GameObject.Find("NameOfNPC");
        dialougeContinueButton = GameObject.Find("DialougeButton").GetComponent<Button>();
        dialougeContinueButton.onClick.AddListener(() => dialougeContinueButtonClicked());
        dialougeText = dialougeWordsGameObject.GetComponent<Text>();
        dialougeNameOfNPCText = dialougeNameOfNPC.GetComponent<Text>();

        canvasGameObject = GameObject.Find("Canvas");
        canvas = canvasGameObject.GetComponent<Canvas>();

        camFollowingMC = GameObject.Find("CamFollowingMCAfterTimeline");


        // Reload the scene if it occured just after a fight scene
        if (ApplicationModel.justFought && ApplicationModel.wonFight)
        {
            deactivateDialoge();
            reInitializeLevel();
        }
        else
        {
            allInactiveInteractables = new List<string>();
            allInactiveMonsters = new List<string>();

            GameObject openingDialogeObj = GameObject.Find("OpeningDialoge");


            Dialouge deathDialoge = new Dialouge("You Died!  Try to relearn the level and play again!", "Narrator");
            if (openingDialogeObj!= null)
            {
                InteractableObject d = openingDialogeObj.GetComponent<InteractableObject>();
                Dialouge dialoge = Utility.generateDialoge(d.dialogeList, d.speaker);

                if (ApplicationModel.died)
                {
                    Debug.Log("I DIEDDDDDD please show this.");
                    deathDialoge.setNextDialouge(dialoge);
                    initiateDialogue(deathDialoge);
                }
                else
                {
                    initiateDialogue(dialoge);
                }
            }
            else
            {
                if (ApplicationModel.died)
                {
                    initiateDialogue(deathDialoge);
                }
            }
            //Reset Scene

        }
        ApplicationModel.wipeLastScene();
    }

    // Overloaded version that is called by monsters. Used so that major refactoring is avoided.  Do not want to change the functionality of the game and change things we dont want changed.
    //In addition, this is setting up the fight scene and the dialogue/the starting and ending of the fight scene
    public void initiateDialogue(Dialouge newDialouge, GameObject monsterCollidedWith)
    {
        monsterCollidedWithGameObject = monsterCollidedWith;
        currentMonster = monsterCollidedWithGameObject.GetComponent<Monster>();

        // If there is no dialoge, start the fight
        if (newDialouge == null)
        {
            initiateFightScene();
            return;
        }
        initiateDialogue(newDialouge);

        monsterDialoge = true;
    }

    public void initiateDialogue(Dialouge newDialouge)
    {
        if (monsterDialoge)
        {
            initiateFightScene();
        }
        // disable the current dialoge to avoid crashing if the user never clicked through it.  Accounting that the user may ignore current dialogue and move onto another.
        deactivateDialoge();
        dialougeContinueButton.GetComponentInChildren<Text>().text = "Continue";


        //implement this later. change button text to finish dialougeContinueButton.

        dialougeContinueButtonGameObject.SetActive(true);
        dialougeWordsGameObject.SetActive(true);
        dialougePanelGameObject.SetActive(true);
        dialougeNameOfNPC.SetActive(true);

        dialouge = newDialouge;

        print(dialougeText.text);
        print(newDialouge.text);
        dialougeText.text = newDialouge.text;
        dialougeNameOfNPCText.text = "- " + dialouge.speaker;

        if (dialouge.nextDialouge == null)
        {
            dialougeContinueButton.GetComponentInChildren<Text>().text = "Close";
            //change the button text to say close. to be implemented later
        }
    }
    //Setting up the deactiffffffvation of the dialogue.  When finished it is making it so that they are all false and will disappear from the scene.
    private void deactivateDialoge()
    {
        dialougeContinueButtonGameObject.SetActive(false);
        dialougeWordsGameObject.SetActive(false);
        dialougePanelGameObject.SetActive(false);
        dialougeNameOfNPC.SetActive(false);
    }
    //Adding interactable sprites to the scene and working with the recttransform function
    public void addInteractableToCanvas(Dialouge d, SpriteRenderer spriteRender) 
    {
        GameObject g = new GameObject();
        //SpriteRenderer sp = g.AddComponent<SpriteRenderer>();
        Image image = g.AddComponent<Image>();
        Button button = g.AddComponent<Button>();

        //sp.sprite = spriteRender.sprite;
        //sp = spriteRender.spri;
       // rectTransform.position = new Vector3(0, 0,0);
        button.onClick.AddListener(() => initiateDialogue(d));
        //button.targetGraphic =
        image.sprite = spriteRender.sprite;
        g.transform.SetParent(canvasGameObject.GetComponent<Transform>());
        
        Vector2 canvasOffsetMax = canvasGameObject.GetComponent<RectTransform>().offsetMax;

        float maxX = canvasGameObject.GetComponent<RectTransform>().transform.position.x;
        float maxY = canvasGameObject.GetComponent<RectTransform>().transform.position.y;

        //l->b->r->t
        //creates a bounding box of 35x35 for found items
        g.GetComponent<RectTransform>().offsetMin = new Vector2(maxX - 200 + (35* totalInteractablesFound), maxY-45);
        g.GetComponent<RectTransform>().offsetMax = new Vector2(maxX - 165 + (35 * totalInteractablesFound), maxY-10);
        g.SetActive(true);
        g.name = "newOpetion";
        totalInteractablesFound++;
    }
    //Setting up the continue button while the dialogue is going.  Is defining what happens when the dialogue with the monster is going on.
    void dialougeContinueButtonClicked()
    {
        dialouge = dialouge.nextDialouge;
        if (dialouge == null)
        {
            if (monsterDialoge)
            {
                dialougeContinueButton.GetComponentInChildren<Text>().text = "Fight";
                initiateFightScene();
                monsterDialoge = false;
            }

            deactivateDialoge();
            //print("The button should say false here " + dialougeContinueButtonGameObject.active);
            //change the button
            return;
        }

        if (dialouge.nextDialouge == null)
        {
            if (monsterDialoge)
            {
                dialougeContinueButton.GetComponentInChildren<Text>().text = "Fight";
            }
            else
            {
                dialougeContinueButton.GetComponentInChildren<Text>().text = "Close";
            }
            //change the button text. to be implemented later
        }
        dialougeText.text = dialouge.text;
        dialougeNameOfNPCText.text = "- " + dialouge.speaker;
    }

    // load a fight scene with the current active monster
    //This is taking the active monster and MC from the scene to the fight scene
    private void initiateFightScene()
    {
        PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
        ApplicationModel.fightSceneQuestionSets = currentMonster.questionSets; // todo, remove current monster and just keep track of monster gameobj

        //ApplicationModel.monsterGameObject = monsterCollidedWithGameObject; //99% sure i dont need this line!
        ApplicationModel.monsterGameObjectName = monsterCollidedWithGameObject.name;
        ApplicationModel.monsterSprite = monsterCollidedWithGameObject.GetComponent<SpriteRenderer>().sprite;
        // Save the scene state
        SceneState prevSceneState = new SceneState();
        prevSceneState.allInactiveInteractables = allInactiveInteractables;
        prevSceneState.allInactiveMonsters = allInactiveMonsters;
        prevSceneState.mcPosition = mc.transform.position;
        ApplicationModel.previousNonFightSceneState = prevSceneState;

        SceneManager.LoadScene("FightScene");
    }
    
    // Used to reinitialize a level after a fight. Also exploit the fact that everything is reset, so only update whats necessary
    private void reInitializeLevel()
    {
        SceneState prevSceneState = ApplicationModel.previousNonFightSceneState;

        // Add all interactables found at top of canvas, then remove it from the stage
        for (int i = 0; i < prevSceneState.allInactiveInteractables.Count; i++) {

            GameObject interactableGO = GameObject.Find(prevSceneState.allInactiveInteractables[i]);
            InteractableObject interactable = interactableGO.GetComponent<InteractableObject>();

            addInteractableToCanvas(Utility.generateDialoge(interactable.dialogeList,interactable.speaker), interactableGO.GetComponent<SpriteRenderer>());
            interactableGO.SetActive(false);
        }

        // Deactivate all the dead monsters
        for (int i = 0; i < prevSceneState.allInactiveMonsters.Count; i++)
        {
            Debug.Log(prevSceneState.allInactiveMonsters[i] + " is an inactive monster!");
            GameObject monsterGO = GameObject.Find(prevSceneState.allInactiveMonsters[i]);
            monsterGO.SetActive(false);
        }

        // reinit the list of inactive objects
        allInactiveInteractables = ApplicationModel.previousNonFightSceneState.allInactiveInteractables;
        allInactiveMonsters = ApplicationModel.previousNonFightSceneState.allInactiveMonsters;

        // Reset player location to last spot
        mc.transform.position = prevSceneState.mcPosition;
        // move this to a static function called wipe sceneState 
        prevSceneState = null;
    }
}
//The dialogue factory that wil lmake it easier to put out dialogue without writing a lot of code.  It will make it a lot easier and shorter to write.
public class DialougeFactory
{
    Dialouge dialouge = null;
    Dialouge headDialouge = null;

    public DialougeFactory addNewDialouge(string text, string speaker)
    {
        Dialouge toAdd = new Dialouge(text, speaker);
        if (dialouge == null)
        {
            dialouge = toAdd;
            headDialouge = dialouge;
        }
        else
        {
            dialouge.setNextDialouge(toAdd);
            dialouge = dialouge.nextDialouge;
        }

        //print("just added a dialouge " + dialouge.speaker + ", " + dialouge);
        return this;
    }
    //Builds the dialogue
    public Dialouge build()
    {
        return headDialouge;
    }


}

// linked list dialouge... making a link list so that the dialogue continues on but does not forget the head stated above... makes it more efficent and easier
public class Dialouge
{
    public Dialouge nextDialouge = null;
    public string speaker = "";
    public string text = "";

    public Dialouge(string text, string speaker)
    {
        this.text = text;
        this.speaker = speaker;
    }

    public void setNextDialouge(Dialouge d)
    {
        nextDialouge = d;
    }
}
