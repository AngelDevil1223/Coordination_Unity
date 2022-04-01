using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QualitySettingsss : MonoBehaviour
{

    public TextMeshProUGUI QualityText;
    public TextMeshProUGUI ResolutionText;

    int index = 2;
    int indexr;

    public Vector2Int[] resolution;
    private void Start()
    {

        index = QualitySettings.GetQualityLevel();
        QualityText.text = index == 0 ? "Low" : index == 1 ? "Medium" : index == 2 ? "High" : null;
        // Screen.SetResolution(resolution[indexr].x, resolution[indexr].y, FullScreenMode.FullScreenWindow, 30);
        //QualitySettings.SetQualityLevel(index, true);
        ResolutionText.text = resolution[indexr].x + "X" + resolution[indexr].y;

    }
    [ContextMenu("quality")]
    public void AddQuality(int q)
    {
        index += q;
        index = Mathf.Clamp(index, 0, 2);

        QualitySettings.SetQualityLevel(index, true);
        QualityText.text = index==0?"Low":index==1?"Medium":index==2?"High":null;
    }
    [ContextMenu("qualityr")]
    public void AddResolution(int q)
    {
        indexr += q;
        indexr = Mathf.Clamp(indexr, 0, resolution.Length-1);
        Screen.SetResolution(resolution[indexr].x, resolution[indexr].y,FullScreenMode.FullScreenWindow,30);
        //QualitySettings.SetQualityLevel(index, true);
        ResolutionText.text = resolution[indexr].x+"X"+ resolution[indexr].y;
    }
}
