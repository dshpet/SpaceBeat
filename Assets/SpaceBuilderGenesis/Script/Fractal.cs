/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Fractal{

	#region Enumeration
	public enum FractalType {Fbm,Ridged};
	public enum Face{ Front, Back, Right,Left,Up,Down};
	#endregion

	#region Members
	public FractalType type;
	public float zoom;
	public float seed;
	public int octave;
	public float lacunarity;
	public float frequency;
	public float gain;
	public float offset;
	public float threshold;
	public bool inverse;
	public float power;
	public float[] exponents;
	
	public float min;
	public float max;
	#endregion

	#region Contructor
	public Fractal(){

		//zoom = 50f;
		//octave = 8;
		//lacunarity = 2f;
		frequency = 2f;
		gain = 0;
		offset = 0;
		threshold = 0;
		seed =0;
		power =1;
	}
	#endregion
	
	public void InitFractal(){

		float freq = frequency;

		exponents = new float[octave+1];
		
		for (int i=0; i<=octave; i++) {
			exponents[i] = Mathf.Pow( freq, -1 );
			freq *= lacunarity;
		}
		
	}
}
}
