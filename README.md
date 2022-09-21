# Platformer-Pathfinding
 
Simple Pathfinding System for platformer games with Rigidbodies on the Unity Game Engine.

Instructions:
1. Create a custom graph by adding nodes and edges in editor mode.
2. Generate a Scriptable Object Graph from your custom map using GraphConstructor script.
3. Add Navigator script on each Actor.
4. Add "Target" tag on your target GameObject.

Execution:

-Used A* Pathfinding algorithm to determine the shortest path.
-Get all the edges from the path and convert them into MovementActions (Walk,Jump)
-Navigator executes MovementActions one after the other until it reaches the destination.
  Path example: Walk(to target Node),Walk(to target Node),Jump(to target Node),Walk(to target Node).
  
To Do:

-Remove test scripts
-Add more comments
-Create an simpler UI for graph creation
