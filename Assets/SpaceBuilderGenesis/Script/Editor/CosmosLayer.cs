/// <summary>
/// This script show you how to setup layer name by code
/// </summary>
using UnityEngine;
using UnityEditor;
using System.Collections;


[InitializeOnLoad]
public class CosmosLayer{
	
	// Static constructor
	static CosmosLayer(){

		EditorApplication.projectWindowItemOnGUI += SetUpLayer;
	}
	
	
	// Setup the layer name
	static void SetUpLayer(string instanceID, Rect selectionRect){
		// Serialize TagManager asset
		SerializedObject so = new SerializedObject (AssetDatabase.LoadAllAssetsAtPath ("ProjectSettings/TagManager.asset")[0]);
		
		// Get the iterator
		SerializedProperty it = so.GetIterator ();
		
		// For each property
		while (it.NextVisible(true)) {
			
			// We want to set up the layer N°31
			if (it.name == "User Layer 31"){
				it.stringValue = "Cosmos";
			}
		}	
		
		// Save change
		so.ApplyModifiedProperties();
	}	
}

