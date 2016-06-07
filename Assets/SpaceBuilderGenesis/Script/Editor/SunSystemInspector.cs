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

[CustomEditor(typeof(SunSystem))]
public class SunSystemInspector : Editor {

	public static int randomCount = 10;

	public override void OnInspectorGUI(){

		SunSystem t = (SunSystem)target;
		Inspector( t);
	}

	public static void Inspector( SunSystem s){

		GuiTools.DrawTitleChapter("Sun",12,true,0,Screen.width,Color.white);

		if (GUILayout.Button(new GUIContent(" Add sun",CosmosInspector.GetIcon(13)))){
			AddSun();
			GuiTools.SetSceneCamera( 0,0);
		}
		
		EditorGUILayout.Space();
	
		GuiTools.DrawSeparatorLine();

		RenderSettings.ambientLight = EditorGUILayout.ColorField("Ambient",RenderSettings.ambientLight);
		
		GuiTools.DrawSeparatorLine();

		Sun[] suns = s.GetComponentsInChildren<Sun>();
		int i=0;
		while (i<suns.Length){
			SunProperties(suns[i]);
			i++;
		}
		
		GuiTools.DrawSeparatorLine();
		
		// Delete All
		if (GUILayout.Button( new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Delete all suns","Are you sure ?","Delete","Cancel")){
				SunSystem.instance.ClearSuns();
			}
			
		}
	}

	public static void SunProperties(Sun s){

		#region Header
		EditorGUILayout.BeginHorizontal();
		Color crossColor = Color.red;
		
		int width = Screen.width-90;
		if (s.isWaitToDelte){
			crossColor= Color.white;
			width =  Screen.width-134;
		}
		
		// Delete
		if (GuiTools.Button("X",crossColor,19)){
			s.isWaitToDelte = !s.isWaitToDelte;
		}
		if (s.isWaitToDelte){
			if (GuiTools.Button("Delete",Color.red,50)){
				DestroyImmediate( s.gameObject );
				return;
			}
		}
		
		bool showProperties = GuiTools.ChildFoldOut( s.inspectorShowProperties,"Sun",new Color(183f/255f,230f/255f,252f/255f),width);
		if (showProperties !=s.inspectorShowProperties){
			s.inspectorShowProperties = showProperties;
		}
		
		
		GUI.backgroundColor = Color.green;
		if (GUILayout.Button (new GUIContent("S"),GUILayout.Width(19))){
			GuiTools.SetSceneCamera( -s.Latitude, s.Longitude);
			s.inspectorShowProperties = true;
		}
		GUI.backgroundColor = Color.white;
		
		EditorGUILayout.EndHorizontal();
		#endregion

		#region treatment
		if (s.inspectorShowProperties){
			EditorGUI.indentLevel++;
			SunInspector.Inspector( s);
			EditorGUI.indentLevel--;
		}
		#endregion
	}

	public static void AddSun(bool rnd=false){

		Sun[] suns = SunSystem.instance.GetComponentsInChildren<Sun>();
		int i=0;
		while (i<suns.Length){
			suns[i].inspectorShowProperties = false;
			i++;
		}

		if (suns.Length <4){
			string sunName = "Sun" + Random.Range(1,3).ToString() +".prefab";

			GameObject sunObj = (GameObject)Instantiate( AssetDatabase.LoadAssetAtPath("Assets/SpaceBuilderGenesis/CosmosResources/Sun/Prefab/" + sunName,typeof(GameObject)), Vector3.zero, Quaternion.identity);
			sunObj.name = "Sun";
			sunObj.transform.parent = SunSystem.instance.transform;

			sunObj.transform.position = new Vector3(0,0,600);
			sunObj.transform.LookAt( Vector3.zero);

			if (rnd){
				sunObj.GetComponent<Sun>().Latitude = Random.Range (-90,90);
				sunObj.GetComponent<Sun>().Longitude = Random.Range (-180,180);
			}

			Sun sun = sunObj.GetComponent<Sun>();
			sun.pointLight.flare = sun.SunFlare;

			sunObj.GetComponent<Sun>().inspectorShowProperties = true;
		}
	}

	public static void RandomSun(){
		
		SunSystem.instance.ClearSuns();
		
		int rnd = Random.Range(1,2);
		for (int i=0;i<rnd;i++){
			AddSun( true);
		}
	}
}
