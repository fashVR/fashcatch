Shader "Unlit/CircleWipeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius("Wipe Radius", Float) = 0
        _Ratio("Ratio", Float) = 1
        _ColorR("ColorR", Float) = 0
        _ColorG("ColorG", Float) = 0
        _ColorB("ColorB", Float) = 0
        _ColorA("ColorA", Float) = 0
        _XAxisPercentage("Ratio", Float) = 1
        _YAxisPercentage("Ratio", Float) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                
            #pragma vertex vert
            #pragma fragment frag
            uniform float _Ratio;
            uniform float _XAxisPercentage;
            uniform float _YAxisPercentage;
            uniform float _ColorR;
            uniform float _ColorG;
            uniform float _ColorB;
            uniform float _ColorA;
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return col;
            float3 pos = float3((i.uv.x - _XAxisPercentage) / 1, (i.uv.y - _YAxisPercentage - 0.015) / _Ratio, 0);
            return length(pos) > _Radius / _Ratio ? fixed4(_ColorR, _ColorG, _ColorB, _ColorA) : col;
            }
            ENDCG
        }
    }
}
