using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class ControlAddForce : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------
    private ControlAddForce instance = null;


    public bool moveit = false; // 이동 가능 여부

    public BoxCollider2D boundBox;  // 맵 바운더리 지정
    public BoxCollider2D characterBox;// 캐릭터 바운더리 지정

    private Vector3 targetpos; // 마우스 좌표
    private Vector3 minBound;
    private Vector3 maxBound;

    private float halfWidth;
    private float rightButtonSec;
    private float leftButtonSec;
    public float speed = 20.0f; // 캐릭터 이동 속도


    //---------------------------------------------------------------------------------------------
    private void Awake()
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
    }
    // Start is called before the first frame update
    void Start()
    {
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
        //---------------------------------------------------------------------------------------------
        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;

        halfWidth = (characterBox.size.x) / 2f;

        float screenHeight = Screen.height; // 스크린 높이
        float screenWidth = Screen.width;   // 스크린 넓이
        //---------------------------------------------------------------------------------------------


        rightButtonSec = (screenWidth / 4) * 3;
        leftButtonSec = screenWidth / 4;
    }

    private void OnLevelWasLoaded(int level)
    {
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();

        minBound = boundBox.bounds.min;
        maxBound = boundBox.bounds.max;

        halfWidth = (characterBox.size.x) / 2f;

        float screenHeight = Screen.height; // 스크린 높이
        float screenWidth = Screen.width;   // 스크린 넓이
        //---------------------------------------------------------------------------------------------


        rightButtonSec = (screenWidth / 4) * 3;
        leftButtonSec = screenWidth / 4;
    }

    // Update is called once per frame
    void Update()
    {
        //캐릭터의 이동 가능 x축 y축 제한
        float clampedX = Mathf.Clamp(transform.position.x, minBound.x, maxBound.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBound.y, maxBound.y);

        transform.position = new Vector3(clampedX, clampedY, 0);
        //---------------------------------------------------------------------------------------------

        if (EventSystem.current.IsPointerOverGameObject() == true) return; // UI창 나오면 클릭 금지
        if (Input.GetMouseButton(0))
        {
            targetpos = Input.mousePosition; // 클릭시 스크린에서의 마우스 포지션

            moveit = true;
        }

        else
        {
            moveit = false;
        }

        //---------------------------------------------------------------------------------------------

        if (moveit)
        {
            float dis = targetpos.x; // 마우스 클릭지점

            if (dis > rightButtonSec)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                GetComponent<Rigidbody2D>().AddForce(Vector3.right * speed);
            }
            else if (dis < leftButtonSec) // 플레이어 터치 가능위치 제한, 캐릭터 이동위치 제한
            {

                transform.eulerAngles = new Vector3(0, 180, 0);
                GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}