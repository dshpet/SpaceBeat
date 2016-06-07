/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
namespace SBGenesis{
using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class NebulaLayer{

	#region Enumeration
	public enum OverlayStyle {Style1, Style2};
	#endregion

	#region Member
	public Fractal fractal;
	public TextureTools.BlendingMode blend;
	public OverlayStyle overlay;

	public Color startColor;
	public Color endColor;
	public float threshold;

	public bool enable;
	#endregion

	public NebulaLayer(){
		blend = TextureTools.BlendingMode.Color;
		startColor = new Color(0,0,0);
		endColor= new Color(0,0,0);
		fractal = new Fractal();
		threshold = 0f;
		enable = true;
	}
	
	public Color[] Render(Fractal.Face face, int quality){

		//int size = Cosmos.instance.spaceBox.GetQuality2Int();

		Color[] colors = new Color[quality*quality];

		float[] rendu = FractalFactory.CreateNoiseTexture( fractal,face, quality);


		for( int i=0;i<rendu.Length;i++){

			float c=0;
			float min=0;
			float max=0;

			switch (blend){
			case TextureTools.BlendingMode.Color:
				c =  rendu[i];

				colors[i] = Color.Lerp( startColor,endColor,c*3);
				colors[i].a =  1;

				break;

			case TextureTools.BlendingMode.Alpha:
			case TextureTools.BlendingMode.Mask:
				min = 0f;
				max = threshold;
				c =  (((rendu[i]/2f) - min) / (max - min));
				if (c<0)c=0;
				if (c>1)c=1;
				colors[i] = Color.Lerp( startColor,endColor,c);
				colors[i].a =Mathf.Lerp( 0f,1f,c);

				break;
			}

		}

		return colors;
	}


}
}
