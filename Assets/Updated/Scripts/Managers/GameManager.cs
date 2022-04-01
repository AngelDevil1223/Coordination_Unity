using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MapViewer
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;


        public List<Point> points;
        public Point point;

        public GameObject line;
        public int maxLines = 4000;
        public Camera cam;
        public GameObject background;
        private List<string> positions = new List<string>();


        public bool isDragging { get; set; }

        Sprite defaultSprite;

        public PanAndZoom panAndZoom;
        private void Awake()
        {
            Instance = this;


        }
        private void Start()
        {
            points = new List<Point>();
            //verticalLines
            for(int i = 0; i < maxLines; i++)
            {
               Transform l= Instantiate(line, new Vector2(i-(maxLines/2)+0.5f, 0+0.5f),Quaternion.identity).transform;
                l.localScale = new Vector3(0.1f, 4000f, 0f);

            }
            //HorizontalLines
            for (int i = 0; i < maxLines; i++)
            {
                Transform l = Instantiate(line, new Vector2( 0 + 0.5f,i - (maxLines / 2) + 0.5f), Quaternion.identity).transform;
                l.localScale = new Vector3( 4000f, 0.1f, 0f);

            }

            defaultSprite = background.GetComponent<SpriteRenderer>().sprite;

            ChangeBounds(defaultSprite);
        }
        void ChangeBounds(Sprite sprite)
        {

            panAndZoom.boundMinX = (background.transform.position.x - sprite.bounds.size.x / 2f) - 1;
            panAndZoom.boundMinY = (background.transform.position.y - sprite.bounds.size.y / 2f) - 1;
            panAndZoom.boundMaxX = (background.transform.position.x + sprite.bounds.size.x / 2f) + 1;
            panAndZoom.boundMaxY = (background.transform.position.y + sprite.bounds.size.y / 2f) + 1;
        }
        public void RemovePoints(string name)
        {
            if (points.Count > 0 && name != null)
            {
                for (int i=0;i<points.Count;i++)
                {
                    if (points[i].myName == name)
                    {
                        positions.Remove("( " + ((int)(points[i].mypos.x)).ToString() + ", " + ((int)(points[i].mypos.y)).ToString() + ")");

                        Destroy(points[i].gameObject);
                        points.Remove(points[i]);
                    }
                }
            }
        }

        
        public void HidePoint(string name,bool isactive)
        {
            if (points.Count > 0 && name != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].myName == name)
                    {
                        points[i].gameObject.SetActive(isactive);
                       
                    }
                }
            }
        }

        public Point GetPointByName(string name)
        {
            if (points.Count > 0 && name != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].myName == name)
                    {
                        return points[i];

                    }
                }
            }
            Debug.LogError("noPointsToHide");
            return null;
        }
        public void AddPoints(Vector3 worldPoition)
        {
            worldPoition.z = 0f;
            worldPoition.x = Mathf.Round(worldPoition.x);
            worldPoition.y = Mathf.Round(worldPoition.y);
            foreach (Point point in points)
            {
                if(point.mypos==new Vector2(worldPoition.x,worldPoition.y))
                {
                    return;
                }
            }      

            positions.Add("( " + ((int)(worldPoition.x)).ToString() + ", " + ((int)(worldPoition.y)).ToString() + ")");

            Point p = Instantiate(point, worldPoition, Quaternion.identity);
            p.SetPos(worldPoition);
            points.Add(p);
            UiManager.Instance.AddPoints(p);
        }

        public void OnOverPoint(string ButtonName,Color c)
        {
            if (points.Count > 0 && ButtonName != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].myName == ButtonName)
                    {
                        points[i].GetComponent<SpriteRenderer>().color = c;

                    }
                }
            }
        }



        public void ImportImageFile()
        {

            var paths = StandaloneFileBrowser.OpenFilePanel("Open Image", "", "png", false);
            byte[] bytes = File.ReadAllBytes(paths[0]);
            Texture2D texture = new Texture2D(2048, 2048, TextureFormat.RGB24, false);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Bilinear;          
            texture.LoadImage(bytes);
            Rect rec = new Rect(0, 0, texture.width, texture.height);
            float k = (texture.width * defaultSprite.pixelsPerUnit) / defaultSprite.texture.width;
            background.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture,rec, new Vector2(0.5f, 0.5f), k);
            ChangeBounds(background.GetComponent<SpriteRenderer>().sprite);


        }

        public void ExportPositions()
        {
            string data = "";
            for (int i = 0; i < positions.Count; i++)
            {
                data += positions[i] + "\n";
            }
            var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "Map", "txt");
            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, data);
            }
        }

        public void ClearData()
        {
            for (int i = 0; i < points.Count; i++)
            {
                Destroy(points[i].gameObject);
            }

            points.Clear();
        }

    }
}
