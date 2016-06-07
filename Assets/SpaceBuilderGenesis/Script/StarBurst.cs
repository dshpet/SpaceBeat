/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System.Collections;

[System.Serializable]
[ExecuteInEditMode]
public class StarBurst : MonoBehaviour {

	public Material material;


	[SerializeField]
	private float sizeX;
	public float SizeX {
		get {
			return sizeX;
		}
		set {
			if (sizeX != value){
				sizeX = value;
				UpdateSize();
			}
		}
	}
	
	[SerializeField]
	private float sizeY;
	public float SizeY {
		get {
			return sizeY;
		}
		set {
			if (sizeY != value){
				sizeY = value;
				UpdateSize();
			}

		}
	}

	[SerializeField]
	private float rotation;
	public float Rotation {
		get {
			return rotation;
		}
		set {
			if (value != rotation){
				rotation = value;
				//transform.Rotate( new Vector3(0,0,1),rotation,Space.Self);
				transform.eulerAngles = new Vector3(  -Latitude,Longitude,-rotation);
			}
		}
	}

	[SerializeField]
	private float longitude;
	public float Longitude {
		get {
			return longitude;
		}
		set {
			if (longitude != value){
				longitude = value;
				UpdatePosition();
			}
		}
	}

	[SerializeField]
	private float latitude;
	public float Latitude {
		get {
			return latitude;
		}
		set {
			if (latitude != value){
				latitude = value;
				UpdatePosition();
			}
			
		}
	}

	public bool inspectorShowProperties = false;
	public bool isWaitToDelte = false;


	void OnDestroy(){
		DestroyImmediate(material);
	}

	private void UpdatePosition(){
		transform.position = Helper.SphericalPosition( -Latitude,Longitude,1000);
		transform.eulerAngles = new Vector3(  -Latitude,Longitude,-rotation);
	}

	private void UpdateSize(){
		transform.localScale = new Vector3(sizeX,sizeY,0);
	}
}
}