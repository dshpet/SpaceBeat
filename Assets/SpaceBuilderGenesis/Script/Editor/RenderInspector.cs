using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using SBGenesis;

public class RenderInspector{

	public static void Inspector(){

		GuiTools.DrawTitleChapter("Rendering",12,true,0,Screen.width,Color.white);

		Cosmos.instance.noRender = GuiTools.Toggle("No skybox generation",Cosmos.instance.noRender,true);
		EditorGUILayout.Space();

		if (!Cosmos.instance.noRender){

			GuiTools.DrawSeparatorLine();

			Cosmos.instance.useRender = GuiTools.Toggle("Use pre-compute render", Cosmos.instance.useRender, true);
			GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);

			EditorGUILayout.Space();

			Cosmos.instance.skyQuality = (Cosmos.SkyboxQuality) EditorGUILayout.EnumPopup( "Skybox Quality",Cosmos.instance.skyQuality);
			GUI.backgroundColor = Color.white;
		
			EditorGUILayout.Space();

			//if (Application.HasProLicense()){
				if (GUILayout.Button("Generate")){
					if (Cosmos.instance.skyboxMat == null){
						Material mat =  new Material(Shader.Find("Mobile/Skybox"));

						string path = "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath;
						AssetDatabase.CreateAsset( mat, path+"/skybox.mat");

						Material[] mats = GuiTools.GetAtPath<Material>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath );
						Cosmos.instance.skyboxMat = mats[0];

					}
					takeScreen();
				}

			//}

			EditorGUILayout.Space();
		}
	}

	public static void takeScreen(){

		SkyboxGenerator sg = new SkyboxGenerator();
		Cosmos.instance.skyFace = sg.CreateSkyBox();

		for (int i=0;i<6;i++){
			byte[] img = Cosmos.instance.skyFace[i].EncodeToPNG();
			System.IO.File.WriteAllBytes("Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath + "/render/render" + i.ToString() +".png", img);
		}
		AssetDatabase.Refresh();

		for (int i=0;i<6;i++){
			SaveSceneTexture.SetTextureImporter(null,TextureImporterFormat.RGB24, Cosmos.instance.GetSkySize(),false, "Assets/SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath + "/render/render" + i.ToString() +".png");
		}

		Texture2D[] skyFace = GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath  +"/render");

		Material[] mats = GuiTools.GetAtPath<Material>( "SpaceBuilderGenesis/CosmosResources/Skybox/" + Cosmos.instance.realPath );

		mats[0].SetTexture("_FrontTex",skyFace[0]);
		mats[0].SetTexture("_BackTex",skyFace[1]);
		mats[0].SetTexture("_LeftTex",skyFace[2]);
		mats[0].SetTexture("_RightTex",skyFace[3]);
		mats[0].SetTexture("_UpTex",skyFace[4]);
		mats[0].SetTexture("_DownTex",skyFace[5]);

	}
}
