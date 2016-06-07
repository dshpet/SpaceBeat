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

public class StarfieldInspector{

	public static void Inspector(Starfield s){

		GuiTools.DrawTitleChapter("Starfield",12,true,0,Screen.width, Color.white);

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button( new GUIContent( " Random starfield",CosmosInspector.GetIcon(19)))){
			s.RandomStarfield();
		}

		EditorGUILayout.EndHorizontal();

		GuiTools.DrawSeparatorLine();

		// Quality
		GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
		Starfield.StarfieldQuality tmpQuality = (Starfield.StarfieldQuality)EditorGUILayout.EnumPopup( "Quality",s.Quality);
		if (tmpQuality != s.Quality){
			s.Quality = tmpQuality;

			for(int i=0;i<6;i++){
				s.starfieldTexture[i].Resize( s.GetStarfieldQuality2Int(), s.GetStarfieldQuality2Int());

			}
			SaveSceneTexture.CreateStarfieldTexture();
			s.RandomStarfield();
		}


		GUI.backgroundColor = Color.white;
		GuiTools.DrawSeparatorLine();

		StarboxInspector(s.cosmosStarfield,"Cosmos",0);
		EditorGUILayout.Space();
		StarboxInspector(s.nebulaStarfield,"Nebula",1);

		GuiTools.DrawSeparatorLine();

		if (GUILayout.Button( new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Clear starfield","Are you sure ?","Delete","Cancel")){
				s.Render(true);
			}
		}
	}

	public static void StarboxInspector( StarfieldBox sb,string label, int type){

		sb.inspectorShowProperties = GuiTools.ChildFoldOut(sb.inspectorShowProperties,label,new Color(183f/255f,230f/255f,252f/255f),Screen.width-40);

		if (sb.inspectorShowProperties){
			EditorGUI.indentLevel++;

			sb.Enable = GuiTools.Toggle("Enable",sb.Enable);
			EditorGUILayout.Space();

			// Cluster
			sb.ClusterCount = EditorGUILayout.IntSlider( "Starcluster", sb.ClusterCount ,1,50);

			sb.Mixing = EditorGUILayout.Slider("Mixing",sb.Mixing,0f,1f);


			EditorGUILayout.Space();

			//star
			if (type==0){

				if (GradientField("starfield.cosmosStarfield.gradient")){
					SpaceBox.instance.starfield.Render();
				}
				sb.Intensity = EditorGUILayout.Slider("Intensity",sb.Intensity,0f,2f);
			}
			else{

				if (GradientField("starfield.nebulaStarfield.gradient")){
					SpaceBox.instance.starfield.Render();
				}
				sb.Intensity = EditorGUILayout.Slider("Intensity",sb.Intensity,1f,20f);
			}

			if (type==0){
				sb.Threshold = EditorGUILayout.Slider("Threshold",sb.Threshold,0,0.5f);
			}
			else{
				sb.Threshold = EditorGUILayout.Slider("Threshold",sb.Threshold,0f,0.5f);
			}
			
			EditorGUILayout.Space();

			sb.SmallCount = EditorGUILayout.IntSlider("Small start",sb.SmallCount,0,16000);
			sb.MediumCount = EditorGUILayout.IntSlider("Medium start",sb.MediumCount,0,2400);
			sb.LargeCount = EditorGUILayout.IntSlider("Large start",sb.LargeCount,0,1000);

			EditorGUI.indentLevel--;

		}

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
