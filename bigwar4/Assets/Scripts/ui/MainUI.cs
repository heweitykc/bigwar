using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {
	public Transform startBtn;
	
	void Start () {
		startBtn.gameObject.SetActive(false);
		Invoke("showBtn",2f);
	}
	
	void showBtn()
	{
		startBtn.gameObject.SetActive(true);
	}
	
	void Update () {
	
	}
	
	public void startBattle()
	{
//		AppGlobal.camera.GetComponent<Animator>().Play("battle");
		startBtn.gameObject.SetActive(false);
        AppGlobal.phase = GamePhase.BATTLE;		
		AppGlobal.playRound.hide();
		AppGlobal.itemMgr.clearSelected();
		Invoke("_startBattle",1f);
	}
	
	void _startBattle()
	{
		AppGlobal.battleMgr.startBattle();
	}
	
	public void startOperate()
	{
//		AppGlobal.camera.GetComponent<Animator>().Play("buzhen");		
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
}
