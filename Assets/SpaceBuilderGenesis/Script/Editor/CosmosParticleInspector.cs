using UnityEditor;
using UnityEngine;
using System.Collections;
using SBGenesis;

[CustomEditor(typeof(CosmosParticle))]
public class CosmosParticleInspector : Editor {

	public override void OnInspectorGUI(){
		
		CosmosParticle t = (CosmosParticle)target;

		GuiTools.DrawTitleChapter("Cosmos particles",12,true,0,Screen.width,Color.white);
		
		Inspector( t);
	}
	

	public  static void InspectorAll(){

		GuiTools.DrawTitleChapter("Cosmos particles",12,true,0,Screen.width,Color.white);

		if (GUILayout.Button( new GUIContent(" Add particle",CosmosInspector.GetIcon(13) ))){
			AddCosmosParticle();
		}

		GuiTools.DrawSeparatorLine();

		CosmosParticle[] cParticles = Cosmos.instance.transform.GetComponentsInChildren<CosmosParticle>();
		int i=0;
		while (i<cParticles.Length){

			CosmosParticleProperties( cParticles[i]);
			i++;
		}
	}

	private static void CosmosParticleProperties(CosmosParticle p){

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
		
		p.inspectorShowProperties = GuiTools.ChildFoldOut( p.inspectorShowProperties,"Cosmos particle",new Color(183f/255f,230f/255f,252f/255f),width);
		EditorGUILayout.EndHorizontal();
		if ( p.inspectorShowProperties){
			Inspector(p);
		}



	}


	private static void Inspector(CosmosParticle p){

		EditorGUILayout.Space();

		EditorGUI.indentLevel++;

		EditorGUI.BeginChangeCheck();
		SerializedObject serializedGradient = new SerializedObject(p);
		SerializedProperty colorGradient = serializedGradient.FindProperty("color");
		
		EditorGUILayout.PropertyField(colorGradient, true, null);
		if(EditorGUI.EndChangeCheck()){
			serializedGradient.ApplyModifiedProperties();
		}

		p.mat = (Material)EditorGUILayout.ObjectField("Material",p.mat,typeof(Material), true);

		EditorGUILayout.Space();
		p.maxParticle = EditorGUILayout.IntSlider("Max particle",p.maxParticle,1,500);

		GuiTools.DrawSeparatorLine(25);

		GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
		p.particleSize = (CosmosParticle.ParticleSize)EditorGUILayout.EnumPopup("Size",p.particleSize);
		GUI.backgroundColor = Color.white;

		if (p.particleSize == CosmosParticle.ParticleSize.Small){
			EditorGUILayout.MinMaxSlider(new GUIContent( string.Format( "Size {0:F2} - {1:F2}", p.minSize , p.maxSize )) ,ref p.minSize, ref p.maxSize,0.2f,5f);
		}
		else{
			EditorGUILayout.MinMaxSlider(new GUIContent( string.Format( "Size {0:F2} - {1:F2}", p.minSize , p.maxSize )) ,ref p.minSize, ref p.maxSize,5f,100f);
		}

		GuiTools.DrawSeparatorLine(25);

		p.enableRotation = GuiTools.Toggle("Enable rotation",p.enableRotation,true);
		if (p.enableRotation){
			p.rotationSpeed = EditorGUILayout.Slider("Rotation speed",p.rotationSpeed,5f,20f);
		}

		GuiTools.DrawSeparatorLine(25);

		p.enableDrift = GuiTools.Toggle("Enable drift",p.enableDrift,true);
		if (p.enableDrift){
			p.driftSpeed = EditorGUILayout.Slider("Drift speed",p.driftSpeed,1f,10f);
		}

		EditorGUI.indentLevel--;
	}

	private static void AddCosmosParticle(){

		GameObject cpObj = new GameObject("Cosmos Particle");
		cpObj.transform.parent = Cosmos.instance.transform;
		CosmosParticle cp = cpObj.AddComponent<CosmosParticle>();

		cp.color = new Gradient();
				
		GradientColorKey[] colork = new GradientColorKey[2];
		colork[0].color = Color.white;
		colork[0].time = 0.0f;
		
		colork[1].color =  Color.white;;
		colork[1].time = 1f;
		

		GradientAlphaKey[] alphak = new GradientAlphaKey[2];
		alphak[0].alpha = 1.0f;
		alphak[0].time = 0.0f;
		
		alphak[1].alpha = 1.0f;
		alphak[1].time = 0.0f;
		
		cp.color.SetKeys(colork,alphak);
	}


	private static bool GradientField(string property){
		
		EditorGUI.BeginChangeCheck();
		SerializedObject serializedGradient = new SerializedObject(SpaceBox.instance);
		SerializedProperty colorGradient = serializedGradient.FindProperty(property);
		
		EditorGUILayout.PropertyField(colorGradient, true, null);
		if(EditorGUI.EndChangeCheck()){
			serializedGradient.ApplyModifiedProperties();
			return true;
		}
		else{
			return false;
		}
	}
}
