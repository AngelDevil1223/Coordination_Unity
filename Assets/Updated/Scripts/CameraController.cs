using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MapViewer
{
    public class CameraController : MonoBehaviour
    {
        float cameraSize;
        Camera cam;
       // public float zoomSpeed = 1f;

        public float minSize = 1f;


        private float rightBound;
        private float leftBound;
        private float topBound;
        private float bottomBound;


        private Bounds bounds;
        public SpriteRenderer map;


        private void Start()
        {
            cam = GetComponent<Camera>();
            cameraSize = cam.orthographicSize;
            size = cameraSize;



        }

        private void LateUpdate()
        {
            //Zoom();

            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                Zoom(-zoomSpeed);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                Zoom(zoomSpeed);
            }

            //if (UiManager.Instance.zoomIn)
            //{
            //    Zoom(-zoomSpeed*Time.deltaTime*2f);
            //}
            //if (UiManager.Instance.zoomOut)
            //{
            //    Zoom(zoomSpeed*Time.deltaTime*2f);
            //}
        }

        private void Zoom()
        {
            // Get MouseWheel-Value and calculate new Orthographic-Size
            // (while using Zoom-Speed-Multiplier)
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            float newZoomLevel = cam.orthographicSize - mouseScrollWheel;

            // Get Position before and after zooming
            Vector3 mouseOnWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            cam.orthographicSize = Mathf.Clamp(newZoomLevel, minCamSize, maxCamSize);
            Vector3 mouseOnWorld1 = cam.ScreenToWorldPoint(Input.mousePosition);

            // Calculate Difference between Positions before and after Zooming
            Vector3 posDiff = mouseOnWorld - mouseOnWorld1;

            // Add Difference to Camera Position
            Vector3 camPos = cam.transform.position;
            Vector3 targetPos = new Vector3(
                camPos.x + posDiff.x,
                camPos.y + posDiff.y,
                camPos.z);

            // Apply Target-Position to Camera
            cam.transform.position = targetPos;
        }
        float size;

        [SerializeField] private float zoomSpeed = 20f;
        [SerializeField] private float minCamSize = 5f;
        [SerializeField] private float maxCamSize = 20f;

        Vector3 worldPosition;
        void Zoom(float speed)
        {
            size += speed;
            size = Mathf.Clamp(size, minSize, cameraSize);
            cam.orthographicSize = size;
            worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            float camVertExtent = cam.orthographicSize;
            float camHorzExtent = cam.aspect * camVertExtent;


            leftBound = bounds.min.x + camHorzExtent;
            rightBound = bounds.max.x - camHorzExtent;
            bottomBound = bounds.min.y + camVertExtent;
            topBound = bounds.max.y - camVertExtent;


            float xdiff = worldPosition.x;
            float ydiff = worldPosition.y ;

            float camX = Mathf.Clamp(cam.transform.position.x - xdiff, leftBound, rightBound);
            float camY = Mathf.Clamp(cam.transform.position.y - ydiff, bottomBound, topBound);

            cam.transform.position = new Vector3(camX, camY, -38);
        }



        // Ortographic camera zoom towards a point (in world coordinates). Negative amount zooms in, positive zooms out
        // TODO: when reaching zoom limits, stop camera movement as well
        void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
        {
            // Calculate how much we will have to move towards the zoomTowards position
            float multiplier = (1.0f / cam.orthographicSize * amount);

            // Move camera
            transform.position += (zoomTowards - transform.position) * multiplier;

            // Zoom camera
            cam.orthographicSize -= amount;

            // Limit zoom
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, cameraSize);
        }
    }
}
