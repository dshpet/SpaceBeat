// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30071,y:32905|normal-698-RGB,emission-1849-OUT,custl-169-OUT;n:type:ShaderForge.SFN_NormalVector,id:3,x:33413,y:32795,pt:True;n:type:ShaderForge.SFN_LightVector,id:5,x:33413,y:32936;n:type:ShaderForge.SFN_Dot,id:7,x:33187,y:32868,dt:1|A-3-OUT,B-5-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:32245,y:32560,ptlb:Diffuse Map,ptin:_DiffuseMap,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:13,x:32556,y:32873,cmnt:Diffuse Power|VAL-7-OUT,EXP-14-OUT;n:type:ShaderForge.SFN_RemapRange,id:14,x:32760,y:32959,frmn:0,frmx:1,tomn:5,tomx:0.5|IN-15-OUT;n:type:ShaderForge.SFN_Slider,id:15,x:32950,y:33054,ptlb:Diffuse Power,ptin:_DiffusePower,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:20,x:31700,y:32838|A-1541-OUT,B-125-RGB,C-1956-OUT;n:type:ShaderForge.SFN_LightColor,id:125,x:31891,y:32935;n:type:ShaderForge.SFN_NormalVector,id:146,x:32798,y:33351,pt:False;n:type:ShaderForge.SFN_ViewVector,id:147,x:32798,y:33512;n:type:ShaderForge.SFN_Dot,id:148,x:32588,y:33425,dt:1|A-146-OUT,B-147-OUT;n:type:ShaderForge.SFN_Vector1,id:151,x:32800,y:33651,v1:1;n:type:ShaderForge.SFN_Subtract,id:152,x:32578,y:33692|A-151-OUT,B-153-OUT;n:type:ShaderForge.SFN_Slider,id:153,x:32800,y:33727,ptlb:Atm Size,ptin:_AtmSize,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:154,x:32370,y:33692,frmn:0,frmx:1,tomn:0,tomx:10|IN-152-OUT;n:type:ShaderForge.SFN_Power,id:155,x:32091,y:33425|VAL-573-OUT,EXP-154-OUT;n:type:ShaderForge.SFN_Multiply,id:165,x:31183,y:33412,cmnt:Final Atmosphere|A-1671-OUT,B-155-OUT,C-168-OUT;n:type:ShaderForge.SFN_Color,id:166,x:31823,y:33520,ptlb:Atm Color,ptin:_AtmColor,glob:False,c1:0.07450981,c2:0.4666667,c3:0.9215686,c4:1;n:type:ShaderForge.SFN_Slider,id:167,x:31823,y:33716,ptlb:Atm Power,ptin:_AtmPower,min:1,cur:2,max:10;n:type:ShaderForge.SFN_Multiply,id:168,x:31564,y:33577|A-166-RGB,B-167-OUT;n:type:ShaderForge.SFN_Add,id:169,x:30614,y:33232|A-1929-OUT,B-165-OUT;n:type:ShaderForge.SFN_OneMinus,id:573,x:32345,y:33425|IN-148-OUT;n:type:ShaderForge.SFN_Tex2d,id:698,x:30466,y:32574,ptlb:Normal Map,ptin:_NormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:751,x:31459,y:31336,ptlb:Emission Map,ptin:_EmissionMap,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:771,x:31657,y:31937,ptlb:Intensity,ptin:_Intensity,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_RemapRange,id:772,x:31443,y:31906,frmn:0,frmx:1,tomn:0,tomx:10|IN-771-OUT;n:type:ShaderForge.SFN_Multiply,id:773,x:30813,y:31851|A-751-RGB,B-1647-OUT,C-1105-RGB,D-1807-OUT;n:type:ShaderForge.SFN_Add,id:774,x:30635,y:32892|A-1945-OUT,B-165-OUT;n:type:ShaderForge.SFN_OneMinus,id:794,x:32075,y:31502|IN-13-OUT;n:type:ShaderForge.SFN_Power,id:795,x:31716,y:31558|VAL-794-OUT,EXP-800-OUT;n:type:ShaderForge.SFN_Slider,id:798,x:32168,y:31763,ptlb:Emission FallOff,ptin:_EmissionFallOff,min:0,cur:0,max:0.5;n:type:ShaderForge.SFN_RemapRange,id:800,x:31918,y:31721,frmn:0,frmx:1,tomn:1,tomx:10|IN-798-OUT;n:type:ShaderForge.SFN_Color,id:1105,x:31463,y:31687,ptlb:Emission Color,ptin:_EmissionColor,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:1541,x:31992,y:32786|A-8-RGB,B-1596-OUT;n:type:ShaderForge.SFN_AmbientLight,id:1562,x:31987,y:33248;n:type:ShaderForge.SFN_Add,id:1596,x:32173,y:32909|A-13-OUT,B-1622-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1622,x:31790,y:33146,ptlb:Enable Ambient,ptin:_EnableAmbient,on:False|A-1629-OUT,B-1562-RGB;n:type:ShaderForge.SFN_Vector1,id:1629,x:32017,y:33168,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1647,x:31463,y:31536,ptlb:Only Shadow,ptin:_OnlyShadow,on:False|A-1650-OUT,B-795-OUT;n:type:ShaderForge.SFN_Vector1,id:1650,x:31731,y:31495,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:1671,x:31484,y:33297,ptlb:Atm Full Bright,ptin:_AtmFullBright,on:False|A-13-OUT,B-1674-OUT;n:type:ShaderForge.SFN_Vector1,id:1674,x:31717,y:33386,v1:1;n:type:ShaderForge.SFN_Time,id:1693,x:32125,y:32015;n:type:ShaderForge.SFN_Cos,id:1694,x:31801,y:32040|IN-1738-OUT;n:type:ShaderForge.SFN_Multiply,id:1738,x:32125,y:32154|A-1693-T,B-1748-OUT;n:type:ShaderForge.SFN_Slider,id:1748,x:32380,y:32119,ptlb:Pulsation Speed,ptin:_PulsationSpeed,min:0,cur:1,max:10;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:1771,x:31358,y:32152|IN-1827-OUT,IMIN-1772-OUT,IMAX-1773-OUT,OMIN-1775-OUT,OMAX-1778-OUT;n:type:ShaderForge.SFN_Vector1,id:1772,x:31801,y:32199,v1:0;n:type:ShaderForge.SFN_Vector1,id:1773,x:31801,y:32252,v1:1;n:type:ShaderForge.SFN_Slider,id:1775,x:31768,y:32324,ptlb:Min Intensity,ptin:_MinIntensity,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:1778,x:31767,y:32419,ptlb:Max Intensity,ptin:_MaxIntensity,min:0,cur:0,max:5;n:type:ShaderForge.SFN_SwitchProperty,id:1807,x:31112,y:32056,ptlb:Pulsation,ptin:_Pulsation,on:False|A-772-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Add,id:1827,x:31639,y:32072|A-1694-OUT,B-1773-OUT;n:type:ShaderForge.SFN_Blend,id:1849,x:30415,y:32933,blmd:2,clmp:True|SRC-773-OUT,DST-774-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:1926,x:31379,y:32696,ptlb:Full Bright,ptin:_FullBright,on:False;n:type:ShaderForge.SFN_If,id:1929,x:31194,y:33012|A-1926-OUT,B-1930-OUT,GT-8-RGB,EQ-20-OUT,LT-20-OUT;n:type:ShaderForge.SFN_Vector1,id:1930,x:31421,y:32750,v1:0;n:type:ShaderForge.SFN_If,id:1945,x:30983,y:32873|A-1926-OUT,B-1930-OUT,GT-1930-OUT,EQ-1929-OUT,LT-1929-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1956,x:32033,y:32993;proporder:1622-8-698-15-1926-166-167-153-1671-1105-751-1647-798-771-1807-1775-1778-1748;pass:END;sub:END;*/

Shader "Space Builder/Planet BAE" {
    Properties {
        [MaterialToggle] _EnableAmbient ("Enable Ambient", Float ) = 0
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _DiffusePower ("Diffuse Power", Range(0, 1)) = 0.5
        [MaterialToggle] _FullBright ("Full Bright", Float ) = 0
        _AtmColor ("Atm Color", Color) = (0.07450981,0.4666667,0.9215686,1)
        _AtmPower ("Atm Power", Range(1, 10)) = 2
        _AtmSize ("Atm Size", Range(0, 1)) = 0.5
        [MaterialToggle] _AtmFullBright ("Atm Full Bright", Float ) = 0
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionMap ("Emission Map", 2D) = "black" {}
        [MaterialToggle] _OnlyShadow ("Only Shadow", Float ) = 1
        _EmissionFallOff ("Emission FallOff", Range(0, 0.5)) = 0
        _Intensity ("Intensity", Range(0, 1)) = 0.2
        [MaterialToggle] _Pulsation ("Pulsation", Float ) = 2
        _MinIntensity ("Min Intensity", Range(0, 5)) = 0
        _MaxIntensity ("Max Intensity", Range(0, 5)) = 0
        _PulsationSpeed ("Pulsation Speed", Range(0, 10)) = 1
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
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Intensity;
            uniform float _EmissionFallOff;
            uniform float4 _EmissionColor;
            uniform fixed _EnableAmbient;
            uniform fixed _OnlyShadow;
            uniform fixed _AtmFullBright;
            uniform float _PulsationSpeed;
            uniform float _MinIntensity;
            uniform float _MaxIntensity;
            uniform fixed _Pulsation;
            uniform fixed _FullBright;
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
                float2 node_1970 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1970.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float node_13 = pow(max(0,dot(normalDirection,lightDirection)),(_DiffusePower*-4.5+5.0)); // Diffuse Power
                float4 node_1693 = _Time + _TimeEditor;
                float node_1773 = 1.0;
                float node_1772 = 0.0;
                float node_1930 = 0.0;
                float node_1945_if_leA = step(_FullBright,node_1930);
                float node_1945_if_leB = step(node_1930,_FullBright);
                float node_1929_if_leA = step(_FullBright,node_1930);
                float node_1929_if_leB = step(node_1930,_FullBright);
                float4 node_8 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_1970.rg, _DiffuseMap));
                float3 node_20 = ((node_8.rgb*(node_13+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient )))*_LightColor0.rgb*attenuation);
                float3 node_1929 = lerp((node_1929_if_leA*node_20)+(node_1929_if_leB*node_8.rgb),node_20,node_1929_if_leA*node_1929_if_leB);
                float3 node_165 = (lerp( node_13, 1.0, _AtmFullBright )*pow((1.0 - max(0,dot(i.normalDir,viewDirection))),((1.0-_AtmSize)*10.0+0.0))*(_AtmColor.rgb*_AtmPower)); // Final Atmosphere
                float3 emissive = saturate((1.0-((1.0-(lerp((node_1945_if_leA*node_1929)+(node_1945_if_leB*node_1930),node_1929,node_1945_if_leA*node_1945_if_leB)+node_165))/(tex2D(_EmissionMap,TRANSFORM_TEX(node_1970.rg, _EmissionMap)).rgb*lerp( 1.0, pow((1.0 - node_13),(_EmissionFallOff*9.0+1.0)), _OnlyShadow )*_EmissionColor.rgb*lerp( (_Intensity*10.0+0.0), (_MinIntensity + ( ((cos((node_1693.g*_PulsationSpeed))+node_1773) - node_1772) * (_MaxIntensity - _MinIntensity) ) / (node_1773 - node_1772)), _Pulsation )))));
                float3 finalColor = emissive + (node_1929+node_165);
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
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Intensity;
            uniform float _EmissionFallOff;
            uniform float4 _EmissionColor;
            uniform fixed _EnableAmbient;
            uniform fixed _OnlyShadow;
            uniform fixed _AtmFullBright;
            uniform float _PulsationSpeed;
            uniform float _MinIntensity;
            uniform float _MaxIntensity;
            uniform fixed _Pulsation;
            uniform fixed _FullBright;
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
                float2 node_1971 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_1971.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_1930 = 0.0;
                float node_1929_if_leA = step(_FullBright,node_1930);
                float node_1929_if_leB = step(node_1930,_FullBright);
                float4 node_8 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_1971.rg, _DiffuseMap));
                float node_13 = pow(max(0,dot(normalDirection,lightDirection)),(_DiffusePower*-4.5+5.0)); // Diffuse Power
                float3 node_20 = ((node_8.rgb*(node_13+lerp( 0.0, UNITY_LIGHTMODEL_AMBIENT.rgb, _EnableAmbient )))*_LightColor0.rgb*attenuation);
                float3 node_1929 = lerp((node_1929_if_leA*node_20)+(node_1929_if_leB*node_8.rgb),node_20,node_1929_if_leA*node_1929_if_leB);
                float3 node_165 = (lerp( node_13, 1.0, _AtmFullBright )*pow((1.0 - max(0,dot(i.normalDir,viewDirection))),((1.0-_AtmSize)*10.0+0.0))*(_AtmColor.rgb*_AtmPower)); // Final Atmosphere
                float3 finalColor = (node_1929+node_165);
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
