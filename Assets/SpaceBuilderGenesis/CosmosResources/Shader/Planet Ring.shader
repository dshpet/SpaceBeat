// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,ufog:True,aust:False,igpj:True,qofs:10,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30071,y:32905|emission-1945-OUT,custl-1929-OUT,alpha-2091-OUT;n:type:ShaderForge.SFN_NormalVector,id:3,x:32784,y:32871,pt:True;n:type:ShaderForge.SFN_LightVector,id:5,x:32784,y:33012;n:type:ShaderForge.SFN_Dot,id:7,x:32558,y:32944,dt:1|A-3-OUT,B-5-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:31600,y:32490,ptlb:Diffuse Map,ptin:_DiffuseMap,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:15,x:32321,y:33130,ptlb:Diffuse Power,ptin:_DiffusePower,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:20,x:31207,y:32926|A-1541-OUT,B-125-RGB,C-1966-OUT;n:type:ShaderForge.SFN_LightColor,id:125,x:31482,y:33012;n:type:ShaderForge.SFN_Multiply,id:1541,x:31499,y:32874|A-2121-OUT,B-1596-OUT;n:type:ShaderForge.SFN_AmbientLight,id:1562,x:32321,y:33325;n:type:ShaderForge.SFN_Add,id:1596,x:31678,y:33012|A-7-OUT,B-1622-OUT,C-2100-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1622,x:32011,y:33245,ptlb:Enable Ambient,ptin:_EnableAmbient,on:False|A-1629-OUT,B-1562-RGB;n:type:ShaderForge.SFN_Vector1,id:1629,x:32321,y:33247,v1:0;n:type:ShaderForge.SFN_ToggleProperty,id:1926,x:30886,y:32784,ptlb:Full Bright,ptin:_FullBright,on:False;n:type:ShaderForge.SFN_If,id:1929,x:30646,y:33061|A-1926-OUT,B-1930-OUT,GT-2121-OUT,EQ-20-OUT,LT-20-OUT;n:type:ShaderForge.SFN_Vector1,id:1930,x:30928,y:32838,v1:0;n:type:ShaderForge.SFN_If,id:1945,x:30516,y:32849|A-1926-OUT,B-1930-OUT,GT-1930-OUT,EQ-1929-OUT,LT-1929-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1966,x:31472,y:33167;n:type:ShaderForge.SFN_Slider,id:1986,x:30995,y:33372,ptlb:Transparency,ptin:_Transparency,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:2065,x:30788,y:33283,frmn:0,frmx:1,tomn:2,tomx:0|IN-1986-OUT;n:type:ShaderForge.SFN_Multiply,id:2091,x:30590,y:33224|A-8-A,B-2065-OUT;n:type:ShaderForge.SFN_RemapRange,id:2100,x:32125,y:33093,frmn:0,frmx:1,tomn:0,tomx:5|IN-15-OUT;n:type:ShaderForge.SFN_Color,id:2120,x:31600,y:32680,ptlb:Color,ptin:_Color,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2121,x:31198,y:32549|A-8-RGB,B-2120-RGB;proporder:1622-2120-8-15-1986-1926;pass:END;sub:END;*/

Shader "Space Builder/Planet Ring" {
    Properties {
        [MaterialToggle] _EnableAmbient ("Enable Ambient", Float ) = 0
        _Color ("Color", Color) = (1,1,1,1)
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _DiffusePower ("Diffuse Power", Range(0, 1)) = 0.5
        _Transparency ("Transparency", Range(0, 1)) = 0.5
        [MaterialToggle] _FullBright ("Full Bright", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+10"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _DiffusePower;
            uniform fixed _EnableAmbient;
            uniform fixed _FullBright;
            uniform float _Transparency;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
////// Emissive:
                float node_1930 = 0.0;
                float node_1945_if_leA = step(_FullBright,node_1930);
                float node_1945_if_leB = step(node_1930,_FullBright);
                float node_1929_if_leA = step(_FullBright,node_1930);
                float node_1929_if_leB = step(node_1930,_FullBright);
                float2 node_2141 = i.uv0;
                float4 node_8 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_2141.rg, _DiffuseMap));
                float3 node_2121 = (node_8.rgb*_Color.rgb);
                float3 node_20 = ((node_2121*(max(0,dot(normalDirection,lightDirection))+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient )+(_DiffusePower*5.0+0.0)))*_LightColor0.rgb*attenuation);
                float3 node_1929 = lerp((node_1929_if_leA*node_20)+(node_1929_if_leB*node_2121),node_20,node_1929_if_leA*node_1929_if_leB);
                float3 emissive = lerp((node_1945_if_leA*node_1929)+(node_1945_if_leB*node_1930),node_1929,node_1945_if_leA*node_1945_if_leB);
                float3 finalColor = emissive + node_1929;
/// Final Color:
                return fixed4(finalColor,(node_8.a*(_Transparency*-2.0+2.0)));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _DiffusePower;
            uniform fixed _EnableAmbient;
            uniform fixed _FullBright;
            uniform float _Transparency;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_1930 = 0.0;
                float node_1929_if_leA = step(_FullBright,node_1930);
                float node_1929_if_leB = step(node_1930,_FullBright);
                float2 node_2142 = i.uv0;
                float4 node_8 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_2142.rg, _DiffuseMap));
                float3 node_2121 = (node_8.rgb*_Color.rgb);
                float3 node_20 = ((node_2121*(max(0,dot(normalDirection,lightDirection))+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient )+(_DiffusePower*5.0+0.0)))*_LightColor0.rgb*attenuation);
                float3 node_1929 = lerp((node_1929_if_leA*node_20)+(node_1929_if_leB*node_2121),node_20,node_1929_if_leA*node_1929_if_leB);
                float3 finalColor = node_1929;
/// Final Color:
                return fixed4(finalColor * (node_8.a*(_Transparency*-2.0+2.0)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
