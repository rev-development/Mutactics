using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Mutation
{
    public class Manager
    {

        public List<Mutation> GlobalMutationPool = new();

        private static float GetMutationWeight(Mutation mutation, IEvolPressure contextTags) {
            // Apply multipliers based on how many tags match
            return contextTags.TagBonuses.Keys.Where
                                   (contextTag => mutation.Tags.Contains(contextTag))
                              .Aggregate
                                   (
                                       mutation.BaseWeight,
                                       (current, contextTag) => current * contextTags.TagBonuses[contextTag]
                                   );
        }

        public void RollMutation(Unit.Unit unit, IEvolPressure contextTags) {
            // This uses the "Roulette Wheel" Selection Method

            // Step 1: Calculate dynamic weights based on tags and context
            var activePool = GlobalMutationPool.ToDictionary
                (mutation => mutation, mutation => GetMutationWeight(mutation, contextTags));

            // Step 2: Roll the weighted random
            var randRoll = Random.Range(0f, activePool.Sum(item => item.Value));

            // Step 3: Find the winning item
            foreach (var kvp in activePool)
            {
                // In an even loot table, you could just find the Nth item and return it
                // Instead, in a weighted loot table, you chip away at the value until it hits 0 because # of items =/= total weight
                randRoll -= kvp.Value;

                if (randRoll <= 0f)
                {
                    unit.Mutations.Add(kvp.Key);
                }
            }
        }

    }
}