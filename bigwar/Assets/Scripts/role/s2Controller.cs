using UnityEngine;
using System.Collections;

public class s2Controller : MonoBehaviour {
	public Transform wq;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		wq.Translate(Vector3.forward * Time.deltaTime * Mathf.Cos(Time.time));
	}
}
