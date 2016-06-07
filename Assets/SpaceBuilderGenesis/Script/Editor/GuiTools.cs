using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class GuiTools{
	
	public static Texture2D gradientTextureStyle;
	public static Texture2D checkerTexture;

	// Camera scene
	public static void SetSceneCamera(float rx, float ry, Vector3 position = default(Vector3), int viewSize=10){

		if (SceneView.lastActiveSceneView!=null){
			SceneView.lastActiveSceneView.size = viewSize;
			SceneView.lastActiveSceneView.rotation = Quaternion.Euler( new Vector3(rx,ry,0f));
			SceneView.lastActiveSceneView.pivot = new Vector3(position.x,position.y+1,position.z );
		}

	}


	// Tooltip
	public static void ShowToolTip(){

		//if (Event.current.type == EventType.Repaint && GUI.tooltip !="") {

		Rect lasRect = GUILayoutUtility.GetLastRect();

		if (GUI.tooltip !=""){
			GUI.DrawTexture( new Rect(lasRect.x,lasRect.y+lasRect.height+8,100,16), EditorGUIUtility.whiteTexture);
		}
		GUI.Label( new Rect( lasRect.x,lasRect.y+lasRect.height+8,100,16),GUI.tooltip);


	}

	// Title
	public static Rect DrawTitleChapter(string text,int size, bool bold, int style, int width,Color textColor)
	{
		Rect lastRect = DrawTitleGradient(0,width);
		
		GUIStyle labelStyle =  new GUIStyle( EditorStyles.label);
		if (bold){
			labelStyle.fontStyle = FontStyle.Bold;
		}
		labelStyle.fontSize = size;
		
			
		labelStyle.onActive.textColor = textColor;
		labelStyle.onFocused.textColor = textColor;
		labelStyle.onHover.textColor = textColor;
		labelStyle.onNormal.textColor = textColor;
		labelStyle.active.textColor = textColor;
		labelStyle.focused.textColor = textColor;
		labelStyle.hover.textColor = textColor;;
		labelStyle.normal.textColor = textColor;
		
		int offsety = 2;
		if (style==2) offsety=0;
		
		GUI.Label(new Rect(lastRect.x + 3, lastRect.y+offsety, width, lastRect.height), text,labelStyle);
		
		return lastRect;
	}
	
	// FoldOut
	public static bool FoldOut( ref bool foldOut,string text, bool addButton=false, string buttonText="+", Color color = default(Color)){
		
		GUIStyle foldOutStyle =  new GUIStyle( EditorStyles.foldout);
		foldOutStyle.fontStyle = FontStyle.Bold;
		foldOutStyle.fontSize = 12;
		foldOutStyle.stretchWidth = true;		
		Color textColor = Color.white;
		
		int offsety=2;
				
		foldOutStyle.onActive.textColor = textColor;
		foldOutStyle.onFocused.textColor = textColor;
		foldOutStyle.onHover.textColor = textColor;
		foldOutStyle.onNormal.textColor = textColor;
		foldOutStyle.active.textColor = textColor;
		foldOutStyle.focused.textColor = textColor;
		foldOutStyle.hover.textColor = textColor;;
		foldOutStyle.normal.textColor = textColor;
		
		int width=Screen.width;
		if (addButton)
			width = Screen.width-37;
		
		Rect lastRect = DrawTitleGradient(0,width);
		
		
		GUI.backgroundColor = Color.black;
		
		GUIContent foldContent = new GUIContent();
		foldContent.text = " " + text;
		
		foldOut =  EditorGUI.Foldout(new Rect(lastRect.x + 3, lastRect.y+offsety, width-37 , lastRect.height),foldOut,foldContent,true,foldOutStyle);	
		GUI.backgroundColor = Color.white;
		
		if (addButton){
			if (color == default(Color))
				GUI.backgroundColor = Color.green;
			else
				GUI.backgroundColor = color;
			
			if (GUI.Button( new Rect(Screen.width-37, lastRect.y,  20 , lastRect.height),buttonText)){
				GUI.backgroundColor = Color.white;
				return true;	
			}
			GUI.backgroundColor = Color.white;
		}
		
		return false;

	}
	
	public static bool ChildFoldOut(bool foldOut,string text, Color color, int width){
		
		string label="[+] " + text;
		if (foldOut) label="[-] " + text;
		if (GuiTools.Button(label,color,width,true)){
			foldOut = !foldOut;		
		}
		
		return foldOut;
	}
	
	// Vector2
	public static Vector2 Vector2Field(string label,Vector2 vector){
		Rect r = EditorGUILayout.BeginHorizontal();	
		EditorGUILayout.PrefixLabel(label);
		r.y-= 18;
		r.x = 50;
		vector = EditorGUI.Vector2Field(r,"",vector);
		EditorGUILayout.EndHorizontal();

		return vector;
	}
	
	// Button
	static public bool Button(string label,Color color,int width, bool leftAligment=false, int height=0, bool space=false, int indent=0){
		GUI.backgroundColor  = color;
		GUIStyle buttonStyle = new GUIStyle("Button");

		if (leftAligment)
			buttonStyle.alignment = TextAnchor.MiddleLeft;

		if (space){
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(indent);
		}

		if (height==0){

			if (GUILayout.Button( label,buttonStyle,GUILayout.Width(width))){
				GUI.backgroundColor = Color.white;
				return true;	
			}
		}
		else{
			if (GUILayout.Button( label,buttonStyle,GUILayout.Width(width),GUILayout.Height(height))){
				GUI.backgroundColor = Color.white;
				return true;	
			}			
		}
		GUI.backgroundColor = Color.white;	
		if (space){
			EditorGUILayout.EndHorizontal();
		}

		return false;
	}

	// Depth control
	static public int Order(int depth){
	

		if (GUILayout.Button("Back",GUILayout.Width(43), GUILayout.Height(15))){
			depth--;	
		}
		depth =EditorGUILayout.IntField(depth,GUILayout.Width(32));

		if (GUILayout.Button("Front",GUILayout.Width(43),GUILayout.Height(15))){
			depth++;	
		}			

		return depth;
	}

	// Toggle
	public static bool Toggle(string text,bool toggle,bool left=false, int width=0){
		Color color = Color.red;
		if (toggle) color= Color.green;
		
		GUI.backgroundColor = color;
		if (!left){
			if (width==0){
				toggle = EditorGUILayout.Toggle( text,toggle);
			}
			else{
				toggle = EditorGUILayout.Toggle( text,toggle, GUILayout.Width(width));

			}
		}
		else{
			if (width==0){
				toggle = EditorGUILayout.ToggleLeft( text,toggle);
			}
			else{
				toggle = EditorGUILayout.ToggleLeft( text,toggle, GUILayout.Width(width));

			}
		}
		GUI.backgroundColor = Color.white;
		
		return toggle;
	}

	// Picture
	public static void DrawTexturePreview( Rect rect, Texture tex){
		DrawTileTexture( rect, GuiTools.GetCheckerTexture());
		if (tex!=null){
			GUI.DrawTexture( rect, tex,ScaleMode.ScaleToFit);	
		}
	}

	public static void DrawTexturePreviewB( Rect rect, Texture tex, Color color){

		GUI.color = color;
		GUI.DrawTexture( rect, EditorGUIUtility.whiteTexture);
		GUI.color = Color.white;

		rect = new Rect(rect.x+3,rect.y+3,rect.width-6,rect.height-6);
		DrawTileTexture( rect, GuiTools.GetCheckerTexture());	

		if (tex != null)
			GUI.DrawTexture( rect, tex,ScaleMode.ScaleToFit);	



	}

	public static void DrawTextureRectPreview( Rect rect, Rect textureRect, Texture2D tex, Color color){
			
		GUI.color = color;
		GUI.DrawTexture( rect, EditorGUIUtility.whiteTexture);
		GUI.color = Color.white;

		rect = new Rect(rect.x+3,rect.y+3,rect.width-6,rect.height-6);
		DrawTileTexture( rect, GuiTools.GetCheckerTexture());

		if (tex!=null){	
			GUI.DrawTextureWithTexCoords( rect, tex,textureRect );
		}

	}

	// Progress
	public static void ProgressBar( float percent){
		Rect LastRect = EditorGUILayout.BeginHorizontal();
		LastRect.height =20;
		LastRect.width =190;
		EditorGUILayout.EndHorizontal();
		EditorGUI.ProgressBar( new Rect(0,0,Screen.width,50),percent/100f,percent.ToString() + "%"  );
	}

	public static Rect ProgressBar(float value,string label){

		EditorGUILayout.Space();
		GUILayout.Space(16);
		Rect lastRect = GUILayoutUtility.GetLastRect();
		lastRect.width = Screen.width-20;

		ProgressBar(lastRect, value, label);
		/*
		EditorGUI.ProgressBar(lastRect,value,label);*/

		return lastRect;
	}

	public static void ProgressBar(Rect rect,float value,string label){

		EditorGUI.ProgressBar(rect,value,label);

	}

	// Draw separator line
	public static void DrawSeparatorLine(int padding=0)
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.Space();
		DrawSeparatorLine(Color.gray, padding);
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
	}
	
	public static void DrawSeparatorLine(Color color,int padding=0)
	{
		
	    GUILayout.Space(10);
        Rect lastRect = GUILayoutUtility.GetLastRect();
		
		GUI.color = color;
	    GUI.DrawTexture(new Rect(padding, lastRect.yMax -lastRect.height/2f, Screen.width, 1f), EditorGUIUtility.whiteTexture);
		GUI.color = Color.white;
	}
	
	public static void DrawTileTexture (Rect rect, Texture tex)
	{
		GUI.BeginGroup(rect);
		{
			int width  = Mathf.RoundToInt(rect.width);
			int height = Mathf.RoundToInt(rect.height);

			for (int y = 0; y < height; y += tex.height)
			{
				for (int x = 0; x < width; x += tex.width)
				{
					GUI.DrawTexture(new Rect(x, y, tex.width, tex.height), tex);
				}
			}
		}
		GUI.EndGroup();
	}
	
	public static void DrawLine(float x1,float y1,float x2,float y2, Color color){
		GUI.color = color;
		GUI.DrawTexture( new Rect(x1,y1,x2-x1,y2-y1), EditorGUIUtility.whiteTexture);
		GUI.color = Color.white;
	}
	
	// Gradient
	private static Rect DrawTitleGradient(int padding, int width)
	{
		int space=35;
				
	    GUILayout.Space(space);
		Rect lastRect = GUILayoutUtility.GetLastRect();
	    lastRect.yMin = lastRect.yMin + 7;
	    lastRect.yMax = lastRect.yMax - 7;
	    lastRect.width =  Screen.width;
	
		if (width==-1)
			GUI.DrawTexture(new Rect(padding,lastRect.yMin+1,Screen.width, lastRect.yMax- lastRect.yMin), GetGradientTexture());
		else
			GUI.DrawTexture(new Rect(padding,lastRect.yMin+1,width, lastRect.yMax- lastRect.yMin), GetGradientTexture());
		
		return lastRect;
	}
	
	private static Texture2D GetGradientTexture(){
		
		if (GuiTools.gradientTextureStyle==null){
			GuiTools.gradientTextureStyle = CreateGradientTexture();
		}
		return gradientTextureStyle;

	}
		
	private static Texture2D CreateGradientTexture()
	{
		// new texture
		int height =18;
		
		Texture2D myTexture = new Texture2D(1, height);
		myTexture.hideFlags = HideFlags.HideInInspector;
		myTexture.hideFlags = HideFlags.DontSave;
		myTexture.filterMode = FilterMode.Bilinear;
				
		Color startColor= new Color(0.4f,0.4f,0.4f);
		Color endColor= new Color(0.6f,0.6f,0.6f);
		
		float stepR = (endColor.r - startColor.r)/18f;
		float stepG = (endColor.g - startColor.g)/18f;
		float stepB = (endColor.b - startColor.b)/18f;
		
		Color pixColor = startColor;
		
		for (int i = 1; i < height-1; i++)
		{
			pixColor = new Color(pixColor.r + stepR,pixColor.g + stepG , pixColor.b + stepB);
			myTexture.SetPixel(0, i, pixColor);
		}
		
		myTexture.SetPixel(0, 0, new Color(0,0,0));
		myTexture.SetPixel(0, 17, new Color(1,1,1));
		
		myTexture.Apply();
		
		return myTexture;
	}
	
	// Checker
	public static Texture2D GetCheckerTexture(){
		if (GuiTools.checkerTexture==null){
			GuiTools.checkerTexture = CreateCheckerTexture();
		}
		return checkerTexture;		
	}
	
	private static Texture2D CreateCheckerTexture(){

		Texture2D myTexture = new Texture2D(16, 16);
		myTexture.hideFlags = HideFlags.DontSave;
		
		Color color1 = new Color(0.5f,0.5f,0.5f);
		for( int x=0;x<8;x++){
			for( int y=0;y<8;y++){
				myTexture.SetPixel(x, y, color1);
				myTexture.SetPixel(x+8, y+8, color1);
			}
		}
		
		color1 = new Color(0.3f,0.3f,0.3f);
		for( int x=0;x<8;x++){
			for( int y=0;y<8;y++){
				myTexture.SetPixel(x+8, y, color1);
				myTexture.SetPixel(x, y+8, color1);
			}
		}
		
		myTexture.Apply();
		
		return myTexture;
	}	
		
	
	#region AssetDatabase tool
	public static bool CreateAssetDirectory(string rootPath,string name){
		string directory = rootPath + "/" +  name;
		if (!System.IO.Directory.Exists(directory)){
			AssetDatabase.CreateFolder(rootPath,name);
			AssetDatabase.Refresh();
			return true;
		}
		return false;
	}

	public static string GetAssetRootPath( string path){
		
		string[] tokens = path.Split('/');
		
		path="";
		for (int i=0;i<tokens.Length-1;i++){
			path+= tokens[i] +"/";
		}
		return path;
	}

	public static T[] GetAtPath<T> (string path) {

		ArrayList al = new ArrayList();
		string [] fileEntries = Directory.GetFiles(Application.dataPath+"/"+path);

		foreach(string fileName in fileEntries){
			//
			string localPath = "Assets/" + path;

			int index=-1;
			index = fileName.LastIndexOf("\\");
			#if UNITY_EDITOR_OSX
				index = fileName.LastIndexOf("/");
			#endif

			if (index > 0)
				localPath += fileName.Substring(index);
			
			Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));
			if(t != null)
				al.Add(t);
		}

		T[] result = new T[al.Count];
		for(int i=0;i<al.Count;i++)
			result[i] = (T)al[i];

		return result;
	}

	public static void SetReadWrite( Texture2D texture,bool readWrite, TextureImporterFormat format, int size){
		
		// Get the path of the texture
		string path = AssetDatabase.GetAssetPath( texture.GetInstanceID());
		
		// Get the textureImporter object
		TextureImporter textureImporter = AssetImporter.GetAtPath(  path ) as TextureImporter;
		
		// Texture type to advanced
		textureImporter.textureType = TextureImporterType.Advanced;	
		
		// Creat a new setting
		TextureImporterSettings st = new TextureImporterSettings();
		textureImporter.ReadTextureSettings(st);
		
		// Texture must be in ARgB32
		st.textureFormat = format;

		// Set write/read flag
		st.readable = readWrite;

		st.maxTextureSize = size;

		st.mipmapEnabled = false;

		st.wrapMode = TextureWrapMode.Clamp;

		// Import the new setting
		textureImporter.SetTextureSettings(st);
		
		// Update the asset
		AssetDatabase.ImportAsset(path);	
	}
	#endregion
	
}
