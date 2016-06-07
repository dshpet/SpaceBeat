/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

using UnityEngine;
using UnityEditor;
using SBGenesis;

[CustomEditor(typeof(SpaceBox))]
public class SpaceBoxInspector : Editor {

	private static int toolIndex=0;

	public override void OnInspectorGUI(){	

		SpaceBox t = (SpaceBox)target;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;

		// Toolbar
		Texture2D[] toolBarIcons = new Texture2D[3];
		toolBarIcons[0] = CosmosInspector.GetIcon(0); // starfield
		toolBarIcons[1] = CosmosInspector.GetIcon(3); // Nebula
		toolBarIcons[2] = CosmosInspector.GetIcon(5); // skybox

		toolIndex = GUILayout.SelectionGrid( toolIndex, toolBarIcons,3);

		GuiTools.DrawSeparatorLine();
			
		switch( toolIndex){
			case 0:
				StarfieldInspector.Inspector( t.starfield);
				break;
			case 1:
				NebulaInspector.Inspector( (SpaceBox)target);
				break;
			case 2:
				SkyboxInspector( (SpaceBox)target);
				break;
		}
	}


	public static void SkyboxInspector(SpaceBox sb){

		GuiTools.DrawTitleChapter("Original skybox",12,true,0,Screen.width,Color.white);

		EditorGUI.BeginChangeCheck();
		sb.skyboxTexture[0] = (Texture2D)EditorGUILayout.ObjectField("Front",sb.skyboxTexture[0],typeof(Texture2D),true);
		sb.skyboxTexture[1] = (Texture2D)EditorGUILayout.ObjectField("Back",sb.skyboxTexture[1],typeof(Texture2D),true);
		sb.skyboxTexture[2] = (Texture2D)EditorGUILayout.ObjectField("Left",sb.skyboxTexture[2],typeof(Texture2D),true);
		sb.skyboxTexture[3] = (Texture2D)EditorGUILayout.ObjectField("Right",sb.skyboxTexture[3],typeof(Texture2D),true);
		sb.skyboxTexture[4] = (Texture2D)EditorGUILayout.ObjectField("Up",sb.skyboxTexture[4],typeof(Texture2D),true);
		sb.skyboxTexture[5] = (Texture2D)EditorGUILayout.ObjectField("Down",sb.skyboxTexture[5],typeof(Texture2D),true);

		if(EditorGUI.EndChangeCheck()){
			sb.UpdateOriginalSkybox();
		}
	}

	private static void GradientField(string property){
		
		EditorGUI.BeginChangeCheck();
		SerializedObject serializedGradient = new SerializedObject(SpaceBox.instance);
		SerializedProperty colorGradient = serializedGradient.FindProperty(property);
		EditorGUILayout.PropertyField(colorGradient, true, null);
		if(EditorGUI.EndChangeCheck()){
			
			serializedGradient.ApplyModifiedProperties();
		}
	}


}


