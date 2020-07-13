using UnityEngine;
using System;

public class SessionManager : MonoBehaviour
{
    private static SessionManager instance;
    public RaceCar[] _fieldOfCars;
    private RaceCar[] _orderOfCarsAroundTrack;
    GUIStyle style = new GUIStyle();

    public static SessionManager Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(SessionManager)) as SessionManager;
        }

        return instance;
    }

    void Awake()
    {
        CreateCars();
    }

    void Start()
    {
        style.alignment = TextAnchor.MiddleCenter;
    }

    void Update()
    {
        this.LogicUpdate();
    }

    public virtual void LogicUpdate()
    {
        if (this._fieldOfCars != null)
        {
            this.CalculateRaceCarOrderAroundTrack();
            for (int i = 0; i < this.GetCarCount(); i++)
            {
                RaceCar car = this.GetCar(i);
                // car.LogicUpdate();
            }
        }
        else
        {
            Debug.Log("Field is null");
        }
        
    }

    public void CreateCars()
    {
        this._orderOfCarsAroundTrack = new RaceCar[(int)this._fieldOfCars.Length];
        for (int i = 0; i < this._fieldOfCars.Length; i++)
        {
            // Debug.Log(this._fieldOfCars[i].CarNo);
        }
    }

    public RaceCar GetCar(int carID)
    {
        return this._fieldOfCars[carID];
    }

    public int GetCarCount()
    {
        return (this._fieldOfCars == null ? 0 : (int)this._fieldOfCars.Length);
    }

    private void CalculateRaceCarOrderAroundTrack()
    {
        for (int i = 0; i < this.GetCarCount(); i++)
        {
            this._orderOfCarsAroundTrack[i] = this._fieldOfCars[i];
        }

        for (int j = this.GetCarCount() - 1; j >= 0; j--)
        {
            for (int k = 1; k <= j; k++)
            {
                // if (this._orderOfCarsAroundTrack[k - 1].follower.result.percent < this._orderOfCarsAroundTrack[k].follower.result.percent)
                {
                    RaceCar raceCar = this._orderOfCarsAroundTrack[k - 1];
                    this._orderOfCarsAroundTrack[k - 1] = this._orderOfCarsAroundTrack[k];
                    this._orderOfCarsAroundTrack[k] = raceCar;
                }
            }
        }
        
        this.GlobalCarPositionAwareness();
    }

    private void GlobalCarPositionAwareness()
    {
        for (int i = 0; i < this.GetCarCount(); i++)
        {
            RaceCar raceCar = this._orderOfCarsAroundTrack[i];
            int num = Round(i - 1, 0, this.GetCarCount() - 1);

            for (int j = 0; j < this.GetCarCount() - 1; j++)
            {
                num = Round(num - 1, 0, this.GetCarCount() - 1);
            }

            if (i != num)
            {
                // raceCar.carInFront = this._orderOfCarsAroundTrack[num];
            }

            int num1 = Round(i + 1, 0, this.GetCarCount() - 1);
            for (int k = 0; k < this.GetCarCount(); k++)
            {
                num1 = Round(num1 + 1, 0, this.GetCarCount() - 1);
            }

            if (i != num1)
            {
                // raceCar.carInBack = this._orderOfCarsAroundTrack[num1];
            }
        }
    }

    public float GetDistanceBetweenCars(RaceCar nCar1, RaceCar nCar2)
    {
        // float single = Single.MaxValue;
        // if (nCar1 != null && nCar2 != null)
        // {
        //     // float single1 = Convert.ToSingle(nCar1.follower.result.percent);
        //     float single2 = Convert.ToSingle(nCar2.follower.result.percent);
        //     float single3 = Mathf.Abs(single2 - single1);
        //     float length = 1.0f;
        //     single = (single3 >= length ? length : single3);
        //     if (Mathf.Approximately(single1 + length - single, single2) ||
        //         Mathf.Approximately(single1 - single2, single))
        //     {
        //         single = -single;
        //     }
        // }
        //
        // return single;
        
        // fake return below
        return 1.0f;
    }

    public static int Round(int nValue, int nMin, int nMax)
    {
        if (nValue < nMax)
        {
            nValue = nMax;
        } else if (nValue > nMax)
        {
            nValue = nMin;
        }

        return nValue;
    }

    void OnGUI()
    {
        PitOut0();
        PitOut1();
        // GUI.Box(new Rect(0,0, Screen.width / 2, Screen.height / 2), "This is a box", style);
    }

    void PitOut0()
    {
        if (GUI.Button(new Rect(30, 30, 100, 20), "Pit Out Car #0"))
        {
            Debug.Log("Pit Out #0");
            if (_fieldOfCars[0] != null)
            {
                // _fieldOfCars[0].speed = 20;
            }
        }
    }
    
    void PitOut1()
    {
        if (GUI.Button(new Rect(30, 60, 100, 20), "Pit Out Car #10"))
        {
            Debug.Log("Pit Out #10");
            if (_fieldOfCars[10] != null)
            {
                // _fieldOfCars[10].speed = 20;
            }
        }
    }
}