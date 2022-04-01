using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetPoint : MonoBehaviour
{
    public TMP_Text pointText;
    public GameObject totalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InsertPoint(float x, float y)
    {
        pointText.text = "( " + ((int)(x)).ToString() + ", " + ((int)(y)).ToString() + ")";
    }
}
