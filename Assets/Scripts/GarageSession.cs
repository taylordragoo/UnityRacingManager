using System;
using UnityEngine;

public class GarageSession : Session
{
    private float _stateTimer;
    private GarageSession.State _state;

    public GarageSession.State state
    {
        get { return this._state; }
    }
    public GarageSession()
    {
        
    }

    private void Awake()
    {
        this._remainingTime = 30f;
    }

    private void SetState(GarageSession.State inState)
    {
        this._state = inState;
        this._stateTimer = 0f;
        switch (this._state)
        {
            case GarageSession.State.Running:
            {
                break;
            }
            case GarageSession.State.SessionComplete:
            {
                break;
            }
            case GarageSession.State.Exit:
            {
                break;
            }
        }
    }

    private void UpdateSessionState()
    {
        this._remainingTime -= GameTimer.instance.deltaTime;
        Debug.Log(this._remainingTime);
        if (this._remainingTime < 0f)
        {
            // insert end session code
            this.SetState(GarageSession.State.SessionComplete);
        }
    }

    public enum State
    {
        Running,
        SessionComplete,
        Exit
    }
}