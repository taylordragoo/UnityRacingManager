using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareerManager : SaveNode
{
    // Class Props & Inits
    #region props and inits
    
    private static CareerManager _instance;
    private CareerManager.State _state;
    private Session _session;
    public Data _data;
    public string ID { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string age { get; set;  }
    public string week { get; set; }
    public string year { get; set; }
    private string filepath;
    private int[] raceWeekends;
    
    public static CareerManager instance
    {
        get
        {
            if (CareerManager._instance == null)
            {
                CareerManager._instance = GameObject.Find("CareerManager").GetComponent<CareerManager>();
            }

            return CareerManager._instance;
        }
    }

    static CareerManager()
    {
        
    }

    public CareerManager()
    {
        
    }
    
    #endregion

    public override void Load(SaveNode.DataType inDataType)
    {
        if (inDataType != SaveNode.DataType.New)
        {
            this._data.firstName = base.GetString("First Name", this._data.firstName);
            this._data.lastName = base.GetString("Last Name", this._data.lastName);
            this._data.age = base.GetInt("Age", this._data.age);
            this._data.week = base.GetInt("Week", this._data.week);
            this._data.year = base.GetInt("Year", this._data.year);
        }
        else
        {
            this._data = (CareerManager.Data) this.data;
        }
    }

    void Start()
    {
        // _instance = instance;
        raceWeekends = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
    }

    public void SetState(CareerManager.State inState)
    {
        switch (this._state)
        {
            case State.Home:
            {
                // SceneManager.LoadScene("CareerMenu");
                break;
            }
            case CareerManager.State.Advance:
            {
                // Set Int to Week from save
                int tempInt;
                // Convert to int from string
                tempInt = Convert.ToInt32(instance._data.week);
                // loop through all array of race weekend #s
                for (int i = 0; i < raceWeekends.Length; i++)
                {
                    // if week from save equals week of a race then load scene
                    if (tempInt == raceWeekends[i])
                    {
                        SetState(CareerManager.State.Session);
                        break;
                    }
                    // else just advance to next week and save
                    else if (tempInt != raceWeekends[i])
                    {
                        tempInt++;
                        // instance._data.week = tempInt.ToString();
                        Debug.Log(instance._data.week);
                        // Save();
                        break;
                    }
                    // error ?
                    else
                    {
                        Debug.Log("Hopefully nothing");
                        break;
                    }
                }
                break;
            }
            case CareerManager.State.Session:
            {
                // StartPracticeSession();
                // SceneManager.LoadScene("Track01");
                break;
            }
        }
    }

    public void Continue()
    {
        // Start Race Weekend Session
        if (this._state == CareerManager.State.Session)
        {
            // SceneManager.LoadScene("Track01");

        } else if (this._state == CareerManager.State.Advance){
            // Set Int to Week from save
            int tempInt;
            // Convert to int from string
            tempInt = Convert.ToInt32(instance._data.week);
            // loop through all array of race weekend #s
            for (int i = 0; i < raceWeekends.Length; i++)
            {
                // if week from save equals week of a race then load scene
                if (tempInt == raceWeekends[i])
                {
                    this._state = CareerManager.State.Session;
                    Continue();
                    break;
                }
                // else just advance to next week and save
                else if (tempInt != raceWeekends[i])
                {
                    tempInt++;
                    // instance._data.week = tempInt.ToString();
                    Debug.Log(instance._data.week);
                    break;
                }
                // error ?
                else
                {
                    Debug.Log("Hopefully nothing");
                    break;
                }
            }
            
        } else if (this._state == CareerManager.State.Home)
        {
            // SceneManager.LoadScene("CareerMenu");
        }
    }

    // Data Related Inquiries
    #region data
    
    public override void Reset()
    {
        this._data = new CareerManager.Data();
        this.data = this._data;
        this._data.ID = "0";
        this._data.firstName = "Ted";
        this._data.lastName = "Jones";
        this._data.age = 30;
        this._data.week = 1;
        this._data.year = 2020;
        Debug.Log("Reset Career");
    }

    [Serializable]
    public class Data
    {
        public string ID;
        public string firstName;
        public string lastName;
        public int age;
        public int week;
        public int year;

        public Data()
        {
        }
    }
    
    #endregion
    
    // Enumerations
    #region enums
    public enum RaceDay
    {
        Garage,
        Practice,
        Qualify,
        Race,
        PostRace
    }

    public enum State
    {
        Home,
        Advance,
        Session
    }
    
    #endregion
}