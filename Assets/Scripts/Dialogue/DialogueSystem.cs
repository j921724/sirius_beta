using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string txtf; // 스크립트 파일 이름
    public Image dialogueBox;
    public Text txt; // 텍스트 오브젝트
    public Image panel; // 대화 중 다른 기능 금지
    public Image skipButton; // 스킵 버튼
    public GameObject dialogueButton;  // 대화 버튼
    //[SerializeField] private float delay = 0.01f;
    [SerializeField] private float error = 8.0f;

    private GameObject player; // 플레이어 오브젝트 
    private int count = 0; // 텍스트 문서 단위
    private bool talking = false; // 텍스트 UI 활성화 트리거

    Vector3 txtPlayer; // 플레이어 쪽 텍스트 위치
    Vector3 txtNPC; // NPC 쪽 텍스트 위치

    List<Dictionary<string, object>> dialogue;

    void Start()
    {
        if(GameObject.FindWithTag("Mary") != null)
        {
            player = GameObject.FindWithTag("Mary").gameObject;
        }
        dialogue = CSVReader.Read(txtf);
        dialogueBox = txt.transform.parent.GetComponent<Image>();
    }

    private void OnOff(bool _flag)
    {
        panel.gameObject.SetActive(_flag);
        skipButton.gameObject.SetActive(_flag);
        dialogueBox.gameObject.SetActive(_flag);
        txt.gameObject.SetActive(_flag);
        dialogueButton.SetActive(_flag);
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
        txt.text = (string)dialogue[count]["dialog"];
        //StartCoroutine(ShowText(fulltext));
        count++;
    }

    private void SetDialoguePosition()
    {
        Vector3 playerPos = player.transform.position; // Player 위치
        Vector3 npcPos = transform.position; // NPC 위치

        if (playerPos.x - npcPos.x < 0) // 플레이어가 왼쪽에 있을 시
        {
            txtPlayer = new Vector3(playerPos.x - 3f, playerPos.y + 10.0f);
            txtNPC = new Vector3(npcPos.x + 3f, playerPos.y + 10.0f);
        }
        else
        {
            txtPlayer = new Vector3(playerPos.x + 3f, playerPos.y + 10.0f);
            txtNPC = new Vector3(npcPos.x - 3f, playerPos.y + 10.0f);
        }
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

   private void ChangeText()
    {
        if ((int)dialogue[count]["name"] == 1) // 메리
        {
            txt.color = Color.red;
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector3(150.0f,-20.0f,0.0f);
        }
        if ((int)dialogue[count]["name"] == 2) // 메들록
        {
            txt.color = Color.black;
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector3(-150.0f, -20.0f, 0.0f);
            print(dialogueBox.GetComponent<RectTransform>().localPosition);
        }
    }

   public void ShowButton()
    {
        Vector3 playerPos = player.transform.position; // Player 위치
        Vector3 npcPos = transform.position; // NPC 위치

        if (Mathf.Abs(playerPos.x - npcPos.x) < error)
        {
            dialogueButton.SetActive(true);
        }
        else
        {
            dialogueButton.SetActive(false);
        }
    }


   void Update()
    {
        
        if (talking) // 대화중
        {
            dialogueButton.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                if (count < dialogue.Count)
                {
                    NextDialogue();
                    print(count);
                }
                else
                    HideDialogue();
            }
        }
        else
        {
            ShowButton();
        }
    }
}
