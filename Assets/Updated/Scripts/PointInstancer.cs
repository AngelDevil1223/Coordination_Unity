using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MapViewer
{
    public class PointInstancer : MonoBehaviour
    {

        Vector3 worldPosition;
        Camera cam;
        private void Start()
        {
            cam = GameManager.Instance.cam;

        }
        private void OnMouseUp()
        {
            draggindValue = 0;
            if (GameManager.Instance.isDragging != true)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                GameManager.Instance.AddPoints(worldPosition);
            }
            else
            {
                GameManager.Instance.isDragging = false;
            }
        }
        private void OnMouseDown()
        {
            worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            pastPos = worldPosition;
        }
        Vector3 pastPos;
        float camzValue;
        float draggindValue;

        public float threshold=5;
        private void OnMouseDrag()
        {
            draggindValue += 10f*Time.deltaTime;

            if (draggindValue > threshold)
            {
                Debug.Log("dragging");
                GameManager.Instance.isDragging = true;
            }           
        }
        
        private void OnMouseOver()
        {
            Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            UiManager.Instance.MousePosition(worldPosition);
        }
    }
}
