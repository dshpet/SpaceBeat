/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using UnityEngine;
using UnityEditor;
using System.Collections;
using SBGenesis;

[CustomEditor(typeof(StarburstField))]
public class StarburstFieldInspector : Editor {

	//public static int randomCount = 10;

	public override void OnInspectorGUI(){	

		StarburstField t = (StarburstField)target;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;

		Inspector( t);
	}

	public static void Inspector(StarburstField sf){

		GuiTools.DrawTitleChapter("Starburst",12,true,0,Screen.width,Color.white);


		if (GUILayout.Button(new GUIContent(" Add starburst",CosmosInspector.GetIcon(13)))){
			AddStarburst();
			GuiTools.SetSceneCamera( 0,0);
		}

		EditorGUILayout.Space();

		if (GUILayout.Button(new GUIContent(" Random starburst",CosmosInspector.GetIcon(21)))){
			RandomStarburstField(sf);
		}

		if (GUILayout.Button(new GUIContent(" Random color",CosmosInspector.GetIcon(14)))){
			sf.RandomColor();
		}

		GuiTools.DrawSeparatorLine();

		StarBurst[] starbursts = sf.GetComponentsInChildren<StarBurst>();
		int i=0;
		while (i<starbursts.Length){
			StarburstProperties(starbursts[i]);
			i++;
		}

		GuiTools.DrawSeparatorLine();

		// Delete All
		if (GUILayout.Button( new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Delete all starburst","Are you sure ?","Delete","Cancel")){
				sf.ClearStarbursts();
			}
			
		}
	}

	public static void StarburstProperties(StarBurst sb ){

		#region Header
		EditorGUILayout.BeginHorizontal();
		Color crossColor = Color.red;
		
		int width = Screen.width-90;
		if (sb.isWaitToDelte){
			crossColor= Color.white;
			width =  Screen.width-134;
		}
		
		// Delete
		if (GuiTools.Button("X",crossColor,19)){
			sb.isWaitToDelte = !sb.isWaitToDelte;
		}
		if (sb.isWaitToDelte){
			if (GuiTools.Button("Delete",Color.red,50)){
				DestroyImmediate( sb.gameObject );
				return;
			}
		}

		bool showProperties = GuiTools.ChildFoldOut( sb.inspectorShowProperties,"Starburst",new Color(183f/255f,230f/255f,252f/255f),width);
		if (showProperties !=sb.inspectorShowProperties){
			sb.inspectorShowProperties = showProperties;
			
			if (sb.inspectorShowProperties){
				GuiTools.SetSceneCamera( -sb.Latitude, sb.Longitude);
			}
		}


		GUI.backgroundColor = Color.green;
		if (GUILayout.Button (new GUIContent("S"),GUILayout.Width(19))){

			StarBurst[] starbursts = StarburstField.instance.GetComponentsInChildren<StarBurst>();
			int i=0;
			while (i<starbursts.Length){
				starbursts[i].inspectorShowProperties = false;
				i++;
			}

			sb.inspectorShowProperties = true;
			GuiTools.SetSceneCamera( -sb.Latitude, sb.Longitude);
			sb.inspectorShowProperties = true;
		}
		GUI.backgroundColor = Color.white;

		EditorGUILayout.EndHorizontal();
		#endregion

		#region treatment
		if (sb.inspectorShowProperties){
			EditorGUI.indentLevel++;
			StarburstInspector.Inspector( sb);
			EditorGUI.indentLevel--;
		}
		#endregion
	}

	public static void AddStarburst(bool rnd=false){

		GameObject starObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
		starObj.name = "Starburst";
		starObj.layer = 31;

		starObj.transform.parent = StarburstField.instance.transform;
		starObj.transform.position = new Vector3(0,0,600);

		// Component
		StarBurst sb = starObj.AddComponent<StarBurst>();
		if (rnd){
			sb.SizeX = Random.Range(80,150);
			sb.SizeY =	sb.SizeX;
			sb.Longitude = Random.Range(-180,180);
			sb.Latitude =  Random.Range(-90,90);
		}
		else{
			sb.SizeX = 300;
			sb.SizeY = 300;
			sb.Longitude = Random.Range(0,0);
			sb.Latitude =  Random.Range(0,0);
		}

		DestroyImmediate( starObj.GetComponent<MeshCollider>());
		
		MeshRenderer mr = starObj.GetComponent<MeshRenderer>();
		mr.shadowCastingMode= UnityEngine.Rendering.ShadowCastingMode.Off;
		mr.receiveShadows = false;
		mr.hideFlags = HideFlags.HideInInspector;
		
		starObj.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;

		bool nebOrStar = Helper.RandomBoolean();
		string pathtex = "SpaceBuilderGenesis/CosmosResources/Starburst/Textures";
		sb.material = new Material(Shader.Find("Space Builder/Nebula"));
		sb.material.SetColor("_Color1",Color.white);
		sb.material.SetColor("_Color2",Color.white);

		if ((nebOrStar && Cosmos.instance.rndSbNeb) || (Cosmos.instance.rndSbNeb && !Cosmos.instance.rndSbStar) ){
			pathtex = "SpaceBuilderGenesis/CosmosResources/Nebula/Textures";
			sb.SizeX = Random.Range(500,1000);
			sb.SizeY = sb.SizeX ;
			Color color = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1) * Cosmos.instance.color;
			Color color2 = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1) * Cosmos.instance.color2;
			sb.material.SetColor("_Color1",color);
			sb.material.SetColor("_Color2",color2);
		}

		Texture2D[] flareTextures =GuiTools.GetAtPath<Texture2D>( pathtex);

		sb.material.SetTexture("_DiffuseMap", flareTextures[ Random.Range(0,flareTextures.Length)]);
		sb.material.SetFloat("_Power",Random.Range(0f,0.2f));
		mr.material = sb.material;

	}

	public static void RandomStarburstField(StarburstField sf){

		sf.ClearStarbursts();

		int randomCount = Random.Range(0,20);
		for(int s=0;s<randomCount;s++){
			AddStarburst( true);
		}

	}
	
}
