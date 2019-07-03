using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTest : MonoBehaviour
{

    private Transform button;   //상호작용을 위한 버튼

    private void OnTriggerStay2D(Collider2D col)    // mary가 상호작용 가능 범위 일 때
    {
        if (col.tag == "Mary")
        {
            button.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)    // mary가 상호작용 가능 범위 벗어날 때
    {
        if (col.tag == "Mary")
        {
            button.gameObject.SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        button = transform.Find("button");  //자식 오브젝트 중 버튼
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
