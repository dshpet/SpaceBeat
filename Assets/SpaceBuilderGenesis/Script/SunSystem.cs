/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System.Collections;

public class SunSystem : Singleton<SunSystem> {

	public void Create(){
		name="SunSystem";
		transform.parent = Cosmos.instance.transform;
		transform.hideFlags = HideFlags.HideInInspector;
	}

	public void ClearSuns(){
		
		Sun[] suns = transform.GetComponentsInChildren<Sun>();
		for (int i=0;i<suns.Length;i++){
			DestroyImmediate( suns[i].gameObject );
		}
	}

}
}