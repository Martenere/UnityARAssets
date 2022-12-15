using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour {
    private string tag;
    int ID;
    public GameObject cube_merge;

    // Start is called before the first frame update
    void Start() {
        tag = "Tag";
        ID = GetInstanceID();
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Tag")) {
            

                //Merge
                if(ID < col.gameObject.GetComponent<Merge>().ID) { return; }
                Debug.Log("Merge " + gameObject.name);
                Debug.Log("position: " +  gameObject.transform.position);
                float x = gameObject.transform.position.x;
                float y = gameObject.transform.position.y;
                float z = gameObject.transform.position.z;
                Destroy(col.gameObject);
                Destroy(gameObject);

                GameObject obj = Instantiate(cube_merge, transform.position = new Vector3(x, y, z), Quaternion.identity);
            

        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
