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
    public float speed; // 캐릭터 이동 속도
    public float error; // 캐릭터와 마우스 좌표와의 오차 범위

    private Vector3 targetpos; // 마우스 좌표
    private bool moveit = false; // 이동 가능 여부

    private void OnCollisionStay2D(Collision2D col)    // 벽에 부딪히면 멈춤
    {
        if (col.gameObject.tag == "wall")
        {
            targetpos = gameObject.transform.position;
        }

    }

    // Start is called before the first frame update
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
    }
}
