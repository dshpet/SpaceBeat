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

[CustomEditor(typeof(AsteroidSystem))]
public class AsteroidSystemInspector : Editor {

	public static int toolIndex=0;

	public override void OnInspectorGUI(){
		
		AsteroidSystem t = (AsteroidSystem)target;
		Inspector( t);
	}

	public static void Inspector(AsteroidSystem p){

		GuiTools.DrawTitleChapter("Asteroid",12,true,0,Screen.width,Color.white);

		// Add
		if (GUILayout.Button(new GUIContent(" Add asteroid field",CosmosInspector.GetIcon(13)))){

			Asteroid a = AddAsteroid();
			SelectAsteroid( a);

		}

		// Random planet
		if (GUILayout.Button(new GUIContent(" Random Asteroid",CosmosInspector.GetIcon(20)))){
			RandomAsteroid();
		}
	
		GuiTools.DrawSeparatorLine();

		// Toolbar
		Texture2D[] toolBarIcons = new Texture2D[3];
		toolBarIcons[0] = CosmosInspector.GetIcon(30);
		toolBarIcons[1] = CosmosInspector.GetIcon(27); // Move
		toolBarIcons[2] = CosmosInspector.GetIcon(28); // Rotate
		toolIndex = GUILayout.SelectionGrid( toolIndex, toolBarIcons,3);
		
		EditorGUILayout.Space();


		Asteroid[] asts = p.GetComponentsInChildren<Asteroid>();
		int i=0;
		while (i<asts.Length){
			AsteroidProperties( asts[i]);
			i++;
		}

		asts = PlanetSystem.instance.transform.GetComponentsInChildren<Asteroid>();
		i=0;
		while (i<asts.Length){
			AsteroidProperties( asts[i]);
			i++;
		}

		GuiTools.DrawSeparatorLine();
		
		if (GUILayout.Button( new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Clear all asteroids","Are you sure ?","Delete","Cancel")){
				p.ClearAsteroids();
			}
		}
	}

	public static void AsteroidProperties(Asteroid a){

		#region Header
		EditorGUILayout.BeginHorizontal();
		Color crossColor = Color.red;
		
		int width = Screen.width-90;
		if (a.isWaitToDelte){
			crossColor= Color.white;
			width =  Screen.width-134;
		}
		
		// Delete
		if (GuiTools.Button("X",crossColor,19)){
			a.isWaitToDelte = !a.isWaitToDelte;
		}
		if (a.isWaitToDelte){
			if (GuiTools.Button("Delete",Color.red,50)){
				DestroyImmediate( a.gameObject );
				return;
			}
		}
		
		bool showProperties = GuiTools.ChildFoldOut( a.inspectorShowProperties,"Asteroid " + a.popMethod.ToString(),new Color(183f/255f,230f/255f,252f/255f),width);
		if (showProperties !=a.inspectorShowProperties){
			a.inspectorShowProperties = showProperties;
			if (showProperties){
				SelectAsteroid( a);
			}
		}
		
		
		GUI.backgroundColor = Color.green;
		if (GUILayout.Button (new GUIContent("S"),GUILayout.Width(19))){
			SelectAsteroid(a);
		}
		GUI.backgroundColor = Color.white;
		EditorGUILayout.EndHorizontal();
		#endregion

		if (a.inspectorShowProperties){
			EditorGUI.indentLevel++;
			AsteroidInspector.Inspector( a );
			EditorGUI.indentLevel--;
		}
	}

	public static void RandomAsteroid(){

		AsteroidSystem.instance.ClearAsteroids();

		Asteroid asteroid = AddAsteroid();
		
		asteroid.minRadius = Random.Range(1000,1300);
		asteroid.maxRadius = Random.Range(1400,2000);
		
		asteroid.minScale = Random.Range(1f,8f);
		asteroid.maxScale = Random.Range(10f,20f);
		asteroid.cloneCount = Random.Range(100,2000);

		
		GameObject[] objAsteroids =GuiTools.GetAtPath<GameObject>( "SpaceBuilderGenesis/CosmosResources/Asteroid/Meshes");
		asteroid.gameobjectReference.Add (objAsteroids[ Random.Range(0, objAsteroids.Length-1)]);
		
		asteroid.Generate();

	}


	public static Asteroid AddAsteroid(){

		GameObject asteroidGameObject = new GameObject("Asteroid");
		asteroidGameObject.transform.parent = AsteroidSystem.instance.transform;

		return asteroidGameObject.AddComponent<Asteroid>();

	}

	private static void SelectAsteroid(Asteroid a){

		Asteroid[] asteroids = AsteroidSystem.instance.GetComponentsInChildren<Asteroid>();
		int j=0;
		while (j<asteroids.Length){
			asteroids[j].inspectorShowProperties = false;
			j++;
		}

		a.inspectorShowProperties = true;
		CosmosInspector.asteroid = a;

		GuiTools.SetSceneCamera(45,-45, new Vector3(600,600,-600),1000);
	}
}
