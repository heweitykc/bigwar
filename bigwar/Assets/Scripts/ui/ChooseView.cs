using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

//选择武将界面
public class ChooseView : MonoBehaviour {
	
	void Start () {
		gameObject.SetActive(false);
	}
	
	void Update () {
		
	}
	
	public void choose(int i){		
		EventsMgr.GetInstance().TrigerEvent(eEventsKey.choosed, i);
		gameObject.SetActive(false);
	}
	
	public void show()
	{
		gameObject.SetActive(true);
	}
}
