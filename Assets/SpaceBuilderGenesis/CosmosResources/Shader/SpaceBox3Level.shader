Shader "Space Builder/SpaceBox3Level" {
Properties {
	_FrontSky ("Front Sky(+Z)", 2D) = "black" {}
    _BackSky ("Back Sky(-Z)", 2D) = "black" {}
    _LeftSky ("Left Sky(+X)", 2D) = "black" {}
    _RightSky ("Right Sky(-X)", 2D) = "black" {}
    _UpSky ("Up Sky(+Y)", 2D) = "black" {}
    _DownSky ("Down Sky(-Y)", 2D) = "black" {}
    
    _FrontNebula ("Front nebula(+Z)", 2D) = "black" {}
    _BackNebula ("Back nebula(-Z)", 2D) = "black" {}
    _LeftNebula ("Left nebula(+X)", 2D) = "black" {}
    _RightNebula ("Right nebula(-X)", 2D) = "black" {}
    _UpNebula ("Up nebula(+Y)", 2D) = "black" {}
    _DownNebula ("Down nebula(-Y)", 2D) = "black" {}
    
    
    _FrontStarfield ("Front Starfield(+Z)", 2D) = "black" {}
    _BackStarfield ("Back Starfield(-Z)", 2D) = "black" {}
    _LeftStarfield ("Left Starfield(+X)", 2D) = "black" {}
    _RightStarfield ("Right Starfield(-X)", 2D) = "black" {}
    _UpStarfield ("Up Starfield(+Y)", 2D) = "black" {}
    _DownNStarfield ("Down Starfield(-Y)", 2D) = "black" {}

}

SubShader {
    Tags { "Queue" = "Background" }
    Cull Off
    Fog { Mode Off }
    Lighting Off        
    Color [_Tint]
    
    Pass {
        SetTexture [_FrontSky] {combine texture,texture}
 		SetTexture [_FrontNebula] {combine texture + previous }
       	SetTexture [_FrontStarfield] {combine texture + previous}       
    }
    Pass {
        SetTexture [_BackSky] { combine texture ,texture}
        SetTexture [_BackNebula] {combine texture + previous}
       	SetTexture [_BackStarfield] {combine texture + previous }
    }
    Pass {
        SetTexture [_LeftSky] { combine texture }
        SetTexture [_LeftNebula] {combine texture + previous}
        SetTexture [_LeftStarfield] {combine texture + previous }
    }
    Pass {
        SetTexture [_RightSky] { combine texture }
        SetTexture [_RightNebula] {combine texture + previous}
        SetTexture [_RightStarfield] {combine texture + previous }
    }
    Pass {
        SetTexture [_UpSky] { combine texture }
        SetTexture [_UpNebula] {combine texture + previous}
        SetTexture [_UpStarfield] { combine texture + previous }
    }
    Pass {
        SetTexture [_DownSky] { combine texture }
        SetTexture [_DownNebula] {combine texture + previous}
        SetTexture [_DownNStarfield] {combine texture + previous }
    }
}

Fallback "RenderFX/Skybox", 1
}