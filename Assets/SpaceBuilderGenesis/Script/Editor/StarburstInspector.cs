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

[CustomEditor(typeof(StarBurst))]
public class StarburstInspector : Editor {

	public override void OnInspectorGUI(){	
		
		StarBurst t = (StarBurst)target;
		t.gameObject.GetComponent<MeshRenderer>().hideFlags = HideFlags.HideInInspector;
		t.gameObject.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;

		GuiTools.DrawTitleChapter("Starburst field",12,true,0,Screen.width,Color.white);

		Inspector(t);

		if (GUI.changed){
			EditorUtility.SetDirty( t);
		}
	}

	public static void Inspector( StarBurst sb){

		EditorGUILayout.Space();

		// Color
		EditorGUI.BeginChangeCheck();
		Color color = EditorGUILayout.ColorField("Color",sb.material.GetColor( "_Color1"));
		if(EditorGUI.EndChangeCheck()){
			sb.material.SetColor("_Color1",color);
		}

		EditorGUI.BeginChangeCheck();
		color = EditorGUILayout.ColorField("Color",sb.material.GetColor( "_Color2"));
		if(EditorGUI.EndChangeCheck()){
			sb.material.SetColor("_Color2",color);
		}

		// Power
		EditorGUI.BeginChangeCheck();
		float power = EditorGUILayout.Slider("Power",sb.material.GetFloat( "_Power"),0,1);
		if(EditorGUI.EndChangeCheck()){
			sb.material.SetFloat("_Power",power);
		}


		Rect rect=EditorGUILayout.BeginVertical();
		rect.height = 82;	
		rect.width = 82;
		Texture2D textureDif = (Texture2D)EditorGUI.ObjectField(rect,"",sb.material.GetTexture("_DiffuseMap"),typeof(Texture2D),false);
		if (textureDif != sb.material.GetTexture("_DiffuseMap")){
			sb.material.SetTexture( "_DiffuseMap",textureDif);
		}
		EditorGUILayout.EndVertical();
		GUILayout.Space(85);


		sb.SizeX = EditorGUILayout.Slider("Size X",sb.SizeX,0.0f,5000.0f);
		sb.SizeY = EditorGUILayout.Slider("Size Y",sb.SizeY,0.0f,5000.0f);

		sb.Rotation =  EditorGUILayout.Slider("Rotation",sb.Rotation,-180f,180f);
		EditorGUILayout.Space();
		
		// Position
		EditorGUI.BeginChangeCheck();
		sb.Longitude = EditorGUILayout.Slider("Longitude",sb.Longitude,-180f,180f);
		if (EditorGUI.EndChangeCheck()){
			GuiTools.SetSceneCamera( -sb.Latitude, sb.Longitude);
		}

		EditorGUI.BeginChangeCheck();
		sb.Latitude = EditorGUILayout.Slider("latitude",sb.Latitude,-90,90f);
		if (EditorGUI.EndChangeCheck()){
			GuiTools.SetSceneCamera( -sb.Latitude, sb.Longitude);
		}

		if (GUI.changed){
			EditorUtility.SetDirty( sb);
		}


	}



}
