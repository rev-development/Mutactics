using System.Collections.Generic;
using System.Linq;
using Core.Mutation;

namespace Core.Unit
{
    public sealed class Unit : IEvolPressure, IStats
    {

        /// <summary>
        ///     This is a list of everything contributing to the unit's evolution (mutation selection weighting).
        ///     IMPORTANT: Items only need to implement the interface IEvolPressure, meaning this can be a heterogenous list.
        /// </summary>
        public List<IEvolPressure> EvolPressures = new();

        public string Id;

        public List<Mutation.Mutation> Mutations = new();

        public Unit(string id) {
            Id = id;
        }

        public Dictionary<string, float> TagBonuses
        {
            get
            {
                return EvolPressures.Aggregate
                    (new Dictionary<string, float>(), (prev, next) => Helpers.Dict.Collate(prev, next.TagBonuses));
            }
        }

        public Dictionary<string, float> Stats
        {
            get
            {
                return Mutations.Aggregate
                    (new Dictionary<string, float>(), (prev, next) => Helpers.Dict.Collate(prev, next.Stats));
            }
        }

    }
}