using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_EndScreenManager : MonoBehaviour
{
    private S_DataManager dataManager;
    void Start()
    {
        dataManager = FindObjectOfType<S_DataManager>();

        dataManager.gameFinished = true;
    }
}