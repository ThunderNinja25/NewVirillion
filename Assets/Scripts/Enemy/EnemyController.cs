using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject exclamation;
    [SerializeField] private GameObject fov;
    [SerializeField] private GameObject battleSystem;

    public BattleScript battleScript;
    [SerializeField] private GameManager gameManager;

    //public bool battleLost;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }

    public IEnumerator TriggerBattle(PlayerMovement player)
    {
        //battleScript = FindObjectOfType<BattleScript>(true);
        exclamation.SetActive(true);
        fov.SetActive(false);
        yield return new WaitForSeconds(2f);
        battleSystem.SetActive(true);
        exclamation.SetActive(false);
        gameManager.GetBattleSystem();
        gameManager.StartBattle();
    }
}
