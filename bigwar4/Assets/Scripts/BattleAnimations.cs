using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleAnimations : MonoBehaviour {
	public float speed = 5f;
	public Action endCallback;

    List<AttackRound> _list;
	bool _inAnim = false;
	
	BattleItem _attcker;
	List<BattleItem> _suffers;
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

            //直播某方死完了
            if (BattleComputer.isFinish())
            {
                _list = null;
                return;
            }

			_inAnim = true;
			_attcker = _list[index].attacker;            
			index++;

            if (_attcker._soldierId == -1) //攻击者死了
            {
                afterExplosion();
                _inAnim = false;
                return;
            }

            _suffers = AppGlobal.battleMgr.searchEnemy(_attcker);
            if (_suffers == null) {
                afterExplosion();
                _inAnim = false;
                return;
            }

			inReturning = false;
            inEffects = false;
			_returnPosition = _attcker.transform.position;
            Vector3 vf = _suffers[0].transform.position - _attcker.transform.position;
            _targetPosition = _attcker.transform.position + vf.normalized * 4;
            print("开始移动动画");
			return;
		}
        if (_attcker._soldierId == -1)
        {
            afterExplosion();
            print("攻击者没了");
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
            foreach (BattleItem suffer in _suffers) {
                if(suffer._soldierId == -1) continue;
                string retstr;
                int ret = BattleComputer.compute(_attcker.data, suffer.data, out retstr);
                suffer.data.currentHp -= ret;
                handleSuffer(suffer);
                GetComponent<ExplosionManager>().startExplosion(suffer.transform.position, retstr + " " +ret.ToString());

            }
            if (_attcker.data.rtype == RoleType.MengJiang) { 
                if (_attcker.data.currentNu >= 3)
                    _attcker.data.currentNu = 0;
                else
                    _attcker.data.currentNu++;
            }
            print("播放爆炸");
            Invoke("afterExplosion", 0.8f);
			return;
		}
        //print("移动。。。。");
        _attcker.itemPos += v;
	}

    void handleSuffer(BattleItem suffer)
    {
        print("当前受着血量：" + suffer.data.currentHp);
        if (suffer.data.currentHp <= 0)
            suffer.clear();
    }

    void afterExplosion()
    {
        inReturning = true;
        inEffects = false;        
    }

    public void StartAnim(List<AttackRound> list)
	{
		_list = list;
		_inAnim = false;
		index = 0;
	}
}
