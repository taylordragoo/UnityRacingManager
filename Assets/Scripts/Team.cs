using UnityEngine;
using System;

public class Team
{
    public int id { get; set; }
    public string name { get; set; }
    public static int carCount;

    private void Start()
    {
    }
}

public struct TeamData
{
    private int id;
    private string name;
}