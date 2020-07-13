using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using SWS;

public class RaceCar : MonoBehaviour
{
    private static RaceCar instance;
    
    public splineMove moveRef;
    public bool bOnTrack = false;
    public bool bPitIn = false;
    public bool bPitOut = false;
    public int pitPos;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitPlayerRaceCar()
    {
        this.Init();
    }

    private void InitVars()
    {
        
    }

    private void Init()
    {
        this.InitVars();
    }
    
}

