using UnityEngine;

namespace Helpers.Hex
{
    public class Cubic
    {

        public int Q;

        public int R;

        public int S;

        public int Z;

        public Cubic(Vector3Int hex) {
            var parity = hex.y & 1;
            Q = hex.y;
            R = hex.x - (hex.y - parity) / 2;
            S = -Q - R;
            Z = hex.z;
        }

        public Vector3Int Value => new(Q, R, S);

    }
}