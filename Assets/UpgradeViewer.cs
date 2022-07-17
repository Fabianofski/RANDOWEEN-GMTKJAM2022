using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeViewer : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] private GameObject text;

    public void DowngradeChanged(int value)
    {
        text.SetActive(value != 0);
        button.interactable = value == 0;
    }

}
