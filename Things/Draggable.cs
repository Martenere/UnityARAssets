using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{

    Camera camera;
    Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDown() {

        mousePositionOffset = gameObject.transform.position = GetMouseWorldPosition();

        Debug.Log("Dragging " + gameObject);


    }
    void OnMouseDrag() {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
