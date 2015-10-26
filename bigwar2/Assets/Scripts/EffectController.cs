using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour {
    public float elapseTime;
	void Start () {
        Invoke("clearit", elapseTime);
	}
	
	
	void Update () {
	
	}

    void clearit()
    {
        Destroy(this.gameObject);
    }
}
