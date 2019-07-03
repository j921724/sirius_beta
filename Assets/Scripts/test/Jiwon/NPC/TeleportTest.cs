using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Threading;

/// <summary>
/// T
/// 
/// </summary>

public class TeleportTest : MonoBehaviour
{
    public string moveScene;    //이동할 씬
    public Vector3 movePos;     //이동한 씬에서 캐릭터 위치
    private GameObject Mary;
    private GameObject teleportButton;

    void Start()
    {
        Mary = GameObject.FindGameObjectWithTag("Mary");
        teleportButton = gameObject.transform.Find("Teleport Button").gameObject;
    }

    public void ChangeFirstScene()
    {
        SceneManager.LoadScene(moveScene);
        Mary.GetComponent<Transform>().position = movePos;
        Thread.Sleep(500);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Mary")
        {
            teleportButton.SetActive(true);
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(pos, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            if (hit.collider.name == teleportButton.name)
            {
                ChangeFirstScene();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mary")
        {
            teleportButton.SetActive(false);
        }
    }

    // Start is called before the first frame update

}
