using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DebugController))]
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [HideInInspector] public DebugController debugController;

    private void Awake()
    {
        instance = this;
        debugController = GetComponent<DebugController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) debugController.OnToggleDebug();
        if (Input.GetKeyDown(KeyCode.Return)) debugController.OnReturn();
    }

    public static void GameMenuBtn()
    {
        Debug.Log("MenuBtn");
        SceneManager.LoadScene("Menu");
    }

    public static void GameStartBtn()
    {
        Debug.Log("StartBtn");
        SceneManager.LoadScene("Game");
    }

    public static void GameLoadBtn()
    {
        Debug.Log("GameLoad");
        SceneManager.LoadScene("Continue");
    }

    public static void OptionBtn()
    {
        Debug.Log("Option");
        SceneManager.LoadScene("Option");
    }

    public static void GameExitBtn()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public static void CreateOject()
    {
        GameManager.instance.CreateObejct(1);
    }

    public static void DeleteObject()
    {
        GameManager.instance.DeleteObject();
    }

}
