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


[CustomEditor(typeof(Planet))]
public class PlanetInspector : Editor {

	public override void OnInspectorGUI(){
		
		Planet t = (Planet)target;
		t.gameObject.transform.hideFlags = HideFlags.HideInInspector;
		Inspector( t);
	}
	
	public static void Inspector(Planet p){

		EditorUtility.SetSelectedWireframeHidden( p.planet.GetComponent<Renderer>(),true);
		EditorUtility.SetSelectedWireframeHidden( p.atmosphere.GetComponent<Renderer>(),true);

		p.render2SkyBox = GuiTools.Toggle("Render to skybox",p.render2SkyBox,true);
		p.name = EditorGUILayout.TextField("Name",p.name);

		EditorGUILayout.Space();

		#region Material
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(17);
		p.inspectorShowMaterial = GuiTools.ChildFoldOut( p.inspectorShowMaterial,"Material",new Color(213f/255f,250f/255f,252f/255f),Screen.width-100);
		EditorGUILayout.EndHorizontal();
		if (p.inspectorShowMaterial){
			EditorGUI.indentLevel++;
			p.PlanetMat = (Material)EditorGUILayout.ObjectField("Material",p.PlanetMat,typeof(Material),true);
			if (p.PlanetMat!=null && p.PlanetMat.name == "Space Builder/Planet BA"){
			
				#region Texture
				EditorGUILayout.Space();

				p.EnableAmbient = GuiTools.Toggle("Ambient",p.EnableAmbient,true);


				// Diffuse
				Rect rect=EditorGUILayout.BeginVertical();

				rect.height = 16;
				EditorGUI.LabelField( rect,"Diffuse");

				// Diffuse
				rect.y += 16;
				rect.height = 82;	
				rect.width = 82;
				Texture2D textureDif = (Texture2D)EditorGUI.ObjectField(rect,"",p.PlanetMat.GetTexture("_DiffuseMap"),typeof(Texture2D),false);
				if (textureDif != p.PlanetMat.GetTexture("_DiffuseMap")){
					p.PlanetMat.SetTexture( "_DiffuseMap",textureDif);
				}

				// Normal
				rect.y -= 16;
				rect.x += 100;
				EditorGUI.LabelField( rect,"Normal");
				rect.y += 16;

				rect.height = 82;	
				Texture2D textureNor = (Texture2D)EditorGUI.ObjectField(rect,"",p.PlanetMat.GetTexture("_NormalMap"),typeof(Texture2D),false);

				if (textureNor != p.PlanetMat.GetTexture("_NormalMap")){
					p.PlanetMat.SetTexture( "_NormalMap",textureNor);
				}

				EditorGUILayout.EndVertical();

				GUILayout.Space(90);
				p.PowerDiffuse = EditorGUILayout.Slider("Power diffuse",p.PowerDiffuse,0f,1f);
				#endregion
			
				GuiTools.DrawSeparatorLine(50);

				#region Atmosphere		  
				p.EnableAtm = GuiTools.Toggle("Internal Atmosphere",p.EnableAtm,true);
				if (p.EnableAtm){
					EditorGUI.indentLevel++;
					p.AtmFullBright = GuiTools.Toggle("Full bright",p.AtmFullBright,true);
					p.AtmColor = EditorGUILayout.ColorField("Color",p.AtmColor);
					p.AtmPower = EditorGUILayout.Slider("Power",p.AtmPower,0f,10f);
					p.AtmSize = EditorGUILayout.Slider("Size",p.AtmSize,0f,1f);
					EditorGUI.indentLevel--;
				}
			
				p.EnableEAtm = GuiTools.Toggle("External Atmosphere",p.EnableEAtm,true);
				if (p.EnableEAtm){
					EditorGUI.indentLevel++;
					p.EAtmFullBright = GuiTools.Toggle("Full bright",p.EAtmFullBright,true);
					p.EAtmColor = EditorGUILayout.ColorField("Color",p.EAtmColor);
					p.EAtmFallOff = EditorGUILayout.Slider("Fall Off",p.EAtmFallOff,1f,15f);
					p.EAtmSize = EditorGUILayout.Slider("Size",p.EAtmSize,0f,5f);
					EditorGUI.indentLevel--;
				}
				#endregion

			}
			EditorGUI.indentLevel--;
			EditorGUILayout.Space();
		}

		#endregion

		#region Ring
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(17);
		p.inspectorShowRing = GuiTools.ChildFoldOut( p.inspectorShowRing,"Ring",new Color(213f/255f,250f/255f,252f/255f),Screen.width-100);
		EditorGUILayout.EndHorizontal();
		if (p.inspectorShowRing){
			EditorGUI.indentLevel++;
			p.EnableRing = GuiTools.Toggle("Ring",p.EnableRing,true);
			if (p.EnableRing){

				// Diffuse
				Rect rectr = EditorGUILayout.BeginVertical();
				
				rectr.height = 16;
				rectr.x += 20;
				EditorGUI.LabelField( rectr,"Diffuse");

				rectr.y += 16;
				rectr.height = 82;	
				rectr.width = 82;
				Texture2D ringDif = (Texture2D)EditorGUI.ObjectField(rectr,"",p.ringMat.GetTexture("_DiffuseMap"),typeof(Texture2D),false);
				if (ringDif != p.PlanetMat.GetTexture("_DiffuseMap")){
					p.ringMat.SetTexture( "_DiffuseMap",ringDif);
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space(90);

				EditorGUI.indentLevel++;
				p.RingColor = EditorGUILayout.ColorField("Color",p.RingColor);
				p.RingDiffusePower = EditorGUILayout.Slider("Diffuse power",p.RingDiffusePower,0f,1f);
				p.RingTransparence = EditorGUILayout.Slider("Transparency",p.RingTransparence,0f,1f);
				EditorGUILayout.Space();
				p.RingSize = EditorGUILayout.Slider("Size",p.RingSize,0f,1f);
				p.RingXAngle = EditorGUILayout.Slider("X angle",p.RingXAngle ,-180f,180f);
				p.RingYAngle = EditorGUILayout.Slider("Y angle",p.RingYAngle ,-89f,89f);
				EditorGUI.indentLevel--;

			}
			EditorGUI.indentLevel--;
			EditorGUILayout.Space();
		}

		#endregion

		#region Position
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(17);
		p.inspectorShowPosition = GuiTools.ChildFoldOut( p.inspectorShowPosition,"Position & size",new Color(213f/255f,250f/255f,252f/255f),Screen.width-100);
		EditorGUILayout.EndHorizontal();
		if (p.inspectorShowPosition){
			EditorGUI.indentLevel++;

			p.Size = EditorGUILayout.IntSlider("Size",p.Size,10,1000);
			EditorGUILayout.Space();


			p.lockView = GuiTools.Toggle("Lock view",p.lockView,true);

			// Position
			p.Distance =  EditorGUILayout.Slider("Distance",p.Distance,10,1000);

			EditorGUI.BeginChangeCheck();
			p.Longitude = EditorGUILayout.Slider("Longitude",p.Longitude,-180f,180f);
			if (EditorGUI.EndChangeCheck() && p.lockView){
				GuiTools.SetSceneCamera( -p.Latitude, p.Longitude);
			}

			EditorGUI.BeginChangeCheck();
			p.Latitude = EditorGUILayout.Slider("latitude",p.Latitude,-90f,90f);
			if (EditorGUI.EndChangeCheck() && p.lockView){
				GuiTools.SetSceneCamera( -p.Latitude, p.Longitude);
			}


			GuiTools.DrawSeparatorLine(25);

			#region Angle
			p.XAngle = EditorGUILayout.Slider("X angle",p.XAngle-270 ,-180f,180f)+270;
			p.YAngle = EditorGUILayout.Slider("Y angle",p.YAngle ,-89f,89f);
			#endregion 
			EditorGUI.indentLevel--;
			EditorGUILayout.Space();
		}


		#endregion

		#region Rotation
		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(17);
		p.inspectorShowRotation = GuiTools.ChildFoldOut( p.inspectorShowRotation,"Auto Rotation",new Color(213f/255f,250f/255f,252f/255f),Screen.width-100);
		EditorGUILayout.EndHorizontal();
		if (p.inspectorShowRotation){
			EditorGUI.indentLevel++;

			p.enableRotation = GuiTools.Toggle("Enable rotation",p.enableRotation,true);
			p.rotationSpeed = EditorGUILayout.Vector3Field("Rotation speed",p.rotationSpeed);

			GuiTools.DrawSeparatorLine( 40);

			p.enableOrbitalRotation = GuiTools.Toggle("Enable orbital",p.enableOrbitalRotation,true);
			p.orbitalParent = (Transform)EditorGUILayout.ObjectField("Parent",p.orbitalParent, typeof(Transform),true);
			p.orbitalSpeed = EditorGUILayout.FloatField("Rotation speed",p.orbitalSpeed);
			p.orbitalVector = EditorGUILayout.Vector3Field("Orientation",p.orbitalVector);
			EditorGUI.indentLevel--;
		}

		if (p.enableRotation || p.enableOrbitalRotation){
			p.render2SkyBox = false;
		}
		#endregion

		if (GUI.changed){
			EditorUtility.SetDirty( p);
		}

		EditorGUILayout.Space();

	}
	
}
