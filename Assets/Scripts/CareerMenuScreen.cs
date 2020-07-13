using System;
using UnityEngine;
using UnityEngine.UI;
// using Doozy.Engine.UI;

public class CareerMenuScreen : MonoBehaviour
{
    public Text driverName;
    public Text age;
    public Text week;
    public Text month;
    public Text year;
    private CareerManager _careerManager;
    
    [Header("Popup Settings")]
    public string PopupName = "Popup2";

    public string Title = "Example Title";
    public string Message = "This is an example message for this UIPopup";

    [Space(10)]
    public string LabelButtonOne = "Yes";

    public string LabelButtonTwo = "No";
    public bool HideOnButtonOne = true;
    public bool HideOnButtonTwo = true;

    [Header("Settings Controls")]
    public InputField TitleInput;

    public InputField MessageInput;

    [Space(10)]
    public InputField LabelButtonOneInput;

    public InputField LabelButtonTwoInput;
    public Toggle ButtonOneToggle;
    public Toggle ButtonTwoToggle;

    /// <summary> Reference to the UIPopup clone used by this script</summary>
    private UnityEngine.UI.Graphic m_popup;

    void Start()
    {
        _careerManager = CareerManager.instance;
    }

    private void Update()
    {
        this.UpdateInfo();
    }

    public void UpdateInfo()
    {
        driverName.text = "Name: " + _careerManager._data.firstName + " " + _careerManager._data.lastName;
        age.text = "Age: " +  _careerManager._data.age;
        week.text = "Week: " + _careerManager._data.week;
        // month.text = "Month: " + _careerManager._data.month;
        year.text = "Year: " + _careerManager._data.year;
    }
    
    private void OnEnable()
        {
            TitleInput.text = Title;
            MessageInput.text = Message;

            TitleInput.onEndEdit.AddListener((value) => { Title = value; });
            MessageInput.onEndEdit.AddListener((value) => { Message = value; });

            LabelButtonOneInput.text = LabelButtonOne;
            LabelButtonTwoInput.text = LabelButtonTwo;

            HideOnButtonOne = ButtonOneToggle.isOn;
            HideOnButtonTwo = ButtonTwoToggle.isOn;

            ButtonOneToggle.onValueChanged.AddListener(value => { HideOnButtonOne = value; });
            ButtonTwoToggle.onValueChanged.AddListener(value => { HideOnButtonTwo = value; });
        }

        private void OnDisable()
        {
            TitleInput.onEndEdit.RemoveAllListeners();
            MessageInput.onEndEdit.RemoveAllListeners();
            LabelButtonOneInput.onEndEdit.RemoveAllListeners();
            LabelButtonTwoInput.onEndEdit.RemoveAllListeners();
            ButtonOneToggle.onValueChanged.RemoveAllListeners();
            ButtonTwoToggle.onValueChanged.RemoveAllListeners();
        }

        // public void ShowPopup()
        // {
        //     //get a clone of the UIPopup, with the given PopupName, from the UIPopup Database
        //     m_popup = UIPopup.GetPopup(PopupName);
        //
        //     //make sure that a popup clone was actually created
        //     if (m_popup == null)
        //         return;
        //
        //     //we assume (because we know) this UIPopup has a Title and a Message text objects referenced, thus we set their values
        //     m_popup.Data.SetLabelsTexts(Title, Message);
        //
        //     //get the values from the label input fields
        //     LabelButtonOne = LabelButtonOneInput.text;
        //     LabelButtonTwo = LabelButtonTwoInput.text;
        //
        //     //set the button labels
        //     m_popup.Data.SetButtonsLabels(LabelButtonOne, LabelButtonTwo);
        //
        //     //set the buttons callbacks as methods
        //     m_popup.Data.SetButtonsCallbacks(ClickButtonOne, ClickButtonTwo);
        //
        //     //OR set the buttons callbacks as lambda expressions
        //     //m_popup.Data.SetButtonsCallbacks(() => { ClickButtonOne(); }, () => { ClickButtonTwo(); });
        //
        //     //if the developer did not enable at least one button to hide it, make the UIPopup hide when its Overlay is clicked
        //     if (!HideOnButtonOne && !HideOnButtonTwo)
        //     {
        //         m_popup.HideOnClickOverlay = true;
        //         Debug.Log("Popup '" + PopupName + "' is set to close when clicking its Overlay because you did not enable any hide option");
        //     }
        //
        //     m_popup.Show(); //show the popup
        // }
        //
        // private void ClickButtonOne()
        // { 
        //     Debug.Log("Clicked button ONE: " + LabelButtonOne);
        //     if (HideOnButtonOne) ClosePopup();
        //     _careerManager.Continue();
        // }
        //
        // private void ClickButtonTwo()
        // {
        //     Debug.Log("Clicked button TWO: " + LabelButtonTwo);
        //     if (HideOnButtonTwo) ClosePopup();
        // }
        //
        // private void ClosePopup()
        // {
        //     if (m_popup != null) m_popup.Hide();
        // }
}