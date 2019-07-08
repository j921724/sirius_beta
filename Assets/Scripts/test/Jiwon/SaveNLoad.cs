using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        // 직렬화하기 위해 vector3 사용 안함
        public float palyerX;
        public float palyerY;
        public float palyerZ;

        public List<int> playerItemInventory;   // item의 id값만 넣어줌
        public List<int> playerItemInventoryCount;  // 각 item 개수

        public string sceneName;

        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<string> varNumberList;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
