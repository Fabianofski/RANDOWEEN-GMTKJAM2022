using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityAtoms.BaseAtoms;

public class UIEvents : MonoBehaviour
{

    [SerializeField] private AtomBaseVariable[] atomsToBeResetted;

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
    
}
