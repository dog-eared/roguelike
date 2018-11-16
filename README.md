# roguelike
**WORK IN PROGRESS**

Roguelike base in C#, intended to be re-usable for multiple game types.

## Implemented:
1. __Entity Movement__ -- Players and enemies are found on scene load and can move around, collide into walls. Everyone moves simultaneously, although if two entities try to occupy the same space, there's a pecking order based on an initiative score. Keyboard and touch movement are OK; pathfinding still needs some work, and non-touch mousebased movement is glitchy. 

2. __Inventory Basics__ -- Players can pick up gold.

3. __Camera__ -- Camera smoothly slides to player. Might play with the speed.

4. __Simple Monster AI__ -- Completed: 

   1. Standing still and attacking all in range
   2. Aimlessly wandering
   3. Following a path rigidly with no variation
   4. Aimlessly wandering until the player enters a boundary, then beelining for them to try to kill them.

   This should be OK for a demo.

## In Progress:

1. __Ability System__ -- This will include any action you can take in combat. The skeleton is there, but things move too quickly -- we need some animation to slow things down and maybe a text log so it's easier to parse.
2. __Animation System__ -- To be triggered by abilities and certain events. Worth investigating viability of a "replay all from last turn" button. 

## To Do (Milestones):
1. __Simple GUI__ -- Health, gold and items.
2. __Usable Inventory__
3. __Combat System__ -- Simple placeholder right now just lets you attack when you collide with enemies.
4. __FIX TOUCH CONTROLS!__ -- It's just way too finicky right now. This is a great chance to learn how to deploy to mobile.

## To Do (Ongoing):
1. __Flesh out monsters.__
2. __Build levels.__
3. __New Special Abilities__
