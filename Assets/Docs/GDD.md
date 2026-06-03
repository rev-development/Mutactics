# Game Design Document

I'm making a Tactics Game in Unity. In my game, the player controls a mutant army and wants to conquer the galaxy.

## Campaign Map

You start with a single world. The world map is a hexagonal grid and the player can choose a neighboring world, 
represented by a separate hex tile,
to invade. 

### Campaign Pacing

For the first portion of the game, the player is free to explore and conquer at their own pace and wherever they
would like. They may lose battles as much as they like because true death only occurs when a player has no army
left via all of their units depleting their longevity.

But after some number of battles, one of many BBEGs appears and threatens the player with true death via conquering 
all their worlds. I plan on the BBEG appearing only after the player has fought enough battles to have fully 
conquered a handful of clusters

If the player chooses to only conquer Capstone Clusters, the BBEG should appear at the earliest time available. If 
the player chooses to conquer all the Scenario Clusters, then they should be able to conquer the whole galaxy before 
the BBEG appears. This is to un-homogenize the end-game experience by making it so each campaign doesn't end with 
building the apex predator with all the best mutations

### Worlds

Each world has a biome and faction associated with it that determines the terrain and the types of enemies present.
Conquering a world doesn't reward a player with guaranteed mutations for their units, only a percentage chance to 
acquire a mutation.


### Capstone Clusters

Across the world map, there are clusters of worlds with shared traits of some kind, ex. a group of polar worlds, 
These are called Capstone Clusters. These allow players to get powerful themed mutations to their units. The more 
powerful the mutations available, the farther from the starting point the cluster is.

### Scenario Clusters

Alternative to Capstone Clusters are Scenario Clusters. Scenario Clusters interact with the BBEG mechanic. In 
addition to foreshadowing what BBEG is going to appear, they can do things like delay the arrival of the BBEG or 
provide a way to gain BBEG-specific mutations such as resistance to the BBEG's biome or abilities

## Units

You start with a group of neutral undifferentiated mutant soldiers.

### Mutation

One of the core ideas of the game is that instead of directly selecting new units to purchase, like the shop in 
autobattler games such as Teamfight Tactics, or choosing an upgrade for a unit directly, like unit specialization 
in XCOM2, your units evolve and mutate in response to the battles they win or lose. Mutations are specific to each 
individual unit.

The mutations gained after a battle are based on one or more of the following:
- The type of enemy fought, ex. becoming venomous after defeating venomous enemies
- The type of biome fought in, ex. becoming heat-resistant after fighting on volcanic worlds
- Habits in combat, ex. developing long-range barbs after consistently getting kills at a distance
- Specific experiences, ex. gaining the ability to cannibalize or reabsorb allied units after having multiple die in 
  adjacent tiles

### Gene Templates

As players mutate their units, they unlock genetic templates to create new units with. These serve as an almost 
evolutionary checkpoint so the player doesn't have to start from scratch every time. But 
units created with templates do not come as carbon-copies.

For each mutation in the genetic template, there is a percent chance for a unit to have that mutation. There is
also a percent chance for a random unstable mutation when creating units from genetic templates.

#### Unstable Mutations

Unstable mutations are upgrades with drawbacks or gameplay warping effects on a unit.

### Longevity

Units dying in combat is impermanent in terms of, the unit is not lost forever. Instead, each unit has a longevity 
stat that works like HP. Doing any kind of battle with a unit removes a point of longevity and losing a battle would 
remove an additional amount
