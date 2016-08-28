using UnityEngine;
using System.Collections;

public class TankMovementTracker : MonoBehaviour {

    //public float maxDistance = 50f;
    public float totalDistance = 0f;
    Vector3 lastPosition;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        totalDistance += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
    }
}
