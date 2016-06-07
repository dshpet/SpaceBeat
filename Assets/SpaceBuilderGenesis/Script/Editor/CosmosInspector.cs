/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using System.IO;
using UnityEngine;
using UnityEditor;
using SBGenesis;

[CustomEditor(typeof(Cosmos))]
public class CosmosInspector : Editor {

	public static Asteroid asteroid;

	private static Texture2D[] proSkinIcon = new Texture2D[31];
	private static int toolIndex=0;

	public override void OnInspectorGUI(){	

		if (Application.isPlaying)
			return;

		Cosmos t = (Cosmos)target;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;

		// Hide wireframe on meshes
		Renderer[] cosmosRenderer = t.transform.GetComponentsInChildren<Renderer>();
		for (int i=0;i<cosmosRenderer.Length;i++){
			EditorUtility.SetSelectedWireframeHidden( cosmosRenderer[i],true);
		}


		// Toolbar
		Texture2D[] toolBarIcons = new Texture2D[10];

		toolBarIcons[0] = GetIcon(3); // Nebula
		toolBarIcons[1] = GetIcon(0); // starfield
		toolBarIcons[2] = GetIcon(1); // starburst
		toolBarIcons[3] = GetIcon(2); // sun

		toolBarIcons[4] = GetIcon(4); // fog

		toolBarIcons[5] = GetIcon(6); // Planet
		toolBarIcons[6] = GetIcon(29); // asteroid 
		toolBarIcons[7] = GetIcon(5); // skybox
		toolBarIcons[8] = GetIcon(8); // Render
		toolBarIcons[9] = GetIcon(9); // setting

		toolIndex = GUILayout.SelectionGrid( toolIndex, toolBarIcons,5);

		EditorGUILayout.Space();

		GuiTools.DrawTitleChapter("Cosmos",12,true,0,Screen.width,Color.white);

		// Random Cosmos
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button(new GUIContent( " Cosmos",GetIcon(17)),GUILayout.Width(77))){

			t.color2 = new Color( Random.Range(0.3f,1),Random.Range(0.3f,1),Random.Range(0.3f,1));
			t.color = new Color( Random.Range(0.3f,1),Random.Range(0.3f,1),Random.Range(0.3f,1));

			// Asteroid
			AsteroidSystem.instance.ClearAsteroids();
			if (t.rndAsteroid){
				if (Helper.RandomBoolean()){
					AsteroidSystemInspector.RandomAsteroid();
				}
			}
			// sun
			SunSystemInspector.RandomSun();

			// Nebula
			SpaceBox.instance.RandomNebula();
			NebulaInspector.RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();

			// Starfield
			SpaceBox.instance.starfield.RandomStarfield();

			// Starburst
			StarburstField.instance.ClearStarbursts();
			if (t.rndStarburst){
				StarburstFieldInspector.RandomStarburstField( StarburstField.instance);
			}

			// Planet
			PlanetSystem.instance.ClearPlanets();
			if (t.rndPlanet){
				PlanetSystemInspector.RandomPlanet();
			}

			GuiTools.SetSceneCamera(0,0);
			
		}

		// colorize
		if (GUILayout.Button(new GUIContent( " Color",GetIcon(14)),GUILayout.Width(77))){
			StarburstField.instance.RandomColor();
			PlanetSystem.instance.RandomColor();

			SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
			SpaceBox.instance.UpdateNebulaColor();
			NebulaInspector.RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();

		}

		// Trash
		if (GUILayout.Button(new GUIContent( " Clear",GetIcon(12)),GUILayout.Width(77))){
		
			if (EditorUtility.DisplayDialog( "Clear all","Are you sure ?","Delete","Cancel")){

				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
				SpaceBox.instance.ClearNebula();
				NebulaInspector.RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.UpdateNebulaSkyBox();

				SpaceBox.instance.starfield.Render(true);

				PlanetSystem.instance.ClearPlanets();

				StarburstField.instance.ClearStarbursts();

				SunSystem.instance.ClearSuns();

				AsteroidSystem.instance.ClearAsteroids();

				GuiTools.SetSceneCamera(0,0);
			}
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		// General properties for random
		t.color2 = EditorGUILayout.ColorField("Doninant tint",t.color2);
		t.color = EditorGUILayout.ColorField("Doninant tint",t.color);

		// Tool buttons
		EditorGUILayout.BeginHorizontal();

		// Center to cosmos
		if (GUILayout.Button (new GUIContent(" ",CosmosInspector.GetIcon(25)),GUILayout.Width(77))){
			GuiTools.SetSceneCamera( 0, 0);
		}
		// Look all
		if (GUILayout.Button (new GUIContent(" ",CosmosInspector.GetIcon(26)),GUILayout.Width(77))){
			GuiTools.SetSceneCamera(45,-45, new Vector3(800,800,-800),500);
		}
		EditorGUILayout.EndHorizontal();

		//Toolbar process
		switch( toolIndex){
			case 1:
				StarfieldInspector.Inspector(SpaceBox.instance.starfield );
				break;
			case 2:
				StarburstFieldInspector.Inspector( StarburstField.instance);
				break;
			case 3:
				SunSystemInspector.Inspector( SunSystem.instance);
				break;
			case 0:
				NebulaInspector.Inspector( SpaceBox.instance);
				break;
			case 4:
				CosmosParticleInspector.InspectorAll();
				break;
			case 5:
				PlanetSystemInspector.Inspector( PlanetSystem.instance);
				break;
			case 6:
				AsteroidSystemInspector.Inspector( AsteroidSystem.instance);
				break;
			case 7:
				SpaceBoxInspector.SkyboxInspector( SpaceBox.instance);
				break;
			case 8:
				RenderInspector.Inspector();
				break;
			case 9:
				SettingInspector(t);
				break;
		}
	


		if (GUI.changed){
			EditorUtility.SetDirty(t);
		}
	}
	
	private void SettingInspector(Cosmos c){
	
		GuiTools.DrawTitleChapter("Camera",12,true,0,Screen.width,Color.white);

		c.SpaceCamera = (Camera)EditorGUILayout.ObjectField("Main camera",c.SpaceCamera,typeof(Camera),true);
		EditorGUILayout.Space();
		c.enableDrift = GuiTools.Toggle("Enable drift", c.enableDrift,true);
		if (c.enableDrift){
			c.driftFactor = EditorGUILayout.FloatField("drift factor",c.driftFactor);
			c.maxDriftDistance = EditorGUILayout.IntSlider("Max drift distance",c.maxDriftDistance,0,300);
		}
		EditorGUILayout.Space();
		c.copyFOV = GuiTools.Toggle("Copy FOV", c.copyFOV,true);
		if (!c.copyFOV){
			c.fov = EditorGUILayout.IntField("FOV",c.fov);
		}

		GuiTools.DrawTitleChapter("Random setting",12,true,0,Screen.width,Color.white);

		c.rndPlanet = GuiTools.Toggle("Planet",c.rndPlanet,true);
		if (c.rndPlanet){
			EditorGUI.indentLevel++;
			c.rndring =  GuiTools.Toggle("Ring",c.rndring,true);
			c.rndAstPlanet = GuiTools.Toggle("Asteroid",c.rndAstPlanet,true);
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.Space();
		c.rndStarburst = GuiTools.Toggle("Starburst",c.rndStarburst,true);
		if (c.rndStarburst){
			EditorGUI.indentLevel++;
			c.rndSbNeb = GuiTools.Toggle("Nebula",c.rndSbNeb,true);
			c.rndSbStar = GuiTools.Toggle("Star",c.rndSbStar,true);
			EditorGUI.indentLevel--;
		}
		EditorGUILayout.Space();
		c.rndAsteroid = GuiTools.Toggle("Asteroid",c.rndAsteroid,true);

		GuiTools.DrawTitleChapter("Migrating from version 1.0.0",12,true,0,Screen.width,Color.white);
		if (GUILayout.Button("Migrate")){
			SaveSceneTexture.Migrate();
		}

	}
	
	public static Texture2D GetIcon(int index){

		//if (EditorGUIUtility.isProSkin){
			string iconName="";
			switch (index){
				case 0:
					iconName = "Starfield24";
					break;
				case 1:
					iconName ="Starburst24";
					break;
				case 2:
					iconName = "Sun24";
					break;
				case 3:
					iconName = "Nebula24";
					break;
				case 4:
					iconName = "Cloud24";
					break;
				case 5:
					iconName = "skybox24";
					break;
				case 6:
					iconName = "Planet24";
					break;
				case 7:
					iconName = "Asteroid32";
					break;
				case 8:
					iconName = "Render24";
					break;
				case 9:
					iconName = "Setting24";
					break;
				case 10:
					iconName="Rnd16";
					break;
				case 11:
					iconName="Palette24";
					break;
				case 12:
					iconName="Trash16";
					break;
				case 13:
					iconName="Add16";
					break;
				case 14:
					iconName="Palette16";
					break;
				case 15:
					iconName="Nebula16";
					break;
				case 16:
					iconName="Render16";
					break;
				case 17:
					iconName="Wizard16";
					break;
				case 18:
					iconName="Render16Green";
					break;
				case 19:
					iconName="Starfield16";
					break;
				case 20:
					iconName="Planet16";
					break;
				case 21:
					iconName="Starburst16";
					break;
				case 22:
					iconName="sun16";
					break;
				case 23:
					iconName="Up16";
					break;
				case 24:	
					iconName="Down16";
					break;
				case 25:
					iconName="Center16";
					break;
				case 26:
					iconName="Global16";
					break;
				case 27:
					iconName="Move16";
					break;
				case 28:
					iconName="Rotate16";
					break;
				case 29:
					iconName="Asteroid24";
					break;
				case 30:
					iconName="None16";
					break;

			}

			if (proSkinIcon[index]==null){
				proSkinIcon[index] = (Texture2D)Resources.Load(iconName);
			}
			return proSkinIcon[index];
		//}
		//else{
		//	return null;
		//}
	}

	void OnSceneGUI(){

		if (toolIndex==6 && asteroid!=null){

			switch (AsteroidSystemInspector.toolIndex ){
			case 1:
				asteroid.transform.position = Handles.PositionHandle( asteroid.transform.position,asteroid.transform.rotation);
				break;
			case 2:
				asteroid.transform.rotation = Handles.RotationHandle( asteroid.transform.rotation, asteroid.transform.position);
				break;
			}

			switch (asteroid.popMethod){
				case Asteroid.PopMethod.Sphere:
					Handles.color=Color.green;
					Handles.DrawWireDisc( asteroid.transform.position,Vector3.up  , asteroid.minRadius);
					Handles.DrawWireDisc( asteroid.transform.position,Vector3.left  , asteroid.minRadius);
					Handles.color=Color.red;
					Handles.DrawWireDisc( asteroid.transform.position,Vector3.up  , asteroid.maxRadius);
					Handles.DrawWireDisc( asteroid.transform.position,Vector3.left  , asteroid.maxRadius);
					break;
				case Asteroid.PopMethod.Ring:

					Handles.matrix = asteroid.transform.localToWorldMatrix;

					Handles.color=Color.green;
					Handles.DrawWireDisc( new Vector3(0,asteroid.height/2,0),Vector3.down  , asteroid.minRadius);
					Handles.DrawWireDisc( new Vector3(0,-asteroid.height/2,0) ,Vector3.down, asteroid.minRadius);
						
					Handles.color=Color.red;
					Handles.DrawWireDisc( new Vector3(0,asteroid.height/2,0),Vector3.down , asteroid.maxRadius);
					Handles.DrawWireDisc(  new Vector3(0,-asteroid.height/2,0) ,Vector3.down , asteroid.maxRadius);
					break;
			}
		}
	}
}

