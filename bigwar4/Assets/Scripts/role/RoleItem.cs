using UnityEngine;
using System.Collections;

public class RoleItem : MonoBehaviour {
    public TextMesh nametxt;
    public PlayerModel data;

    public TextMesh hptxt;
	void Start () {
        nametxt.transform.localPosition = new Vector3(0,25f,0);
        nametxt.transform.localScale = new Vector3(5f,5f,5f);
	}
	
	void Update () {        
        nametxt.transform.rotation = Camera.main.transform.rotation;
        if(data.rtype == RoleType.MengJiang)
            hptxt.text = data.currentHp.ToString() + " 怒" + data.currentNu;
        else
            hptxt.text = data.currentHp.ToString();
	}
}
