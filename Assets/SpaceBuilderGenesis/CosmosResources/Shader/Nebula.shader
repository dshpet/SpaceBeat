// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32108,y:32618|diff-197-OUT,emission-197-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33629,y:32473,ptlb:Diffuse Map,ptin:_DiffuseMap,tex:d418380852ab97e4895e29c1a6d078b3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:11,x:33640,y:32675,ptlb:Color1,ptin:_Color1,glob:False,c1:0.4705882,c2:0.05536332,c3:0.08113588,c4:1;n:type:ShaderForge.SFN_Color,id:12,x:33640,y:32835,ptlb:Color2,ptin:_Color2,glob:False,c1:0.2,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:71,x:33051,y:32717|A-11-RGB,B-12-RGB,T-103-OUT;n:type:ShaderForge.SFN_Multiply,id:72,x:32673,y:32643|A-2-RGB,B-71-OUT;n:type:ShaderForge.SFN_Slider,id:102,x:33931,y:33048,ptlb:Mixing,ptin:_Mixing,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Power,id:103,x:33319,y:32968|VAL-2-RGB,EXP-122-OUT;n:type:ShaderForge.SFN_RemapRange,id:122,x:33640,y:33030,frmn:0,frmx:1,tomn:0,tomx:3|IN-102-OUT;n:type:ShaderForge.SFN_Power,id:197,x:32387,y:32652|VAL-72-OUT,EXP-207-OUT;n:type:ShaderForge.SFN_Slider,id:200,x:32838,y:32900,ptlb:Power,ptin:_Power,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_RemapRange,id:207,x:32626,y:32806,frmn:0,frmx:1,tomn:1.5,tomx:0.5|IN-200-OUT;proporder:2-11-12-102-200;pass:END;sub:END;*/

Shader "Space Builder/Nebula" {
    Properties {
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _Color1 ("Color1", Color) = (0.4705882,0.05536332,0.08113588,1)
        _Color2 ("Color2", Color) = (0.2,1,0,1)
        _Mixing ("Mixing", Range(0, 1)) = 0.5
        _Power ("Power", Range(0, 1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
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
            uniform float4 _LightColor0;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float _Mixing;
            uniform float _Power;
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
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float2 node_249 = i.uv0;
                float4 node_2 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_249.rg, _DiffuseMap));
                float3 node_72 = (node_2.rgb*lerp(_Color1.rgb,_Color2.rgb,pow(node_2.rgb,(_Mixing*3.0+0.0))));
                float3 node_197 = pow(node_72,(_Power*-1.0+1.5));
                float3 emissive = node_197;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_197;
                finalColor += emissive;
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
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform float _Mixing;
            uniform float _Power;
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
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float2 node_250 = i.uv0;
                float4 node_2 = tex2D(_DiffuseMap,TRANSFORM_TEX(node_250.rg, _DiffuseMap));
                float3 node_72 = (node_2.rgb*lerp(_Color1.rgb,_Color2.rgb,pow(node_2.rgb,(_Mixing*3.0+0.0))));
                float3 node_197 = pow(node_72,(_Power*-1.0+1.5));
                finalColor += diffuseLight * node_197;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
