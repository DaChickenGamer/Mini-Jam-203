using System;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // MAKE A PROPER UI MANAGER IN THE FUTURE THIS IS NOT MEANT FOR LONG TERM
    public EventReference mainMenuMusic;
    private EventInstance mainMenuMusicInstance; 
    private void Start()
    {
        mainMenuMusicInstance = RuntimeManager.CreateInstance(mainMenuMusic);
        mainMenuMusicInstance.start();
    }

    public void NextScene()
    {
        DOTween.KillAll();
        mainMenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ExitGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
