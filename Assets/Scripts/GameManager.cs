using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FREEROAM, MENU, BAG, BATTLESTART, CUTSCENE, PAUSED, DIALOGUE }

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    private GameState state;

    private MenuController menuController;

    private void Awake()
    {
        menuController = GetComponent<MenuController>();
    }

    private void Start()
    {
        menuController.onBack += () =>
        {
            state = GameState.FREEROAM;
        };

        menuController.onMenuSelected += OnMenuSelected;
    }

    private void Update()
    {
        if(state == GameState.FREEROAM)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                menuController.OpenMenu();
                state = GameState.MENU;
            }
        }
        else if(state == GameState.MENU)
        {
            menuController.HandleUpdate();
        }
        else if(state == GameState.BAG)
        {
            
            Action onBack = () =>
            {
                inventoryUI.gameObject.SetActive(false);
                state = GameState.FREEROAM;
            };
            inventoryUI.HandleUpdate(onBack);

        }
    }

    public void OnMenuSelected(int selectedItem)
    {
        if(selectedItem == 0)
        {
            //Weapons
        }
        else if(selectedItem == 1)
        {
            inventoryUI.gameObject.SetActive(true);
            state = GameState.BAG;
            
        }
        else if (selectedItem == 2)
        {
            //Save
        }
        else if((selectedItem == 3))
        {
            //Load
        }
        state = GameState.FREEROAM;
    }
}
