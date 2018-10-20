# roguelike
**WORK IN PROGRESS**

Roguelike base in C#, intended to be re-usable for multiple game types.

## Implemented:
1. __Entity Movement__ -- Players and enemies are found on scene load and can move around, collide into walls. Everyone moves simultaneously, although if two entities try to occupy the same space, there's a pecking order based on an initiative score. Keyboard and touch movement are OK; pathfinding still needs some work, and non-touch mousebased movement is glitchy. 
2. __Inventory Basics__ -- Players can pick up gold.
3. __Camera__ -- Camera smoothly slides to player. Might play with the speed.

## In Progress:
1. __Ability System__ (Skeleton) -- This will include any action you can take in combat. The skeleton is there, but things move too quickly -- we need some animation to slow things down and maybe a text log so it's easier to parse.

## To Do (Milestones):
1. __Simple GUI__ -- Health, gold and items.
2. __Usable Inventory__
3. __Combat System__ -- Simple placeholder right now just lets you attack when you collide with enemies.

## To Do (Ongoing):
1. __Flesh out monsters.__
2. __Build levels.__
