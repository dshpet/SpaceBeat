using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using SBGenesis;

public class MenuSBG {

	[MenuItem ("Tools/Space builder/Genesis/Create cosmos",false,1)]
	static void CreateCosmos(){
		
		Cosmos.instance.Create();
		SunSystemInspector.AddSun(true);

		// Create Temporary texture
		if (string.IsNullOrEmpty( Cosmos.instance.realPath)){

			// Create Path
			Cosmos.instance.realPath = "_tmp";

			// Create dirtectory
			SaveSceneTexture.CreateAssetDirectory("Assets/SpaceBuilderGenesis/CosmosResources/Skybox","_tmp");
			SaveSceneTexture.CreateAssetDirectory("Assets/SpaceBuilderGenesis/CosmosResources/Skybox/_tmp","starfield");
			SaveSceneTexture.CreateAssetDirectory("Assets/SpaceBuilderGenesis/CosmosResources/Skybox/_tmp","nebula");
			SaveSceneTexture.CreateAssetDirectory("Assets/SpaceBuilderGenesis/CosmosResources/Skybox/_tmp","render");

			// Create starfield texture
			SaveSceneTexture.CreateStarfieldTexture();

			// Create Nebula Texture
			SaveSceneTexture.CreateNebulaTexture();
		}


		// Select & set camera position
		Selection.activeGameObject = Cosmos.instance.gameObject;

		try{
			SceneView.currentDrawingSceneView.m_SceneLighting = true;
			GuiTools.SetSceneCamera(0,0);
		}
		catch{};

	}



}


/*
			// Create texture
			for (int i=0;i<6;i++){
				// Starfield
				int quality = SpaceBox.instance.starfield.GetStarfieldQuality2Int();
				SaveSceneTexture.SaveTexture( quality,path+"/starfield/starfield"+i.ToString()+".png");
				// Nebula
				quality = SpaceBox.instance.GetNebulaQuality2Int();
				SaveSceneTexture.SaveTexture( quality,path+"/nebula/nebula"+i.ToString()+".png");
			}*/

/*
			// Save texture
			SpaceBox.instance.starfield.starfieldTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/_tmp/starfield");
			SpaceBox.instance.nebulaTexture = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/_tmp/nebula");


			for (int i=0;i<6;i++){
				int quality = SpaceBox.instance.starfield.GetStarfieldQuality2Int();
				GuiTools.SetReadWrite( SpaceBox.instance.starfield.starfieldTexture[i],true,TextureImporterFormat.RGB24,quality);

				quality = SpaceBox.instance.GetNebulaQuality2Int();
				GuiTools.SetReadWrite( SpaceBox.instance.nebulaTexture[i],true,TextureImporterFormat.RGB24,quality);
			}*/