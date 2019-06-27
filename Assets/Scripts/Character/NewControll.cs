using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 캐릭터 조작 관련 스크립트
/// </summary>
public class NewControll : MonoBehaviour
{

    public float speed; // 캐릭터 이동 속도
    public bool moveit = false; // 이동 가능 여부

    private NewControll instance = null;
    private Vector3 targetpos; // 마우스 좌표
    float rightButtonSec;
    float leftButtonSec;

    // Start is called before the first frame update
    void Start()
    {
        float height = Screen.height;
        float width = Screen.width;


        // 텔레포트할 시 도플갱어 삭제
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


        rightButtonSec = (width / 4) * 3;
        leftButtonSec = width / 4;

        Debug.Log(leftButtonSec);


    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true) return; // UI창 나오면 클릭 금지
        if (Input.GetMouseButton(0))
        {
            targetpos = Input.mousePosition; // 클릭시 스크린에서의 마우스 포지션

            moveit = true;
        }
        else//(Input.GetMouseButtonUp(0))
        {
            moveit = false;
        }


        if (moveit)
        {
            float dis = targetpos.x; // 마우스 클릭지점

            if (dis > rightButtonSec)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else if (dis < leftButtonSec)
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }
}
