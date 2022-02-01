using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace : MonoBehaviour
{
    public GameObject gObj;
    private GameObject spwanedObj;
    private ARRaycastManager aCM;
    private Vector2 touchPos;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

  private void Awake()
    {
        aCM = GetComponent<ARRaycastManager>();
    }
    bool isTouch(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isTouch(out Vector2 touchPos))
        {
            return;
        }
        if (aCM.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (spwanedObj == null)
            {
                spwanedObj = Instantiate(gObj, hitPose.position, hitPose.rotation);
            }
            else
            {
                spwanedObj.transform.position = hitPose.position;
            }
        }
    }
}
