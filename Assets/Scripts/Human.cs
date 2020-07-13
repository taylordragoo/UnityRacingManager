using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Human : MonoBehaviour
{
    public string firstName;
    public string lastName;
    public int age;
    
    public virtual Dictionary<string, object> Save ()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("First Name", firstName);
        data.Add("Last Name", lastName);
        data.Add("Age", age);
        return data;
    }
  
    public virtual void Load (Dictionary<string, object> data)
    {
        firstName = Convert.ToString(data["First Name"]);
        lastName = Convert.ToString(data["Last Name"]);
        age = Convert.ToInt32( data["Age"] );
    }
}