using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    public Choice choice;
    private ChoiceManager theChoice;
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        theChoice = FindObjectOfType<ChoiceManager>();
    }

    private void OnMouseDown()
    {
        if (!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        // 캐릭터움직임 멈춤
        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => theChoice.choiceIng);
        // 캐릭터 움직이게 함
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
