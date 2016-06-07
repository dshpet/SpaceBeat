 /***********************************************
	Generic MonoBehaviour singleton
	Copyright © 2014-2015 Dark Anvil
		http://www.darkanvil.com
		
		contact@darkanvil.com
		
**********************************************/
using UnityEngine;

[System.Serializable]
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>{
	
	private static T m_Instance = null;
    
	public static T instance{
        get{
			if( m_Instance == null ){
            	m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if( m_Instance == null ){
                    m_Instance = new GameObject( typeof(T).ToString(), typeof(T)).GetComponent<T>();
					m_Instance.Init();
                }
            }
            return m_Instance;
        }
    }

    private void Awake(){
   
        if( m_Instance == null ){
            m_Instance = this as T;
        }
    }
 
    public virtual void Init(){}
 
		
    private void OnApplicationQuit(){
        m_Instance = null;
    }
}