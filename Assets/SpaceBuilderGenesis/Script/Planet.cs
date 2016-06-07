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
public class Planet : MonoBehaviour {

	#region Member

	#region planet
	public GameObject planet;
	public bool render2SkyBox=true;

	[SerializeField]
	private Material planetMat = null;
	public Material PlanetMat {
		get {
			return planetMat;
		}
		set {
			if (value != planetMat){
				if (planetMat!=null && planetMat.name == "Space Builder/Planet BA")
					DestroyImmediate( planetMat);
				planetMat = value;
				MeshRenderer mr = planet.GetComponent<MeshRenderer>();
				mr.material = planetMat;
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
	
	[SerializeField]
	private float distance;
	public float Distance {
		get {
			return distance;
		}
		set {
			if (value != distance){
				distance = value;
				UpdatePosition();
			}
		}
	}
	
	[SerializeField]
	private int size;
	public int Size {
		get {
			return size;
		}
		set {
			if (size != value){
				size = value;
				transform.localScale = new Vector3(size,size,size);
			}
		}
	}
	
	[SerializeField]
	private float xAngle;
	public float XAngle {
		get {
			return xAngle;
		}
		set {
			if (value != xAngle){
				xAngle = value;
				planet.transform.localRotation = Quaternion.Euler( new Vector3(xAngle,yAngle,0));
			}
		}
	}
	
	[SerializeField]
	private float yAngle;
	public float YAngle {
		get {
			return yAngle;
		}
		set {
			if (value != yAngle){
				yAngle = value;
				planet.transform.localRotation = Quaternion.Euler( new Vector3(xAngle,yAngle,0));
			}
		}
	}

	private Transform cacheTransformPlanet;
	private Transform cacheTransform;
	#endregion

	#region diffuse
	[SerializeField]
	public float PowerDiffuse{
		get {
			return planetMat.GetFloat("_DiffusePower");
		}
		set {
			if (value != planetMat.GetFloat("_DiffusePower") ){
				planetMat.SetFloat("_DiffusePower",value);
			}
		}
	}

	[SerializeField]
	public bool EnableAmbient{
		get {
			float e =  planetMat.GetFloat("_EnableAmbient");
			if (e==0)
				return false;
			else
				return true;
		}
		set {
			if (value)
				planetMat.SetFloat("_EnableAmbient",1);
			else
				planetMat.SetFloat("_EnableAmbient",0);
		}
	}
	#endregion

	#region Atmosphere member
	public GameObject atmosphere;
	public Material atmosphereMat = null;

	// External
	[SerializeField]
	public bool EnableEAtm{
		get {
			return atmosphere.activeSelf;
		}
		set {
			atmosphere.SetActive( value);
		}
	}

	[SerializeField]
	public bool EAtmFullBright{
		get {
			float e =  atmosphereMat.GetFloat("_FullBright");
			if (e==0)
				return false;
			else
				return true;
		}
		set {
			if (value)
				atmosphereMat.SetFloat("_FullBright",1);
			else
				atmosphereMat.SetFloat("_FullBright",0);
		}
	}

	[SerializeField]
	public float EAtmSize{
		get {
			return atmosphereMat.GetFloat( "_Size");
		}
		set {
			if (value != atmosphereMat.GetFloat( "_Size")){
				atmosphereMat.SetFloat("_Size",value);
			}
		}
	}

	[SerializeField]
	public Color EAtmColor {
		get {
			return atmosphereMat.GetColor( "_Color");
		}
		set {
			if (value != atmosphereMat.GetColor( "_Color")){
				atmosphereMat.SetColor("_Color",value);
			}
		}
	}

	public float EAtmFallOff{
		get {
			return atmosphereMat.GetFloat( "_FallOff");
		}
		set {
			if (value != atmosphereMat.GetFloat( "_FallOff")){
				atmosphereMat.SetFloat("_FallOff",value);
			}
		}
	}

	// Internal
	[SerializeField]
	public bool EnableAtm {
		get {
			float e =  planetMat.GetFloat("_EnableAtm");
			if (e==0)
				return false;
			else
				return true;
		}
		set {
			if (value)
				planetMat.SetFloat("_EnableAtm",1);
			else
				planetMat.SetFloat("_EnableAtm",0);
		}
	}

	[SerializeField]
	public Color AtmColor {
		get {
			return planetMat.GetColor( "_AtmColor");
		}
		set {
			if (value != planetMat.GetColor( "_AtmColor")){
				planetMat.SetColor("_AtmColor",value);
			}
		}
	}	

	[SerializeField]
	public bool AtmFullBright{
		get {
			float e =  planetMat.GetFloat("_AtmFullBright");
			if (e==0)
				return false;
			else
				return true;
		}
		set {
			if (value)
				planetMat.SetFloat("_AtmFullBright",1);
			else
				planetMat.SetFloat("_AtmFullBright",0);
		}
	}

	[SerializeField]
	public float AtmPower{
		get {
			return  planetMat.GetFloat("_AtmPower");
		}
		set {
			planetMat.SetFloat("_AtmPower",value);
		}
	}

	[SerializeField]
	public float AtmSize{
		get {
			return  planetMat.GetFloat("_AtmSize");
		}
		set {
			planetMat.SetFloat("_AtmSize",value);
		}
	}


	#endregion

	#region Ring
	public GameObject ring;
	public Material ringMat = null;

	[SerializeField]
	public Color RingColor {
		get {
			return ringMat.GetColor( "_Color");
		}
		set {
			if (value != ringMat.GetColor( "_Color")){
				ringMat.SetColor("_Color",value);
			}
		}
	}

	[SerializeField]
	public bool EnableRing{
		get {
			return ring.activeSelf;
		}
		set {
			ring.SetActive( value);
		}
	}

	[SerializeField]
	public float RingDiffusePower{
		get {
			return ringMat.GetFloat("_DiffusePower");
		}
		set {
			if (value != ringMat.GetFloat("_DiffusePower")){
				ringMat.SetFloat("_DiffusePower",value);
			}

		}
	}

	[SerializeField]
	public float RingTransparence{
		get {
			return ringMat.GetFloat("_Transparency");
		}
		set {
			if (value != ringMat.GetFloat("_Transparency")){
				ringMat.SetFloat("_Transparency",value);
			}
			
		}
	}

	[SerializeField]
	private float ringSize=0.3f;
	public float RingSize {
		get {
			return ringSize;
		}
		set {
			if (value != ringSize){
				ringSize = value;
				ring.transform.localScale = new Vector3(ringSize,ringSize,ringSize);
			}
		}
	}

	[SerializeField]
	private float ringXAngle;
	public float RingXAngle {
		get {
			return ringXAngle;
		}
		set {
			if (value != ringXAngle){
				ringXAngle = value;
				ring.transform.localRotation = Quaternion.Euler( new Vector3(ringXAngle,0,ringYAngle));
			}
		}
	}
	
	[SerializeField]
	private float ringYAngle;
	public float RingYAngle {
		get {
			return ringYAngle;
		}
		set {
			if (value != ringYAngle){
				ringYAngle = value;
				ring.transform.localRotation = Quaternion.Euler( new Vector3(ringXAngle,0,ringYAngle));
			}
		}
	}
	#endregion

	#region auto rotation
	public bool enableRotation;
	public Vector3 rotationSpeed;
	public bool enableOrbitalRotation;
	public Transform orbitalParent;
	public float orbitalSpeed;
	public Vector3 orbitalVector = Vector3.up;
	#endregion

	#region Inspector
	public bool lockView = false;
	public bool inspectorShowProperties = false;
	public bool isWaitToDelte = false;
	public bool inspectorShowMaterial = false;
	public bool inspectorShowRing = false;
	public bool inspectorShowRotation = false;
	public bool inspectorShowPosition = false;
	#endregion

	#endregion

	#region Monobehaviour callback
	void Start(){
		cacheTransformPlanet = planet.transform;
		cacheTransform = transform;
	}

	void OnDestroy(){
		if (planetMat!=null && planetMat.name =="Space Builder/Planet BA")
			DestroyImmediate(planetMat);
		DestroyImmediate(atmosphereMat);
		DestroyImmediate( ringMat);
	}

	void Update(){

		if (enableRotation){
			cacheTransformPlanet.Rotate( rotationSpeed * Time.deltaTime);
		}

		if (enableOrbitalRotation && orbitalParent!=null){
			cacheTransform.RotateAround( orbitalParent.position, orbitalVector,orbitalSpeed * Time.deltaTime);
		}
	}
	#endregion

	#region Private method
	private void UpdatePosition(){
		transform.position = Helper.SphericalPosition( -Latitude,Longitude,distance);
	}
	#endregion
}
}
