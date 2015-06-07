using UnityEngine;
using System.Collections;

public class bloodbar : MonoBehaviour {
    public Animator txtanimator;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void PlayNum(Vector3 v)
    {
        gameObject.SetActive(true);
        transform.position = v;
        txtanimator.Play("blood",0,0);
        Invoke("playEnd", 0.8f);
    }

    void playEnd()
    {
        gameObject.SetActive(false);
    }
}
