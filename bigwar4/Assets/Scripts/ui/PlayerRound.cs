using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerRound : MonoBehaviour {
	public Text msgtxt;
	
	void Start () {
		gameObject.SetActive(false);
	}
		
	void Update () {
	
	}
	
	public void show()
	{
		gameObject.SetActive(true);
		msgtxt.GetComponent<Animator>().Play("fly",0,0);		
	}
	
	public void hide()
	{
		gameObject.SetActive(false);
	}
}
