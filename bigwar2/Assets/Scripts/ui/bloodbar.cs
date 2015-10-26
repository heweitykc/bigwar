using UnityEngine;
using System.Collections;

public class bloodbar : MonoBehaviour {
    public bloodtxtController[] txtanimators;

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void PlayNum(Vector3 v, string num)
    {
        foreach (bloodtxtController anim in txtanimators)
        {
            if (anim.gameObject.activeSelf == false) {
                anim.gameObject.SetActive(true);
                anim.transform.position = v;
                anim.StartAnim(num);
                return;
            }                
        }        
    }
}
