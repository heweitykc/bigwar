using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

//一个攻击回合数据
sealed public class AttackRound
{
    public BattleItem attacker;
    public List<BattleItem> suffers = new List<BattleItem>();     
}

//战斗管理
public class BattleManager : MonoBehaviour {
	public BattleItem[] team0;
	public BattleItem[] team1;
    public List<AttackRound> rounds = new List<AttackRound>();
	
	bool inbattle = false;
	bool inoperate = false;
	int battleIndex;

	int battleRound;
	BattleItem _selectedItem;
	
	void Start () {
		EventsMgr.GetInstance().AttachEvent(eEventsKey.item_choosed, onItemChoosed);
		battleRound = -1;
		for(int i=0;i<9;i++){
			team0[i].index = i+1;
		}
		for(int i=0;i<9;i++){
			team1[i].index = i+1;
		}
	}
	
	void onItemChoosed(object data){
        Debug.Log("onItemChoosed");
		if(AppGlobal.phase != GamePhase.OPERATE) return;

		if(AppGlobal.itemMgr.selectedItem._soldierId < 0 && _selectedItem == null){
			AppGlobal.itemMgr.clearSelected();
			print("请先选中一个武将再移动");
			return; 
		}
		
		if(_selectedItem == null){
			_selectedItem = AppGlobal.itemMgr.selectedItem;
		} else {
			if(AppGlobal.itemMgr.selectedItem._soldierId >= 0){
				print("不能移动到这里");
			} else {
				AppGlobal.itemMgr.selectedItem.setSoldier(_selectedItem._soldierId);
				AppGlobal.itemMgr.selectedItem._HP = _selectedItem._HP;
                _selectedItem.clear();
				_selectedItem = null;
				AppGlobal.itemMgr.clearSelected();
			}
		}
	}
	
	void Update () {
		
	}
	
	//自动战斗
	void doBattle()
	{
		rounds.Clear();
		int r = battleRound % 2;
		BattleItem[] items = (r == 0 ? team0 : team1);
		
		for(int i=0;i<9;i++){
			BattleItem item = items[i];
			if(item._soldierId < 0) continue;
			List<BattleItem> enemys = searchEnemy(item);
            if (enemys == null) continue;
            AttackRound round = new AttackRound();
            round.attacker = item;
            round.suffers = enemys;
            rounds.Add(round);
		}
		
		items = (items == team1 ? team0 : team1);
		for(int i=0;i<9;i++){
			BattleItem item = items[i];
			if(item._soldierId < 0) continue;
            List<BattleItem> enemys = searchEnemy(item);
			if(enemys == null) continue;
            AttackRound round = new AttackRound();
            round.attacker = item;
            round.suffers = enemys;
            rounds.Add(round);
		}       

        AppGlobal.battleAnim.endCallback = animEnd;
		AppGlobal.battleAnim.StartAnim(rounds);
	}
	
	//回合动画播放结束
	void animEnd()
	{
		print("anim end!!!");
		doOperate();
	}
	
	//操作武将
	void doOperate()
	{
        if (BattleComputer.isFinish())
        {
            return;
        }
        AppGlobal.phase = GamePhase.OPERATE; //操作模式
		AppGlobal.itemMgr.clearSelected();
		AppGlobal.playRound.show();
		_selectedItem = null;
	}
	
	//开始战斗
	public void startBattle()
	{
        if (BattleComputer.isFinish())
        {
            return;
        }
        AppGlobal.phase = GamePhase.BATTLE; //战斗模式
		inbattle = true;
		battleIndex = 0;
		battleRound++;
		doBattle();
	}
	
	//搜寻目标
	public List<BattleItem> searchEnemy(BattleItem item)
	{
        BattleItem target = searchSingle(item);
        BattleItem[] items;
        if (Array.IndexOf(team0, item) >= 0)
            items = team1;
        else
            items = team0;
        if (item.data.type == AttackType.single)
            return new List<BattleItem>() { target };
        else if (item.data.type == AttackType.vertical)
            return searchVertical(target, items);
        else if (item.data.type == AttackType.horizon)
            return searchHorizon(target, items);
        else if (item.data.type == AttackType.bias)
            return searchBias(target, items);
		return null;
	}

    //单攻搜索
    BattleItem searchSingle(BattleItem item)
    {        
        int i = Array.IndexOf(team0, item);
        BattleItem[] items = (i >= 0 ? team1 : team0);
        int[] ss = AppGlobal.S2;
        if (i == 1 || i == 4 || i == 7)
        {
            ss = AppGlobal.S0;
        }
        else if (i == 2 || i == 5 || i == 8)
        {
            ss = AppGlobal.S1;
        }
        else if (i == 3 || i == 6 || i == 9)
        {
            ss = AppGlobal.S2;
        }

        foreach (int s in ss)
        {
            BattleItem itm = items[s - 1];
            if (itm._soldierId >= 0)
            {
                return itm;
            }
        }
        return null;
    }

    List<BattleItem> searchHorizon(BattleItem item, BattleItem[] items)
    {
        int i = Array.IndexOf(items, item);
        i /= 3;
        List<BattleItem> rets = new List<BattleItem>();
        for (int j = 0; j < 3; j++) {
            BattleItem itm = items[j+i*3];
            if (itm._soldierId>=0)
                rets.Add(itm);
        }
        if (rets.Count > 0) return rets;
        return null;
    }

    List<BattleItem> searchVertical(BattleItem item, BattleItem[] items)
    {
        int i = Array.IndexOf(items, item);
        i %= 3;
        Debug.Log(i);
        List<BattleItem> rets = new List<BattleItem>();
        for (int j = 0; j < 3; j++)
        {
            BattleItem itm = items[j * 3 + i];
            if (itm._soldierId >= 0)
                rets.Add(itm);
        }
        if (rets.Count > 0) return rets;        
        return null;
    }

    List<BattleItem> searchBias(BattleItem item, BattleItem[] items)
    {
        int i = Array.IndexOf(items, item);
        int[] ids = AppGlobal.SS[i];
        List<BattleItem> rets = new List<BattleItem>();
        foreach (int id in ids) {
            BattleItem itm = items[id-1];
            if (itm._soldierId >= 0)
                rets.Add(itm);
        }
        if (rets.Count <= 0) return null;
        return rets;
    }
}
