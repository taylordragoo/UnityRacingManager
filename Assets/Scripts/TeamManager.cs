using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    private static TeamManager _instance;
    private TeamManager.Data _data;
    public static TeamManager instance
    {
        get
        {
            if (TeamManager._instance == null)
            {
                TeamManager._instance = GameObject.Find("TeamManager").GetComponent<TeamManager>();
            }

            return TeamManager._instance;
        }
    }

    [Serializable]
    public class Data
    {
        public List<Team> teams;

        public Data()
        {
            
        }
    }
}