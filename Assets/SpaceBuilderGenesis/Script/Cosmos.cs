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

[System.Serializable]
public class Cosmos : Singleton<Cosmos> {

	#region Enumeration
	public enum SkyboxQuality { Low,Medium,High,Ultra};
	#endregion

	#region Event
	public delegate void CosmosReady();
	public static event CosmosReady On_CosmosReady;
	#endregion

	#region Members

	private bool isComosReady = false;

	#region Camera
	[SerializeField]
	private Camera spaceCamera;
	public Camera SpaceCamera {
		get {
			return spaceCamera;
		}
		set {
			if (spaceCamera != value){
				spaceCamera = value;
				
				if (spaceCamera!=null){
					spaceCamera.backgroundColor = Color.black;

					if (Application.isPlaying){
						SetupSpaceCamera();

						if (On_CosmosReady!=null && spaceCamera!=null){
							On_CosmosReady();
							isComosReady = true;
						}
					}
				}
			}
		}
	}
	private Vector3 oldPosition;

	private Camera cosmosCamera;
	public int fov;
	public bool copyFOV;
	public bool enableDrift;
	public int maxDriftDistance;
	public float driftFactor;
	#endregion

	#region global tint
	public Color color;
	public Color color2;
	#endregion
	
	#region random setting
	public bool rndPlanet=true;
	public bool rndring=true;
	public bool rndAstPlanet=true;
	public bool rndStarburst=true;
	public bool rndSbStar=true;
	public bool rndSbNeb=true;
	public bool rndAsteroid=true;
	#endregion

	#region Render Option
	public bool noRender = false;
	public bool useRender = false;
	public Material skyboxMat;
	public SkyboxQuality skyQuality;
	public Texture2D[] skyFace;	

	public string realPath;
	#endregion
	
	#endregion
	
	#region Constructor
	public void Create(){

		name="Cosmos";

		noRender = true;

		transform.hideFlags = HideFlags.HideInInspector;

		// other
		enableDrift = false;
		driftFactor = 150;
		maxDriftDistance = 300;
		
		// Camera
		fov = 60;
		copyFOV = true;

		// Global tint
		color = Color.white;
		color2 = Color.white;
		
		// Rendering
		//skyboxMat = new Material(Shader.Find("RenderFX/Skybox"));
		skyFace = new Texture2D[6];

		// Global setting
		RenderSettings.ambientLight = new Color(0.2f,0.2f,0.2f);
		SpaceCamera = Camera.main;

		// SpaceBox
		SpaceBox.instance.Create();

		// Starburstfield
		StarburstField.instance.Create();

		// Planet
		PlanetSystem.instance.Create();

		// Sun
		SunSystem.instance.Create();

		// Asteroid
		AsteroidSystem.instance.Create();
	}
	#endregion

	#region Mononbehaviour Call back
	void Start(){

		#region skybox
		if (!noRender){


			// Auto compute skybox
			//if (Application.HasProLicense() && !useRender){
			if ( !useRender){
				
				SkyboxGenerator sg = new SkyboxGenerator();
				skyFace = sg.CreateSkyBox();
				
				sg = null;
			}
			
			// Use existing render
			//if ((useRender || (Application.HasProLicense() && !useRender))){
			if ((useRender || !useRender)){

				if (useRender)
					RenderSettings.skybox = skyboxMat;
				
				// Destroy object that are rendering in skybox
				int count=0;
				int childCount=0;
				foreach( Transform t in PlanetSystem.instance.transform){
					if (t.GetComponent<Planet>().render2SkyBox){
						Destroy( t.gameObject );
						count++;
					}
					childCount++;
				}
				if (count==childCount){
					Destroy( PlanetSystem.instance.gameObject);
				}

				count=0;
				childCount=0;
				foreach( Transform t in AsteroidSystem.instance.transform){
					if (t.GetComponent<Asteroid>().render2SkyBox){
						Destroy( t.gameObject );
						count++;
					}
					childCount++;
				}
				if (count==childCount ){
					Destroy( AsteroidSystem.instance.gameObject);
				}

				Destroy ( StarburstField.instance.gameObject);

				
			}
			Destroy ( SpaceBox.instance.gameObject);
		}


		#endregion
		
		#region Camera setup
		//Space camera
		if (spaceCamera!=null){
			SetupSpaceCamera();
		}
		
		
		// Cosmos camera
		GameObject axis = new GameObject("CosmosCamera");
		axis.transform.parent = gameObject.transform;
		
		cosmosCamera = axis.AddComponent<Camera>();
		cosmosCamera.backgroundColor = Color.black;
		cosmosCamera.farClipPlane = 3000;
		cosmosCamera.depth = -10;
		cosmosCamera.cullingMask = 1<<31;
		cosmosCamera.fieldOfView = fov; 
		cosmosCamera.gameObject.AddComponent<FlareLayer>();
		#endregion
		
		if (On_CosmosReady!=null && spaceCamera!=null && !isComosReady){
			On_CosmosReady();
		}

		if (spaceCamera!=null){
		   isComosReady = true;
		}
	}


	void Update(){

		if (isComosReady ){
			cosmosCamera.transform.rotation = spaceCamera.transform.rotation;

			// Drift
			if (enableDrift){

				Vector3 delta = spaceCamera.transform.position - oldPosition;

				if (Vector3.Distance( Vector3.zero, cosmosCamera.transform.position+delta / driftFactor )<= maxDriftDistance){

					cosmosCamera.transform.position += (delta / driftFactor);
				}
			}

			if (copyFOV){
				cosmosCamera.fieldOfView = spaceCamera.fieldOfView;
			}

			oldPosition = new Vector3(spaceCamera.transform.position.x,spaceCamera.transform.position.y,spaceCamera.transform.position.z);
		}

	}
	
	void OnDrawGizmos(){

		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere(transform.position, 1000);
	}
	#endregion

	private void SetupSpaceCamera(){

		spaceCamera.cullingMask = ~(1<<31);
		spaceCamera.clearFlags = CameraClearFlags.Depth;
		Behaviour flareLayer = (Behaviour)spaceCamera.gameObject.GetComponent ("FlareLayer");
		if (Application.isEditor){
			DestroyImmediate(flareLayer);
		}
		else{
			Destroy( flareLayer );
		}
		oldPosition = spaceCamera.transform.position;
	}

	#region public method
	public int GetSkySize(){
		int size=0;
		switch (skyQuality){
		case SkyboxQuality.Low:
			size=512;
			break;
		case SkyboxQuality.Medium:
			size=1024;
			break;
		case SkyboxQuality.High:
			size = 2048;
			break;
		case SkyboxQuality.Ultra:
			size = 4096;
			break;

		}

		return size;
	}
	#endregion
	
}

}
