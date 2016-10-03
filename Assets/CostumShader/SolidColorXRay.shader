Shader "XRay/SolidColorXray" 
{
    Properties 
    {
    	_Color ("Base Color", Color) = (0, 0, 0, 0)
        _MainTex ("Base (RGB)", 2D) = "black" { }
    }
    SubShader 
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Geometry+1" }

        Blend SrcAlpha OneMinusSrcAlpha
	    Cull Off
	    LOD 200
	 
	    CGPROGRAM
	    #pragma surface surf Lambert
	 
	    fixed4 _Color;
	 
	    // Note: pointless texture coordinate. I couldn't get Unity (or Cg)
	    //       to accept an empty Input structure or omit the inputs.
	    struct Input {
	      float2 uv_MainTex;
	    };
	 
	    void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color.rgb;
			o.Emission = _Color.rgb; // * _Color.a;
			o.Alpha = _Color.a;
	    }
	    ENDCG

        Pass 
        { 
            ZTest Less
        }
        Pass 
        { 
            ZTest Greater

            Color [_Color]
        }
    }
}