using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Threading;
/*
현재 포탈의 태그를 가져옴(이동할 씬의 이름을 태그로 설정)
moveScene: 이동할 씬의 이름 - 현재 포탈의 태그
currScene: 현재 씬의 이름 - 씬이 바뀐뒤, 바뀐 씬에 존재하는 포탈의 태그를 가져올 때 사용(캐릭터 이동후 위치)
*/
public class changeScene : MonoBehaviour
{
    public string moveScene;    //이동할 씬
    public Vector3 movePos;     //이동한 씬에서 캐릭터 위치
    private GameObject Mary;

    public void changeFirstScene()
    {
        SceneManager.LoadScene(moveScene);
        Mary.GetComponent<Transform>().position = movePos;
        Thread.Sleep(500);
    }

    private void OnMouseDown()
    {
        changeFirstScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        Mary = GameObject.FindGameObjectWithTag("Mary");
    }
}