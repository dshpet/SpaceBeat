/***********************************************
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System.Collections;

public class Helper{

	public static T GetRandomEnum<T>(){
		System.Array a = System.Enum.GetValues (typeof(T));
		T v = (T)a.GetValue (Random.Range (0, a.Length));
		return v;
	}

	public static bool RandomBoolean(){

		if (Random.Range(0f,2f)>1){
			return true;
		}
		else{
			return false;
		}
	}

	public static int RandomBoolean2Int(){

		bool rnd = RandomBoolean();
		if (rnd)
			return 1;
		else
			return -1;
	}

	public static Vector3 SphericalPosition(float latitude,float Longitude,  float distance){

		GameObject axis = new GameObject("Axis");
		axis.transform.rotation = Quaternion.Euler( new Vector3(latitude,Longitude,0f));

		Vector3 position = axis.transform.TransformDirection( new Vector3(0,0,distance));

		if (Application.isPlaying){
			Object.Destroy( axis);
		}
		else{
			Object.DestroyImmediate(axis);
		}

		return position;
	}
}
}
