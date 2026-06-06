using UnityEngine;

namespace Helpers.Hex
{
    public class Axial
    {

        public int Q;

        public int R;

        public int Z;

        public Axial(Vector3Int hex) {
            var parity = hex.y & 1;
            Q = hex.y;
            R = hex.x + (hex.y - parity) / 2;
            Z = hex.z;
        }

        public Vector2Int Value => new(Q, R);

    }
}