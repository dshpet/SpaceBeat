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

public class NebulaInspector{

	static int buttonCloudy =16;
	static int buttonVeinded =16;

	public static void Inspector(SpaceBox sb){

		if (Application.isPlaying) return;

		GuiTools.DrawTitleChapter("Nebula",12,true,0,Screen.width,Color.white);

		#region Toolbar
		if (GUILayout.Button( new GUIContent(" Add nebula",CosmosInspector.GetIcon(13) ))){
			SpaceBox.instance.starfield.Render(true);
			sb.AddNebula();
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			SpaceBox.instance.nebulaNeed2Save = true;
		}

		EditorGUILayout.Space();

		// Random
		if (GUILayout.Button(new GUIContent(" Random nebula",CosmosInspector.GetIcon(15)))){
			SpaceBox.instance.starfield.Render(true);
			sb.RandomNebula();
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			SpaceBox.instance.nebulaNeed2Save = true;

		}

		// Color
		if (GUILayout.Button( new GUIContent("Random color",CosmosInspector.GetIcon(14)))){
			SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
			SpaceBox.instance.UpdateNebulaColor();
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			SpaceBox.instance.nebulaNeed2Save = true;
		}

		GuiTools.DrawSeparatorLine();

		// Quality
		GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
		SpaceBox.NebulaQuality tmpQuality = (SpaceBox.NebulaQuality)EditorGUILayout.EnumPopup("Quality",sb.Quality);
		if (tmpQuality != sb.Quality){
			sb.Quality = tmpQuality;
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			SpaceBox.instance.nebulaNeed2Save = true;
		}
		GUI.backgroundColor = Color.white;

		EditorGUILayout.Space();
		#endregion

		#region Nebula inspector process
		int i=0;
		while (i<sb.nebula.Count){
			NebulaProperties(sb.nebula[i]);
			i++;
		}
		#endregion

		GuiTools.DrawSeparatorLine();

		// Clear
		if (GUILayout.Button(new GUIContent( " Clear",CosmosInspector.GetIcon(12)))){
			if (EditorUtility.DisplayDialog( "Delete all nebulas","Are you sure ?","Delete","Cancel")){
				SpaceBox.instance.starfield.Render(true);
				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
				SpaceBox.instance.ClearNebula();
				RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.starfield.Render();
				SpaceBox.instance.UpdateNebulaSkyBox();
				SpaceBox.instance.nebulaNeed2Save = true;
			}
		}
		
	}

	private static void NebulaProperties(Nebula nebula){

		#region Header
		EditorGUILayout.BeginHorizontal();
		Color crossColor = Color.red;
		
		int width = Screen.width-125;
		if (nebula.isWaitToDelte){
			crossColor= Color.white;
			width =  Screen.width-179;
		}

		// Delete
		if (GuiTools.Button("X",crossColor,19)){
			nebula.isWaitToDelte = !nebula.isWaitToDelte;
		}
		if (nebula.isWaitToDelte){
			if (GuiTools.Button("Delete",Color.red,50)){
				SpaceBox.instance.nebula.Remove( nebula );
				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
				RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.UpdateNebulaSkyBox();
				SpaceBox.instance.starfield.Render(true);
				SpaceBox.instance.starfield.Render();
				EditorGUILayout.EndHorizontal();
				SpaceBox.instance.nebulaNeed2Save = true;
				return;
			}
		}

		nebula.inspectorShowProperties = GuiTools.ChildFoldOut( nebula.inspectorShowProperties,nebula.name + " " + " " + nebula.Type.ToString() + " - " + nebula.nebulaMode.ToString(),new Color(183f/255f,230f/255f,252f/255f),width);

		// Up
		if (GUILayout.Button (new GUIContent("",CosmosInspector.GetIcon(23)),GUILayout.Width(19))){
			int index = SpaceBox.instance.nebula.IndexOf( nebula);
			index--;
			if (index>=0){ 

				SpaceBox.instance.nebula.Remove( nebula);
				SpaceBox.instance.nebula.Insert( index, nebula);
				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
				RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.UpdateNebulaSkyBox();
				SpaceBox.instance.starfield.Render(true);
				SpaceBox.instance.starfield.Render();
				SpaceBox.instance.nebulaNeed2Save = true;
			}
		}
		// Down
		if (GUILayout.Button (new GUIContent("",CosmosInspector.GetIcon(24)),GUILayout.Width(19))){
			int index = SpaceBox.instance.nebula.IndexOf( nebula);
			index++;
			if (index<SpaceBox.instance.nebula.Count){
				SpaceBox.instance.nebula.Remove( nebula);
				SpaceBox.instance.nebula.Insert( index, nebula);
				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
				RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.UpdateNebulaSkyBox();
				SpaceBox.instance.starfield.Render(true);
				SpaceBox.instance.starfield.Render();
				SpaceBox.instance.nebulaNeed2Save = true;
			}
		}

		// Copy
		if (GUILayout.Button (new GUIContent("C"),GUILayout.Width(19))){
			Nebula newNebula = SpaceBox.instance.AddNebula();
			newNebula.PasteNebula( nebula);
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			SpaceBox.instance.nebulaNeed2Save = true;
		}

		EditorGUILayout.EndHorizontal();
		#endregion

		#region treatment
		if (nebula.inspectorShowProperties){
			
			EditorGUI.indentLevel++;
			
			EditorGUILayout.Space();
			
			// random
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(14);
			bool autoCompute = false;
			bool random = GUILayout.Button(new GUIContent( " Rnd " + nebula.Type.ToString(),CosmosInspector.GetIcon(15)),GUILayout.Width(100));
			bool randomColor = GUILayout.Button(new GUIContent( " Rnd Color",CosmosInspector.GetIcon(14)),GUILayout.Width(100));

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			// Enable
			bool enable  = GuiTools.Toggle("Enable",nebula.enable, true);
			if (nebula.enable != enable){
				nebula.enable = enable;
				autoCompute = true;
			}
			EditorGUILayout.Space();

			// Name
			nebula.name = EditorGUILayout.TextField("Name",nebula.name);

			// Mode
			GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
			Nebula.NebulaMode mode = (Nebula.NebulaMode)EditorGUILayout.EnumPopup("Mode",nebula.nebulaMode);
			if (mode !=  nebula.nebulaMode){
				nebula.nebulaMode = mode;
				autoCompute = true;
			}
			GUI.backgroundColor = Color.white;

			EditorGUILayout.Space();

			// Type
			GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
			Nebula.NebulaType type = (Nebula.NebulaType)EditorGUILayout.EnumPopup("Type",nebula.Type);
			if (nebula.Type != type){
				nebula.Type = type;
				autoCompute=true;
			}
			GUI.backgroundColor = Color.white;

			if (random || randomColor){
				SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
			}

			switch(nebula.Type){
				case Nebula.NebulaType.Cloudy:

					if (random) {nebula.RandomCloudy(nebula.layers[1].overlay);autoCompute=true;}

					if (randomColor) {nebula.RandomCloudyColor();autoCompute=true;}
					CloudyNebula( nebula,autoCompute );
					break;
				case Nebula.NebulaType.Veined:
					if (random) {nebula.RandomVeined();autoCompute=true;}
					if (randomColor) {nebula.RandomVeinedColor();autoCompute=true;}
					VeindedNebula( nebula,autoCompute);
					break;
			}
			EditorGUI.indentLevel--;
		}
		#endregion
	}


	private static void CloudyNebula( Nebula neb,bool autoCompute){
		
		// Variation
		if (neb.layers.Count==2){

			// Variation
			neb.layers[0].fractal.seed = (EditorGUILayout.Slider("Variation",neb.layers[0].fractal.seed,1,1000));
			neb.layers[1].fractal.seed = neb.layers[0].fractal.seed;

			GuiTools.DrawSeparatorLine( 30);
			
			// Color
			neb.layers[0].startColor = EditorGUILayout.ColorField( "Color 1",neb.layers[0].startColor);
			neb.layers[0].endColor = EditorGUILayout.ColorField( "Color 2",neb.layers[0].endColor);
			
			EditorGUILayout.Space();
			
			// Mixing color;
			neb.layers[0].fractal.zoom = EditorGUILayout.Slider("Mixing colors",neb.layers[0].fractal.zoom,1,100);
			neb.power = EditorGUILayout.Slider("Power",neb.power,0f,5f);

			GuiTools.DrawSeparatorLine( 30);
			
			// Overlay
			GUI.backgroundColor = new Color(33f/255f,180f/255f,252f/255f);
			NebulaLayer.OverlayStyle overlay = (NebulaLayer.OverlayStyle)EditorGUILayout.EnumPopup( "Overlay style",neb.layers[1].overlay );
			if (neb.layers[1].overlay!=overlay){
				neb.layers[1].overlay = overlay;
				autoCompute = true;
			}
			GUI.backgroundColor = Color.white;
			
			// alpha power
			neb.layers[1].threshold = EditorGUILayout.Slider("Alpha power",neb.layers[1].threshold,0f,1f);
			
			switch (neb.layers[1].overlay){
				case NebulaLayer.OverlayStyle.Style1:
					neb.layers[1].blend = TextureTools.BlendingMode.Mask;
					//scale
					neb.layers[1].fractal.zoom = EditorGUILayout.Slider("scale",neb.layers[1].fractal.zoom-29,0f,300f)+29;
					// cutout
					neb.layers[1].fractal.threshold = EditorGUILayout.Slider("Cutout",neb.layers[1].fractal.threshold*-1,0f,1f)*-1;
					break;
				case NebulaLayer.OverlayStyle.Style2:
					neb.layers[1].blend = TextureTools.BlendingMode.Alpha;
					//scale
					neb.layers[1].fractal.zoom = EditorGUILayout.Slider("scale",neb.layers[1].fractal.zoom-29,0f,300f)+29;
					// cutout
					neb.layers[1].fractal.threshold = EditorGUILayout.Slider("Cutout",neb.layers[1].fractal.threshold,0f,1f);
					break;
			}
			
			EditorGUILayout.Space();

			if (GUI.changed){
				buttonCloudy=18;
				//SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
			}

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(14);
			if (GUILayout.Button(new GUIContent( " Render",CosmosInspector.GetIcon(buttonCloudy))) || autoCompute){
				SpaceBox.instance.starfield.Render(true);
				RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
				SpaceBox.instance.UpdateNebulaSkyBox();
				buttonCloudy = 16;
				SpaceBox.instance.nebulaNeed2Save = true;
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space();
		}
		
	}
	
	private static void VeindedNebula( Nebula neb,bool autoCompute){
		
		if (neb.layers.Count==3){
			// Variation
			neb.layers[0].fractal.seed = (EditorGUILayout.Slider("Variation 1",neb.layers[0].fractal.seed,1f,1000f));
			neb.layers[1].fractal.seed = neb.layers[0].fractal.seed/2 ;
			neb.layers[2].fractal.seed = (EditorGUILayout.Slider("Variation 2",neb.layers[2].fractal.seed,1f,1000f));

			GuiTools.DrawSeparatorLine( 30);
			
			// Color
			neb.layers[0].endColor = EditorGUILayout.ColorField( "Color 1",neb.layers[0].endColor);
			neb.layers[1].endColor = EditorGUILayout.ColorField( "Color 2",neb.layers[1].endColor);
			
			EditorGUILayout.Space();
			
			// Power
			neb.layers[0].fractal.zoom = EditorGUILayout.Slider("Turbulence",neb.layers[0].fractal.zoom,20,70);
			neb.layers[1].fractal.zoom = neb.layers[0].fractal.zoom;
			neb.power = EditorGUILayout.Slider("Power",neb.power,0f,5f);

			GuiTools.DrawSeparatorLine( 30);
			
			neb.layers[2].fractal.zoom = EditorGUILayout.Slider("scale",neb.layers[2].fractal.zoom-100,0f,200f)+100;
			neb.layers[2].fractal.threshold = EditorGUILayout.Slider("Cutout",neb.layers[2].fractal.threshold,0.1f,1f);
			
			
		}

		EditorGUILayout.Space();
	
		if (GUI.changed){
			buttonVeinded=18;
			//SpaceBox.instance.Quality = SpaceBox.NebulaQuality.Draft;
		}


		EditorGUILayout.BeginHorizontal();
		GUILayout.Space(14);
		if (GUILayout.Button(new GUIContent( " Render",CosmosInspector.GetIcon(buttonVeinded))) || autoCompute){
			SpaceBox.instance.starfield.Render(true);
			RenderNebula( SpaceBox.instance.GetNebulaQuality2Int());
			SpaceBox.instance.UpdateNebulaSkyBox();
			buttonVeinded =16;

		}
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.Space();
		
	}


	public static void RenderNebula(int size){

		Fractal.Face face = Fractal.Face.Front;

		// clear face
		bool canceled = false;
		int i=0;
		while (i<6 && !canceled){
			int nc = 1;

			Color[] dest = new Color[size*size];
			foreach( Nebula neb in SpaceBox.instance.nebula){
				if (EditorUtility.DisplayCancelableProgressBar("Rendering skybox textures",	"Render " + face.ToString() +" on Nebula " + nc.ToString(),(1f/( 6f*SpaceBox.instance.nebula.Count)) * ((i*SpaceBox.instance.nebula.Count)+nc))){
					EditorUtility.ClearProgressBar();
					return;
				}

				if (neb.enable){
					switch(i){
						case 0:
							face = Fractal.Face.Front;
							break;
						case 1:
							face = Fractal.Face.Back;
							break;
						case 2:
							face = Fractal.Face.Left;
							break;
						case 3:
							face = Fractal.Face.Right;
							break;
						case 4:
							face = Fractal.Face.Up;
							break;
						case 5:
							face = Fractal.Face.Down;
							break;
					}
					
					dest = RenderNebulaFace( dest, neb,face,size);
				}
				nc++;
			}
			SpaceBox.instance.nebulaTexture[i].SetPixels( dest);
			SpaceBox.instance.nebulaTexture[i].Apply();

			i++;
		}

		EditorUtility.ClearProgressBar();

	}

	private static Color[] RenderNebulaFace(Color[] dest,Nebula neb,Fractal.Face face,int size){
		
		Color[] source = neb.Render(face,size);

		for(int i=0;i<dest.Length;i++){

			switch (neb.nebulaMode){
			case Nebula.NebulaMode.Color:
				dest[i] += source[i];
				break;
			case Nebula.NebulaMode.ColorMask:
					float grey  = (source[i].r +  source[i].g + source[i].b)/3f;
					dest[i] = dest[i] * (Color.white - new Color(grey,grey,grey)*2)  +  source[i]*1.5f;
				break;
			case Nebula.NebulaMode.Mask:
				dest[i] = (dest[i] * source[i])*2;
				break;
			}

			// Original 
			//dest[i] = source[i] * TextureTools.GetBlendingFactor( TextureTools.BlendingMode.Color, source[i] , dest[i]) + dest[i] * TextureTools.GetBlendingFactor( TextureTools.BlendingMode.Color, source[i] , dest[i]);
			dest[i].a = (dest[i].r + dest[i].g + dest[i].b)/3f;
			
		}

		return dest;
	}
	
}
