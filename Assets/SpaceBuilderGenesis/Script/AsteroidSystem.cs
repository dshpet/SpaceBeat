/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System.Collections;

public class AsteroidSystem :  Singleton<AsteroidSystem> {

	public void Create(){
		name="AsteroidSystem";
		transform.parent = Cosmos.instance.transform;
	}

	public void ClearAsteroids(){
		Asteroid[] asteroids = transform.GetComponentsInChildren<Asteroid>();
		for (int i=0;i<asteroids.Length;i++){
			DestroyImmediate( asteroids[i].gameObject );
		}

		asteroids = PlanetSystem.instance.transform.GetComponentsInChildren<Asteroid>();
		for (int i=0;i<asteroids.Length;i++){
			DestroyImmediate( asteroids[i].gameObject );
		}
	}
}
}