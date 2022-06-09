using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRect : MonoBehaviour
{
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleWidth = ((float)Screen.height / Screen.width) / ((float)9 / 16);
        float scaleHeight = 1f / scaleWidth;
        if(scaleWidth < 1)
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        else
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        camera.rect = rect;
    }

    private void OnPreCull() => GL.Clear(true, true, Color.black);
}
