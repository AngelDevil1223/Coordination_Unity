using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MapViewer
{
    public class Point : MonoBehaviour
    {

        public TextMeshPro pos;

        public string myName;
       public  Vector2 mypos { get; set; }

        public bool isHide;
        // Start is called before the first frame update
        void Start()
        {
            pos = GetComponentInChildren<TextMeshPro>();
        }

        public void SetPos(Vector2 position)
        {
            mypos = position;
            myName = position.ToString();
            Debug.Log(mypos);
            pos.text = position.ToString();
        }

    }
}
