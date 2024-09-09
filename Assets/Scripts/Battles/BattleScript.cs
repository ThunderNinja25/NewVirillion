using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON,LOST }

public class BattleScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerBattleSpawn;
    [SerializeField] private Transform enemyBattleSpawn;

    private CharacterSystem playerSystem;
    private CharacterSystem enemySystem;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private BattleUIScript playerUI;
    [SerializeField] private BattleUIScript enemyUI;
    [SerializeField] private PlayerInputUI uiSwitch;

    private EnemyController enemyController;
    public bool battleLost;

    public BattleState state;

    [SerializeField] private Camera playerCamera;

    public SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        playerCamera = FindObjectOfType<Camera>();
        playerCamera.gameObject.SetActive(false);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleSpawn);
        playerSystem = playerGO.GetComponent<CharacterSystem>();
        playerSystem.currentHealth = playerSystem.maxHealth;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleSpawn);
        enemySystem = enemyGO.GetComponent<CharacterSystem>();
        enemySystem.currentHealth = enemySystem.maxHealth;

        dialogueText.text = enemySystem.characterName + " approaches...";

        playerUI.SetUI(playerSystem);
        enemyUI.SetUI(enemySystem);

        yield return new WaitForSeconds(2);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemySystem.TakeDamage(playerSystem.damage);
        enemyUI.SetHealth(enemySystem.currentHealth);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(2);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemySystem.characterName + " attacks!";

        yield return new WaitForSeconds(1);

        bool isDead = playerSystem.TakeDamage(enemySystem.damage);

        playerUI.SetHealth(playerSystem.currentHealth);

        yield return new WaitForSeconds(1);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            battleLost = true;
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
            battleLost = false;
        }
        loader.ReturnToFreeroam();
    }

    private void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
        
    }

    IEnumerator PlayerHeal()
    {
        playerSystem.Heal(5);

        playerUI.SetHealth(playerSystem.currentHealth);
        dialogueText.text = "You have been healed!";

        yield return new WaitForSeconds(2);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
        uiSwitch.DisableMoves();
    }
    
    public void OnMovesButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        uiSwitch.ShowMoves();
    }

    public void OnBackpackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        uiSwitch.ShowBackpack();
        StartCoroutine(PlayerHeal());
        uiSwitch.DisableBackpack();
    }
}
