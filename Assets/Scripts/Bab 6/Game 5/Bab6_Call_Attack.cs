using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bab6_Call_Attack : MonoBehaviour {

	[Header("Reference To Another Script")]
	public Bab6_Stratus stratus;
	public Bab6_Nimbus nimbus;

	public void CallAttackStratus(){
		stratus.Attack ();
	}

	public void CallAttackNimbus(){
		nimbus.Attack ();
	}

}
