using UnityEngine;
using System.Collections;

public class SceneInit : MonoBehaviour {
	
	void Awake () {
		EventsMgr.GetInstance().Init();
		AppGlobal.embattleMgr = 
			GameObject.Find("ScriptObject").GetComponent<EmbattleManager>();
		AppGlobal.battleMgr = 
			GameObject.Find("ScriptObject").GetComponent<BattleManager>();
		AppGlobal.battleAnim = 
			GameObject.Find("ScriptObject").GetComponent<BattleAnimations>();
		AppGlobal.itemMgr = 
			GameObject.Find("ScriptObject").GetComponent<ItemManager>();
		AppGlobal.camera = 
			GameObject.Find("Main Camera").transform;
		AppGlobal.battleResult = 
			GameObject.Find("BattleResult").GetComponent<BattleResult>();
		
		AppGlobal.playRound = 
			GameObject.Find("PlayerRound").GetComponent<PlayerRound>();
		AppGlobal.phase = 0;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
