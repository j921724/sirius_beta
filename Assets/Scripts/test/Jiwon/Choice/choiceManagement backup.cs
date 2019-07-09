﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class choiceManagerbackup : MonoBehaviour
{
    public static choiceManagerbackup instance;

    #region Singleton
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
    #endregion Singleton
    private AudioManager theAudio;  //사운드 재생
    private string question;
    private List<string> answerList;

    public GameObject go;   //평소에 비활성화 시킬 목적으로 선언. setActive
    public Text questionText;
    public Text[] answerText;
    public GameObject[] answerPanel;

    public Animator anim;

    public string keySound;
    public string enterSound;

    public bool choiceIng;  // 선택지 활성화된 동안 대기
    private bool keyInput;  // 키처리 활성화-비활성화

    private int count;  // 배열의 크기
    private int result; // 선택한 선택창

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public string clickedObject;   // 선택한 오브젝트(선택지)

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>();
        for (int i = 0; i <= 1; i++)
        {
            answerText[i].text = "";
            answerPanel[i].SetActive(false);
        }
        questionText.text = "";
    }

    public void ShowChoice(Choice _choice)
    {
        choiceIng = true;
        go.SetActive(true);
        result = 0;
        question = _choice.question;
        for (int i = 0; i < _choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answerPanel[i].SetActive(true); //배열의 크기만큼 패널 활성화
            count = i;
        }

        anim.SetBool("Appear", false);
        Selection();
        StartCoroutine(ChoiceCoroutine());

    }

    public int GetResult()
    {
        return result;
    }

    public void ExitChoice()
    {
        questionText.text = "";
        for (int i = 0; i <= count; i++)
        {
            answerText[i].text = "";
            answerPanel[i].SetActive(false);
        }

        anim.SetBool("Appear", true);
        answerList.Clear();
        choiceIng = false;  // 선택 종료
        go.SetActive(false);

    }

    // 아래의 서브 코루틴을 모두 실행
    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer_0());
        if (count >= 1)
        {
            StartCoroutine(TypingAnswer_1());
        }
        yield return new WaitForSeconds(0.5f);

        keyInput = true;
    }

    //동시에 실행시키기 위해 코루틴을 나눔
    IEnumerator TypingQuestion()
    {
        for (int i = 0; i < question.Length; i++)
        {
            questionText.text += question[i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_0()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answerText[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    IEnumerator TypingAnswer_1()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < answerList[1].Length; i++)
        {
            answerText[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false) return; // UI가 아니면 return
        if (Input.GetMouseButtonDown(0))
        {   // 마우스 위치의 오브젝트를 반환
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            //ray.origin = new Vector2(0.0f,0.0f);
            //ray.direction = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (hit.collider.name == "Answer Panal 0")
            {
                Debug.Log("null");
            }
            else
            {
                Debug.Log("not null");
            }

            //if (!hit)
            //{
            //    Debug.Log(ray.origin);
            //    Debug.Log(ray.direction);

            //    Debug.Log("null");
            //}
            //else
            //{
            //    if (hit.collider.gameObject.name == "Answer Panal 0")
            //    {
            //        result = 0;
            //    }
            //    else if (hit.collider.gameObject.name == "Answer Panal 1")
            //    {
            //        result = 1;
            //    }
            //}

            ExitChoice();
        }

        //if (keyInput)   // 키를 입력하는 동안 선택지 변경
        //{
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        theAudio.Play(keySound);
        //        if (result > 0)
        //            result--;
        //        else
        //            result = count;
        //        Selection();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {
        //        theAudio.Play(keySound);
        //        if (result < count)
        //            result++;
        //        else
        //            result = 0;
        //        Selection();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Z))
        //    {
        //        theAudio.Play(enterSound);
        //        keyInput = false;
        //        ExitChoice();
        //    }
        //}

    }

    // 어떤 게 선택 되었는지 색상으로 알려줌
    public void Selection()
    {
        Color color = answerPanel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for (int i = 0; i <= count; i++)
        {
            answerPanel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answerPanel[result].GetComponent<Image>().color = color;
    }
}
