/***********************************************
		 Space Builder : Genesis
	Copyright © 2014-2015 Dark Anvil
	http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/

namespace SBGenesis{
using UnityEngine;
using System.Collections;

public class CosmosParticle : MonoBehaviour {

	public enum ParticleSize {Small,Big};

	public Material mat;

	public int maxParticle;

	public Gradient color;

	public ParticleSize particleSize;
	public float minSize;
	public float maxSize;

	public bool enableRotation;
	public float rotationSpeed;

	public bool enableDrift;
	public float driftSpeed;

	private ParticleSystem cacheCosmosParticle;
	private Transform cacheTransform;

	private bool isReady=false;

	public bool inspectorShowProperties;
	public bool isWaitToDelte;

	#region Constructor
	public CosmosParticle(){
		maxParticle = 200;
		minSize = 0.2f;
		maxSize = 0.9f;
		enableRotation = false;
		rotationSpeed = 3;
	}
	#endregion

	#region Monobehaviour callback
	void OnEnable(){
		Cosmos.On_CosmosReady += On_CosmosReady;
	}

	void OnDisable(){
		Cosmos.On_CosmosReady -= On_CosmosReady;
	}

	void OnDestroy(){
		Cosmos.On_CosmosReady -= On_CosmosReady;
	}
		
	void Update(){

		float spawnDistance = 200;
		float fade = 50;

		if (isReady){
			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[cacheCosmosParticle.particleCount];
			cacheCosmosParticle.GetParticles(particles);

			for (int i = 0; i < particles.Length; i++) {

				float dist = Vector3.Distance( cacheTransform.position, particles[i].position);

				if (dist>spawnDistance){
					particles[i].position = cacheTransform.position + Random.onUnitSphere * spawnDistance;
					particles[i].size = Random.Range(minSize, maxSize );
					particles[i].rotation = Random.Range(-180,180);
					particles[i].color = color.Evaluate( Random.Range(0f,1f));

					if (enableDrift){
						particles[i].velocity  =new Vector3( Random.Range(-1f,1f),Random.Range(-1f,1f),0) * driftSpeed;
					}
					else{
						particles[i].velocity = Vector3.zero;
					}
				}

				// Fade
				float alpha = Mathf.Clamp01(1.0f - ((dist - fade) / (spawnDistance - fade)));
				particles[i].color = new Color( particles[i].color.r/255f,particles[i].color.g/255f,particles[i].color.b/255f, alpha);

				if (enableRotation){
					particles[i].rotation += rotationSpeed * Mathf.Sign(particles[i].rotation)  * Time.deltaTime;
				}
			}

			cacheCosmosParticle.SetParticles(particles, cacheCosmosParticle.particleCount);  
		}
	}
	#endregion

	#region event
	void On_CosmosReady (){

		isReady = true;
		
		cacheTransform = Cosmos.instance.SpaceCamera.transform;
		
		// Create particle system
		gameObject.AddComponent<ParticleSystem>();
		cacheCosmosParticle = GetComponent<ParticleSystem>();

		cacheCosmosParticle.playOnAwake = false;
		cacheCosmosParticle.enableEmission = false;
		cacheCosmosParticle.simulationSpace = ParticleSystemSimulationSpace.Local;
		cacheCosmosParticle.emissionRate = 0;
		cacheCosmosParticle.startSpeed = 0;
		cacheCosmosParticle.startLifetime = Mathf.Infinity;

		cacheCosmosParticle.startRotation = Random.Range(-Mathf.PI,Mathf.PI);

		cacheCosmosParticle.GetComponent<Renderer>().material = mat;
			cacheCosmosParticle.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			cacheCosmosParticle.GetComponent<Renderer>().receiveShadows = false;


		// Pre Spawn 
		for (int i=0; i < maxParticle; i ++) {
			Vector3 drift = Vector3.zero;
			if (enableDrift){
				drift =new Vector3( Random.Range(-1f,1f),Random.Range(-1f,1f),0) * driftSpeed;
			}

			cacheCosmosParticle.Emit( cacheTransform.position + (UnityEngine.Random.insideUnitSphere * 200), drift, Random.Range(minSize, maxSize ) , Mathf.Infinity, color.Evaluate( Random.Range(0f,1f)));
		}


	}
	#endregion
}
}
