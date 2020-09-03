// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SimerrorObject"
{
	Properties
	{
		_Speed("Speed", Float) = -0.5
		_Scale("Scale", Float) = 0.33
		_Smoothing("Smoothing", Float) = 0.47
		_Intensity("Intensity", Float) = 2
		_Color("Color", Color) = (0,0.7953818,1,1)
		_DefaultAlpha("DefaultAlpha", Float) = 0
		_DefaultColor("DefaultColor", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _DefaultColor;
		uniform float _Smoothing;
		uniform float _Speed;
		uniform float _Scale;
		uniform float _Intensity;
		uniform float4 _Color;
		uniform float _DefaultAlpha;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float mulTime25 = _Time.y * _Speed;
			float smoothstepResult17 = smoothstep( _Smoothing , 1.0 , ( abs( ( frac( ( ( ase_worldPos.y + mulTime25 ) * _Scale ) ) + -0.5 ) ) * 2.0 ));
			float4 lerpResult35 = lerp( _DefaultColor , ( ( smoothstepResult17 * _Intensity ) * _Color ) , smoothstepResult17);
			o.Albedo = lerpResult35.rgb;
			o.Emission = lerpResult35.rgb;
			float clampResult33 = clamp( ( smoothstepResult17 + _DefaultAlpha ) , 0.0 , 1.0 );
			o.Alpha = clampResult33;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
1920;0;1920;1059;513.4904;-370.8029;1.130666;True;True
Node;AmplifyShaderEditor.RangedFloatNode;26;-2683,420.4756;Inherit;False;Property;_Speed;Speed;0;0;Create;True;0;0;False;0;-0.5;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;25;-2471.505,355.1818;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-2534.044,79.83476;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-2099.614,140.848;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-2099.656,382.4586;Inherit;False;Property;_Scale;Scale;2;0;Create;True;0;0;False;0;0.33;0.33;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1780.171,157.8502;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;7;-1581.344,156.6875;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1549.019,532.1336;Inherit;False;Constant;_Float1;Float 1;0;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-1279.034,265.984;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;13;-911.6978,260.1604;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1113.427,629.5812;Inherit;False;Constant;_Float2;Float 2;0;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-627.427,298.5812;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-695.5901,614.4291;Inherit;False;Property;_Smoothing;Smoothing;3;0;Create;True;0;0;False;0;0.47;0.47;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-667.0729,779.7189;Inherit;False;Constant;_Float4;Float 4;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;17;-302.5231,463.9001;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-238.2006,760.6311;Inherit;False;Property;_Intensity;Intensity;4;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-69.9211,1180.176;Inherit;False;Property;_DefaultAlpha;DefaultAlpha;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;27.79938,665.6311;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;27;-290.2006,926.6311;Inherit;False;Property;_Color;Color;5;0;Create;True;0;0;False;0;0,0.7953818,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;32;266.8281,1065.094;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;232.7994,713.6311;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;34;332.8356,431.0063;Inherit;False;Property;_DefaultColor;DefaultColor;7;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;33;424.9663,1131.034;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;587.7786,630.856;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;814.1995,752.8988;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SimerrorObject;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;7;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;TransparentCutout;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;26;0
WireConnection;24;0;1;2
WireConnection;24;1;25;0
WireConnection;8;0;24;0
WireConnection;8;1;3;0
WireConnection;7;0;8;0
WireConnection;11;0;7;0
WireConnection;11;1;14;0
WireConnection;13;0;11;0
WireConnection;20;0;13;0
WireConnection;20;1;21;0
WireConnection;17;0;20;0
WireConnection;17;1;22;0
WireConnection;17;2;23;0
WireConnection;28;0;17;0
WireConnection;28;1;29;0
WireConnection;32;0;17;0
WireConnection;32;1;31;0
WireConnection;30;0;28;0
WireConnection;30;1;27;0
WireConnection;33;0;32;0
WireConnection;35;0;34;0
WireConnection;35;1;30;0
WireConnection;35;2;17;0
WireConnection;0;0;35;0
WireConnection;0;2;35;0
WireConnection;0;9;33;0
ASEEND*/
//CHKSM=733E5057873FA94D40FA510864320D18F7EB478C