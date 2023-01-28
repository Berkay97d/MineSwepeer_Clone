using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private Camera camera;


    private void Start()
    {
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        var y = (float)Data.CurrentGameData.RowSize / 2;
        var x = (float) Data.CurrentGameData.ColumSize / 2;

        camera.transform.position = new Vector3(x, y+1, -1);

        var zoomSize = (Mathf.Max(x*2, y*2) / 2) + 1;
        camera.orthographicSize = zoomSize;
    }

    

}
