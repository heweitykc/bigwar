using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleAnimations : MonoBehaviour {
	public float speed = 5f;
	public Action endCallback;
	
	List<BattleItem> _list;
	bool _inAnim = false;
	
	BattleItem _attcker;
	BattleItem _suffer;
	int index = 0;
	
	Vector3 _targetPosition;
	Vector3 _returnPosition;
	bool inReturning;	//是否返回中
	
	void Start () {
		_inAnim = false;
	}
	
	void Update () {
		if(_list == null) return;
		if(_list.Count <= 0) return;
		if(!_inAnim){
			if(index >= _list.Count){  //播放完成
				_list = null;
				endCallback();
				return;
			}
			
			_inAnim = true;
			_attcker = _list[index];
			_suffer = _list[index+1];
			index += 2;
			inReturning = false;
			_returnPosition = _attcker.transform.position;
			_targetPosition = _suffer.transform.position;
			return;
		}
		
		Vector3 forward = _targetPosition - _attcker.transform.position;
		Vector3 v = forward.normalized * speed * Time.deltaTime;			
		//print("v="+v);
		
		if(forward.magnitude <= v.magnitude){
			if(inReturning){
				_inAnim = false;
				_attcker.transform.position = _returnPosition;				
				return;
			}
			_targetPosition = _returnPosition;
			inReturning = true;
			//_suffer.addHP(-50);
			return;
		}
		
		_attcker.transform.position += v;		
	}
	
	public void StartAnim(List<BattleItem> list)
	{
		_list = list;
		_inAnim = false;
		index = 0;
	}
}
