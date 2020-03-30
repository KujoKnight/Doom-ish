using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenus : MonoBehaviour
{
    public AudioSource playSound;

    public void PlayGame()
    {
        StartCoroutine(Play(playSound));
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        StartCoroutine(Play(playSound));
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public IEnumerator Play(AudioSource audio)
    {
        audio.Stop();
        audio.Play();
        yield return new WaitWhile(() => audio.isPlaying);
    }
}
