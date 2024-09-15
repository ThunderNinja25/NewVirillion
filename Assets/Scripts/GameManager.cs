using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { FREEROAM, MENU, BAG, WEAPONS, BATTLESTART, CUTSCENE, PAUSED, DIALOGUE }

public class GameManager : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] WeaponsUI weaponsUI;
    [SerializeField] InputManager inputManager;
    [SerializeField] private BattleScript battleScript;
    [SerializeField] private MenuController menuController;
    [SerializeField] private PlayerMovement playerMovement;

    public GameState state;

    FadeInOut fade;

    private bool transitioning;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
        menuController.onBack += () =>
        {
            state = GameState.FREEROAM;
        };

        menuController.onMenuSelected += OnMenuSelected;
        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.DIALOGUE;
        };

        DialogueManager.Instance.OnCloseDialogue += () =>
        {
            if(state == GameState.DIALOGUE)
            {
                state = GameState.FREEROAM;
            }
        };
    }

    private void Update()
    {
        if(state != GameState.FREEROAM)
        {
            inputManager.moveAction.Reset();
        }
        else
        {
            inputManager.gameObject.SetActive(true);
        }
        if (state == GameState.FREEROAM)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                menuController.OpenMenu();
                state = GameState.MENU;
            }
        }
        else if (state == GameState.DIALOGUE)
        {
            DialogueManager.Instance.HandleUpdate();
        }
        else if (state == GameState.MENU)
        {
            menuController.HandleUpdate();
        }
        else if (state == GameState.BAG)
        {
            Action onBack = () =>
            {
                menuController.CloseMenu();
                state = GameState.FREEROAM;
            };
            inventoryUI.HandleUpdate(onBack);
        }
        else if(state == GameState.WEAPONS)
        {
            Action onBack = () =>
            {
                menuController.CloseMenu();
                state = GameState.FREEROAM;
            };
            weaponsUI.HandleUpdate(onBack);
        }
    }

    public void OnMenuSelected(int selectedItem)
    {
        if(selectedItem == 0)
        {
            weaponsUI.gameObject.SetActive(true);
            state = GameState.WEAPONS;
        }
        else if(selectedItem == 1)
        {
            inventoryUI.gameObject.SetActive(true);
            state = GameState.BAG;
            
        }
        else if (selectedItem == 2)
        {
            SavingSystem.i.Save("saveSlot1");
        }
        else if((selectedItem == 3))
        {
            SavingSystem.i.Load("saveSlot1");
        }
    }

    public void StartCutsceneState()
    {
        state = GameState.CUTSCENE;
    }

    public void StartFreeroam()
    {
        state = GameState.FREEROAM;
    }

    public void GetBattleSystem()
    {
        battleScript.GetComponentInChildren<BattleScript>();
    }

    public void StartBattle()
    {
        state = GameState.BATTLESTART;
        battleScript.gameObject.SetActive(true);
        battleScript.StartBattle();
    }

    public void EndBattle()
    {
        if (battleScript.battleOver)
        {
            battleScript.gameObject.SetActive(false);
            state = GameState.FREEROAM;
        }
    }

    public IEnumerator ChangeScene(int sceneSelection, DestinationIdentifier destinationPortal)
    {
        if (!transitioning)
        {
            transitioning = true;
            fade.FadeIn();
            yield return new WaitForSeconds(2);
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneSelection);
            yield return op;
            var destinationPortals = FindObjectsOfType<SceneSwitch>().First(x => x != this && x.destinationPortal == destinationPortal);
            playerMovement.transform.position = destinationPortals.SpawnPoint.position;
            transitioning = false;
            fade.FadeOut();
        }
        
    }

    public void StartChangeScene(int sceneSelection, DestinationIdentifier destinationPortal)
    {
        StartCoroutine(ChangeScene(sceneSelection, destinationPortal));
    }
}
