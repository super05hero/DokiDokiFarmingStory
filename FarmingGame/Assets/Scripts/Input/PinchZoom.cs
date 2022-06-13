using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    Camera camera = null;

    [Header("Camera Zoom In & Out")]
    [SerializeField] float perspectiveZoomSpeed = 0.1f;
    [SerializeField] float orthoZoomSpeed = 0.1f;
    [SerializeField] float MaxFieldOfVision = 0f;
    [SerializeField] float MinFieldOfVision = 0f;

    bool isPanning = false;

    Vector3 touchStart;

    TouchMode touchMode;


    enum TouchMode
    {
        SingleTouch = 1,
        MultipleTouch = 2,
    }


    private void Start()
    {
        camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            if (Input.touchCount == 1)
            {
                if (touchMode != TouchMode.SingleTouch) isPanning = false;
                touchMode = TouchMode.SingleTouch;
            }
            else if (Input.touchCount >= 2)
            {
                if (touchMode != TouchMode.MultipleTouch) isPanning = false;
                touchMode = TouchMode.MultipleTouch;
            }

            if (!isPanning)
            {
                isPanning = true;
                if (touchMode == TouchMode.SingleTouch)
                    touchStart = camera.ScreenToWorldPoint(Input.GetTouch(0).position);
                else
                    touchStart = camera.ScreenToWorldPoint(MiddlePoint(Input.GetTouch(0).position, Input.GetTouch(1).position));
            }

            Vector3 diff;
            if (Input.touchCount == 1)
                diff = touchStart - camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            else
                diff = touchStart - camera.ScreenToWorldPoint(MiddlePoint(Input.GetTouch(0).position, Input.GetTouch(1).position));

            camera.transform.position += diff;

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;


                float preTouchDeltaMag = (touchZeroPrePos - touchOnePrePos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = preTouchDeltaMag - touchDeltaMag;

                if (camera.orthographic)
                {
                    camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                    // Debug.Log(camera.orthographicSize);
                    
                    camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, MinFieldOfVision, MaxFieldOfVision);

                }

            }
        }
        else isPanning = false;
    }
    
   Vector3 MiddlePoint(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, (vec1.z + vec2.z) / 2);
    }

    Vector2 MiddlePoint(Vector2 vec1, Vector2 vec2)
    {
        return new Vector2((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2);
    }

}
