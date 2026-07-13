# Game Design Document — Mutactics

## Overview

A tactics game in Unity where the player controls a mutant army and conquers the galaxy. Units evolve and mutate in response to battles, enemies, environments, and events — creating emergent army identities without direct player selection of upgrades.

---

## Campaign Map

You start with a single world. The world map is a hexagonal grid and the player can choose a neighboring world, represented by a separate hex tile, to invade.

### Campaign Pacing

For the first portion of the game, the player is free to explore and conquer at their own pace and wherever they would like. They may lose battles as much as they like because true death only occurs when a player has no army left via all of their units depleting their longevity.

But after some number of battles, one of many BBEGs appears and threatens the player with true death via conquering all their worlds. The BBEG appears only after the player has fought enough battles to have fully conquered a handful of clusters.

If the player chooses to only conquer Capstone Clusters, the BBEG should appear at the earliest time available. If the player chooses to conquer all the Scenario Clusters, then they should be able to conquer the whole galaxy before the BBEG appears. This is to un-homogenize the end-game experience by making it so each campaign doesn't end with building the apex predator with all the best mutations.

### Worlds

Each world has a biome and faction associated with it that determines the terrain and the types of enemies present. Conquering a world doesn't reward a player with guaranteed mutations for their units, only a percentage chance to acquire a mutation.

### Capstone Clusters

Across the world map, there are clusters of worlds with shared traits of some kind — for example, a group of polar worlds. These are called Capstone Clusters. They allow players to get powerful themed mutations to their units. The more powerful the mutations available, the farther from the starting point the cluster is.

Each Capstone Cluster has a distinct board-interaction identity — the types of mutations available define a new axis of gameplay for units that fight through it. For example, a volcanic cluster might produce fire-terrain mutations that create and interact with burning hexes, while an arctic cluster might produce conditional passives around unit proximity and warmth. Conquering a cluster should feel like the army has *become* that identity.

### Scenario Clusters

Alternative to Capstone Clusters are Scenario Clusters. Scenario Clusters interact with the BBEG mechanic. In addition to foreshadowing what BBEG is going to appear, they can do things like delay the arrival of the BBEG or provide a way to gain BBEG-specific mutations such as resistance to the BBEG's biome or abilities.

---

## Units

You start with a group of neutral undifferentiated mutant soldiers.

### Longevity

Units have a longevity stat representing their lifespan as a soldier. True death of a unit occurs when their longevity is depleted. The campaign ends when all units have been lost this way.

---

## Mutation System

### Design Philosophy

Mutations are **enablers, not rewards**. They exist to support archetypes, behaviors, and identities — not to make units numerically larger. Stat increases exist to support a playstyle, not the other way around. The player doesn't chase bigger numbers, they chase ways to play.

Mutations should define how units **interact with the game board**, not just their stat profile. Different mutation identities create new axes of play — a summoner-type occupies board space with minions, a terrain-controller blocks or alters movement, a fire-focused unit creates environmental hazards. This ties mutation identity to world identity and creates emergent army composition as units evolve to their regions.

This philosophy mirrors Magic: The Gathering's Color Pie — each mutation category has a defined role and clear rules about what it is and isn't allowed to do.

---

### Mutation Categories

#### Behavioral Mutations
*Sourced from: how the player uses units in combat*

Driven by positioning, aggression, support roles, adjacency, and other in-combat habits. Always additive or enabling — never work against a unit's existing capabilities. Can push a unit toward a playstyle archetype or open new tactical options that complement existing ones.

**The only category the player has direct agency over**, giving them a way to intentionally steer their army's identity. A player who consistently plays a unit aggressively will unlock aggressive mutations; a player who keeps a unit in the back will unlock support mutations.

Behavioral mutations are not restricted to being purely rewarding or purely proactive — they can both reward a playstyle the player has established *and* open new expressions of that playstyle going forward.

---

#### Predator Mutations
*Sourced from: the types of enemies fought*

Driven by what kinds of enemies a unit fights — the identity and type of enemy, not merely the act of killing. Fighting water-world armies, psionic enemies, or volcanic factions each applies different predator pressures.

Always unlock new tactical options — active or passive abilities, new move interactions, expanded toolkit. Never work against a unit, though they may not always perfectly align with the current archetype. A sniper picking up a charge ability from fighting aggressive melee enemies won't be harmed by it — it simply adds a tool they may or may not use.

---

#### Environmental Mutations
*Sourced from: the biome and terrain of worlds fought in*

Driven by the terrain, climate, and environmental conditions a unit is exposed to during battle. Always positive — never work against a unit.

Environmental mutations come in two forms:

- **Pure passives**: Always active. Examples: fire resistance, radiation aura, toxic breath.
- **Conditional passives**: Always available, but only activate when the player makes a specific tactical decision. No button press required — the player positions or behaves in a way that triggers the effect automatically. Examples: "gain toughness when adjacent to allies" (requires positioning decision), "deal damage to attackers when in poison terrain" (requires terrain awareness).

Environmental mutations reward smart tactical decision-making in response to the world. They incentivize interesting board play without adding UI complexity.

---

#### Event Mutations
*Sourced from: specific narrative moments during combat*

Triggered by unique combat situations — a unit landing all the killing blows in a battle, watching allies die nearby, being the only unit to take damage, surviving a near-death moment, or achieving a significant individual feat.

Event mutations sit in a **weighted pool** and emerge unpredictably. When a triggering event occurs during combat, the mutation may manifest immediately — mid-battle — rather than waiting for the post-battle mutation phase. This makes them feel like narrative consequences rather than progression unlocks.

**The only category that can warp or derail a non-new unit's playstyle.** Some event mutations work against an existing archetype, some introduce compulsive behaviors the unit can't ignore. A unit that absorbs biomass from fallen allies may be drawn toward corpses even when that's tactically inconvenient. A unit that becomes a maniac after landing every killing blow may behave more aggressively than the player intended.

Key rules for event mutations:
- Always permanent and always interactive — the unit has to live with the scar
- Not always negative, but always consequential
- Not exclusively trauma-based — achievements and positive feats can trigger them too
- The scar doesn't have to be monumental, but it must be present and non-ignorable

---

#### Unstable Mutations
*Sourced from: genetic template generation only*

Unstable mutations only appear on units spawned from **genetic templates**. They do not occur on units acquired through normal play.

**Genetic Templates** allow players to create new units based on an existing veteran unit's mutation profile. Each of the template unit's mutations has an independent chance to pass on to the new unit — ranging from a perfect copy (all mutations passed) to a blank slate (none passed). The closer the result is to a perfect copy, the higher the chance that an unstable mutation is introduced as a balancing factor, preventing template spam and clone armies.

**The only category that can completely invalidate a unit** — either by ruining their desired playstyle (a sniper that can only see adjacent tiles) or making the unit non-functional entirely. However, no single unstable mutation should be capable of outright ruining a unit on its own. Complete invalidation only happens through circumstance — for example, a unit already carrying several heavy mobility-reducing mutations that then picks up "tiny legs" and hits zero movement as a result.

Unstable mutations can fundamentally warp or brick a unit. This is acceptable because:
- They only ever apply to fresh spawns the player hasn't invested time or story into
- Veterans are completely protected from unstable mutations
- A bricked unit is simply not deployed — no emotional or strategic loss

---

### Mutation Category Summary

| Category | Source | Can Work Against Unit? | Can Brick Unit? | Player Agency |
|---|---|---|---|---|
| Behavioral | Player habits | Never | Never | Direct |
| Predator | Enemy types fought | Never | Never | Indirect |
| Environmental | Biome/terrain | Never | Never | Indirect |
| Event | Narrative moments | Yes (non-new units only) | Never | None |
| Unstable | Template generation | Yes | Yes (via circumstance) | None |

---

### Regional Pressure Loop

Players start with undifferentiated units and fight through clustered biome systems with distinct identities. Each biome applies environmental and predator pressure toward specific mutation types, shaping army playstyle organically. Conquering a region makes the army feel like it has *become* that identity.

Moving to a new region creates strategic friction as evolved units meet new challenges — but behavioral mutations give the player agency to steer through it intentionally. Aggressive units fighting in volcanic zones naturally pick up both aggressive and fire mutations, creating compounding specialization that feels earned rather than assigned.

---

### Mutations and AI Behavior

Enemy units use the same mutation system as player units. The mutation profile of an enemy unit directly informs its AI behavior — a unit with brawler mutations prefers close-range engagement, a unit with ranged mutations prefers distance and zone denial.

This means:
- **Player units** earn mutations through behavior, then those mutations reinforce that behavior
- **Enemy units** use their existing mutations to determine preferred behavior

The same feedback loop runs in opposite directions. Player discovers playstyle through mutation reward; AI skips discovery and acts according to its mutation profile. No separate AI behavior trees or weighting systems required — the mutation system *is* the AI behavior system.

---

### Legacy System

Units that survive campaigns carry their full mutation history — including event mutation scars — into future runs. Template systems let players propagate veteran unit identities into new recruits, with variance and unstable mutations keeping the roster from becoming a collection of Xerox copies.

Veteran units with accumulated event mutations are narratively distinct — their scars, compulsions, and quirks tell the story of what they've survived.

---

## Combat

### Action System

Combat follows a flow inspired by Magic: The Gathering's stack — select unit, choose action, pay cost, execute. This keeps action initiation clear and extensible for future ability types.

1. **Select unit** (analogous to selecting a card)
2. **Choose action** (the action defines valid targets and displays relevant UI — tile highlights, range indicators, etc.)
3. **Pay cost** (action economy resource, AP, etc.)
4. **Execute** (command is queued and resolved)

Actions own their targeting rules. The UI reads from the action to determine what is selectable and how to display it. Commands are purely mechanical — they execute, undo, and carry no UI logic.

### Command Pattern

Unit movement and actions use a reversible Command Pattern (`IReversibleCommand`) with a history stack on `Pawn.Manager`. This enables undo functionality and future "habits in combat" tracking via command history.

Map-layer state mutations (move commands) live on `Pawn.Manager`, not `Combat.Manager`.

### Combat Metrics and Evolutionary Pressure

Every game action tracks at least one metric. These metrics serve dual purposes:

1. **Mutation pressure** — metrics determine which mutations a unit is eligible for after battle
2. **AI behavior weights** — enemy AI scores possible actions using the same metric dimensions

Metrics tracked include (but are not limited to): adjacency to allies/enemies, attack range, damage dealt/received, kills, defensive actions taken, positioning relative to terrain.

### Post-Battle Mutation Phase

After each battle, units receive mutation opportunities based on accumulated metrics. Mutations are not guaranteed — each world grants only a percentage chance. The mutation that emerges is drawn from a weighted pool shaped by the battle's metrics.

Event mutations may bypass this phase and manifest mid-combat when their triggering conditions are met.

---

## Board Interaction Philosophy

Different unit archetypes should interact with the game board on different axes — not just "this unit hits harder" but "this unit changes how the board works." Examples:

- **Summoner mutations**: Unit occupies board space with spawned minions
- **Terrain-control mutations**: Unit blocks, alters, or creates terrain features
- **Fire mutations**: Unit creates burning hexes that affect movement and damage
- **Proximity mutations**: Unit gains effects based on adjacency to allies or enemies
- **Scavenger mutations**: Unit interacts with fallen unit positions on the board

This ensures that a diverse army isn't just a collection of different stat blocks — it's a collection of units that each change the tactical possibilities of the map in different ways.