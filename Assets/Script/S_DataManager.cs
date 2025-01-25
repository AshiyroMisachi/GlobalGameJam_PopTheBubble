using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_DataManager : MonoBehaviour
{
    public bool gameFinished = false;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainScreen");
    }
}
