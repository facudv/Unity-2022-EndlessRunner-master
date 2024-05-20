// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_CannonSubstance_DefaultMaterial_AlbedoTransparency("CannonSubstance_DefaultMaterial_AlbedoTransparency", 2D) = "white" {}
		_CannonSubstance_DefaultMaterial_Emission("CannonSubstance_DefaultMaterial_Emission", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _CannonSubstance_DefaultMaterial_AlbedoTransparency;
		uniform float4 _CannonSubstance_DefaultMaterial_AlbedoTransparency_ST;
		uniform sampler2D _CannonSubstance_DefaultMaterial_Emission;
		uniform float4 _CannonSubstance_DefaultMaterial_Emission_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_CannonSubstance_DefaultMaterial_AlbedoTransparency = i.uv_texcoord * _CannonSubstance_DefaultMaterial_AlbedoTransparency_ST.xy + _CannonSubstance_DefaultMaterial_AlbedoTransparency_ST.zw;
			float2 uv_CannonSubstance_DefaultMaterial_Emission = i.uv_texcoord * _CannonSubstance_DefaultMaterial_Emission_ST.xy + _CannonSubstance_DefaultMaterial_Emission_ST.zw;
			float4 tex2DNode2 = tex2D( _CannonSubstance_DefaultMaterial_Emission, uv_CannonSubstance_DefaultMaterial_Emission );
			o.Albedo = saturate( ( tex2D( _CannonSubstance_DefaultMaterial_AlbedoTransparency, uv_CannonSubstance_DefaultMaterial_AlbedoTransparency ) - tex2DNode2 ) ).rgb;
			float mulTime11 = _Time.y * 3.0;
			o.Emission = ( tex2DNode2 * ( ( sin( mulTime11 ) + 1.3 ) / 2.0 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16301
7;320;1586;511;1348.931;-116.0541;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;12;-1122.412,327.4425;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;11;-968.412,316.4426;Float;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;10;-791.3123,319.6426;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-785.6851,60.57896;Float;True;Property;_CannonSubstance_DefaultMaterial_Emission;CannonSubstance_DefaultMaterial_Emission;1;0;Create;True;0;0;False;0;f69f63d28ec1a9142aaa985c3e498e0e;f69f63d28ec1a9142aaa985c3e498e0e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-804.7399,-224.8809;Float;True;Property;_CannonSubstance_DefaultMaterial_AlbedoTransparency;CannonSubstance_DefaultMaterial_AlbedoTransparency;0;0;Create;True;0;0;False;0;b55fd1b4e7e1a9546b135d220f3db10a;b55fd1b4e7e1a9546b135d220f3db10a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-583.7662,319.5098;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;7;-292.0134,-56.57585;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;-465.7662,323.5098;Float;True;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;8;-137.3461,-64.34988;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-198.6403,215.1674;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;111,-83;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;12;0
WireConnection;10;0;11;0
WireConnection;14;0;10;0
WireConnection;7;0;1;0
WireConnection;7;1;2;0
WireConnection;15;0;14;0
WireConnection;8;0;7;0
WireConnection;13;0;2;0
WireConnection;13;1;15;0
WireConnection;0;0;8;0
WireConnection;0;2;13;0
ASEEND*/
//CHKSM=1EEAA55B6E21F7E7E895EB8B70E2FED39E8C0F09