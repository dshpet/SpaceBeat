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
public class StarCluster{

	[System.Serializable]
	public class StarClass{
		[SerializeField]
		public Vector4[] star;
		[SerializeField]
		public float[] gradientTime;
	}

	[SerializeField]
	private int clusterCount;
	public int ClusterCount {
		get {
			return clusterCount;
		}
		set {
			if (value!=clusterCount){
				clusterCount = value;
				SetupCluster();
			}
		}
	}

	[SerializeField]
	public Vector3[] clusters;

	[SerializeField]
	public StarClass[] stars;

	public StarCluster(){
		stars = new StarClass[6];

		for (int i=0;i<6;i++){
			stars[i] = new StarClass();
		}
	}

	private void SetupCluster(){

		clusters = new Vector3[clusterCount];

		int quality = SpaceBox.instance.starfield.GetStarfieldQuality2Int();

		for (int i=1;i<clusterCount;i++){
			int x = Random.Range(96,quality - 96);
			int y = Random.Range(96,quality - 96);
			int max = x<y?x:y;

			clusters[i] = new Vector4(x,y, Random.Range(50,max/2 ));
		}
	}

 }
}
