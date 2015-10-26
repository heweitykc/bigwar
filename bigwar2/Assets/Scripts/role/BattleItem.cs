using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

//战斗单位
public class BattleItem : MonoBehaviour {
	public int _HP = 100;
	public int _soldierId;
	public int index;
    public PlayerModel data;
	
    RoleItem _soldier;
	
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
        if (_soldierId != -1) return;   //已经有兵了

		_soldierId = i;
		
		if(_soldierId == -1){
			GameObject.Destroy(_soldier.gameObject);
			_soldier = null;
			return;
		}
        data = AppGlobal.cfg.getModel(_soldierId);
		Transform perfab = AppGlobal.embattleMgr.soldierPerfabs[i%3];
		_soldier = 
            (GameObject.Instantiate(perfab) as Transform).GetComponent<RoleItem>();
        _soldier.data = data;
		_soldier.transform.parent = transform;
		_soldier.transform.localPosition = Vector3.zero;
        _soldier.nametxt.text = data.pname;        
	}

    public void clear()
    {
        if (_soldier == null) return;
        _soldier.gameObject.SetActive(false);
        GameObject.Destroy(_soldier.gameObject);
        _soldier = null;
        _soldierId = -1;
    }

    public Vector3 itemPos
    {
        get{ return _soldier.transform.position; }
        set { _soldier.transform.position = value; }         
    }

    void OnMouseDown()
    {                
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (AppGlobal.phase == GamePhase.LINEUP) {
            lineUpMouseDown();
        }
        else if (AppGlobal.phase == GamePhase.OPERATE)
        {
            operateMouseDown();
        }
    }

    void OnMouseUp()
    {       
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("OnMouseUp : " + index);
        if (AppGlobal.phase == GamePhase.OPERATE)
            operateMouseUp();
    }

    void lineUpMouseDown()
    {
        if (_soldierId != -1) return;
        Debug.Log("OnMouseDown : " + index);
        selectMe();
        EventsMgr.GetInstance().TrigerEvent(eEventsKey.item_choosed, null);
    }

    void selectMe()
    {
        if (AppGlobal.itemMgr.selectedItem == this)
            return;
        if (AppGlobal.itemMgr.selectedItem != null)
            AppGlobal.itemMgr.selectedItem.selected = false;
        AppGlobal.itemMgr.selectedItem = this;
        selected = true;
    }

    void operateMouseDown()
    {        
        selectMe();
        EventsMgr.GetInstance().TrigerEvent(eEventsKey.item_choosed, null);
    }

    void operateMouseUp()
    {

    }
}
