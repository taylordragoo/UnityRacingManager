using UnityEngine;

public class PracticeHUD : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height),
        "This is a box"
        );
    }
}