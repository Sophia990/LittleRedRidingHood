using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSettings", menuName = "ScriptableObjects/ButtonSettings", order = 1)]
public class ButtonSettings : ScriptableObject
{
    public Color normalColor;
    public Color highlightedColor;
    public Color pressedColor;
}
