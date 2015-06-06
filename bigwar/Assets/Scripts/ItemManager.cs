using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//战斗单元管理
public class ItemManager : MonoBehaviour {
	public BattleItem selectedItem;
	
	void Start () {
	
	}
	
	public void clearSelected()
	{
		if(selectedItem == null) return;
		selectedItem.selected = false;
		selectedItem = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		
		if (Input.touchCount != 1 )
			return;

		if (Input.GetTouch(0).phase == TouchPhase.Began){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, AppGlobal.TouchLayer)){
				Debug.Log("name: " + hit.transform.name);
				BattleItem item = hit.transform.GetComponent<BattleItem>();
				if(selectedItem != null){
					selectedItem.selected = false;
				}
				item.selected = true;
				selectedItem = item;
				EventsMgr.GetInstance().TrigerEvent(eEventsKey.item_choosed, null);
			}
		}
	}
}
