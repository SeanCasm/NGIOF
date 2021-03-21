using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
public class GameSettings : MonoBehaviour
{
    [SerializeField] GameObject redCrossMute;
    [SerializeField] AudioMixer soundsEffects;
    public void MuteAll(){
        bool muted = AudioListener.pause = !AudioListener.pause;
        if (muted) redCrossMute.SetActive(true);
        else redCrossMute.SetActive(false);
    }
    public void AddSoundEffectsLevel(float amount){

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(int index){
        StartCoroutine(CheckSceneLoaded(index));
    }
    IEnumerator CheckSceneLoaded(int index){
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}