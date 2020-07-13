﻿using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;
using System.Linq;
using System;
using SWS;

public class VehicleExample : MonoBehaviour
{
    private static VehicleExample instance;

    public float autoSpeed = 40f;
    public float rotationSpeed = 1.0f;
    public ERRoad currentRoad;
    public float minRight = 2.4f;
    public float maxRight = 6.8f;

    private const float REACH_DISTANCE = 2.0f;
    protected int currentWayPoint = 0;
    private List<Vector3> waypoints = new List<Vector3>();
    private System.Random random = new System.Random(System.DateTime.Now.Millisecond);
    private bool startAtStart = true;
    
    public static VehicleExample Instance()
    {
        if (instance != null)
        {
            return instance;
        }

        instance = FindObjectOfType<VehicleExample>();
        if (instance != null)
        {
            return instance;
        }
        
        return instance;
    }
    
    void Start()
    {
        if (currentRoad == null)
        {
            Debug.Log("Error: VehicleExample is set active before a ERRoad object has been added.");
        }
        else
        {
            GetNextPath();
        }
    }

    void Update()
    {
        try
        {
            float distance = Vector3.Distance(waypoints[currentWayPoint], transform.position);

            if (distance <= REACH_DISTANCE)
            {
                currentWayPoint++;

                if (currentWayPoint >= waypoints.Count)
                {
                    GetNextPath();
                }
            }

            transform.position =
                Vector3.MoveTowards(transform.position, waypoints[currentWayPoint], Time.deltaTime * autoSpeed);
            var toTarget = waypoints[currentWayPoint] - transform.position;
            if (toTarget != Vector3.zero)
            {
                var rotation = Quaternion.LookRotation(toTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Update ex: " + ex.Message);
        }
    }

    private void GetNextPath()
    {
        try
        {
            ERConnection erConnection;

            if (startAtStart)
            {
                Debug.Log("Get Connection at Start 1");
                erConnection = currentRoad.GetConnectionAtStart();
            }
            else
            {
                Debug.Log("Get Connection at End 1");
                erConnection = currentRoad.GetConnectionAtEnd();
            }

            //This happens if the road is a dead-end (and??)
            // if (erConnection == null)
            // {
            //     if (startAtStart)
            //     {
            //         Debug.Log("Get Connection at End 2");
            //         erConnection = currentRoad.GetConnectionAtEnd();
            //     }
            //     else
            //     {
            //         Debug.Log("Get Connection at Start 2");
            //         erConnection = currentRoad.GetConnectionAtStart();
            //     }
            // }

            List<ERConnectionData> erConnectionData = erConnection.GetConnectionData().ToList();
            
            //If there is more than one connection, delete the current connection from the list (don't want to go back the way we came)
            if (erConnectionData.Count() > 1)
            {
                for (int i = 0; i < erConnectionData.Count(); i++)
                {
                    if (erConnectionData[i].road.GetName() == currentRoad.GetName())
                    {
                        Debug.Log(erConnectionData[i].road.GetName());
                        erConnectionData.RemoveAt(i);
                        break;
                    }
                }
            }

            //Get the next road
            int iNext = random.Next(0, erConnectionData.Count);
            currentRoad = erConnectionData[iNext].road;
            
            Vector3[] centerPoints = currentRoad.GetSplinePointsCenter();
            float distanceStart = Vector3.Distance(centerPoints[0], transform.position);
            float distanceEnd = Vector3.Distance(centerPoints[centerPoints.Count() - 1], transform.position);
            
            if (distanceStart > distanceEnd)
            {
                Array.Reverse(centerPoints);
                startAtStart = true;
            }
            else
            {
                startAtStart = false;
            }

            //Add a marker part way into the intersection to add as a way-point between the two roads
            //This is so the autos don't cut across the curb and/or into buildings
            if (waypoints.Count > 0)
            {
                var intersectionPoint = IntersectionVector(erConnection.gameObject.transform.position, waypoints[waypoints.Count - 1]);
                waypoints.Clear();
                waypoints.Add(intersectionPoint);
            }
            else
            {
                waypoints.Clear();
            }

            //Reset
            currentWayPoint = 0;

            //No need for ALL markers to be used, MINOR memory savings
            //For some reason the value at 0 in the ERRoad's center points is sometimes 0,0,0.  So ignoring that one.
            for (int iPath = 1; iPath < centerPoints.Count(); iPath += 4)
            {
                waypoints.Add(centerPoints[iPath]);
            }

            //Cleanup, just in case
            erConnectionData.Clear();
        }
        catch (System.Exception ex)
        {
            Debug.Log("GetNextPath ex: " + ex.Message);
        }
    }
    
    //This function is to find the center of the intersection and then make a point x% before that value.
    //This improves the auto's turn radius so the autos don't drive over sidewalks
    private Vector3 IntersectionVector(Vector3 start, Vector3 end)
    {
        Vector3 result = start + (end - start) * 0.15f;

        return result;
    }
}

