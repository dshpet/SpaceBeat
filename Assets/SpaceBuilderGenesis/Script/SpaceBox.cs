/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[ExecuteInEditMode]
public class SpaceBox : Singleton<SpaceBox> {

	#region Enumeration
	public enum NebulaQuality{ Draft,Low,Medium,High,Ultra };
	#endregion

	#region Members
	public Material cosmosMaterial;

	[SerializeField]
	private NebulaQuality quality;
	public NebulaQuality Quality {
		get {
			return quality;
		}
		set {
			if (quality != value){
				quality = value;
				SetNebulaQuality( GetNebulaQuality2Int());
			}
		}
	}

	[SerializeField]
	public List<Nebula> nebula;

	public Starfield starfield;

	public Texture2D[] nebulaTexture;
	public bool nebulaNeed2Save=false;

	public Texture2D[] skyboxTexture;	
	 



	public bool wait2Render;
	#endregion

	#region Constructor
	public void Create(){

		name = "SpaceBox";

		transform.parent = Cosmos.instance.transform;
		transform.hideFlags = HideFlags.HideInInspector;

		wait2Render = false;

		CreateCosmosMaterial(3);

		CreateSkyBox();
		CreateStarfield();

		CreateNebula();

	}
	#endregion

	#region Monobehaviors callback
	void OnDestroy(){

		if (Application.isEditor){
			DestroyImmediate( cosmosMaterial);
		}
		else{
			Destroy( cosmosMaterial);
		}

		nebulaTexture = null;
		skyboxTexture = null;
	}
	#endregion

	#region Private Method
	private void CreateCosmosMaterial(int level){

		switch( level){
			case 3:
				cosmosMaterial = new Material(Shader.Find("Space Builder/SpaceBox3Level"));
				break;
		}

	}

	private void CreateSkyBox(){

		skyboxTexture = new Texture2D[6];

		for (int i=0;i<6;i++){
			skyboxTexture[i] = new Texture2D(128,128,TextureFormat.RGB24,false);
			TextureTools.Fill( skyboxTexture[i],Color.black);
		}

		UpdateOriginalSkybox();
	}

	private void CreateStarfield(){

		starfield = new Starfield();
		starfield.Create();

	}

	private void CreateNebula(){

		nebula = new List<Nebula>();
		nebulaTexture = new Texture2D[6];

		Quality = NebulaQuality.Draft;
		//SetNebulaQuality( GetNebulaQuality2Int());
	}
	#endregion
	
	#region Public Method
	public void SetNebulaQuality(int quality){


		// Nebula
		for (int i=0;i<6;i++){
			if (nebulaTexture[i]==null){
				nebulaTexture[i] = new Texture2D (quality, quality, TextureFormat.RGBA32,false);
				nebulaTexture[i].wrapMode = TextureWrapMode.Clamp;
				
			}
			else{
				nebulaTexture[i].Resize( quality, quality,TextureFormat.RGBA32,false);
			}
			nebulaTexture[i].hideFlags = HideFlags.HideAndDontSave;

			TextureTools.Fill( nebulaTexture[i], Color.black);
		}

		nebulaNeed2Save = true;
	}

	public Nebula AddNebula(Nebula.NebulaType type = Nebula.NebulaType.Cloudy){
		
		Nebula neb = new Nebula();
		neb.CreateNebula(Nebula.NebulaType.Cloudy);
		nebula.Add( neb);
		SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;

		nebulaNeed2Save = true;
		return neb;
	}

	public void RandomNebula(){

		int count = Random.Range(1,4);

		nebula.Clear();

		for(int i=0;i<count;i++){
			Nebula neb = new Nebula();
			if (i < count/2){
				if (count==1){
					neb.CreateNebula(Helper.GetRandomEnum<Nebula.NebulaType>());
				}
				else{
				
					neb.CreateNebula(Nebula.NebulaType.Cloudy);
				}
			}
			else{
				neb.CreateNebula(Nebula.NebulaType.Veined);
			}
			nebula.Add( neb);
		}

		SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;

		nebulaNeed2Save = true;
	}
	
	public void UpdateOriginalSkybox(){

		cosmosMaterial.SetTexture("_FrontSky",skyboxTexture[0]);
		cosmosMaterial.SetTexture("_BackSky",skyboxTexture[1]);
		cosmosMaterial.SetTexture("_LeftSky",skyboxTexture[2]);
		cosmosMaterial.SetTexture("_RightSky",skyboxTexture[3]);
		cosmosMaterial.SetTexture("_UpSky",skyboxTexture[4]);
		cosmosMaterial.SetTexture("_DownSky",skyboxTexture[5]);

		RenderSettings.skybox = cosmosMaterial;
	}

	public void UpdateNebulaSkyBox(){

		cosmosMaterial.SetTexture("_FrontNebula",nebulaTexture[0]);
		cosmosMaterial.SetTexture("_BackNebula",nebulaTexture[1]);
		cosmosMaterial.SetTexture("_LeftNebula",nebulaTexture[2]);
		cosmosMaterial.SetTexture("_RightNebula",nebulaTexture[3]);
		cosmosMaterial.SetTexture("_UpNebula",nebulaTexture[4]);
		cosmosMaterial.SetTexture("_DownNebula",nebulaTexture[5]);

		RenderSettings.skybox = cosmosMaterial;
		SpaceBox.instance.starfield.Render();
	}

	public int GetNebulaQuality2Int(){
		int size = 0;
		switch( quality){
			case NebulaQuality.Draft:
				size=128;
				break;
			case NebulaQuality.Low:
				size=256;
				break;
			case NebulaQuality.Medium:
				size=512;
				break;
			case NebulaQuality.High:
				size=1024;
				break;
			case NebulaQuality.Ultra:
				size=2048;
				break;
		}
		return size;
	}

	public void UpdateNebulaColor(){
		foreach(Nebula neb in nebula){
			switch(neb.Type){
			case Nebula.NebulaType.Cloudy:
				neb.RandomCloudyColor();
				break;
			case Nebula.NebulaType.Veined:
				neb.RandomVeinedColor();
				break;
			}
		}
	}

	public void ClearNebula(){

		for(int i=0;i<nebula.Count;i++){
			nebula.Remove( nebula[i] );
			i--;
		}
	}
	#endregion
}
}

