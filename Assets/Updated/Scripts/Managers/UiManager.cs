using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace MapViewer
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;


        public Button point;
        public Transform pointlistParent;
        List<Button> points;
        List<string> selectedPoint;

        public TextMeshProUGUI xValue;
        public TextMeshProUGUI yValue;

        public TMP_InputField Goalx;
        public TMP_InputField Goaly;


        public GameObject selectButton;
        public GameObject selectPanel;

        public GameObject statusPanel;
        public GameObject statusButton;

        public GameObject hideALLbUTTON;

        public GameObject startButon;

        public Texture2D Map;
        public TextMeshProUGUI unHidePointText;
        public GameObject settingsPanel;
        private void Awake()
        {
            Instance = this;
            
        }

        
        private void Start()
        {
            //Map.Apply();

            points = new List<Button>();
            selectedPoint = new List<string>();
            HideAll();
        }
        public void AddPoints(Point p)
        {
            Button b = Instantiate(point, pointlistParent);
            b.name = p.mypos.ToString();
            b.GetComponentInChildren<TextMeshProUGUI>().text = p.mypos.ToString();
            b.onClick.AddListener(PointOnClickEvent);    
            points.Add(b);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                keyDown = true;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                keyDown = true;
            }
        }
        bool keyDown;
        void PointOnClickEvent()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                selectedPoint.Add(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            }
            else
            {
                selectedPoint.Clear();
                selectedPoint.Add(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            }
            if (points.Count > 0 && selectedPoint != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < selectedPoint.Count; j++)
                    {
                        if (points[i].name == selectedPoint[j])
                        {
                            Point p = GameManager.Instance.GetPointByName(selectedPoint[j]);

                            if (p.isHide)
                            {
                                unHidePointText.text = "UnHide";

                            }
                            else
                            {
                                unHidePointText.text = "Hide";

                            }
                        }
                    }
                }
            }
            Debug.Log("selectedPointForRemove..." + selectedPoint);
        }
        bool ishide;
        public void HideSelectedPoint()
        {
            HidePoint();

        }
       void HidePoint()
        {
            if (points.Count > 0 && selectedPoint != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < selectedPoint.Count; j++)
                    {
                        if (points[i].name == selectedPoint[j])
                        {
                            Point p = GameManager.Instance.GetPointByName(selectedPoint[j]);
                            if (p.isHide)
                            {
                                p.gameObject.SetActive(true);
                                points[i].gameObject.GetComponent<Image>().color = Color.white;
                                unHidePointText.text = "Hide";
                                p.isHide = false;

                            }
                            else
                            {
                                p.gameObject.SetActive(false);
                                points[i].gameObject.GetComponent<Image>().color = Color.cyan;
                                p.isHide = true;
                                unHidePointText.text = "UnHide";

                            }

                        }
                    }
                }
            }
            selectedPoint.Clear();
        }
        public void RemovePoint()
        {
            if (points.Count > 0&&selectedPoint!=null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < selectedPoint.Count; j++)
                    {
                        if (points[i].name == selectedPoint[j])
                        {

                            Destroy(points[i].gameObject);
                            GameManager.Instance.RemovePoints(selectedPoint[j]) ;
                            points.Remove(points[i]);

                        }
                    }
                }
            }

        }

        public void MousePosition(Vector2 pos)
        {
            xValue.text = ((int)(pos.x)).ToString();
            yValue.text = ((int)(pos.y)).ToString();

        }

        public void Mark()
        {
            if (Goalx.text == null || Goaly.text == null)
                return;
            int x = int.Parse(Goalx.text);
            int y = int.Parse(Goaly.text);
            Debug.Log("MarkPos.." + x + "..." + y);
            GameManager.Instance.AddPoints(new Vector3(x, y, 0));
        }

        public void SelectButton()
        {
            if (selectPanel.gameObject.activeInHierarchy)
            {
                selectPanel.gameObject.SetActive(false);
            }
            else
            {
                selectPanel.gameObject.SetActive(true);

            }
        }

        public void HideAll()
        {
            statusPanel.SetActive(false);
            selectPanel.SetActive(false);

            statusButton.SetActive(false);
           
            selectButton.SetActive(false);
            startButon.SetActive(false);
            hideALLbUTTON.SetActive(true);

        }

        public void Reveal()
        {
            statusPanel.SetActive(true);
            statusButton.SetActive(true);
            startButon.SetActive(true);
            hideALLbUTTON.SetActive(false);

        }

        public void StartButton()
        {
            selectButton.SetActive(true);
            selectPanel.gameObject.SetActive(true);
            statusButton.SetActive(true);
            startButon.SetActive(false);


        }


        public void Settings()
        {
            if (settingsPanel.activeInHierarchy)
            {
                settingsPanel.SetActive(false);
            }
            else
            {
                settingsPanel.SetActive(true);

            }
        }
        public void ExitApp()
        {
            Application.Quit();
        }
        public void ImportData()
        {
            GameManager.Instance.ImportImageFile();
        }

        public void ExportData()
        {
            GameManager.Instance.ExportPositions();
        }
        public void ClearData()
        {
            GameManager.Instance.ClearData();
            for(int i = 0; i < points.Count; i++)
            {
                Destroy(points[i].gameObject);
            }

            points.Clear();
        }
    }
}