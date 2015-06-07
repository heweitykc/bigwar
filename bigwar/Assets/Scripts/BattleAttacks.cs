using UnityEngine;
using System;
using System.Collections.Generic;

public class BattleAttacks : MonoBehaviour {
    public GameObject explosionPerfab;

    public float speed = 5f;
    public Action endCallback;

    List<BattleItem> _list;
    bool _inAnim = false;

    BattleItem _attcker;
    BattleItem _suffer;
    int index = 0;

	void Start () {
        _inAnim = false;
	}
		
	void Update () {
        if (_list == null) return;
        if (_list.Count <= 0) return;

	}

    public void StartAnim(List<BattleItem> list)
    {
        _list = list;
        _inAnim = false;
        index = 0;
    }
}
