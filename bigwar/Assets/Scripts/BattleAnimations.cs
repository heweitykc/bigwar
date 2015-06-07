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
    bool inEffects = false;
	
	void Start () {
		_inAnim = false;
	}
	
	void Update () {
		if(_list == null) return;
		if(_list.Count <= 0) return;
        if (inEffects) return; //特效播放中
		if(!_inAnim){
			if(index >= _list.Count){  //播放完成
				_list = null;
				endCallback();
                print("endCallback");
				return;
			}
			
			_inAnim = true;
			_attcker = _list[index];
			_suffer = _list[index+1];
			index += 2;
			inReturning = false;
            inEffects = false;
			_returnPosition = _attcker.transform.position;
            Vector3 vf = _suffer.transform.position - _attcker.transform.position;
            _targetPosition = _attcker.transform.position + vf.normalized * 4;
            print("开始移动动画");
			return;
		}

        Vector3 forward = _targetPosition - _attcker.itemPos;
		Vector3 v = forward.normalized * speed * Time.deltaTime;			
		//print("v="+v);
		
		if(forward.magnitude <= v.magnitude){
			if(inReturning){
				_inAnim = false;				
                _attcker.itemPos = _returnPosition;
                print("返回移动完成");
				return;
			}
			_targetPosition = _returnPosition;
            inEffects = true;
            GetComponent<ExplosionManager>().startExplosion(afterExplosion, _suffer.transform.position);
            print("播放爆炸");
			return;
		}
        print("移动。。。。");
        _attcker.itemPos += v;
	}

    void afterExplosion()
    {
        inReturning = true;
        inEffects = false;
        //_suffer.addHP(-50);
    }
	
	public void StartAnim(List<BattleItem> list)
	{
		_list = list;
		_inAnim = false;
		index = 0;
	}
}
