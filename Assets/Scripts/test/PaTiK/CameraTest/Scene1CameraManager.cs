using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1CameraManager : MonoBehaviour
{
    static public Scene1CameraManager instance;
    static public NewDialogueSystem camCommand;
    public GameObject target; // 카메라 중심축 대상
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로
    private Vector3 cameraCenter; // 카메라 중심축
    private float cameraSizeDefault;
    private string cameraCommnad;

    private bool zoomOut = false;
    private bool carriageStop = false;
    private bool logoActive = false;

    


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



    private void Scene1CameraControl(string command)
    {
       if(command == "Zoom Out")
        {
            zoomOut = true;
        }
        else if (command == "Zoom In")
        {
            zoomOut = false;
        }
        else if (command == "OnEvent")
        {
            carriageStop = true;
        }
        else if (command == "OffEvent")
        {
            carriageStop = false;
        }
        else if (command == "Pop Out")
        {
            logoActive = true;
        }
    }

    private void InRunningCarriage()
    {

    }

    private void InStopedCarriage()
    {

    }

    private void outOfCarriage()
    {

    }

    private void logoPopOut()
    {

    }








    // Update is called once per frame
    public void Update()
    {
        Scene1CameraControl(camCommand.GetCameraCommand());


        if (logoActive == true)
        {
            logoPopOut();
        }
        else if (zoomOut == false || carriageStop == false)
        {
            InRunningCarriage();
        }

        else if (zoomOut == false || carriageStop == true)
        {
            InStopedCarriage();
        }
        else if (zoomOut == true)
        {
            outOfCarriage();
        }




        //if (target.gameObject != null)
        //{
        //    targetPosition.Set(target.transform.position.x, this.transform.position.y, this.transform.position.z); // z값은 카메라 값으로

        //    this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //}

        //if (isDialogueOn)
        //{
        //    cameraZoom.orthographicSize = Mathf.Lerp(cameraZoom.orthographicSize, cameraSizePlus, Time.deltaTime / cameraSpeed);
        //}
        //else {
        //    cameraZoom.orthographicSize = Mathf.Lerp(cameraZoom.orthographicSize, cameraSizeMinus, Time.deltaTime / cameraSpeed);
        //}


    }
}
