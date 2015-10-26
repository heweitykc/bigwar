using UnityEngine;
using System.Collections;

public class BattleComputer {
    static public int compute(PlayerModel attacker, PlayerModel suffer, out string atkstr)
    {
        AttackType type = attacker.type;

        if (attacker.rtype == RoleType.MengJiang) {
            if (attacker.currentNu >= 3) {
                atkstr = "怒气";
                return mjnu(attacker, suffer, type);
            }
            atkstr = "";
            return mjphy(attacker, suffer);
        }
        if (attacker.rtype == RoleType.TongShuai)
        {
            atkstr = "";
            return ts(attacker, suffer,type);
        }
        if (attacker.rtype == RoleType.JunShi)
        {
            atkstr = "";
            return js(attacker, suffer, type);
        }
        atkstr = "";
        return 0;
    }

    //猛将物攻
    static public int mjphy(PlayerModel attacker, PlayerModel suffer)
    {
        if (suffer.rtype == RoleType.MengJiang)
            suffer.currentNu++; //被打的怒气增加
        return (int)((attacker.phyatk - suffer.phydef) * (1 + (attacker.defend - suffer.defend) * 0.03));
    }

    //猛将技攻
    static public int mjnu(PlayerModel attacker, PlayerModel suffer, AttackType type)
    {
        float rate = getRate(type);
        float nu = attacker.currentNu;
        //attacker.currentNu = 0;
        return (int)((attacker.nuatk - suffer.nudef) * (1 + (attacker.strength - suffer.strength) * 0.03) * rate * (1 + nu));
    }

    //统帅攻击
    static public int ts(PlayerModel attacker, PlayerModel suffer, AttackType type)
    {
        float rate = getRate(type);
        return (int)((attacker.phyatk - suffer.phydef) * (1 + (attacker.defend - suffer.defend) * 0.03) * rate);        
    }

    //军师攻击
    static public int js(PlayerModel attacker, PlayerModel suffer, AttackType type)
    {
        float rate = getRate(type);
        return (int)((attacker.ceatk - suffer.cedef) * (1 + (attacker.intelligence - suffer.intelligence) * 0.03) * rate); 
    }

    static float getRate(AttackType type)
    {
        if (type == AttackType.single) return 1.7f;
        else if (type == AttackType.horizon) return 1f;
        else if (type == AttackType.vertical) return 1f;
        else if (type == AttackType.bias) return 1;
        return 1;
    }

    static public bool isFinish()
    {
        if (!hasAlive(AppGlobal.battleMgr.team0))
        {
            AppGlobal.battleResult.showResult(false);
            return true;
        }

        if (!hasAlive(AppGlobal.battleMgr.team1))
        {
            AppGlobal.battleResult.showResult(true);
            return true;
        }

        return false;
    }

    static public bool hasAlive(BattleItem[] items)
    {
        int cnt = 0;
        foreach (BattleItem item in items)
        {
            if (item._soldierId == -1) continue;
            if (item.data.currentHp <= 0) continue;
            cnt++;
        }
        return cnt > 0;
    }
}
