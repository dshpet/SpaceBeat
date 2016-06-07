/***********************************************
			Texture Tools
	Copyright © 2014-2015 Dark Anvil
		http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using UnityEngine;
using System.Collections;

public static class TextureTools{

	public enum BlendingMode {Color,Alpha,Mask};

	public static Color[] MakeSmealess(Color[] origine){

		int N = (int)Mathf.Sqrt (origine.Length);

		Color[] diagonal = new Color[N*N];
		float[] mask = new float[N*N];
		Color[] tile =  new Color[N*N];

		// Diagonale image
		for (int j=0;j<N;j++) {
			for (int i=0;i<N;i++) {
				diagonal[((i+N/2)%N) + ((j+N/2)%N) * N] = origine[i +j*N];
			}
		}

		// Mask
		for (int i=0;i<N/2;i++) {
			for (int j=0;j<N/2;j++) {
				float d=0;
				/* d ranges from 0 to 1 */
				//switch (masktype) { 
				//case RADIAL:
				//	d = Mathf.Sqrt((i-N/2)*(i-N/2) + (float)(j-N/2)*(j-N/2)) / (N/2);
				//	break;
				//case LINEAR:
					d = Mathf.Max((N/2-i),(N/2-j)) / (float)(N/2); 
				//	break;
				//}
				/* Scale d to range from 1 to 255 */
				d = 1 -  d;
				if (d < 0) d = 0;
				if (d > 1) d = 1;
				/* Form the mask in each quadrant */

				//Color col = new Color(d,d,d);
				mask[i+j*N] = d;
				mask[i + (N-1-j)*N] = d;
				mask[(N-1-i)+ j*N] = d;
				mask[(N-1-i)+(N-1-j)*N] = d;
			}
		}

		for (int j=0;j<N;j++) {
			for (int i=0;i<N;i++) {
				float a1 = mask[i+j*N];
				float a2 = mask[((i+N/2)%N) + ((j+N/2)%N)*N];
				tile[i+j*N].r = a1*origine[i+j*N].r/(a1+a2) + a2*diagonal[i+j*N].r/(a1+a2);
				tile[i+j*N].g = a1*origine[i+j*N].g/(a1+a2) + a2*diagonal[i+j*N].g/(a1+a2);
				tile[i+j*N].b = a1*origine[i+j*N].b/(a1+a2) + a2*diagonal[i+j*N].b/(a1+a2);
			}
		}

		return tile;
	}

	public static Color GetBlendingFactor( BlendingMode blend, Color s, Color d){
		
		Color factor = new Color(1,1,1,1);
		
		switch (blend) {
		case BlendingMode.Color:
			factor = Color.white;
			break;
		case BlendingMode.Mask:
			factor = factor - new Color( s.a, s.a, s.a, s.a);
			break;
		case BlendingMode.Alpha:
			factor = new Color( s.a, s.a, s.a, s.a);
			break;
		}
		
		return factor;
	}

	public static Color[] Fill(Texture2D texture, Color color, bool apply=true){

		Color[] colors = new Color[texture.width * texture.height];

		for(int i=0;i<colors.Length;i++){
			colors[i] = color;
		}

		texture.SetPixels( colors);

		if (apply){
			texture.Apply();
		}

		return colors;
	}
	
}
