using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using SFB;

public class GeneratePoint : MonoBehaviour, IPointerDownHandler
{
    
    public TMP_InputField xField;
    public TMP_InputField yField;
    private float x, y;
    public GameObject pointPrefab;
    public GameObject Parent;

    public TMP_Text currentX;
    public TMP_Text currentY;

    public GameObject rowLine;
    public GameObject columnLine;

    public GameObject rowPrefab;
    public GameObject columnPrefab;

    public GameObject rowParent;
    public GameObject columnParent;

    public GameObject background;
    private float scale = 1f;

    private float xPivot = 1f, yPivot = 1f;
    private float xOdd, yOdd;
    private float xAdd, yAdd;

    private List<string> positions = new List<string>();

    private int flag = 0;

    public TMP_Text showHideText;

    private GameObject[] points;

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("point");
        positions.Clear();
        xOdd = ((Screen.width) * 12f) / 4000f;
        yOdd = ((Screen.height) * 12f) / 4000f;

        xAdd = ((Screen.width) * 2000f) / 4000f;
        yAdd = ((Screen.height) * 2000f) / 4000f;

        for (int i = 0; i < 167; i++)
        {
            GameObject row = Instantiate(rowPrefab, new Vector3(0, yOdd * (i),0), Quaternion.identity);
            row.transform.SetParent(rowParent.transform);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, yOdd * (i), 0);

            GameObject column = Instantiate(columnPrefab, new Vector3(xOdd * (i), 0, 0), Quaternion.identity);
            column.transform.SetParent(columnParent.transform);
            column.GetComponent<RectTransform>().localPosition = new Vector3(xOdd * (i), 0, 0);
        }
        for (int i = 0; i < 167; i++)
        {
            GameObject row = Instantiate(rowPrefab, new Vector3(0, -yOdd * (i + 1), 0), Quaternion.identity);
            row.transform.SetParent(rowParent.transform);
            row.GetComponent<RectTransform>().localPosition = new Vector3(0, -yOdd * (i + 1), 0);

            GameObject column = Instantiate(columnPrefab, new Vector3(-xOdd * (i), 0, 0), Quaternion.identity);
            column.transform.SetParent(columnParent.transform);
            column.GetComponent<RectTransform>().localPosition = new Vector3(-xOdd * (i), 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scale < 1)
        {
            scale = 1;
        }

        xPivot = ((Input.mousePosition.x) / Screen.width);
        yPivot = ((Input.mousePosition.y) / Screen.height);

        rowLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (Input.mousePosition.y - yAdd), 0);
        columnLine.GetComponent<RectTransform>().localPosition = new Vector3((Input.mousePosition.x - xAdd), 0, 0);

        currentX.text = "X= " + ((int)((Input.mousePosition.x - xAdd) / (xOdd))).ToString();
        currentY.text = "Y= " + ((int)((Input.mousePosition.y - yAdd) / (yOdd))).ToString();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ZommIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            ZommOut();
        }
        SetPointScale();
        if (zoomIn)
        {
            ZommIn();
        }
        if (zoomOut)
        {
            ZommOut();
        }
    }
    bool zoomIn, zoomOut;
    public void ZommInIn()
    {
        zoomIn = true;
    }
    public void ZommInout()
    {
        zoomIn = false;
    }
    public void ZommOutIn()
    {
        zoomOut = true;
    }
    public void ZommOutout()
    {
        zoomOut = false;
    }
    public void ZommIn()
    {
        if (scale < 5f)
            scale += 0.05f;
        background.GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
        background.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }
    public void ZommOut()
    {
        if (scale > 1f)
            scale -= 0.05f;
        background.GetComponent<RectTransform>().pivot = new Vector2(xPivot, yPivot);
        background.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
    }
    public void SetPointScale()
    {

        GameObject[] points = GameObject.FindGameObjectsWithTag("square");
        for (int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<RectTransform>().sizeDelta = new Vector2(xOdd, yOdd);
        }
    }

    public void SetTextScale()
    {
        GameObject[] posTexts = GameObject.FindGameObjectsWithTag("postext");
        for (int i = 0; i < posTexts.Length; i++)
        {
            posTexts[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    public void GenerateBtnClick()
    {
        if (scale == 1)
        {
            x = float.Parse(xField.text);
            y = float.Parse(yField.text);
            GameObject obj = Instantiate(pointPrefab, new Vector2(x * xOdd, y * yOdd), Quaternion.identity);
            obj.transform.SetParent(Parent.transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(x * xOdd + xOdd/2, y * yOdd + yOdd/2, 0);
            obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);

            obj.GetComponent<SetPoint>().InsertPoint(x, y);
            positions.Add("( " + ((int)(x)).ToString() + ", " + ((int)(y)).ToString() + ")");
        }        
    }

    public void ClearBtnClick()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("point");
        for(int i=0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
        positions.Clear();
    }

    public void ToggleBtn()
    {
        flag++;
        if (flag % 2 == 1)
        {
            points = GameObject.FindGameObjectsWithTag("point");
            showHideText.text = "Show";
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(false);
            }
        }
        if (flag % 2 == 0)
        {
            showHideText.text = "Hide";
            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(true);
            }
        }
    }
    public float units;
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //if (scale == 1)
        //{
            x = (int)((((Input.mousePosition.x) - xAdd) / (xOdd))*((scale* units)-units));
            y = (int)((((Input.mousePosition.y) - yAdd) / (yOdd)) * ((scale * units)-units));

            GameObject obj = Instantiate(pointPrefab, new Vector2((x - xAdd), (y - yAdd)), Quaternion.identity);
            obj.transform.SetParent(Parent.transform);
            obj.GetComponent<RectTransform>().localPosition = new Vector3((x * xOdd) + xOdd / 2, (y * yOdd) + yOdd / 2, 0);// (x - xAdd)/(scale*xOdd), (y - yAdd)/(yOdd*scale), 0);
            obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);

            obj.GetComponent<SetPoint>().InsertPoint(x , y);
            positions.Add("( " + ((int)(x)).ToString() + ", " + ((int)(y)).ToString() + ")");
        //}
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void ImportImageFile()
    {

        var paths = StandaloneFileBrowser.OpenFilePanel("Open Image", "", "png", false);
        byte[] bytes = File.ReadAllBytes(paths[0]);
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        texture.LoadImage(bytes);

        background.GetComponent<RawImage>().texture = texture;
    }

    public void ExportPositions()
    {
        string data = "";
        for(int i = 0; i < positions.Count; i++)
        {
            data += positions[i] + "\n";
        }
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "Map", "txt");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, data);
        }
    }
}
