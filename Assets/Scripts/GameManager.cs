using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { FREEROAM, MENU, BAG, WEAPONS, BATTLESTART, CUTSCENE, PAUSED, DIALOGUE }

public class GameManager : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] WeaponsUI weaponsUI;
    [SerializeField] InputManager inputManager;

    GameState state;

    MenuController menuController;

    PlayerMovement playerMovement;

    private void Awake()
    {
        menuController = GetComponent<MenuController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        menuController.onBack += () =>
        {
            state = GameState.FREEROAM;
        };

        playerMovement.OnEnterEnemyView += (Collider2D enemyCollider) =>
        {
            var enemy = enemyCollider.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                StartCoroutine(enemy.TriggerBattle(playerMovement));
            }
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
}
