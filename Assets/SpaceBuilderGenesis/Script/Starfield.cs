/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Starfield {

	public enum StarfieldQuality {Medium, High};

	#region Members
	[SerializeField]
	private StarfieldQuality quality;
	public StarfieldQuality Quality {
		get {
			return quality;
		}
		set {
			if (value != quality){
				quality = value;
				//SetUpTexture();
				//RandomStarfield();
			}
		}
	}

	public StarfieldBox nebulaStarfield;

	public StarfieldBox cosmosStarfield;

	public Texture2D[] starfieldTexture;

	public bool rendered;


	public bool need2save;
	#endregion

	#region public method
	public void Create(){

		rendered = false;

		// Init texture;
		quality = StarfieldQuality.Medium;
		starfieldTexture = new Texture2D[6];

		nebulaStarfield = new StarfieldBox();
		nebulaStarfield.Create();

		cosmosStarfield = new StarfieldBox();
		cosmosStarfield.Create();
	}


	public void RandomStarfield(){
		nebulaStarfield.RandomStarfieldBox(1);
		cosmosStarfield.RandomStarfieldBox(0);
		Render();
	}

	public void Render(bool clear=false){

		rendered = true;

		float nebCoef = (float)SpaceBox.instance.GetNebulaQuality2Int()/ (float)GetStarfieldQuality2Int();

		Color nebColor = Color.white;

		for (int i=0;i<6;i++){

			nebColor = new Color(0,0,0,0);

			if (cosmosStarfield.Enable){

				for (int j=0;j<3;j++){

					int count=0;

					switch (j){
					case 0:
						count = cosmosStarfield.SmallCount;
						break;
					case 1:
						count = cosmosStarfield.MediumCount;
						break;
					case 2:
						count = cosmosStarfield.LargeCount;
						break;
					}

					for(int s=0;s<count;s++){
						
						Vector4 star = cosmosStarfield.clusterBox[i].stars[j].star[s];
						
						if (!clear){
							float  grad = cosmosStarfield.clusterBox[i].stars[j].gradientTime[s];
							
							if (SpaceBox.instance.nebula.Count>0){
								nebColor = SpaceBox.instance.nebulaTexture[i].GetPixel((int)(star.x * nebCoef),(int)(star.y* nebCoef));
							}
							
							if ((nebColor.r + nebColor.g + nebColor.b)/3f <= cosmosStarfield.Threshold || SpaceBox.instance.nebula.Count==0){

								starfieldTexture[i].SetPixel((int)star.x,(int)star.y, new Color( star.z*star.w,star.z*star.w,star.z*star.w) * cosmosStarfield.gradient.Evaluate(grad )  );

								if (j==2){
									Color starcolor =  new Color( star.z*star.w,star.z*star.w,star.z*star.w) * cosmosStarfield.gradient.Evaluate(grad ) /3;
									
									starfieldTexture[i].SetPixel((int)(star.x)+1,(int)(star.y), starcolor);
									starfieldTexture[i].SetPixel((int)(star.x)-1,(int)(star.y), starcolor);
									starfieldTexture[i].SetPixel((int)(star.x),(int)(star.y)+1, starcolor);
									starfieldTexture[i].SetPixel((int)(star.x),(int)(star.y)-1, starcolor);
								}

							}
						}
						else{
							starfieldTexture[i].SetPixel((int)star.x,(int)star.y,Color.black);
							if (j==2){
								starfieldTexture[i].SetPixel((int)star.x+1,(int)star.y, Color.black);
								starfieldTexture[i].SetPixel((int)star.x-1,(int)star.y, Color.black);
								starfieldTexture[i].SetPixel((int)star.x,(int)star.y+1, Color.black);
								starfieldTexture[i].SetPixel((int)star.x,(int)star.y-1, Color.black);
							}
						}
						
					}

				}

			}


			if (nebulaStarfield.Enable){


				for (int j=0;j<3;j++){
					
					int count=0;
					
					switch (j){
					case 0:
						count = nebulaStarfield.SmallCount;
						break;
					case 1:
						count = nebulaStarfield.MediumCount;
						break;
					case 2:
						count = nebulaStarfield.LargeCount;
						break;
					}
					
					for(int s=0;s<count;s++){
						
						Vector4 star = nebulaStarfield.clusterBox[i].stars[j].star[s];
						
						if (!clear){
							float  grad = nebulaStarfield.clusterBox[i].stars[j].gradientTime[s];
							
							if (SpaceBox.instance.nebula.Count>0){
								nebColor = SpaceBox.instance.nebulaTexture[i].GetPixel((int)(star.x * nebCoef),(int)(star.y* nebCoef));
							}
							
							if ((nebColor.r + nebColor.g + nebColor.b)/3f >= nebulaStarfield.Threshold && SpaceBox.instance.nebula.Count>0){
								starfieldTexture[i].SetPixel((int)star.x,(int)star.y, new Color( star.z*star.w,star.z*star.w,star.z*star.w) * nebulaStarfield.gradient.Evaluate(grad ) * nebColor );
								
								if (j==2){
									Color starcolor =  new Color( star.z*star.w,star.z*star.w,star.z*star.w) * nebulaStarfield.gradient.Evaluate(grad ) * nebColor /3;
									
									starfieldTexture[i].SetPixel((int)star.x+1,(int)star.y, starcolor);
									starfieldTexture[i].SetPixel((int)star.x-1,(int)star.y, starcolor);
									starfieldTexture[i].SetPixel((int)star.x,(int)star.y+1, starcolor);
									starfieldTexture[i].SetPixel((int)star.x,(int)star.y-1, starcolor);
								}
								
							}
						}
						else{
							starfieldTexture[i].SetPixel((int)star.x,(int)star.y,Color.black);
							if (j==2){
								starfieldTexture[i].SetPixel((int)star.x+1,(int)star.y, Color.black);
								starfieldTexture[i].SetPixel((int)star.x-1,(int)star.y, Color.black);
								starfieldTexture[i].SetPixel((int)star.x,(int)star.y+1, Color.black);
								starfieldTexture[i].SetPixel((int)star.x,(int)star.y-1, Color.black);
							}
						}
						
					}
					
				}
			}

			starfieldTexture[i].Apply();
		}

		UpdateStarfieldSkyBox();

		need2save = true;
	}

	public int GetStarfieldQuality2Int(){
		int size = 0;
		switch( quality){
			case StarfieldQuality.Medium:
				size=1024;
				break;
			case StarfieldQuality.High:
				size=2048;
				break;
		}
		return size;
	}

	public void UpdateStarfieldSkyBox(){
		SpaceBox.instance.cosmosMaterial.SetTexture("_FrontStarfield",starfieldTexture[0]);
		SpaceBox.instance.cosmosMaterial.SetTexture("_BackStarfield",starfieldTexture[1]);
		SpaceBox.instance.cosmosMaterial.SetTexture("_LeftStarfield",starfieldTexture[2]);
		SpaceBox.instance.cosmosMaterial.SetTexture("_RightStarfield",starfieldTexture[3]);
		SpaceBox.instance.cosmosMaterial.SetTexture("_UpStarfield",starfieldTexture[4]);
		SpaceBox.instance.cosmosMaterial.SetTexture("_DownNStarfield",starfieldTexture[5]);
	}

	#endregion
	
}
}
