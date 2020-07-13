using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class Session : MonoBehaviour
{
    private System.Random random = new System.Random(System.DateTime.Now.Millisecond);
    private int _lap;
    protected float _remainingTime = Single.MaxValue;
    protected RaceCar _raceCar;
    private RaceCar[] _playerCar = new RaceCar[Team.carCount];
    private RaceCar[] _carRunningOrderAroundTrack;
    protected float _remainingSessionTime = Single.MaxValue;
    protected bool _isSessionComplete;

    public bool isGarage
    {
        get { return base.GetType() == typeof(GarageSession); }
    }
    public bool isPractice
    {
        get { return base.GetType() == typeof(PracticeSession); }
    }

    public bool isQualify
    {
        get { return base.GetType() == typeof(QualifySession); }
    }

    public bool isRace
    {
        get { return base.GetType() == typeof(RaceSession); }
    }

    public int lap
    {
        get { return this._lap; }
    }

    public Session()
    {
    }

    public RaceCar GetCar(int inputID)
    {
        return this._raceCar;
    }

    void Start()
    {

    }
}