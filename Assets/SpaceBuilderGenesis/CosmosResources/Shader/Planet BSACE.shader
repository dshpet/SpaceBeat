// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30071,y:32905|normal-698-RGB,emission-774-OUT,custl-169-OUT;n:type:ShaderForge.SFN_NormalVector,id:3,x:33483,y:32731,pt:True;n:type:ShaderForge.SFN_LightVector,id:5,x:33483,y:32872;n:type:ShaderForge.SFN_Dot,id:7,x:33257,y:32804,dt:1|A-3-OUT,B-5-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:32167,y:32496,ptlb:Diffuse Map,ptin:_DiffuseMap,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:13,x:32626,y:32809,cmnt:Diffuse Power|VAL-7-OUT,EXP-14-OUT;n:type:ShaderForge.SFN_RemapRange,id:14,x:32830,y:32895,frmn:0,frmx:1,tomn:5,tomx:0.5|IN-15-OUT;n:type:ShaderForge.SFN_Slider,id:15,x:33020,y:32990,ptlb:Diffuse Power,ptin:_DiffusePower,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:20,x:31137,y:32857|A-1541-OUT,B-125-RGB,C-592-OUT,D-1668-OUT;n:type:ShaderForge.SFN_LightColor,id:125,x:31469,y:32917;n:type:ShaderForge.SFN_NormalVector,id:146,x:33462,y:33280,pt:False;n:type:ShaderForge.SFN_ViewVector,id:147,x:33462,y:33441;n:type:ShaderForge.SFN_Dot,id:148,x:33252,y:33354,dt:1|A-146-OUT,B-147-OUT;n:type:ShaderForge.SFN_Vector1,id:151,x:33464,y:33580,v1:1;n:type:ShaderForge.SFN_Subtract,id:152,x:33242,y:33621|A-151-OUT,B-153-OUT;n:type:ShaderForge.SFN_Slider,id:153,x:33464,y:33656,ptlb:Atm Size,ptin:_AtmSize,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:154,x:33034,y:33621,frmn:0,frmx:1,tomn:0,tomx:10|IN-152-OUT;n:type:ShaderForge.SFN_Power,id:155,x:32755,y:33354|VAL-573-OUT,EXP-154-OUT;n:type:ShaderForge.SFN_Multiply,id:165,x:31847,y:33341,cmnt:Final Atmosphere|A-13-OUT,B-155-OUT,C-168-OUT;n:type:ShaderForge.SFN_Color,id:166,x:32487,y:33449,ptlb:Atm Color,ptin:_AtmColor,glob:False,c1:0.07450981,c2:0.4666667,c3:0.9215686,c4:1;n:type:ShaderForge.SFN_Slider,id:167,x:32487,y:33645,ptlb:Atm Power,ptin:_AtmPower,min:1,cur:2,max:10;n:type:ShaderForge.SFN_Multiply,id:168,x:32228,y:33506|A-166-RGB,B-167-OUT;n:type:ShaderForge.SFN_Add,id:169,x:30504,y:33312|A-20-OUT,B-165-OUT,C-1289-OUT,D-486-OUT;n:type:ShaderForge.SFN_Tex2d,id:471,x:32125,y:34884,ptlb:Cloud Map,ptin:_CloudMap,ntxv:2,isnm:False|UVIN-482-OUT;n:type:ShaderForge.SFN_Slider,id:472,x:33054,y:34792,ptlb:Cloud Height,ptin:_CloudHeight,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:474,x:32843,y:34786,frmn:0,frmx:1,tomn:0,tomx:2|IN-472-OUT;n:type:ShaderForge.SFN_Append,id:476,x:32841,y:35024|A-1253-OUT,B-477-OUT;n:type:ShaderForge.SFN_Vector1,id:477,x:33074,y:35180,v1:0;n:type:ShaderForge.SFN_TexCoord,id:478,x:32843,y:34616,uv:0;n:type:ShaderForge.SFN_Parallax,id:479,x:32592,y:34741,cmnt:Final Height|UVIN-478-UVOUT,HEI-474-OUT;n:type:ShaderForge.SFN_Time,id:480,x:32841,y:35205;n:type:ShaderForge.SFN_Multiply,id:481,x:32623,y:35089,cmnt:Rotation|A-476-OUT,B-480-T;n:type:ShaderForge.SFN_Add,id:482,x:32369,y:34914|A-479-UVOUT,B-481-OUT;n:type:ShaderForge.SFN_Multiply,id:486,x:31863,y:34886|A-471-RGB,B-1596-OUT,C-1576-RGB,D-1668-OUT;n:type:ShaderForge.SFN_OneMinus,id:572,x:31599,y:35201|IN-486-OUT;n:type:ShaderForge.SFN_OneMinus,id:573,x:33009,y:33354|IN-148-OUT;n:type:ShaderForge.SFN_RemapRange,id:591,x:31584,y:35360,frmn:0,frmx:1,tomn:0,tomx:10|IN-593-OUT;n:type:ShaderForge.SFN_Power,id:592,x:31362,y:35317|VAL-572-OUT,EXP-591-OUT;n:type:ShaderForge.SFN_Slider,id:593,x:31841,y:35427,ptlb:Cloud Opacity,ptin:_CloudOpacity,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:698,x:30510,y:32639,ptlb:Normal Map,ptin:_NormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:751,x:31562,y:31605,ptlb:Emission Map,ptin:_EmissionMap,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:771,x:31776,y:31805,ptlb:Intensity,ptin:_Intensity,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_RemapRange,id:772,x:31562,y:31774,frmn:0,frmx:1,tomn:0,tomx:10|IN-771-OUT;n:type:ShaderForge.SFN_Multiply,id:773,x:31168,y:31730|A-751-RGB,B-772-OUT,C-795-OUT,D-1105-RGB;n:type:ShaderForge.SFN_Add,id:774,x:30510,y:32891|A-773-OUT,B-20-OUT,C-165-OUT;n:type:ShaderForge.SFN_OneMinus,id:794,x:31974,y:31940|IN-13-OUT;n:type:ShaderForge.SFN_Power,id:795,x:31562,y:31953|VAL-794-OUT,EXP-800-OUT;n:type:ShaderForge.SFN_Slider,id:798,x:31974,y:32201,ptlb:Emission FallOff,ptin:_EmissionFallOff,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_RemapRange,id:800,x:31793,y:32181,frmn:0,frmx:1,tomn:1,tomx:10|IN-798-OUT;n:type:ShaderForge.SFN_Color,id:1105,x:31562,y:32137,ptlb:Emission Color,ptin:_EmissionColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:1253,x:33398,y:35153,ptlb:Cloud Speed,ptin:_CloudSpeed,glob:False,v1:0.015;n:type:ShaderForge.SFN_ViewReflectionVector,id:1273,x:32760,y:33991;n:type:ShaderForge.SFN_Dot,id:1274,x:32528,y:33915,dt:1|A-1277-OUT,B-1273-OUT;n:type:ShaderForge.SFN_Power,id:1275,x:32267,y:33943|VAL-1274-OUT,EXP-1345-OUT;n:type:ShaderForge.SFN_LightVector,id:1277,x:32760,y:33855;n:type:ShaderForge.SFN_Color,id:1286,x:32332,y:34345,ptlb:Specular Color,ptin:_SpecularColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1287,x:32332,y:34165,ptlb:Specular Map,ptin:_SpecularMap,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1288,x:32044,y:34210|A-1287-RGB,B-1286-RGB;n:type:ShaderForge.SFN_Multiply,id:1289,x:31809,y:33964,cmnt:Final Specular|A-1275-OUT,B-1288-OUT,C-592-OUT;n:type:ShaderForge.SFN_Slider,id:1344,x:32831,y:34188,ptlb:Gloss,ptin:_Gloss,min:1,cur:7,max:10;n:type:ShaderForge.SFN_Exp,id:1345,x:32535,y:34165,et:1|IN-1344-OUT;n:type:ShaderForge.SFN_Multiply,id:1541,x:31893,y:32646|A-8-RGB,B-1596-OUT;n:type:ShaderForge.SFN_AmbientLight,id:1562,x:32651,y:33177;n:type:ShaderForge.SFN_Color,id:1576,x:32089,y:35089,ptlb:Cloud Color,ptin:_CloudColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Add,id:1596,x:32243,y:32845|A-13-OUT,B-1622-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1622,x:32471,y:33121,ptlb:Enable Ambient,ptin:_EnableAmbient,on:False|A-1629-OUT,B-1562-RGB;n:type:ShaderForge.SFN_Vector1,id:1629,x:32681,y:33097,v1:0;n:type:ShaderForge.SFN_LightAttenuation,id:1668,x:31483,y:33083;proporder:1622-8-698-15-1286-1287-1344-166-167-153-1576-471-472-593-1253-1105-751-771-798;pass:END;sub:END;*/

Shader "Space Builder/Planet BSACE" {
    Properties {
        [MaterialToggle] _EnableAmbient ("Enable Ambient", Float ) = 0
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _DiffusePower ("Diffuse Power", Range(0, 1)) = 0.5
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _SpecularMap ("Specular Map", 2D) = "white" {}
        _Gloss ("Gloss", Range(1, 10)) = 7
        _AtmColor ("Atm Color", Color) = (0.07450981,0.4666667,0.9215686,1)
        _AtmPower ("Atm Power", Range(1, 10)) = 2
        _AtmSize ("Atm Size", Range(0, 1)) = 0.5
        _CloudColor ("Cloud Color", Color) = (1,1,1,1)
        _CloudMap ("Cloud Map", 2D) = "black" {}
        _CloudHeight ("Cloud Height", Range(0, 1)) = 0.5
        _CloudOpacity ("Cloud Opacity", Range(0, 1)) = 0.5
        _CloudSpeed ("Cloud Speed", Float ) = 0.015
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionMap ("Emission Map", 2D) = "black" {}
        _Intensity ("Intensity", Range(0, 1)) = 0.2
        _EmissionFallOff ("Emission FallOff", Range(0, 1)) = 0.2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _DiffusePower;
            uniform float _AtmSize;
            uniform float4 _AtmColor;
            uniform float _AtmPower;
            uniform sampler2D _CloudMap; uniform float4 _CloudMap_ST;
            uniform float _CloudHeight;
            uniform float _CloudOpacity;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Intensity;
            uniform float _EmissionFallOff;
            uniform float4 _EmissionColor;
            uniform float _CloudSpeed;
            uniform float4 _SpecularColor;
            uniform sampler2D _SpecularMap; uniform float4 _SpecularMap_ST;
            uniform float _Gloss;
            uniform float4 _CloudColor;
            uniform fixed _EnableAmbient;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_1709 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1709.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float node_13 = pow(max(0,dot(normalDirection,lightDirection)),(_DiffusePower*-4.5+5.0)); // Diffuse Power
                float3 node_1596 = (node_13+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient ));
                float4 node_480 = _Time + _TimeEditor;
                float2 node_482 = ((0.05*((_CloudHeight*2.0+0.0) - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0.rg).rg+(float2(_CloudSpeed,0.0)*node_480.g));
                float node_1668 = attenuation;
                float3 node_486 = (tex2D(_CloudMap,TRANSFORM_TEX(node_482, _CloudMap)).rgb*node_1596*_CloudColor.rgb*node_1668);
                float3 node_592 = pow((1.0 - node_486),(_CloudOpacity*10.0+0.0));
                float3 node_20 = ((tex2D(_DiffuseMap,TRANSFORM_TEX(node_1709.rg, _DiffuseMap)).rgb*node_1596)*_LightColor0.rgb*node_592*node_1668);
                float3 node_165 = (node_13*pow((1.0 - max(0,dot(i.normalDir,viewDirection))),((1.0-_AtmSize)*10.0+0.0))*(_AtmColor.rgb*_AtmPower)); // Final Atmosphere
                float3 emissive = ((tex2D(_EmissionMap,TRANSFORM_TEX(node_1709.rg, _EmissionMap)).rgb*(_Intensity*10.0+0.0)*pow((1.0 - node_13),(_EmissionFallOff*9.0+1.0))*_EmissionColor.rgb)+node_20+node_165);
                float3 finalColor = emissive + (node_20+node_165+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_Gloss))*(tex2D(_SpecularMap,TRANSFORM_TEX(node_1709.rg, _SpecularMap)).rgb*_SpecularColor.rgb)*node_592)+node_486);
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
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _DiffusePower;
            uniform float _AtmSize;
            uniform float4 _AtmColor;
            uniform float _AtmPower;
            uniform sampler2D _CloudMap; uniform float4 _CloudMap_ST;
            uniform float _CloudHeight;
            uniform float _CloudOpacity;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Intensity;
            uniform float _EmissionFallOff;
            uniform float4 _EmissionColor;
            uniform float _CloudSpeed;
            uniform float4 _SpecularColor;
            uniform sampler2D _SpecularMap; uniform float4 _SpecularMap_ST;
            uniform float _Gloss;
            uniform float4 _CloudColor;
            uniform fixed _EnableAmbient;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_1710 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1710.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_13 = pow(max(0,dot(normalDirection,lightDirection)),(_DiffusePower*-4.5+5.0)); // Diffuse Power
                float3 node_1596 = (node_13+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient ));
                float4 node_480 = _Time + _TimeEditor;
                float2 node_482 = ((0.05*((_CloudHeight*2.0+0.0) - 0.5)*mul(tangentTransform, viewDirection).xy + i.uv0.rg).rg+(float2(_CloudSpeed,0.0)*node_480.g));
                float node_1668 = attenuation;
                float3 node_486 = (tex2D(_CloudMap,TRANSFORM_TEX(node_482, _CloudMap)).rgb*node_1596*_CloudColor.rgb*node_1668);
                float3 node_592 = pow((1.0 - node_486),(_CloudOpacity*10.0+0.0));
                float3 node_20 = ((tex2D(_DiffuseMap,TRANSFORM_TEX(node_1710.rg, _DiffuseMap)).rgb*node_1596)*_LightColor0.rgb*node_592*node_1668);
                float3 node_165 = (node_13*pow((1.0 - max(0,dot(i.normalDir,viewDirection))),((1.0-_AtmSize)*10.0+0.0))*(_AtmColor.rgb*_AtmPower)); // Final Atmosphere
                float3 finalColor = (node_20+node_165+(pow(max(0,dot(lightDirection,viewReflectDirection)),exp2(_Gloss))*(tex2D(_SpecularMap,TRANSFORM_TEX(node_1710.rg, _SpecularMap)).rgb*_SpecularColor.rgb)*node_592)+node_486);
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
