using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private Animation cutscene;

    public IEnumerator PlayCutscene()
    {
        menuScreen.SetActive(false);
        cutscene.Play();
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene(1);
    }

    public void Play()
    {
        StartCoroutine(PlayCutscene());
    }
}
