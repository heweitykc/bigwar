using UnityEngine;
using System.Collections;

public class BattleItem : MonoBehaviour {
	Transform soldier;
	
	void Start () {
		selected = false;
	}
	
	void Update () {
	
	}
	
	bool _selected;
	public bool selected
	{
		get{return _selected;}
		set {
			_selected = value;
			GetComponent<MeshRenderer>().material.color = 
				value ? Color.green : Color.white;
		}
	}
	
	public void setSoldier(int i)
	{
		Transform perfab = AppGlobal.battleMgr.soldierPerfabs[i%3];
		soldier = GameObject.Instantiate(perfab) as Transform;
		soldier.parent = transform;
		soldier.localPosition = Vector3.zero;
	}
}
