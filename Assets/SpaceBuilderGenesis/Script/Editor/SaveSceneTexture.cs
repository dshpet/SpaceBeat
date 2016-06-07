/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using SBGenesis;

public class SaveSceneTexture : UnityEditor.AssetModificationProcessor {

	private static string[] OnWillSaveAssets (string[] paths) {


		GameObject cosmos = GameObject.Find("SBGenesis.Cosmos");

		if (cosmos==null){
			cosmos = GameObject.Find("Cosmos");
		}

		if (cosmos != null){
			string sceneName = string.Empty;

			// extract scene name
			foreach (string path in paths){

				if (path.Contains(".unity")){
					sceneName = Path.GetFileNameWithoutExtension(path);
				}
			}



			// If it is a scene
			if (!string.IsNullOrEmpty( sceneName) || (string.IsNullOrEmpty( sceneName) && paths.Length==0)){



				// Create real folder
				if (sceneName != Cosmos.instance.realPath && !string.IsNullOrEmpty( sceneName)  ){

					string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox";
					CreateAssetDirectory(path,sceneName);
					CreateAssetDirectory(path+"/"+sceneName,"nebula");
					CreateAssetDirectory(path+"/"+sceneName,"starfield");
					CreateAssetDirectory(path+"/"+sceneName,"render");

					for (int i=0;i<6;i++){
						AssetDatabase.MoveAsset(path+"/"+ Cosmos.instance.realPath +"/starfield/starfield"+i.ToString() + ".png",path+"/"+ sceneName +"/starfield/starfield"+i.ToString() +".png");
						AssetDatabase.MoveAsset(path+"/"+ Cosmos.instance.realPath +"/nebula/nebula"+i.ToString() + ".png",path+"/"+ sceneName +"/nebula/nebula"+i.ToString() +".png");
						AssetDatabase.MoveAsset(path+"/"+ Cosmos.instance.realPath +"/render/render"+i.ToString() + ".png",path+"/"+ sceneName +"/render/render"+i.ToString() +".png");
					}
					AssetDatabase.MoveAsset(path+"/"+ Cosmos.instance.realPath +"/skybox.mat",path+"/"+ sceneName +"/skybox.mat");

					AssetDatabase.Refresh();

					if (!string.IsNullOrEmpty( sceneName)){
						Cosmos.instance.realPath = sceneName;
					}
					else{
						Cosmos.instance.realPath = "_tmp";
					}

				}


				// Save Texture
				if (SpaceBox.instance.starfield.need2save){
					SaveStarfieldTexture();
					SpaceBox.instance.starfield.need2save = false;
				}

				if (SpaceBox.instance.nebulaNeed2Save){
					SaveNebulaTexture();
					SetNebulaTextureImporter();
					SpaceBox.instance.nebulaNeed2Save = false;
				}

			}



		}
		return paths;
	}

	
	public static void CreateStarfieldTexture(){
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;

		Texture2D texture = new Texture2D( SpaceBox.instance.starfield.GetStarfieldQuality2Int(),SpaceBox.instance.starfield.GetStarfieldQuality2Int(),TextureFormat.RGB24,false);
		texture.hideFlags = HideFlags.HideAndDontSave;
		TextureTools.Fill( texture,Color.black,true);
		for (int i=0;i<6;i++){
			SaveSceneTexture.SaveTexture( texture,path+"/starfield/starfield"+i.ToString()+".png");
		}
		SpaceBox.instance.starfield.starfieldTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/starfield");
		SaveSceneTexture.SetStarfieldTextureImporter();
	}

	public static void CreateNebulaTexture(){
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;

		Texture2D  texture = new Texture2D(SpaceBox.instance.GetNebulaQuality2Int(),SpaceBox.instance.GetNebulaQuality2Int(),TextureFormat.ARGB32,false);
		texture.hideFlags = HideFlags.HideAndDontSave;
		TextureTools.Fill( texture,Color.black,true);
		for (int i=0;i<6;i++){
			SaveSceneTexture.SaveTexture( texture,path+"/nebula/nebula"+i.ToString()+".png");
		}
		SpaceBox.instance.nebulaTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/nebula");
		SaveSceneTexture.SetNebulaTextureImporter();
	}

	public static void SaveStarfieldTexture(){
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;
		for(int i=0;i<6;i++){
			SaveTexture(  SpaceBox.instance.starfield.starfieldTexture[i],path+"/starfield/starfield"+i.ToString()+".png");
		}
	}

	public static void SaveNebulaTexture(){
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;
		for(int i=0;i<6;i++){
			SaveTexture( SpaceBox.instance.nebulaTexture[i],path+"/nebula/nebula"+i.ToString()+".png");
		}
	}

	public static void SaveRenderTexture(){
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;
		for(int i=0;i<6;i++){
			SaveTexture( Cosmos.instance.skyFace[i],path+"/render/render"+i.ToString()+".png");
		}
	}


	public static void SetStarfieldTextureImporter(){
		for (int i=0;i<6;i++){
			SetTextureImporter( SpaceBox.instance.starfield.starfieldTexture[i],TextureImporterFormat.RGB24,SpaceBox.instance.starfield.GetStarfieldQuality2Int());
		}
	}

	public static void SetNebulaTextureImporter(){
		for (int i=0;i<6;i++){
			SetTextureImporter( SpaceBox.instance.nebulaTexture[i],TextureImporterFormat.ARGB32,SpaceBox.instance.GetNebulaQuality2Int());
		}
	}


	public static void SaveTexture( Texture2D texture,string path){
		Byte[] img = texture.EncodeToPNG();
		System.IO.File.WriteAllBytes(path, img);

		texture = null;
		
		AssetDatabase.Refresh();
	}

	public static void SetTextureImporter( Texture2D texture,TextureImporterFormat format, int size,bool readWrite=true, string path=null){
		
		// Get the path of the texture
		if (string.IsNullOrEmpty(path)){
			path = AssetDatabase.GetAssetPath( texture.GetInstanceID());
		}
		
		// Get the textureImporter object
		TextureImporter textureImporter = AssetImporter.GetAtPath(  path ) as TextureImporter;
		
		// Texture type to advanced
		textureImporter.textureType = TextureImporterType.Advanced;	
		
		// Creat a new setting
		TextureImporterSettings st = new TextureImporterSettings();
		textureImporter.ReadTextureSettings(st);
		
		// Texture must be in ARgB32
		st.textureFormat = format;
		
		// Set write/read flag
		st.readable = readWrite;
		st.maxTextureSize = size;
		st.mipmapEnabled = false;
		st.wrapMode = TextureWrapMode.Clamp;
		
		// Import the new setting
		textureImporter.SetTextureSettings(st);
		
		// Update the asset
		AssetDatabase.ImportAsset(path);	
	}

	public static void Migrate(){
		
		string sceneName = Path.GetFileNameWithoutExtension(EditorApplication.currentScene);
		
		string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox";
		CreateAssetDirectory(path,sceneName);
		CreateAssetDirectory(path+"/"+sceneName,"nebula");
		CreateAssetDirectory(path+"/"+sceneName,"starfield");
		CreateAssetDirectory(path+"/"+sceneName,"render");
		
		Cosmos.instance.realPath = sceneName;

		SaveStarfieldTexture();
		SaveNebulaTexture();

		if (Cosmos.instance.useRender){
			SaveRenderTexture();
		}

		for (int i=0;i<6;i++){

			SpaceBox.instance.starfield.starfieldTexture[i].hideFlags = HideFlags.HideAndDontSave;
			SpaceBox.instance.nebulaTexture[i].hideFlags = HideFlags.HideAndDontSave;
			Cosmos.instance.skyFace[i].hideFlags = HideFlags.HideAndDontSave;
		}

		SpaceBox.instance.starfield.starfieldTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/starfield");
		SpaceBox.instance.nebulaTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/nebula");

		SetStarfieldTextureImporter();
		SetNebulaTextureImporter();

		SpaceBox.instance.UpdateNebulaSkyBox();
		SpaceBox.instance.starfield.UpdateStarfieldSkyBox();

		if (Cosmos.instance.useRender){
			Material mat =  new Material(Shader.Find("Mobile/Skybox"));
			AssetDatabase.CreateAsset( mat, path+ "/"+ Cosmos.instance.realPath+"/skybox.mat");

			Texture2D[] skyFace = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/render");
			mat.SetTexture("_FrontTex",skyFace[0]);
			mat.SetTexture("_BackTex",skyFace[1]);
			mat.SetTexture("_LeftTex",skyFace[2]);
			mat.SetTexture("_RightTex",skyFace[3]);
			mat.SetTexture("_UpTex",skyFace[4]);
			mat.SetTexture("_DownTex",skyFace[5]);

			Material[] mats = GuiTools.GetAtPath<Material>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath );
			Cosmos.instance.skyboxMat = mats[0];


		}

		EditorApplication.SaveScene();

		EditorUtility.DisplayDialog( "Migration","The migration is complete","Ok");
	}


	public static bool CreateAssetDirectory(string rootPath,string name){
		string directory = rootPath + "/" +  name;
		if (!System.IO.Directory.Exists(directory)){
			AssetDatabase.CreateFolder(rootPath,name);
			AssetDatabase.Refresh();
			return true;
		}
		return false;
	}
}
