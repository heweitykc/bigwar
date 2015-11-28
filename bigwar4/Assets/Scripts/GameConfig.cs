using UnityEngine;
using System.Collections;

public enum AttackType
{    
    single,
    vertical,
    horizon,
    bias
}

public enum RoleType
{
    MengJiang,  //猛将
    TongShuai,  //统帅
    JunShi      //军师
}

public class PlayerModel{
    public string pname;
    public AttackType type;
    public RoleType rtype;  //武将类型

    public float phyatk;
    public float phydef;
    public float nuatk;
    public float nudef;
    public float ceatk;
    public float cedef;        

    public float health;
    public float strength;
    public float defend;
    public float intelligence;

    public float currentHp;     //当前HP
    public int currentNu;       //当前怒气

    public PlayerModel(
        string name, AttackType type, RoleType rtype,
        float phyatk, float phydef, float nuatk, float nudef, float ceatk, float cedef, 
        float health,float strength,float defend,float intelligence
        )
    {       
        this.pname = name;
        this.type = type;
        this.rtype = rtype;
        this.phyatk = phyatk;
        this.phydef = phydef;
        this.nuatk = nuatk;
        this.nudef = nudef;
        this.ceatk = ceatk;
        this.cedef = cedef;
        this.health = health;
        this.strength = strength;
        this.defend = defend;
        this.intelligence = intelligence;

        this.currentHp = health;
        this.currentNu = 0;
    }
}

public class ConfigManager
{
    PlayerModel guanyu;
    PlayerModel xiahou;
    PlayerModel caocao;
    PlayerModel zhangliao;
    PlayerModel zhugeliang;
    PlayerModel guojia;

    public PlayerModel[] models;
    public PlayerModel getModel(int id)
    {
        if (models == null) { 
            models = 
                new PlayerModel[]{guanyu, xiahou, caocao, zhangliao, zhugeliang, guojia};
        }
        PlayerModel model = models[id];
        return new PlayerModel(
                model.pname, model.type, model.rtype,
                model.phyatk, model.phydef, model.nuatk, model.nudef, model.ceatk, model.cedef, 
                model.health,model.strength,model.defend,model.intelligence
            ); 
    }

    public ConfigManager()
    {
        guanyu = new PlayerModel(
            "关羽", AttackType.horizon, RoleType.MengJiang,
            1000, 700, 1000, 700, 1000, 700, 6000, 97, 86, 78
        );
        xiahou = new PlayerModel(
            "夏侯惇", AttackType.single, RoleType.MengJiang,
            1000, 700, 1000, 700, 1000, 700, 6000, 96, 82, 67
        );
        caocao = new PlayerModel(
            "曹操", AttackType.vertical, RoleType.TongShuai,
            1000, 700, 1000, 700, 1000, 700, 6000, 85, 97, 88
         );
        zhangliao = new PlayerModel(
            "张辽", AttackType.single, RoleType.TongShuai,
            1000, 700, 1000, 700, 1000, 700, 6000, 91, 94, 82
         );
        zhugeliang = new PlayerModel(
            "诸葛亮", AttackType.bias, RoleType.JunShi,
            1000, 700, 1000, 700, 1000, 700, 6000, 59, 82, 100
         );
        guojia = new PlayerModel(
            "郭嘉", AttackType.single, RoleType.JunShi,
            1000, 700, 1000, 700, 1000, 700, 6000, 52, 76, 97
         );
    }
}
