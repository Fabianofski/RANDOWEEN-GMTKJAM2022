using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{

    [SerializeField] private AtomBaseVariable[] atomsToBeResetted;
    private AudioMixer mixer;    
    [SerializeField] private GameObject popSound;


    public void Restart()
    {
        ResetAtoms();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetAtoms()
    {
        foreach (AtomBaseVariable atomBaseVariable in atomsToBeResetted)
        {
            atomBaseVariable.Reset();
        }
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleFullscreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }

    public void SetMixer(AudioMixer mix)
    {
        mixer = mix;
    }
    
    public void ChangeVolume(float volume)
    {
        PlayerPrefs.SetFloat(mixer.name, volume);
        PlayerPrefs.Save();

        PlaySoundRandomPitch(popSound);
        volume = Mathf.Log(volume) * 20;
        mixer.SetFloat("volume", volume);
    }
    
    public void UpdateSlider(Slider slider)
    {
        float volume = PlayerPrefs.GetFloat(mixer.name, 1);
        slider.value = volume;

        volume = Mathf.Log(volume) * 20;
        mixer.SetFloat("volume", volume);
    }

    public void UpdateFullscreenToggle(Toggle toggle)
    {
        toggle.isOn = Screen.fullScreen;
    }

    public void PlaySoundRandomPitch(GameObject sound)
    {
        sound = Instantiate(sound);
        sound.GetComponent<AudioSource>().pitch = Random.Range(1f, 1.4f);
        Destroy(sound, 1f);
    }
}
