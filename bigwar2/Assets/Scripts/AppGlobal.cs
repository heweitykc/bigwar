using UnityEngine;
using System.Collections;

public enum GamePhase { 
    LINEUP,     //布阵
    BATTLE,     //战斗
    OPERATE     //行走
}

public class AppGlobal {
	static public int TouchLayer = 1 << 9;
	static public EmbattleManager embattleMgr;
	static public BattleManager battleMgr;
	static public BattleAnimations battleAnim;
	static public ItemManager itemMgr;
	static public Transform camera;
	static public BattleResult battleResult;
	static public PlayerRound playRound;

    static public GamePhase phase;	//0:布阵，1:战斗 2:行走
	
	//3列的分别搜索次序
	static public int[] S0 = {1,4,7,2,5,8,3,6,9};
	static public int[] S1 = {2,5,8,1,4,7,3,6,9};
	static public int[] S2 = {3,6,9,2,5,8,1,4,7};
}
