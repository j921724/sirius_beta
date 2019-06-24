using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private GameObject npc;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        npc = transform.root.gameObject;
        if (FindObjectOfType<Controll>() != null)
        {
            target = FindObjectOfType<Controll>().gameObject;
        }
        print(target);
        print(npc);
    }


    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) < 4.0f)
            {
                gameObject.SetActive(true);
            }
        }
    }

    private void OnMouseDown()
    {
        npc.GetComponent<DialogueSystem>().ShowDialogue();
        print("dd");
        gameObject.SetActive(false);
    }
    
}
