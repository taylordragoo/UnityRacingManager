using PreviewLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class SaveUtility : MonoBehaviour
{
    private static SaveUtility _instance;
    private List<SaveNode> _saveNodes = new List<SaveNode>();
    private SaveUtility.SaveData _saveData = new SaveUtility.SaveData();
    
    private string _oldSaveFile = string.Empty;
    private string _newSaveFile = string.Empty;
    private string _newSaveBackupFile = String.Empty;

    public static SaveUtility instance
    {
        get
        {
            if (SaveUtility._instance == null)
            {
                SaveUtility._instance = GameObject.Find("DataManager").GetComponent<SaveUtility>();
            }

            return SaveUtility._instance;
        }
    }

    static SaveUtility()
    {
        
    }

    public SaveUtility()
    {
        
    }

    public void Clear()
    {
        PreviewLabs.PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        this._oldSaveFile = string.Concat(Application.persistentDataPath,  "/data.loc");
        this._newSaveFile = string.Concat(Application.persistentDataPath,"/data.sav2");
        this._newSaveBackupFile = string.Concat(this._newSaveFile, "backup");
        this._saveNodes.Add(CareerManager.instance);
        LoadLocalData();
    }

    private void Update()
    {
        this._saveData.playTime += Time.deltaTime;
    }

    public void LoadLocalData()
    {
        Debug.Log("Checking for data... at " + PreviewLabs.PlayerPrefs.fileName);
        byte[] numArray = null;
        if (File.Exists(this._newSaveFile))
        {
            Debug.Log("Old File Found");
            numArray = File.ReadAllBytes(this._newSaveFile);
        } 
        // else if (File.Exists(PreviewLabs.PlayerPrefs.fileName))
        // {
        //     Debug.Log("New File Found");
        //     numArray = File.ReadAllBytes(PreviewLabs.PlayerPrefs.fileName);
        // }
        else
        {
            Debug.Log("Nothing Found...");
            numArray = File.ReadAllBytes(PreviewLabs.PlayerPrefs.fileName);
        }
        Debug.Log("Num Array: " + numArray.Length);
        if(numArray != null && (int)numArray.Length > 0)
        {
            this.LoadData(numArray);
        }
    }

    private void LoadData(byte[] inData)
    {
        Debug.Log("Data Loaded...");
        UTF8Encoding utf8Encoding = new UTF8Encoding();
        // this.Reset();
        PreviewLabs.PlayerPrefs.SetEncryptedData(utf8Encoding.GetString(inData));
        for (int i = 0; i < this._saveNodes.Count; i++)
        {
            this._saveNodes[i].Load(SaveNode.DataType.Old);
        }

        this._saveData.saveID = PreviewLabs.PlayerPrefs.GetInt("SaveID", 0);
        GC.Collect();
    }

    public void Reset()
    {
        PreviewLabs.PlayerPrefs.DeleteAll();
        for (int i = 0; i < this._saveNodes.Count; i++)
        {
            this._saveNodes[i].Reset();
        }
        GC.Collect();
    }

    public void Save()
    {
        this._saveData.saveID++;
        if (this._saveData.saveID < 0)
        {
            this._saveData.saveID = 0;
        }
        Debug.Log("Saving..." + this._saveData.data);

        if (File.Exists(this._newSaveFile))
        {
            File.Copy(this._newSaveFile, this._newSaveBackupFile, true);
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(this._newSaveFile);
        // this._saveData.data.Clear();
        for (int i = 0; i < this._saveNodes.Count; i++)
        {
            this._saveNodes[i].Save();
            if(this._saveNodes[i].data != null && this._saveData.data != null) {
                Debug.Log("Data Set");
                this._saveData.data.Add(this._saveNodes[i].data);
            }
        }
    
        binaryFormatter.Serialize(fileStream, this._saveData);
        fileStream.Close();
        Debug.Log("...Saved at " + fileStream);
        GC.Collect();
    }

    public void SetData()
    {
        for (int i = 0; i < this._saveNodes.Count; i++)
        { 
            this._saveNodes[i].Reset();
            if (this._saveData.data.Count == 0)
            {
                for (int k = 0; k < this._saveNodes.Count; k++)
                {
                    this._saveData.data.Add(this._saveNodes[k].data);
                }
            }
            else
            {
                this._saveNodes[i].data = this._saveData.data[i];
            }
            this._saveNodes[i].Load(SaveNode.DataType.New);
        }
        Debug.Log("Data Set...");
    }

    [Serializable]
    public class SaveData
    {
        public int saveID;
        public float playTime;
        public List<object> data;
        
        public SaveData()
        {
            
        }
    }

}