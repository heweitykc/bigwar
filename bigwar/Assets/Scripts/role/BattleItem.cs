using UnityEngine;
using System.Collections;

//战斗单位
public class BattleItem : MonoBehaviour {
	public int _HP = 100;
	public int _soldierId;
	public int index;
	
	Transform _soldier;	
	
	void Start () {
		selected = false;
		_soldierId = -1;
	}
	
	void Update () {
	
	}
	
	bool _selected;
	public bool selected
	{
		get{return _selected;}
		set {
			_selected = value;
			/*
			GetComponent<MeshRenderer>().material.color = 
				value ? Color.green : Color.white;
				*/
		}
	}
	
	public void setSoldier(int i)
	{
		_soldierId = i;
		
		if(_soldierId == -1){
			GameObject.Destroy(_soldier.gameObject);
			_soldier = null;
			return;
		}
		
		Transform perfab = AppGlobal.embattleMgr.soldierPerfabs[i%3];
		_soldier = GameObject.Instantiate(perfab) as Transform;
		_soldier.parent = transform;
		_soldier.localPosition = Vector3.zero;
	}
	
	//扣血
	public void addHP(int hp)
	{
		_HP += hp;
		if(_HP <= 0){			
			GameObject.Destroy(_soldier.gameObject);
			_soldier = null;
			_soldierId = -1;
		}
	}
}
