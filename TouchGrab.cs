using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class TouchGrab : MonoBehaviour
{
    ARRaycastManager rays;
    public Camera myCamera;
    Vector3 screenCenter;
    Transform toDrag;
    bool dragging;
    float dist;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = 
        this.gameObject.transform.Find
                ("AR Camera").gameObject.GetComponent<Camera>();
        rays = this.gameObject.GetComponent<ARRaycastManager>();        
    }

    // Update is called once per frame
    void Update()
    {
     Vector3 v3;
    

         if (Input.touchCount != 1) {
             dragging = false; 
             return;
             Debug.Log("not grabbing");
         }

         Touch touch = Input.touches[0];
         Vector3 pos = touch.position;

         if(touch.phase == TouchPhase.Began) {
                             Debug.Log ("Here");

             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(pos); 
            List<ARRaycastHit> myHits = new List <ARRaycastHit>();

             if(rays.Raycast(screenCenter, myHits))
             {

                Debug.Log("taaaaaaaaaaaaaaaaaaaaaaaaag " + myHits[0]);
                // toDrag = hit.transform;
                // dist = hit.transform.position.z - Camera.main.transform.position.z;
                 v3 = new Vector3(pos.x, pos.y, dist);
                 v3 = Camera.main.ScreenToWorldPoint(v3);
                 offset = toDrag.position - v3;
                 dragging = true;
             }
         }
         if (dragging && touch.phase == TouchPhase.Moved) {
             v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
             v3 = Camera.main.ScreenToWorldPoint(v3);
             toDrag.position = v3 + offset;
         }
         if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
             dragging = false;
         }   
    }
}
