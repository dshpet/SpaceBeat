/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System;
using System.Collections;

public class SkyboxGenerator{

	private GameObject axis = null;
	private Camera skyCamera = null;
	

	public Texture2D[] CreateSkyBox(){

		// Dsactivate object that won't be rendering into skybox
		foreach( Transform t in PlanetSystem.instance.transform){
			if (!t.GetComponent<Planet>().render2SkyBox){
				t.gameObject.SetActive( false);
			}
		}

		foreach( Transform t in AsteroidSystem.instance.transform){
			if (!t.GetComponent<Asteroid>().render2SkyBox){
				t.gameObject.SetActive( false);
			}
		}

		Texture2D[] skyFace = new Texture2D[6];

		CreateCamera();

		skyFace[0] = Render(new Vector3 (0,0,0));
		skyFace[1] = Render(new Vector3 (0,180,0));
		skyFace[2] = Render(new Vector3 (0,90,0));
		skyFace[3] = Render(new Vector3 (0,-90,0));
		skyFace[4] = Render(new Vector3 (-90,0,0));
		skyFace[5] = Render(new Vector3 (90,0,0));


		if (Application.isPlaying){
			for (int i=0;i<6;i++){
				Byte[]  bytes = skyFace[i].EncodeToPNG();
				skyFace[i].LoadImage( bytes);
				skyFace[i].wrapMode = TextureWrapMode.Clamp;
				skyFace[i].filterMode = FilterMode.Trilinear;
				skyFace[i].anisoLevel = 3;
			}
		}

		// Activate object
		foreach( Transform t in PlanetSystem.instance.transform){
			if (!t.GetComponent<Planet>().render2SkyBox){
				t.gameObject.SetActive( true);
			}
		}

		foreach( Transform t in AsteroidSystem.instance.transform){
			if (!t.GetComponent<Asteroid>().render2SkyBox){
				t.gameObject.SetActive( true);
			}
		}

		if (Application.isPlaying){
			GameObject.Destroy( axis);
		}
		else{
			GameObject.DestroyImmediate( axis);
		}

		return skyFace;
	}

	private void CreateCamera(){

		axis = new GameObject("CosmosCamera");
		skyCamera = axis.AddComponent<Camera>();
		skyCamera.backgroundColor = Color.black;
		skyCamera.nearClipPlane = 0.01f;
		skyCamera.farClipPlane = 5000;
		skyCamera.clearFlags = CameraClearFlags.Skybox;
		skyCamera.fieldOfView = 90;    
		skyCamera.aspect = 1.0f;

	}

	private Texture2D Render(Vector3 orientation){


		skyCamera.transform.eulerAngles = orientation;

		int screenSize = Cosmos.instance.GetSkySize();

		RenderTexture rt = RenderTexture.GetTemporary( screenSize,screenSize,24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default,1);
	
		skyCamera.targetTexture = rt;
	

		RenderTexture.active = rt;
		skyCamera.GetComponent<Camera>().Render();
	

		Texture2D screenShot = new Texture2D (screenSize, screenSize,TextureFormat.ARGB32,false);
		screenShot.hideFlags = HideFlags.DontSave;
		screenShot.ReadPixels (new Rect (0, 0, screenSize, screenSize), 0, 0); 

		RenderTexture.ReleaseTemporary( rt);

			RenderTexture.active = null;

		return screenShot;

	}
}
}
