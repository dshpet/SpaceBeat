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
public class Sun : MonoBehaviour {

	public Light pointLight;
	public Light directionalLight;

	[SerializeField]
	public bool noFlare{
		get{
			return !pointLight.gameObject.activeSelf;
		}
		set{
			if (value != !pointLight.gameObject.activeSelf){
				pointLight.gameObject.SetActive( !value);
			}
		}
	}

	[SerializeField]
	public bool onlyFlare{
		get{
			return !directionalLight.gameObject.activeSelf;
		}
		set{
			if (value != !directionalLight.gameObject.activeSelf){
				directionalLight.gameObject.SetActive( !value);
			}
		}
	}

	[SerializeField]
	private Flare sunflare;
	public Flare SunFlare {
		get {
			return sunflare;
		}
		set {
			if (value != sunflare){
				sunflare = value;
				pointLight.flare = sunflare;
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
			if (value!= longitude){
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
			if (value != latitude){
				latitude = value;
				UpdatePosition();
			}
		}
	}

	public bool lockView = false;
	public bool inspectorShowProperties = false;
	public bool isWaitToDelte = false;

	private void UpdatePosition(){
		transform.position = Helper.SphericalPosition( -Latitude,Longitude,1000);
		transform.LookAt( Vector3.zero);
	}
}
}
