using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bloodtxtController : MonoBehaviour {
    public float elapseTime;
    Text _numtxt = null;

	void Update () {
	
	}

    public void StartAnim(string num)
    {
        if (_numtxt == null)
            _numtxt = transform.Find("txt").GetComponent<Text>();
        _numtxt.text = num;
        _numtxt.GetComponent<Animator>().Play("blood", 0, 0);        
        Invoke("clearit", elapseTime);
    }

    void clearit()
    {
        this.gameObject.SetActive(false);
    }
}
