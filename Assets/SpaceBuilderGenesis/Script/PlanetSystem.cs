/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System.Collections;

public class PlanetSystem : Singleton<PlanetSystem> {

	public void Create(){
		name = "PlanetSystem";
		transform.parent = Cosmos.instance.transform;
		transform.hideFlags = HideFlags.HideInInspector;
	}
	
	public void ClearPlanets(){

		Planet[] planets = transform.GetComponentsInChildren<Planet>();
		for (int i=0;i<planets.Length;i++){
			DestroyImmediate( planets[i].gameObject );
		}
	}

	public void RandomColor(){

		Planet[] planets = GetComponentsInChildren<Planet>();
		for (int i=0;i<planets.Length;i++){

			if (planets[i].EnableAtm){
				Color color = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1);
				if (Helper.RandomBoolean()){
					color = color * (Cosmos.instance.color * Random.Range(0.4f,1f));
					color.a = 1;
					planets[i].AtmColor = color ;
				}
				else{
					color = color * (Cosmos.instance.color2 * Random.Range(0.4f,1f));
					color.a = 1;
					planets[i].AtmColor = color;
				}
			}
		}
	}
}
}
