using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleResult : MonoBehaviour {
	public Text resultTxt;	
	
	void Start () {
		gameObject.SetActive(false);
	}
	
	void Update () {
	
	}
	
	public void restart()
	{
		Application.LoadLevel(0);		
	}
	
	public void showResult(bool r)
	{
        print("showResult");
		gameObject.SetActive(true);
		resultTxt.text = r ? "战斗胜利" : "战斗失败";
	}
}
