using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EventDialogueTextPosion
{
    public Vector3 txtPlayer = new Vector3(-150.0f, 200.0f, 0.0f); // 플레이어 쪽 텍스트 위치
    public Vector3 txtNPC = new Vector3(150.0f, 200.0f, 0.0f);// NPC 쪽 텍스트 위치
}

public class EventDialogueSystem : MonoBehaviour
{

    // 텍스트 UI를 연동하기 위한 변수
    public string txtFile; // 스크립트 파일 이름
    private Image dialogueBox;
    private Text txt; // 텍스트 오브젝트
    private Image panel; // 대화 중 다른 기능 금지
    private GameObject dialogueButton;  // 대화 버튼
    private GameObject dialogueUI;   // 대화 UI

    // 인게임 대화에 필요한 변수
    private GameObject player; // 플레이어 오브젝트 
    private float error = 8.0f; // 버튼 출력하기 위한 거리
    public int count = 0; // 텍스트 문서 단위
    public bool talking = false; // 텍스트 UI 활성화 트리거

    // 텍스트 위치에 필요한 변수 
    public bool setTextPosition; // 사용자 정의 위치로 설정
    public EventDialogueTextPosion eventDialogueTextPosion;
    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC; // NPC 쪽 텍스트 위치

    // 카메라 조작에 필요한 변수
    //public DollyZoom cameraCtrl;

    public List<Dictionary<string, object>> dialogueData;

    void Start()
    {
        if (GameObject.FindWithTag("Mary") != null)
        {
            player = GameObject.FindWithTag("Mary").gameObject;
        }
       
        dialogueData = CSVReader.Read(txtFile);
        dialogueUI = GameObject.Find("Dialogue UI");
        dialogueBox = dialogueUI.transform.Find("Dialogue Box").GetComponent<Image>();
        txt = dialogueBox.transform.Find("Dialogue Text").GetComponent<Text>();
        panel = dialogueUI.transform.Find("Dialogue Panel").GetComponent<Image>();

    }

    private void OnOff(bool _flag)
    {
        panel.gameObject.SetActive(_flag);
        dialogueBox.gameObject.SetActive(_flag);
        txt.gameObject.SetActive(_flag);
    }

    public void ShowDialogue()
    {
        SetDialoguePosition();
        OnOff(true);
        count = 0;
        talking = true;
    }

    public void HideDialogue()
    {
        OnOff(false);
        talking = false;
    }

    private void NextDialogue()
    {
        //string fulltext = (string)dialogue[count]["dialog"];

        ChangeText();
        txt.text = (string)dialogueData[count]["dialog"];
        //StartCoroutine(ShowText(fulltext));
        count++;
    }

    private void SetDialoguePosition()
    {
        txtPlayer = eventDialogueTextPosion.txtPlayer;
        txtNPC = eventDialogueTextPosion.txtNPC;
    }

    //IEnumerator ShowText(string fulltext)
    //{
    //    for (int i = 0; i <= fulltext.Length; i++)    // 글자 하나하나씩 출력
    //    {
    //        string currentText = fulltext.Substring(0, i);
    //        txt.text = currentText;
    //        yield return new WaitForSecondsRealtime(delay);
    //    }
    //}

    public void ChangeText()
    {
        if ((int)dialogueData[count]["name"] == 1) // 메리
        {
            txt.color = Color.black;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtPlayer;
        }
        if ((int)dialogueData[count]["name"] == 2) // 메들록
        {
            txt.color = Color.red;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
        }
        if ((int)dialogueData[count]["name"] == 3) // 마샤
        {
            txt.color = Color.blue;
            dialogueBox.GetComponent<RectTransform>().localPosition = txtNPC;
        }
    }

    void Update()
    {

        if (talking) // 대화중
        {
            //cameraCtrl.dollyZoomIn = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (count < dialogueData.Count)
                {
                    NextDialogue();
                    print(count);
                }
                else
                    HideDialogue();
            }
        }
    }
}
