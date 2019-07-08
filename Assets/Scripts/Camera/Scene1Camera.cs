using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Camera : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상.

    private Vector3 targetPosition; // 대상의 현재 위치 
    private Vector3 minBound;
    private Vector3 maxBound;

    public float moveSpeed; // 카메라 속도
    private float halfWidth;
    private float halfHeight;

    public BoxCollider2D boundBox; // 카메라 활동 영역

    private Camera getCamera;

    public void SetBound(BoxCollider2D newBound)
    {
        boundBox = newBound;
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;
    }

    private void Start()
    {
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();

        getCamera = GetComponent<Camera>();
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;
        halfHeight = getCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);       // clap() 함수로 영역 제한
        }
    }
}