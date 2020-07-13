using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private static GameTimer _instance;
    private int _frameRate;
    private float _deltaTime;
    public float deltaTime
    {
        get { return this._deltaTime; }
    }

    public static GameTimer instance
    {
        get
        {
            if (GameTimer._instance == null)
            {
                GameTimer._instance = GameObject.Find("Game").GetComponent<GameTimer>();
            }

            return GameTimer._instance;
        }
    }

    static GameTimer()
    {
        
    }

    public GameTimer()
    {
        
    }

    public void LogicUpdate()
    {
        this._deltaTime += Time.deltaTime;
    }
}