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

[CustomEditor(typeof(Asteroid))]
public class AsteroidInspector : Editor {

	public override void OnInspectorGUI(){
		
		Asteroid t = (Asteroid)target;
		//t.gameObject.transform.hideFlags = HideFlags.HideInInspector;

		GuiTools.DrawTitleChapter("Asteroid",12,true,0,Screen.width,Color.white);

		Inspector( t);

	}

	
	public static void Inspector(Asteroid a){

		a.render2SkyBox = GuiTools.Toggle("Render to skybox",a.render2SkyBox,true);
		EditorGUILayout.Space();

		a.parent = (Transform)EditorGUILayout.ObjectField("Cosmos parent",a.parent, typeof(Transform),true);

		if (a.parent == AsteroidSystem.instance.transform){
			a.transform.position = EditorGUILayout.Vector3Field("Position",a.transform.position);
		}

		a.transform.eulerAngles = EditorGUILayout.Vector3Field("Rotation",a.transform.eulerAngles);

		EditorGUILayout.Space();

		GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
		a.popMethod = (Asteroid.PopMethod)EditorGUILayout.EnumPopup( "Method",a.popMethod);
		GUI.backgroundColor = Color.white;

		EditorGUILayout.Space();

		a.minRadius = EditorGUILayout.FloatField("Min radius",a.minRadius);
		a.maxRadius = EditorGUILayout.FloatField("Max radius",a.maxRadius);
		if (a.popMethod == Asteroid.PopMethod.Ring){
			a.height = EditorGUILayout.FloatField("Height",a.height);
		}

		EditorGUILayout.Space();

		a.minScale = EditorGUILayout.FloatField("Min scale", a.minScale);
		a.maxScale = EditorGUILayout.FloatField("Max scale", a.maxScale);

		GuiTools.DrawSeparatorLine(25);

		a.cloneCount = EditorGUILayout.IntField("Number of copies", a.cloneCount);


		EditorGUILayout.Space();

		// Reference element
		SerializedObject ast = new SerializedObject(a);
		SerializedProperty objReference = ast.FindProperty("gameobjectReference");
		bool go=true;
		while (go){
			go = EditorGUILayout.PropertyField(objReference, true, null);
				go = objReference.NextVisible(go);
		}
		ast.ApplyModifiedProperties();

		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(14);
		if (GUILayout.Button(new GUIContent("Generate",CosmosInspector.GetIcon(18)),GUILayout.Width(100))){
			a.Clear();
			a.Generate();
		}
		if (GUILayout.Button(new GUIContent("Clear",CosmosInspector.GetIcon(12)),GUILayout.Width(100))){
			a.Clear();
		}
		EditorGUILayout.EndHorizontal();

		GuiTools.DrawSeparatorLine(25);

		a.enableRotation = GuiTools.Toggle("Enable rotation",a.enableRotation,true);
		a.rotationSpeed = EditorGUILayout.Vector3Field("Rotation speed",a.rotationSpeed);
		if (a.enableRotation) a.render2SkyBox = false;
	}

	void OnSceneGUI(){
		DrawHandle( (Asteroid)target);
	}

	public static void DrawHandle( Asteroid a){

		switch (a.popMethod){
		case Asteroid.PopMethod.Sphere:
			Handles.color=Color.green;
			Handles.DrawWireDisc( a.transform.position,Vector3.up  , a.minRadius);
			Handles.DrawWireDisc( a.transform.position,Vector3.left  , a.minRadius);
			Handles.color=Color.red;
			Handles.DrawWireDisc( a.transform.position,Vector3.up  , a.maxRadius);
			Handles.DrawWireDisc( a.transform.position,Vector3.left  , a.maxRadius);
			break;
		}
	}
}
