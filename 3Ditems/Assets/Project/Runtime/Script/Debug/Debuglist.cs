using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debuglist : MonoBehaviour
{
    public static Debuglist instance;

    private void Awake()
    {
        instance = this;
    }

    public void loadSceneValue(string value)
    {
        try
        {
            SceneManager.LoadScene(value);
        }
        catch (System.IndexOutOfRangeException ex)
        {
            Debug.Log("there is no scene name.");
        }
    }
}
