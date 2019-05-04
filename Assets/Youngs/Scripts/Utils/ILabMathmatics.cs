using Unity.Mathematics;
using System;
using Unity.Collections;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace ILab.Youngs
{
    public static class ILabMathmatics
    {

        public static float3 _positionVibrate(float3 _pos)
        {
            byte[] randomBytes = new byte[4];
            new System.Random().NextBytes(randomBytes);
            Random randomGenerator = new Random((uint)System.BitConverter.ToUInt32(randomBytes, 0));

            var aAxisOffset = randomGenerator.NextFloat(0.75f, 1.25f);
            var bAxisOffset = randomGenerator.NextFloat(0.95f, 1.05f);
            var heightOffset = randomGenerator.NextFloat(1.5f, 3.5f);
            var planeOffset = randomGenerator.NextFloat(20f, 30f);
            var x = _pos.x;
            var y = _pos.y;
            var z = _pos.z;

            var _val = new float3
                (
                    x + noise.cnoise(new float2(y * aAxisOffset, z * bAxisOffset))* planeOffset,
                    y + noise.cnoise(new float2(x * aAxisOffset, z * bAxisOffset))* heightOffset,
                    z + noise.cnoise(new float2(x * aAxisOffset, y * bAxisOffset))* planeOffset
                );


            return _val;
        }

    }

}

