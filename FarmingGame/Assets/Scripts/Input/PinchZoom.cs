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

    private void Start()
    {
        camera = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            if (!isPanning)
            {
                isPanning = true;
                touchStart = camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            Vector3 diff = touchStart - camera.ScreenToWorldPoint(Input.GetTouch(0).position);

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
    
   
}
