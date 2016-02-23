using UnityEngine;
using System.Collections;

public class TestPlayer : MonoBehaviour {

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(0, 0, speed * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
