using System;
using System.Collections;
using UnityEngine;

public class CodeToolkit
{
    public CodeToolkit(){
        
    }

    public static int GetRandom(int MinIn, int MaxIn)
    {
        return UnityEngine.Random.Range(MinIn, MaxIn);
    }

    public static float GetRandom01()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }

    public static int GetRandomInc(int MinIn, int MaxIn)
    {
        return UnityEngine.Random.Range(MinIn, MaxIn + 1);
    }
    
}