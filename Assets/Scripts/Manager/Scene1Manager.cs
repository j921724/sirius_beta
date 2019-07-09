using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public bool showDialogue = false;
    public EventDialogueSystem eventDialogueSystem;
    private Scene scene;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventDialogueSystem = GetComponent<EventDialogueSystem>();
        scene = GetComponent<Scene>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!showDialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                showDialogue = true;
                eventDialogueSystem.ShowDialogue();
            }
        }

        if (eventDialogueSystem.count == eventDialogueSystem.dialogueData.Count && !eventDialogueSystem.talking)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                // count를 넣는 이유는 대화 종료 후, 한 번 더 클릭하면 다음 씬으로 갈 수 있게 임시방편
                count++;
                if (count == 2)
                    scene.SceneChange(scene.sceneName);
            }
        }
    }
}
