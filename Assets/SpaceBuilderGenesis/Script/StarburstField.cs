/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarburstField : Singleton<StarburstField> {
	
	public void Create(){
		name="StarburstField";

		transform.parent = Cosmos.instance.transform;
		transform.hideFlags = HideFlags.HideInInspector;
	}


	public void ClearStarbursts(){

		StarBurst[] star = transform.GetComponentsInChildren<StarBurst>();
		for (int i=0;i<star.Length;i++){
			DestroyImmediate( star[i].gameObject );
		}
	}

	public void RandomColor(){
		
		StarBurst[] star = GetComponentsInChildren<StarBurst>();
		for (int i=0;i<star.Length;i++){
			
			Color color1 = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1);
			Color color2 = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1);

			color1 = color1 * (Cosmos.instance.color * Random.Range(0.4f,1f));
			color2 = color2 * (Cosmos.instance.color2 * Random.Range(0.4f,1f));
			color1.a = 1;
			color2.a = 1;

			star[i].material.SetColor("_Color1", color1 );
			star[i].material.SetColor("_Color2", color2 );

			star[i].material.SetFloat("_Mixing", Random.Range(0f,1f) );
			star[i].material.SetFloat("_Power", Random.Range(0f,1f) );
		}
	}
}
}