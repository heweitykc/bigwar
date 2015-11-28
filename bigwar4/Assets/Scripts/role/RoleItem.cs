using UnityEngine;
using System.Collections;

public class RoleItem : MonoBehaviour {
    public TextMesh nametxt;
    public PlayerModel data;

    public TextMesh hptxt;
	void Start () {
	
	}
	
	void Update () {        
        nametxt.transform.rotation = Camera.main.transform.rotation;
        if(data.rtype == RoleType.MengJiang)
            hptxt.text = data.currentHp.ToString() + " 怒" + data.currentNu;
        else
            hptxt.text = data.currentHp.ToString();
	}
}
