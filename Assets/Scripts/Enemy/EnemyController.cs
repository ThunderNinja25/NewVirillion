using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour, ISavable
{
    [SerializeField] private GameObject exclamation;
    [SerializeField] private GameObject fov;

    public SceneLoader loader;

    public BattleScript battleScript;

    //public bool battleLost;

    private void Start()
    {
        GetComponent<BattleScript>();
    }

    public IEnumerator TriggerBattle(PlayerMovement player)
    {
        exclamation.SetActive(true);
        yield return new WaitForSeconds(2f);
        exclamation.SetActive(false);
        loader.BattleStart();
    }

    public void BattleLost()
    {
        if (battleScript.battleLost)
        {
            fov.SetActive(false);
        }
    }

    public object CaptureState()
    {
        return battleScript.battleLost;
    }

    public void RestoreState(object state)
    {
        battleScript.battleLost = (bool)state;
        BattleLost();
        
    }
}
