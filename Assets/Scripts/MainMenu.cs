using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GameObject continueCareerButton;
    public GameObject newCareerButton;
    
    public MainMenu()
    {
    }

    public void OnEnter()
    {
        SaveUtility.instance.SetData();
        if (CareerManager.instance._data.lastName != null)
        {
            newCareerButton.SetActive(false);
            continueCareerButton.SetActive(true);
        }
        else
        {
            newCareerButton.SetActive(true);
            continueCareerButton.SetActive(false);
        }
    }

    private void Start()
    {
        newCareerButton.SetActive(true);
        continueCareerButton.SetActive(false);
    }
    
    public void LoadMain()
    {
        SceneManager.LoadScene("03_UGUI_Main");
    }

    public void LoadCareer()
    {
        SceneManager.LoadScene("05_UGUI_Career");
    }

    public void LoadNewCareerMenu()
    {
        // Debug.Log("Start New Career");
        SceneManager.LoadScene("05_UGUI_Career");
        // CareerManager.instance.Reset();
        SaveUtility.instance.Save();
    }
}