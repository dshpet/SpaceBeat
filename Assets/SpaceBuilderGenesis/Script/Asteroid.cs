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

public class Asteroid : MonoBehaviour {

	public enum PopMethod { Sphere, Ring};

	#region Inspector
	public bool inspectorShowProperties = false;
	public bool isWaitToDelte = false;
	#endregion
	
	#region Members
	public PopMethod popMethod;

	public bool render2SkyBox=true;

	public int cloneCount;

	public float minRadius = 800;
	public float maxRadius = 1000;
	public float height = 50;

	public float minScale = 1;
	public float maxScale = 1;

	public bool enableRotation;
	public Vector3 rotationSpeed;
	
	[SerializeField]
	public Transform parent{
		get{
			return transform.parent;
		}
		set{
			if (value != transform.parent){
				
				if (value == null){
					transform.parent = AsteroidSystem.instance.transform;
				}
				else{
					transform.parent = value;
					transform.localPosition = Vector3.zero;
				}
			}
		}
	}	

	private Transform cacheTransform;

	public List<GameObject> gameobjectReference = new List<GameObject>();


	#endregion

	#region Monobehavours callback
	void Start(){
		cacheTransform = transform;
	}

	void Update(){
		if (enableRotation){
			cacheTransform.Rotate( rotationSpeed * Time.deltaTime);
		}
	}
	#endregion

	#region Public Members
	public void Generate(){


		Quaternion rot = transform.rotation;

		transform.rotation = Quaternion.identity;
		for(int i=1;i<=cloneCount;i++){
			
			int rand = Random.Range(0,gameobjectReference.Count);
			
			if (gameobjectReference[rand]!=null){

				GameObject clone = null;

				switch (popMethod){
					case PopMethod.Sphere:
						clone= (GameObject)Instantiate( gameobjectReference[rand], transform.position + Random.insideUnitSphere * maxRadius, Quaternion.identity);
						clone.layer = 31;
						foreach(Transform t in clone.transform){
							t.gameObject.layer = 31;
						}
						break;
					case PopMethod.Ring:
						float angle = Random.Range(-Mathf.PI*2,Mathf.PI *2);
						clone = (GameObject)Instantiate( gameobjectReference[rand], transform.position +  new Vector3( Mathf.Cos(angle),0,Mathf.Sin(angle)) * Random.Range(minRadius, maxRadius), transform.rotation);
						clone.transform.Translate( Vector3.up * Random.Range(-height/2,height/2), Space.Self) ;
						clone.layer = 31;
						foreach(Transform t in clone.transform){
							t.gameObject.layer = 31;
						}
					break;


				}

				if ( Vector3.Distance( clone.transform.position, transform.position)>= minRadius ){
					clone.transform.parent = transform;
					float size = Random.Range(minScale,maxScale);
					clone.transform.localScale = new Vector3( size,size,size);
					clone.transform.rotation = Random.rotation;
				}
				else{
					DestroyImmediate( clone);
					i--;
				}
			}
		}

		transform.rotation = rot;
	}

	public void Clear(){
		while (transform.childCount>0){
			DestroyImmediate( transform.GetChild(0).gameObject);
		}
	}
	#endregion
}
}


