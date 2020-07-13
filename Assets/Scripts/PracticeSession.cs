using UnityEngine;
using System.Collections.Generic;
using SWS;

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

public class PracticeSession : Session
{
	public bool onWaypointsOnly;
	public GameObject[] objects;
	List<RaceCar> driverList;

	[Range(0.1f, 100f)] public float distanceMin = 1;
	[Range(0.1f, 100f)] public float distanceMax = 100;
	public int maxSpawnCount = 0;
	
	public PathManager pitPath;
	public PathManager pitStalls;
	public PathManager trackPath;

	private splineMove move;

	void Start()
	{
		// Move Cars to Pit stalls
		distanceMin = Mathf.Clamp(distanceMin, distanceMin, distanceMax);
		move = GetComponent<splineMove>();
		move.StartMove();
		move.tween.ForceInit();
		move.Pause();
		driverList = new List<RaceCar>();
		Spawn();
		move.Stop();
	}

	void Update()
	{
		// Find RaceCar
		RaceCar _carCheck = driverList[0];
		
		// Check if at end of pit lane
		if (_carCheck.bPitOut && !_carCheck.bOnTrack && _carCheck.moveRef.currentPoint == 48)
		{
			OutLap(_carCheck);
		}
		
		// check if ready to pit in and at point in track to enter pit lane
		if (_carCheck.bPitIn && _carCheck.bOnTrack && _carCheck.moveRef.currentPoint == 48)
		{
			_carCheck.bOnTrack = false;
			_carCheck.moveRef.SetPath(pitPath);
			_carCheck.moveRef.startPoint = 0;
			_carCheck.moveRef.ChangeSpeed(50);
			_carCheck.moveRef.StartMove();
		}
		
		// check if ready to enter pit stall when going through pit lane
		if (!_carCheck.bOnTrack && _carCheck.moveRef.currentPoint == 44)
		{
			_carCheck.bPitIn = false;
			_carCheck.moveRef.SetPath(pitStalls);
			_carCheck.moveRef.startPoint = 40;
			_carCheck.moveRef.StartMove();
		}
		
		// Check if at pit stall
		if (!_carCheck.bOnTrack && !_carCheck.bPitIn && !_carCheck.bPitOut && _carCheck.moveRef.currentPoint == 40)
		{
			_carCheck.moveRef.Stop();	
		}
	}

	void OnGUI()
	{
		// Create GUI Button To Move Car
		RaceCar _guiCheck = driverList[0];
		if (!_guiCheck.bOnTrack && !_guiCheck.bPitOut && !_guiCheck.bPitIn)
		{
			PitOut();	
		}
		else if(_guiCheck.bOnTrack && !_guiCheck.bPitIn && !_guiCheck.bPitOut)
		{
			PitIn();
		}
		
	}

	// Init all 40 Cars on the PitStall path
	void Spawn()
	{
		// Keep Track of how many cars we spawn
		int spawnCount = 0;
		
		if (onWaypointsOnly)
		{
			// Loop through each car to init values
			for (int i = 40; i > 0; --i)
			{
				if (spawnCount == maxSpawnCount)
					break;
				
				GameObject obj = SpawnAtPos(move.pathContainer.waypoints[i].position);
				splineMove objMove = obj.GetComponent<splineMove>();
				_raceCar = obj.GetComponent<RaceCar>();
				_raceCar.pitPos = spawnCount;
				driverList.Add(_raceCar);
				if (objMove != null)
				{
					objMove.pathContainer = move.pathContainer;
					objMove.startPoint = _raceCar.pitPos;
					objMove.StartMove();
				}

				spawnCount++;
			}

			return;
		}
	}

	GameObject SpawnAtPos(Vector3 pos)
	{
		return (GameObject) Instantiate(objects[Random.Range(0, objects.Length)], pos, Quaternion.identity);
	}
	
	void PitOut()
	{
		RaceCar _RaceCar = driverList[0];
		if (GUI.Button(new Rect(30, 30, 100, 20), "Pit Out Car #1"))
		{
			_RaceCar.moveRef.startPoint = _RaceCar.pitPos;
			_RaceCar.bPitOut = true;
			_RaceCar.moveRef.SetPath(pitPath);
			_RaceCar.moveRef.startPoint = 45;
			_RaceCar.moveRef.ChangeSpeed(25);
			_RaceCar.moveRef.StartMove();
		}
	}

	void OutLap(RaceCar outCar)
	{
		RaceCar _outCar = outCar;
		_outCar.bPitOut = false;
		_outCar.bOnTrack = true;
		_outCar.moveRef.SetPath(trackPath);
		_outCar.moveRef.loopType = splineMove.LoopType.loop;
		_outCar.moveRef.startPoint = 28;
		// _outCar.moveRef.closeLoop = true;
		_outCar.moveRef.ChangeSpeed(50);
		_outCar.moveRef.StartMove();
	}

	void PitIn()
	{
		RaceCar _RaceCar = driverList[0];
		if (GUI.Button(new Rect(30, 30, 100, 20), "Pit In Car #1"))
		{
			_RaceCar.bPitIn = true;
		}
	}
}