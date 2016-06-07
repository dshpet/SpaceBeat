// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:1,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:2,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30900,y:32821|emission-1511-OUT,custl-1511-OUT,voffset-1499-OUT;n:type:ShaderForge.SFN_NormalVector,id:1477,x:32297,y:32087,pt:False;n:type:ShaderForge.SFN_LightVector,id:1478,x:32297,y:32246;n:type:ShaderForge.SFN_Dot,id:1479,x:32088,y:32136,dt:1|A-1477-OUT,B-1478-OUT;n:type:ShaderForge.SFN_ViewVector,id:1486,x:32509,y:33128;n:type:ShaderForge.SFN_Color,id:1488,x:31907,y:32690,ptlb:Color,ptin:_Color,glob:False,c1:0.006704159,c2:0.9117647,c3:0.9117647,c4:1;n:type:ShaderForge.SFN_NormalVector,id:1489,x:32509,y:32978,pt:False;n:type:ShaderForge.SFN_Dot,id:1490,x:32227,y:33059,dt:0|A-1489-OUT,B-1486-OUT;n:type:ShaderForge.SFN_Clamp01,id:1491,x:31986,y:33059|IN-1490-OUT;n:type:ShaderForge.SFN_Power,id:1492,x:31772,y:33046,cmnt:Glow|VAL-1491-OUT,EXP-1497-OUT;n:type:ShaderForge.SFN_Multiply,id:1494,x:31515,y:32769,cmnt:Final Glow|A-1488-RGB,B-1501-OUT,C-1492-OUT,D-1534-RGB;n:type:ShaderForge.SFN_Vector1,id:1497,x:32013,y:33230,v1:3;n:type:ShaderForge.SFN_NormalVector,id:1498,x:31483,y:33743,pt:False;n:type:ShaderForge.SFN_Multiply,id:1499,x:31283,y:33787|A-1498-OUT,B-1512-OUT;n:type:ShaderForge.SFN_Slider,id:1501,x:31907,y:32865,ptlb:Fall Off,ptin:_FallOff,min:1,cur:5,max:15;n:type:ShaderForge.SFN_Subtract,id:1511,x:31262,y:32656|A-1494-OUT,B-1517-OUT;n:type:ShaderForge.SFN_RemapRange,id:1512,x:31483,y:33907,frmn:0,frmx:1,tomn:0.01,tomx:0.03|IN-1520-OUT;n:type:ShaderForge.SFN_Power,id:1513,x:31760,y:32298|VAL-1479-OUT,EXP-1516-OUT;n:type:ShaderForge.SFN_Vector1,id:1516,x:32090,y:32367,v1:1.5;n:type:ShaderForge.SFN_SwitchProperty,id:1517,x:31514,y:32343,ptlb:Full Bright,ptin:_FullBright,on:False|A-1513-OUT,B-1518-OUT;n:type:ShaderForge.SFN_Vector1,id:1518,x:31760,y:32440,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1520,x:31785,y:33911,ptlb:Size,ptin:_Size,glob:False,v1:0.025;n:type:ShaderForge.SFN_Tex2d,id:1534,x:31754,y:33526,ptlb:Diffuse,ptin:_Diffuse,ntxv:0,isnm:False;proporder:1534-1488-1520-1501-1517;pass:END;sub:END;*/

Shader "Space Builder/Atmosphere" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Color ("Color", Color) = (0.006704159,0.9117647,0.9117647,1)
        _Size ("Size", Float ) = 0.025
        _FallOff ("Fall Off", Range(1, 15)) = 5
        [MaterialToggle] _FullBright ("Full Bright", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+2"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Front
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _FallOff;
            uniform fixed _FullBright;
            uniform float _Size;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
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
                o.normalDir = mul(float4(-v.normal,0), _World2Object).xyz;
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float2 node_1547 = i.uv0;
                float3 node_1511 = ((_Color.rgb*_FallOff*pow(saturate(dot(i.normalDir,viewDirection)),3.0)*tex2D(_Diffuse,TRANSFORM_TEX(node_1547.rg, _Diffuse)).rgb)-lerp( pow(max(0,dot(i.normalDir,lightDirection)),1.5), 0.0, _FullBright ));
                float3 emissive = node_1511;
                float3 finalColor = emissive + node_1511;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Front
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
            uniform float4 _Color;
            uniform float _FallOff;
            uniform fixed _FullBright;
            uniform float _Size;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
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
                o.normalDir = mul(float4(-v.normal,0), _World2Object).xyz;
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
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
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float2 node_1548 = i.uv0;
                float3 node_1511 = ((_Color.rgb*_FallOff*pow(saturate(dot(i.normalDir,viewDirection)),3.0)*tex2D(_Diffuse,TRANSFORM_TEX(node_1548.rg, _Diffuse)).rgb)-lerp( pow(max(0,dot(i.normalDir,lightDirection)),1.5), 0.0, _FullBright ));
                float3 finalColor = node_1511;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Front
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _Size;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.normalDir = mul(float4(-v.normal,0), _World2Object).xyz;
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _Size;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.normalDir = mul(float4(-v.normal,0), _World2Object).xyz;
                v.vertex.xyz += (v.normal*(_Size*0.02+0.01));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
