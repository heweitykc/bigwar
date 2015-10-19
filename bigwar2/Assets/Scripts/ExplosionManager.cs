using UnityEngine;
using System;
using System.Collections;

//爆炸效果管理
public class ExplosionManager : MonoBehaviour {
    public GameObject explosionPerfab;
    public bloodbar bloodBar;

    Action _callback;
    GameObject _obj;

	void Start () {
	
	}
		
	void Update () {
	
	}

    public void startExplosion(Action callback,Vector3 pos)
    {
        _callback = callback;
        _obj = Instantiate(explosionPerfab) as GameObject;
        _obj.transform.position = pos;
        _obj.transform.rotation = Quaternion.identity;
        Vector3 sp = Camera.main.WorldToScreenPoint(pos);
        bloodBar.PlayNum(sp);
        Invoke("aftercall", 0.8f);
    }

    void aftercall()
    {
        _callback();
        _callback = null;
        GameObject.Destroy(_obj);        
    }
}
