Shader "Custom/BalatroSpinEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Colour1("Colour 1", Color) = (0.871, 0.267, 0.231, 1)
        _Colour2("Colour 2", Color) = (0.0, 0.42, 0.706, 1)
        _Colour3("Colour 3", Color) = (0.086, 0.137, 0.145, 1)
        _SpinSpeed("Spin Speed", Float) = 1
        _MoveSpeed("Move Speed", Float) = 2
        _Offset("Offset", Vector) = (0, 0, 0, 0)
        _Contrast("Contrast", Float) = 3.5
        _Lighting("Lighting", Float) = 0.4
        _SpinAmount("Spin Amount", Float) = 0.25
        _PixelFilter("Pixel Filter", Float) = 6000
        _IsRotating("Is Rotating", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Colour1, _Colour2, _Colour3;
            float _SpinSpeed, _MoveSpeed, _Contrast, _Lighting, _SpinAmount, _PixelFilter, _IsRotating;
            float4 _Offset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 screenPos : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = o.vertex.xy;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float time = _Time.y;
                float2 screenSize = float2(_ScreenParams.x, _ScreenParams.y);
                float pixelSize = length(screenSize) / _PixelFilter;

                float2 uv = i.screenPos.xy;
                uv = (floor(uv / pixelSize) * pixelSize - 0.5 * screenSize) / length(screenSize) - _Offset.xy;
                float uv_len = length(uv);

                float speed = _SpinSpeed * 1.0 * 0.2;
                if (_IsRotating > 0.5) speed = time * speed;
                speed += 302.2;

                float angle = atan2(uv.y, uv.x) + speed - 1.0 * 20.0 * (1.0 * _SpinAmount * uv_len + (1.0 - 1.0 * _SpinAmount));
                float2 mid = (screenSize / length(screenSize)) / 2.0;
                uv = float2((uv_len * cos(angle) + mid.x), (uv_len * sin(angle) + mid.y)) - mid;

                uv *= 30.0;
                speed = time * _MoveSpeed;
                float2 uv2 = float2(uv.x, uv.y);

                for (int j = 0; j < 5; j++) {
                    uv2 += sin(max(uv.x, uv.y)) + uv;
                    uv += 0.5 * float2(cos(5.1123314 + 0.353 * uv2.y + speed * 0.131121),
                                       sin(uv2.x - 0.113 * speed));
                    uv -= 1.0 * cos(uv.x + uv.y) - 1.0 * sin(uv.x * 0.711 - uv.y);
                }

                float contrast_mod = (0.25 * _Contrast + 0.5 * _SpinAmount + 1.2);
                float paint_res = clamp(length(uv) * 0.035 * contrast_mod, 0.0, 2.0);
                float c1p = max(0.0, 1.0 - contrast_mod * abs(1.0 - paint_res));
                float c2p = max(0.0, 1.0 - contrast_mod * abs(paint_res));
                float c3p = 1.0 - min(1.0, c1p + c2p);

                float light = (_Lighting - 0.2) * max(c1p * 5.0 - 4.0, 0.0)
                            + _Lighting * max(c2p * 5.0 - 4.0, 0.0);

                float3 rgbPart = c3p * _Colour3.rgb;
                float aPart = c3p * _Colour1.a;
                float4 color3 = float4(rgbPart, aPart);

                float4 col = (0.3 / _Contrast) * _Colour1 + (1.0 - 0.3 / _Contrast) * (_Colour1 * c1p + _Colour2 * c2p + color3) + light;

                return col;
            }
            ENDCG
        }
    }
}
