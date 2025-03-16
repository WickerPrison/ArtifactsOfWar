using UnityEngine;

[CreateAssetMenu(fileName = "ColorScheme", menuName = "Scriptable Objects/ColorScheme")]
public class ColorScheme : ScriptableObject
{
    public Color frontline;
    public Color backline;
    public Color collapsed;
    public Color turnMeter;
}
