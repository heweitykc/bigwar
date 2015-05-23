using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//布阵
public class EmbattleManager : MonoBehaviour {
	public ChooseView chooseview;
	public Transform[] soldierPerfabs;
	
	const int TouchLayer = 1 << 9;
	BattleItem selectedItem;
	
	void Awake()
	{
		EventsMgr.GetInstance().AttachEvent(eEventsKey.choosed, onChoosed);
	}
	
	void onChoosed(object data)
	{
		print("choose " + (int)data);
		if(selectedItem == null) return;
		selectedItem.setSoldier((int)data);
	}
	
	void Update () {
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		
		if (Input.touchCount != 1 )
			return;

		if (Input.GetTouch(0).phase == TouchPhase.Began){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, TouchLayer)){
				Debug.Log("name: " + hit.transform.name);
				BattleItem item = hit.transform.GetComponent<BattleItem>();
				if(selectedItem != null){
					selectedItem.selected = false;
				}
				item.selected = true;
				selectedItem = item;
				chooseview.show();
				//Debug.Log(hit.transform.tag);
			}
		}
	}
}
