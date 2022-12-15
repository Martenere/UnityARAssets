using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge2 : MonoBehaviour {
    private string tag;
    int ID;

    // Start is called before the first frame update
    void Start() {
        ID = GetInstanceID();
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("mergeJoint")) {
            

                //Merge
                if(ID < col.gameObject.GetComponent<Merge2>().ID) { return; }
                Debug.Log("Merge " + gameObject.name);
                Debug.Log("position: " +  gameObject.transform.position);
                gameObject.AddComponent<FixedJoint>();
                gameObject.GetComponent<FixedJoint>().connectedBody = col.rigidbody;
                gameObject.tag = "merged";
                col.gameObject.tag = "merged";


        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
