using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Camera cam;

    public float zoomStep, minCamSize, maxCamSize;


    private float minx;
    private float miny;
    private float maxx;
    private float maxy;


    private Bounds bounds;
    public SpriteRenderer map;
    // Start is called before the first frame update
    void Start()
    {
        minx = map.transform.position.x - map.bounds.size.x/2f;
        miny = map.transform.position.y - map.bounds.size.y/2f;
        maxx = map.transform.position.x + map.bounds.size.x/2f;
        maxy = map.transform.position.y + map.bounds.size.y/2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // PanCamera();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            Zoom(-zoomStep);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Zoom(zoomStep);
        }

    }
    Vector3 origin;
    bool drag;
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            origin = cam.ScreenToWorldPoint(Input.mousePosition);
            drag = true;
        }
        if (drag==true)
        {
            Vector3 diff=origin- cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.LogError("paning");

            ClampPosition(cam.transform.position+diff);

            //cam.transform.position += diff;
        }

        if (Input.GetMouseButtonUp(0))
        {
           
            drag = false;
        }
    }

    void Zoom(float step)
    {
        float size = cam.orthographicSize + step;
        cam.orthographicSize = Mathf.Clamp(size, minCamSize, maxCamSize);
        ClampPosition(cam.transform.position);
    }

   public Vector3 ClampPosition(Vector3 target)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = minx + camWidth;
        float minY = miny + camHeight;
        float maxX = maxx - camWidth;
        float maxY = maxy - camHeight;

        float newX = Mathf.Clamp(target.x, minX, maxX);

        float newY = Mathf.Clamp(target.y, minY, maxY);

        return new Vector3(newX, newY, target.z);
    }

}
