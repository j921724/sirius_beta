using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCameraManager : MonoBehaviour
{
    static public DefaultCameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상.

    private Vector3 targetPosition; // 대상의 현재 위치 
    private Vector3 minBound;  
    private Vector3 maxBound;

    public float adjustX;
    public float adjustY; // 카메라 높이 조절
    public float moveSpeed; // 카메라 속도
    private float halfWidth;
    private float halfHeight;

    public BoxCollider2D bound; // 카메라 활동 영역
    
    private Camera getCamera;

    

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }


    // Use this for initialization
    void Start()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        getCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = getCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }


        // Update is called once per frame
        void Update()
    {

        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y+adjustY, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); 

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + (halfWidth + adjustX), maxBound.x - (halfWidth+adjustX));
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);       // clap() 함수로 영역 제한

        }
    }

   
}



