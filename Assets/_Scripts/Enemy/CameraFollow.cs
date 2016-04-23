using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 1000;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetCampPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCampPosition, smoothing * Time.deltaTime);
	}
}
