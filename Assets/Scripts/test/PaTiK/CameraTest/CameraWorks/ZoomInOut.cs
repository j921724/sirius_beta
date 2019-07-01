using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    private Camera cameraZoom; // 카메라 오브젝트
    public float cameraSizePlus = 5f; // 카메라 사이즈 조절
    public float cameraSizeMinus = 10f;
    public float cameraSpeed = 0.5f; // 카메라 줌인/아웃 속도
    private bool isZoomIn = false;
    private bool isZoomOut = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraZoom = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isZoomIn)
        {
            cameraZoom.orthographicSize = Mathf.Lerp(cameraZoom.orthographicSize, cameraSizePlus, Time.deltaTime / cameraSpeed);
        }
        else if(isZoomOut)
        {
            cameraZoom.orthographicSize = Mathf.Lerp(cameraZoom.orthographicSize, cameraSizeMinus, Time.deltaTime / cameraSpeed);
        }
    }
}
