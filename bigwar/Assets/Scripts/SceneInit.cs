using UnityEngine;
using System.Collections;

public class SceneInit : MonoBehaviour {
	
	void Awake () {
		EventsMgr.GetInstance().Init();
		AppGlobal.battleMgr = 
			GameObject.Find("ScriptObject").GetComponent<EmbattleManager>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
