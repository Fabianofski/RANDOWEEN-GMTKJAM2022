using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBox : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MusicBox").Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
