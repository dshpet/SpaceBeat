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

[CustomEditor(typeof(PlanetSystem))]
public class PlanetSystemInspector : Editor {

	public override void OnInspectorGUI(){
		
		PlanetSystem t = (PlanetSystem)target;
		Inspector( t);
	}

	public static void Inspector(PlanetSystem p){

		GuiTools.DrawTitleChapter("Planets",12,true,0,Screen.width,Color.white);

		// Add
		if (GUILayout.Button(new GUIContent(" Add planet",CosmosInspector.GetIcon(13)))){
			Planet[] planetsI = p.GetComponentsInChildren<Planet>();
			int j=0;
			while (j<planetsI.Length){
				planetsI[j].inspectorShowProperties = false;
				j++;
			}

			AddPlanet().inspectorShowProperties=true;
			GuiTools.SetSceneCamera( 0,0);
		}

		EditorGUILayout.Space();


		// Random planet
		if (GUILayout.Button(new GUIContent(" Random planet",CosmosInspector.GetIcon(20)))){
			RandomPlanet();
		}

		// Random color
		if (GUILayout.Button(new GUIContent(" Random color",CosmosInspector.GetIcon(14)))){
			p.RandomColor();
		}

		GuiTools.DrawSeparatorLine();

		Planet[] planets = p.GetComponentsInChildren<Planet>();
		int i=0;
		while (i<planets.Length){
			EditorUtility.SetSelectedWireframeHidden( planets[i].planet.GetComponent<Renderer>(),true);
			EditorUtility.SetSelectedWireframeHidden( planets[i].atmosphere.GetComponent<Renderer>(),true);
			PlanetProperties( planets[i]);
			i++;
		}

		GuiTools.DrawSeparatorLine();
		
		if (GUILayout.Button( new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Clear all planets","Are you sure ?","Delete","Cancel")){
				p.ClearPlanets();
			}
		}
	}
	
	public static void PlanetProperties(Planet p){

		#region Header
		EditorGUILayout.BeginHorizontal();
		Color crossColor = Color.red;
		
		int width = Screen.width-90;
		if (p.isWaitToDelte){
			crossColor= Color.white;
			width =  Screen.width-134;
		}
		
		// Delete
		if (GuiTools.Button("X",crossColor,19)){
			p.isWaitToDelte = !p.isWaitToDelte;
		}
		if (p.isWaitToDelte){
			if (GuiTools.Button("Delete",Color.red,50)){
				DestroyImmediate( p.gameObject );
				return;
			}
		}
		
		bool showProperties = GuiTools.ChildFoldOut( p.inspectorShowProperties,p.name,new Color(183f/255f,230f/255f,252f/255f),width);
		if (showProperties !=p.inspectorShowProperties){
			p.inspectorShowProperties = showProperties;

			if (p.inspectorShowProperties){
				GuiTools.SetSceneCamera( -p.Latitude, p.Longitude);
			}
		}


		GUI.backgroundColor = Color.green;
		if (GUILayout.Button (new GUIContent("S"),GUILayout.Width(19))){
			GuiTools.SetSceneCamera( -p.Latitude, p.Longitude);
			p.inspectorShowProperties = true;
		}
		GUI.backgroundColor = Color.white;
		EditorGUILayout.EndHorizontal();
		#endregion
		
		if (p.inspectorShowProperties){
			EditorGUI.indentLevel++;
			PlanetInspector.Inspector( p );
			EditorGUI.indentLevel--;
		}
	}

	public static Planet AddPlanet(bool rnd = false){

		GameObject planetObj = (GameObject)Instantiate( AssetDatabase.LoadAssetAtPath("Assets/SpaceBuilderGenesis/CosmosResources/Planet/Prefab/PlanetAxis.prefab",typeof(GameObject)), Vector3.zero, Quaternion.identity);
		planetObj.name = "Planet";
		planetObj.transform.parent = PlanetSystem.instance.transform;

		Planet planet = planetObj.GetComponent<Planet>();

		planet.inspectorShowProperties = false;

		planet.Size = 200;
		planet.Distance = 500;
		planet.XAngle = 270;
		planet.YAngle = 0;

		// Material
		planet.PlanetMat = new Material(Shader.Find("Space Builder/Planet BA"));
		planet.atmosphereMat = new Material(Shader.Find("Space Builder/Atmosphere"));


		MeshRenderer mr = planet.atmosphere.GetComponent<MeshRenderer>();
		mr.material = planet.atmosphereMat;

		// diffuse
		Texture2D[] planetTexture =GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Planet/Textures/Diffuse");
		planet.PlanetMat.SetTexture("_DiffuseMap", planetTexture[Random.Range(0,planetTexture.Length-1)]);

		planet.PlanetMat.SetFloat("_EnableAmbient",0);

		// Normal
		/*
		planetTexture =GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Planet/Textures/Normal");
		if (Helper.RandomBoolean()){
			planet.PlanetMat.SetTexture("_NormalMap", planetTexture[Random.Range(0,planetTexture.Length-1)]);
		}*/

		// Atmosphere
		Color color = new Color( Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f),1);
		planet.EnableAtm = Helper.RandomBoolean();
		planet.EnableEAtm = planet.EnableAtm;
		if (planet.EnableAtm){
			color = color * (Cosmos.instance.color * Random.Range(0.4f,1f));
			color.a = 1;
			planet.AtmColor = color;

			planet.EAtmSize =1;
			planet.EAtmColor = color;
		}

		// Ring
		planet.ringMat = new Material(Shader.Find("Space Builder/Planet Ring"));
		planet.ring.GetComponent<MeshRenderer>().material = planet.ringMat;
		Texture2D[] ringTexture =GuiTools.GetAtPath<Texture2D>( "SpaceBuilderGenesis/CosmosResources/Planet/Textures/Ring");
		planet.ringMat.SetTexture("_DiffuseMap", ringTexture[Random.Range(0,ringTexture.Length-1)]);
		planet.EnableRing = false;
			
		return planet;
	}

	public static void RandomPlanet(){
		
		PlanetSystem.instance.ClearPlanets();

		Planet planet = null;
		Planet smallPlanet= null;

		// Get Sun information
		Sun sun = SunSystem.instance.GetComponentInChildren<Sun>();


		// One Big
		planet = AddPlanet();

		planet.Size = Random.Range(500,800);

		float longitude = GetOpposite(sun.Longitude,180);
		float latitude = GetOpposite(sun.Latitude,90);
		planet.Longitude =  longitude + (longitude/100 *30 * Helper.RandomBoolean2Int());
		planet.Latitude = latitude + (latitude/100 *30 * Helper.RandomBoolean2Int());

		// Distance
		if (Helper.RandomBoolean()){
			planet.Distance = Random.Range( 500 ,1000);
		}

		// Ring
		if (Helper.RandomBoolean() && Cosmos.instance.rndring){
			planet.EnableRing = true;
			planet.RingDiffusePower =0;
			planet.RingXAngle = Random.Range( -180,180);
			planet.RingYAngle = Random.Range( -90,90);
		}

		// Asteroid
		if (Helper.RandomBoolean() && Cosmos.instance.rndAstPlanet){
			Asteroid asteroid = AsteroidSystemInspector.AddAsteroid();
			asteroid.transform.position = planet.transform.position;
			asteroid.minRadius = planet.Size * Random.Range(1f,1.2f) /2f;
			asteroid.maxRadius = planet.Size * Random.Range(0.6f,3f) ;
			
			asteroid.minScale = Random.Range(1f,2f);
			asteroid.maxScale = Random.Range(8f,15f);
			
			asteroid.cloneCount = Random.Range(100,500);
			asteroid.popMethod = Helper.GetRandomEnum<Asteroid.PopMethod>();
			
			GameObject[] objAsteroids =GuiTools.GetAtPath<GameObject>( "SpaceBuilderGenesis/CosmosResources/Asteroid/Meshes");
			asteroid.gameobjectReference.Add (objAsteroids[ Random.Range(0, objAsteroids.Length-1)]);

			
			asteroid.Generate();


			asteroid.transform.parent = planet.transform;
		}


		// Small
		for (int i=0;i<3;i++){
			if (Helper.RandomBoolean()){
				smallPlanet = AddPlanet();

				smallPlanet.Size = Random.Range(10,400);
				longitude = GetOpposite(planet.Longitude + Random.Range(-50,50),180);
				latitude = Random.Range(-90,90);
				smallPlanet.Longitude =  longitude ;
				smallPlanet.Latitude = latitude;

				smallPlanet.Distance = Random.Range( 500 ,1000);
			}
		}

	}

	private static float GetOpposite(float value, float limite){
		return (limite * (Mathf.Sign(value) *-1)) +  value;	 
	}
}
