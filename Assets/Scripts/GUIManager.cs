using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public EventSystem ES;
    private GameObject storeSelected;

    void Start()
    {
        ES = EventSystem.current;
        storeSelected = ES.firstSelectedGameObject;
    }

    void Update()
    {
        if (ES != null)
        {
            if (ES.currentSelectedGameObject != storeSelected)
            {
                if (ES.currentSelectedGameObject == null)
                {
                    ES.SetSelectedGameObject(storeSelected);
                }
                else
                {
                    storeSelected = ES.currentSelectedGameObject;
                }
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("03_UGUI_Main");
    }

    public void LoadNewCareerMenu()
    {
        SceneManager.LoadScene("04_UGUI_NewCareer");
    }
    
    public void LoadCareerMenu()
    {
        SceneManager.LoadScene("05_UGUI_Career");
    }
    
    public void LoadRaceWeekend()
    {
        SceneManager.LoadScene("06_Track01");
    }

    public void OpenKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
    }
}
