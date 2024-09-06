using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void BattleStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToFreeroam()
    {
        SceneManager.LoadScene(0);
    }
}
