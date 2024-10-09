Shader "Custom/TileWithVisibleBorder"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} 
        _BorderColor ("Border Color", Color) = (1,1,1,1)
        _BorderWidth ("Border Width", Range(0, 0.5)) = 0.1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        // Blend pour gestion de la transparence
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BorderColor;
            float _BorderWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculer la distance par rapport aux bords
                float distToBorder = min(i.uv.x, min(1.0 - i.uv.x, min(i.uv.y, 1.0 - i.uv.y)));

                // Si on est dans la bordure, on affiche la couleur de la bordure
                if (distToBorder < _BorderWidth)
                {
                    return _BorderColor;
                }

                // Sinon, rendre transparent
                return fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
