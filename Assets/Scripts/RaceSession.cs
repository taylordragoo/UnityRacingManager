using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;

public class RaceSession : Session
{
    public float minHeight = 1;
    public float maxHeight = 2;
    public int autoCount = 1;
    public List<GameObject> autos = new List<GameObject>();

    public System.Random random = new System.Random(System.DateTime.Now.Millisecond);
    private ERRoad[] roads;
    private const int AUTO_MIN_DISTANCE = 400;

    void Start()
    {
    }
}