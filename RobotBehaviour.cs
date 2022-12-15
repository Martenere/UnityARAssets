using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class RobotBehaviour: MonoBehaviour {
    private ARRaycastManager rays;
    public GameObject robotPrefab;
    public Camera myCamera;
    public float cooldown, cooldownCount;
    private ARAnchorManager anc;
    private ARPlaneManager plan;
    private static ILogger logger = Debug.unityLogger;
    private ARPlane currentPlane = null;
    private GameObject spawnedItem;

    bool hit;
    Vector3 screenCenter;
    ARRaycastHit nearest;
    Transform spawnCoords;
    ARAnchor point;


    void Start() {
        cooldown = 2;
        myCamera = 
        this.gameObject.transform.Find
                ("AR Camera").gameObject.GetComponent<Camera>();
        rays = this.gameObject.GetComponent<ARRaycastManager>();
        anc = this.gameObject.GetComponent<ARAnchorManager>();
        plan = this.gameObject.GetComponent<ARPlaneManager>();
        spawn(robotPrefab);
    }
   void Update(){

        plan.planesChanged += OnPlanesChanged;

        List<ARRaycastHit> myHits = new List <ARRaycastHit>();
        hit = rays.Raycast(screenCenter,
            myHits);
        nearest = myHits[0];
        currentPlane = plan.GetPlane(nearest.trackableId);
        logger.Log(currentPlane.center);
        
        updateItem();
        
        cooldownCount += Time.deltaTime;
        
        if (Input.touchCount == 1) {
            doFusRoDah();
        }
        else if (cooldownCount > cooldown && Input.touchCount == 2) {
            cooldownCount = 0;
            doSpawnRobot();
        }
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs planeEvent){
        
        if (planeEvent.added.Count > 0){
            if ( currentPlane == null){
                currentPlane = planeEvent.added[0]; 
                foreach(ARPlane plane in planeEvent.added){
                    if (plane != currentPlane){
                        Destroy(plane);
                        }
                }
            }else{
                foreach(ARPlane plane in planeEvent.added){
                    Destroy(plane);

            }
            
        }
        }
        
        plan.planesChanged -= OnPlanesChanged;

    }
    public void spawn(GameObject item){ 
    
        
        spawnedItem = Instantiate(item,new Vector3(-0.1816921f,0f, 4.5f), Quaternion.identity );
        
        }

    public void updateItem(){
        spawnedItem.transform.position = currentPlane.center;
        
    }


    public void doSpawnRobot() {
        GameObject robot;
        Vector3 screenCenter;
        bool hit;
        ARRaycastHit nearest;
        List<ARRaycastHit> myHits = new List <ARRaycastHit>();
        ARPlane plane;
        ARAnchor point;
        screenCenter = myCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        hit = rays.Raycast(screenCenter,
            myHits,
            TrackableType.FeaturePoint | TrackableType.PlaneWithinPolygon);
        // logger.Log("Hit: " + hit);
       
        // if (hit == true) {
        //     nearest = myHits[0];
        //     robot = Instantiate(robotPrefab, nearest.pose.position 
        //         + nearest.pose.up * 0.1f, nearest.pose.rotation);
        //     robot.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //     robot.tag = "SpawnedObject";
        //     logger.Log("spawned at " + robot.transform.position.x + ", " 
        //        + robot.transform.position.y + ", " + robot.transform.position.z);
        //     plane = plan.GetPlane(nearest.trackableId);
        //     if (plane != null) {
        //         point = anc.AttachAnchor(plane, nearest.pose);
        //         logger.Log("Added an anchor to a plane " + nearest);
        //     } else {
        //         point = anc.AddAnchor(nearest.pose);
        //         logger.Log("Added another anchor " + nearest);
        //     }
        //     robot.transform.parent = point.transform;
        // }
    }
    
    public void doFusRoDah() {
        RaycastHit[] myHits;
        Ray r;

        r = myCamera.ScreenPointToRay(Input.GetTouch(0).position);

        myHits = Physics.RaycastAll (r);

        foreach (RaycastHit hit in myHits) {
            logger.Log ("Detected " + hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "LEG") {
                logger.Log ("Applying force");
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce 
                         (r.direction * 100);
                }

            }
        }


}