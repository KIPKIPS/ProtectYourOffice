using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public int index;

    void Awake() {
        index = 0;
    }
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            print(index);
            GameObject go = (GameObject)Resources.Load("Test/Room1"); 
            go.transform.position = new Vector3(0.95f * index, 0, 0);
            
            Instantiate(go);
            index++;
        }
    }
}
