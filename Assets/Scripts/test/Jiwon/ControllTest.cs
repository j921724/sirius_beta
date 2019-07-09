﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class ControllTest : MonoBehaviour
{
    static private ControllTest instance;
    public float speed = 8; // 캐릭터 이동 속도
    public float error = 0.5f; // 캐릭터와 마우스 좌표와의 오차 범위

    private Vector3 targetpos; // 마우스 좌표
    private bool moveit = false; // 이동 가능 여부
    private Vector3 minBound;
    private Vector3 maxBound;

    public BoxCollider2D boundBox;  // 맵 바운더리 지정
    public BoxCollider2D characterBox;// 캐릭터 바운더리 지정

    private float halfWidth;
    private float rightButtonSec;
    private float leftButtonSec;

    public Animator animator;   // 애니메이션 동작을 위한 선언  

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
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //characterBox = gameObject.GetComponent<BoxCollider2D>();
        //boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();

        //if (instance == null)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    instance = this;
        //}
        //else
        //{
        //    //Destroy(this.gameObject);
        //}

        float screenHeight = Screen.height; // 스크린 높이
        float screenWidth = Screen.width;   // 스크린 넓이



        halfWidth = (characterBox.size.x) / 2f;
        rightButtonSec = (screenWidth / 4) * 3;
        leftButtonSec = screenWidth / 4;
    }

    private void OnLevelWasLoaded(int level)
    {
        characterBox = gameObject.GetComponent<BoxCollider2D>();
        boundBox = GameObject.FindGameObjectWithTag("Background").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true) return; // UI창 나오면 클릭 금지
        if (Input.GetMouseButtonDown(0))
        {
            targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            moveit = true;
        }
        if (moveit)
        {
            animator.SetBool("isRunning", true);
            float dis = targetpos.x - transform.position.x; // 마우스 좌표 - 현재 캐릭터 좌표
            if (Mathf.Abs(dis) <= error)
            {
                moveit = false;
            }
            if (dis > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (dis < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);   // 움직이지 않는 동안
        }
    }
}
