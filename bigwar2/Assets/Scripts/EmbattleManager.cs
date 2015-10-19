using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//布阵
public class EmbattleManager : MonoBehaviour {
	public ChooseView chooseview;
	public Transform[] soldierPerfabs;
	
	void Awake()
	{
		EventsMgr.GetInstance().AttachEvent(eEventsKey.ui_choosed, onChoosed);		
		EventsMgr.GetInstance().AttachEvent(eEventsKey.item_choosed, onItemChoosed);
	}
	
	void onItemChoosed(object data)
	{
        if (AppGlobal.phase != GamePhase.LINEUP) return;
		chooseview.show();
	}
	
	void onChoosed(object data)
	{
		print("choose " + (int)data);
		
		AppGlobal.itemMgr.selectedItem.setSoldier((int)data);
	}
}
