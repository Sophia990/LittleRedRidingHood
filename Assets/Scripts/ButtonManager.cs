using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public ButtonSettings buttonSettings;

    // Start is called before the first frame update
    void Start()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = buttonSettings.normalColor;
            colors.highlightedColor = buttonSettings.highlightedColor;
            colors.pressedColor = buttonSettings.pressedColor;
            button.colors = colors;
        }
    }
}
