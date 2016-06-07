/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Nebula {

	#region enumeration
	public enum NebulaType { Cloudy, Veined };
	public enum NebulaMode { Color, ColorMask,Mask};
	#endregion

	#region Members
	public bool enable;

	public string name;

	public NebulaMode nebulaMode;

	[SerializeField]
	private NebulaType type;
	public NebulaType Type {
		get {
			return type;
		}
		set {
			if (type != value){
				type = value;
				layers.Clear();
				CreateNebula( type);
			}
		}
	}
		
	[SerializeField]
	public List<NebulaLayer> layers;

	public float power;

	public bool inspectorShowProperties;
	public bool isWaitToDelte;
	#endregion
	
	#region Constructor
	public Nebula(){
		type = NebulaType.Cloudy;
		name = "New Nebula";
		nebulaMode = NebulaMode.Color;
		layers = new List<NebulaLayer>();
		isWaitToDelte = false;
		inspectorShowProperties = false;
		power=1;
		enable = true;
	}
	#endregion

	#region Public Method
	public void CreateNebula(NebulaType type){

		this.type = type;

		switch( type){
			case NebulaType.Cloudy:

				NebulaLayer layer = new NebulaLayer();
				layers.Add( layer);
										
				layer = new NebulaLayer();
				layers.Add( layer);
				
				RandomCloudyColor();
				RandomCloudy(Helper.GetRandomEnum<NebulaLayer.OverlayStyle>());

				break;

			case NebulaType.Veined:
				layer = new NebulaLayer();
				layers.Add( layer);

				layer = new NebulaLayer();
				layers.Add( layer);
				
				layer = new NebulaLayer();
				layers.Add( layer);

				RandomVeined();
				RandomVeinedColor();
				break;
		}


	}
	
	public void RandomCloudy(NebulaLayer.OverlayStyle overStyle){

		int variation = UnityEngine.Random.Range(1,1000);

		layers[0].enable = true;
		layers[0].blend = TextureTools.BlendingMode.Color;
		layers[0].fractal.type = Fractal.FractalType.Fbm;
		layers[0].fractal.octave = 8;
		layers[0].fractal.zoom = UnityEngine.Random.Range(40f,100f);
		layers[0].fractal.seed = variation;
		layers[0].fractal.lacunarity = 2;
		layers[0].fractal.frequency = 2;
		layers[0].fractal.gain=0;
		layers[0].fractal.offset=0;
		layers[0].fractal.threshold=0;
		layers[0].fractal.power =1;

		layers[1].enable = true;
		layers[1].blend = TextureTools.BlendingMode.Mask;
		layers[1].fractal.type = Fractal.FractalType.Ridged;
		layers[1].fractal.octave = 8;
		layers[1].fractal.seed = variation;
		layers[1].fractal.lacunarity = 1.8f;
		layers[1].fractal.frequency =0.5f;
		layers[1].fractal.gain = 25f;

		layers[1].fractal.offset = 0.48f;
		layers[1].fractal.threshold = 0;
		layers[1].fractal.power =1;

		NebulaLayer.OverlayStyle over = Helper.GetRandomEnum<NebulaLayer.OverlayStyle>();

		over = overStyle;
		switch (over){
			case NebulaLayer.OverlayStyle.Style1:
				layers[1].blend = TextureTools.BlendingMode.Mask;
				layers[1].overlay = over; 
				layers[1].fractal.zoom = UnityEngine.Random.Range(30,300);//120

				this.power = UnityEngine.Random.Range(0.9f,1.2f);
				layers[1].threshold = UnityEngine.Random.Range(0.2f,0.3f );

				layers[1].fractal.threshold  = 0; 
				break;
			case NebulaLayer.OverlayStyle.Style2:
				layers[1].blend = TextureTools.BlendingMode.Alpha;
				layers[1].overlay = over; 
				layers[1].fractal.zoom = UnityEngine.Random.Range(29,69);

				this.power = UnityEngine.Random.Range(1f,1.8f);
				layers[1].threshold = UnityEngine.Random.Range(0.25f,0.5f );
				
				layers[1].fractal.threshold  = UnityEngine.Random.Range(0.25f, 0.4f);
				break;
		}

	}

	public void RandomCloudyColor(){

		layers[0].startColor = new Color(UnityEngine.Random.Range(0.3f,1f)*0.5f,UnityEngine.Random.Range(0.3f,1f)*0.5f,UnityEngine.Random.Range(0.3f,1f)*0.5f) * Cosmos.instance.color2;
		layers[0].endColor = new Color(UnityEngine.Random.Range(0.4f,1f),UnityEngine.Random.Range(0.4f,1f),UnityEngine.Random.Range(0.4f,1f)) * Cosmos.instance.color;
	}
	
	public void RandomVeined(){

		int variation = UnityEngine.Random.Range(1,1000);

		this.power = UnityEngine.Random.Range(1.6f,2.0f);

		layers[0].enable = layers[1].enable = true;
		layers[0].blend = layers[1].blend = TextureTools.BlendingMode.Color;
		layers[0].fractal.type = layers[1].fractal.type = Fractal.FractalType.Ridged;
		layers[0].fractal.octave = layers[1].fractal.octave =8;
		layers[0].fractal.seed = variation;
		layers[1].fractal.seed = layers[0].fractal.seed/2 ;

		layers[0].fractal.lacunarity = layers[1].fractal.lacunarity =2.5f;
		layers[0].fractal.frequency = layers[1].fractal.frequency = 0.8f;
		layers[0].fractal.gain = layers[1].fractal.gain = 50f; 
		layers[0].fractal.zoom = layers[1].fractal.zoom = UnityEngine.Random.Range(20,70);
		layers[0].threshold = layers[1].threshold = 0;
		layers[0].fractal.threshold  = layers[1].fractal.threshold  = 0;		
		layers[0].fractal.offset = layers[1].fractal.offset = 0.324f;
		layers[0].fractal.threshold = layers[0].fractal.threshold =0;
		layers[0].fractal.power = layers[1].fractal.power = 1.7f;
		layers[0].startColor = layers[1].startColor = Color.black;
	
		layers[2].enable = true;
		layers[2].blend = TextureTools.BlendingMode.Alpha;
		layers[2].fractal.type = Fractal.FractalType.Ridged;
		layers[2].fractal.octave =7;
		layers[2].fractal.seed = UnityEngine.Random.Range(1,1000);
		layers[2].fractal.lacunarity = 2f;
		layers[2].fractal.frequency =0.2f;
		layers[2].fractal.gain = 50f; 
		layers[2].fractal.zoom = UnityEngine.Random.Range(100,200);
		layers[2].fractal.offset = 0.4f;
		layers[2].fractal.threshold = UnityEngine.Random.Range(0.5f,0.8f);
		layers[2].threshold = 0.6f;

		layers[2].fractal.power =1.4f;

	}

	public void RandomVeinedColor(){
		
		layers[0].endColor = new Color(UnityEngine.Random.Range(0.3f,1f)* 0.5f,UnityEngine.Random.Range(0.3f,1f)* 0.5f,UnityEngine.Random.Range(0.3f,1f)* 0.5f) * Cosmos.instance.color2;
		layers[1].endColor = new Color(UnityEngine.Random.Range(0.4f,1f),UnityEngine.Random.Range(0.4f,1f),UnityEngine.Random.Range(0.4f,1f)) * Cosmos.instance.color;
		
		layers[2].startColor = Color.black;
		layers[2].endColor = Color.black;

	}
	
	public Color[] Render(Fractal.Face face, int quality){

		Color[] dest = new Color[quality*quality];

		foreach(NebulaLayer layer in layers){

			if (layer.enable) {

				Color[] source = layer.Render(face,quality);

				TextureTools.BlendingMode lastBend = TextureTools.BlendingMode.Color;

				for(int i=0;i<dest.Length;i++){
					dest[i] = source[i] * TextureTools.GetBlendingFactor( layer.blend, source[i] , dest[i]) + dest[i] * TextureTools.GetBlendingFactor( lastBend, source[i] , dest[i]);
					lastBend =  layer.blend;
					dest[i].a=1;

				}
			}

		}
		for( int i=0;i<dest.Length;i++){
			dest[i] *= power;
		}

		dest[0]=dest[1];

		return dest;
	}

	public void PasteNebula( Nebula neb){

		Type = neb.type;

		switch (Type){
			case NebulaType.Veined:

				// Variation
				layers[0].fractal.seed = neb.layers[0].fractal.seed;
				layers[1].fractal.seed = neb.layers[0].fractal.seed;
				layers[2].fractal.seed = neb.layers[2].fractal.seed;
				
				// Color
				layers[0].endColor = neb.layers[0].endColor;
				layers[1].endColor = neb.layers[1].endColor;

				
				// Power
				layers[0].fractal.zoom = neb.layers[0].fractal.zoom;
				layers[1].fractal.zoom = layers[0].fractal.zoom;
				power = neb.power;
				
				layers[2].fractal.zoom = neb.layers[2].fractal.zoom;
				layers[2].fractal.threshold = neb.layers[2].fractal.threshold;

				break;

			case NebulaType.Cloudy:

				layers[0].startColor = neb.layers[0].startColor;
				layers[0].endColor = neb.layers[0].endColor;

				layers[0].fractal.zoom = neb.layers[0].fractal.zoom;
				layers[0].fractal.seed = neb.layers[0].fractal.seed;
				layers[0].fractal.lacunarity = neb.layers[0].fractal.lacunarity;
				layers[0].fractal.frequency = neb.layers[0].fractal.frequency;
				layers[0].fractal.gain= neb.layers[0].fractal.gain;
				layers[0].fractal.offset= neb.layers[0].fractal.offset;
				layers[0].fractal.threshold= neb.layers[0].fractal.threshold;
				layers[0].fractal.power = neb.layers[0].fractal.power;
			
				layers[1].fractal.seed = neb.layers[1].fractal.seed;
				layers[1].fractal.lacunarity = neb.layers[1].fractal.lacunarity;
				layers[1].fractal.frequency = neb.layers[1].fractal.frequency;
				layers[1].fractal.gain = neb.layers[1].fractal.gain;
				
				layers[1].fractal.offset =  neb.layers[1].fractal.offset;
				layers[1].fractal.threshold = neb.layers[1].fractal.threshold;
				layers[1].fractal.power = neb.layers[1].fractal.power;
			
			
				layers[1].blend = neb.layers[1].blend;
				layers[1].overlay = neb.layers[1].overlay;
				layers[1].fractal.zoom = neb.layers[1].fractal.zoom;
				this.power = neb.power;
				layers[1].threshold = neb.layers[1].threshold;
				layers[1].fractal.threshold  = neb.layers[1].fractal.threshold;
				
				break;

		}
	}
#endregion

}
}
