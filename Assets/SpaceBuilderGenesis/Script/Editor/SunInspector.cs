/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;
using SBGenesis;

[CustomEditor(typeof(Sun))]
public class SunInspector : Editor {

	public override void OnInspectorGUI(){	
		
		Sun t = (Sun)target;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;
		
		GuiTools.DrawTitleChapter("Sun",12,true,0,Screen.width,Color.white);
		
		Inspector(t);
		
		if (GUI.changed){
			EditorUtility.SetDirty( t);
		}
	}

	public static void Inspector(Sun s){

		s.noFlare = GuiTools.Toggle("No lensflare",s.noFlare,true);
		if (!s.noFlare){
			s.SunFlare = (Flare)EditorGUILayout.ObjectField("Flare",s.SunFlare,typeof(Flare),true);
		}

		EditorGUILayout.Space();
		s.onlyFlare = GuiTools.Toggle("Only lensflare",s.onlyFlare,true);
		if (!s.onlyFlare){
			s.directionalLight.color = EditorGUILayout.ColorField( "Light color",s.directionalLight.color);
		}

		EditorGUILayout.Space();

		s.lockView = GuiTools.Toggle("Lock view",s.lockView,true);

		// Position
		EditorGUI.BeginChangeCheck();
		s.Longitude = EditorGUILayout.Slider("Longitude",s.Longitude,-180f,180f);
		if (EditorGUI.EndChangeCheck()){
			if (s.lockView)
				GuiTools.SetSceneCamera( -s.Latitude, s.Longitude);
		}
		
		EditorGUI.BeginChangeCheck();
		s.Latitude = EditorGUILayout.Slider("latitude",s.Latitude,-90f,90f);
		if (EditorGUI.EndChangeCheck()){
			if (s.lockView)
				GuiTools.SetSceneCamera( -s.Latitude, s.Longitude);
		}


		if (GUI.changed){
			EditorUtility.SetDirty( s);
		}
	}

}
