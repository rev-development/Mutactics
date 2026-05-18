using System.Collections.Generic;
using Core.Unit;
using UnityEngine;

namespace Core.Mutation
{
    [CreateAssetMenu]
    public sealed class Mutation : ScriptableObject, IStats
    {

        public float BaseWeight = 1f;

        public string Name = "";

        public List<string> Tags = new();

        public Dictionary<string, float> Stats { get; set; }

    }
}