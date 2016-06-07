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
public class StarfieldBox{

	#region Members
	[SerializeField]
	private bool enable;
	public bool Enable {
		get {
			return enable;
		}
		set {
			if (value != enable){

				if (!value){
					SpaceBox.instance.starfield.Render(true);
				}
				enable = value;
				SpaceBox.instance.starfield.Render();
			}
		}
	}

	[SerializeField]
	private float intensity;
	public float Intensity {
		get {
			return intensity;
		}
		set {
			if (value != intensity){
				intensity = value;
				SetIntensity();
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				SpaceBox.instance.starfield.Render();

			}
		}
	}

	[SerializeField]
	private int smallCount;
	public int SmallCount {
		get {
			return smallCount;
		}
		set {
			if (value !=smallCount){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				smallCount = value;
				ComputeCluster(0);
			}
		}
	}

	[SerializeField]
	private int mediumCount;
	public int MediumCount {
		get {
			return mediumCount;
		}
		set {
			if (value !=mediumCount){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				mediumCount = value;
				ComputeCluster(1);
			}
		}
	}

	[SerializeField]
	private int largeCount;
	public int LargeCount {
		get {
			return largeCount;
		}
		set {
			if (value !=largeCount){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				largeCount = value;
				ComputeCluster(2);
			}
		}
	}

	[SerializeField]
	private float mixing;
	public float Mixing {
		get {
			return mixing;
		}
		set {
			if (value != mixing){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				mixing = value;
				ComputeAllStars();
			}
		}
	}

	[SerializeField]
	private int clusterCount;
	public int ClusterCount {
		get {
			return clusterCount;
		}
		set {
			if (value != clusterCount){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				clusterCount = value;
				SetupCluster();
				ComputeAllStars();
			}
		}
	}

	[SerializeField]
	private float threshold;
	public float Threshold {
		get {
			return threshold;
		}
		set {
			if (value != threshold){
				if (SpaceBox.instance.starfield.rendered){
					SpaceBox.instance.starfield.Render(true);
				}
				threshold = value;
				SpaceBox.instance.starfield.Render();
			}
		}
	}

	[SerializeField]
	public StarCluster[] clusterBox;

	public Gradient gradient;

	public bool inspectorShowProperties;
	#endregion

	#region Public Method
	public void Create(){

		// Init
		enable = true;

		threshold =0;
		mixing =0.7f;
		intensity =1;

		smallCount = 0;
		mediumCount = 0;
		largeCount = 0;

		// Color gradient
		gradient = new Gradient();

		GradientColorKey[] colork = new GradientColorKey[4];
		colork[0].color = Color.white;
		colork[0].time = 0.0f;

		colork[1].color = new Color( 1,245f/255f,200f/255f);
		colork[1].time = 0.33f;

		colork[2].color = new Color( 1,190f/255f,190f/255f);
		colork[2].time = 0.66f;

		colork[3].color = new Color( 189f/255f,1,1);
		colork[3].time = 1f;

		GradientAlphaKey[] alphak = new GradientAlphaKey[2];
		alphak[0].alpha = 1.0f;
		alphak[0].time = 0.0f;

		alphak[1].alpha = 1.0f;
		alphak[1].time = 0.0f;

		gradient.SetKeys(colork,alphak);

		// Cluster
		clusterBox = new StarCluster[6];
		for (int i=0;i<6;i++){
			clusterBox[i] = new StarCluster();
		}

		clusterCount = Random.Range(1,50);
		SetupCluster();

	}

	public void RandomStarfieldBox(int type){

		if (SpaceBox.instance.starfield.rendered){
			SpaceBox.instance.starfield.Render(true);
		}

		if (type==0){
			intensity = 0.8f;
			threshold = 0.05f;
		}
		else{
			intensity = 10;
			threshold = 0f;
		}

		smallCount = Random.Range(0,16000);
		mediumCount = Random.Range(0,2400);
		largeCount = Random.Range (0,1000);

		mixing = Random.Range(0.7f,1f);

		clusterCount = Random.Range(5,50);

		SetupCluster();
		ComputeCluster(0,false);
		ComputeCluster(1,false);
		ComputeCluster(2,false);
	}

	public void ComputeAllStars(){
		ComputeCluster(0,false);
		ComputeCluster(1,false);
		ComputeCluster(2,false);
		SpaceBox.instance.starfield.Render();
	}

	#endregion
	
	#region Private Method


	private void ComputeCluster(int size, bool render=true){

		int x =0;
		int y =0;
		int starCount=0;
		float greyScale =0;

		switch (size){
			case 0:
				starCount = smallCount;
				break;
			case 1:
				starCount = mediumCount;
				break;
			case 2:
				starCount = largeCount;
				break;
		}

		int quality = SpaceBox.instance.starfield.GetStarfieldQuality2Int();

		// foreach face
		for(int cb=0;cb<6;cb++){

			clusterBox[cb].stars[size].star = new Vector4[starCount];
			clusterBox[cb].stars[size].gradientTime = new float[starCount];

			int starIndex=0;

			// Foreach cluster
			for(int c=0;c<clusterBox[cb].ClusterCount;c++){

				// foreach stars
				for(int s=0;s< (c==0f?(int)(starCount*mixing):(int)((starCount*(1.0f - mixing))/clusterBox[cb].ClusterCount));s++){

					// Color
					switch (size){
						case 0:
							greyScale = Random.Range(0.2f,0.4f);
							break;
						case 1:
							greyScale = Random.Range(0.5f,0.7f);
							break;
						case 2:
							greyScale = Random.Range(0.6f,0.8f);
							break;
					}

					// Position
					if (c==0){
						x = Random.Range(0,quality);
						y = Random.Range(0,quality);
					}
					else{
						float angle = Random.Range(-2*Mathf.PI, 2* Mathf.PI);
						x = (int)((clusterBox[cb].clusters[c].x )  + Mathf.Cos(angle) * (clusterBox[cb].clusters[c].z * Random.Range(0f,1f)));
						y = (int)((clusterBox[cb].clusters[c].y )  + Mathf.Sin(angle) * (clusterBox[cb].clusters[c].z * Random.Range(0f,1f)));
					}

					clusterBox[cb].stars[size].star[starIndex] = new Vector4( x ,y, greyScale,intensity);
					clusterBox[cb].stars[size].gradientTime[starIndex] = Random.Range(0f,1f);

					starIndex++;
			
				}

			}
		}

		if (render){
			SpaceBox.instance.starfield.Render();
		}

	}
	
	public void SetupCluster(){
		for (int i=0;i<6;i++){
			clusterBox[i].ClusterCount = clusterCount;
		}
	
	}

	private void SetIntensity(){

		for (int i=0;i<6;i++){
			for (int s=0;s<smallCount;s++){
				clusterBox[i].stars[0].star[s].w = intensity;
			}
			for (int s=0;s<mediumCount;s++){
				clusterBox[i].stars[1].star[s].w = intensity;
			}
			for (int s=0;s<largeCount;s++){
				clusterBox[i].stars[2].star[s].w = intensity;
			}
		}
	}
	#endregion
}
}

