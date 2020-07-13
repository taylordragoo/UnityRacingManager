using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public GameObject[] dontDestroyOnLoadObjects;

    public static Game instance
    {
        get
        {
            if (Game._instance == null)
            {
                Game._instance = GameObject.Find("Game").GetComponent<Game>();
            }

            return Game._instance;
        }
    }

    static Game()
    {
        
    }

    public Game()
    {
        
    }

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void Init()
    {
        // this.LoadGameData();
        GameObject[] gameObjectArray = this.dontDestroyOnLoadObjects;
        for (int i = 0; i < (int)gameObjectArray.Length; i++)
        {
            UnityEngine.Object.DontDestroyOnLoad(gameObjectArray[i]);
        }
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("DataManager"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("CareerManager"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("MenuManager"));
        UnityEngine.Object.DontDestroyOnLoad(GameObject.Find("TeamManager"));
        this.Init();
    }

    // private void LoadGameData()
    // {
    //     SaveUtility.instance.SetData();
    // }
}