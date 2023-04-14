#Tower Defense Challenge (in 7 Days)

### DefenseTower Implementation Plan

- Grid System
- Placement + blocking constraint
- Enemies (3 Types)
- Player (health)
- Movement (navmesh, agents, obstacles)
- Spawners
- Wave system/Game Stages
- Use Unity NavMesh
- ~~Custom Pathfinding implementation~~
- Scalable stats
- Towers
- Projectiles
- Score
- Currency
- Upgrades
- UI
- Leaderboard (score/kills/perUnity/time/hitsTaken)

###Technical Goals
- Scalability
- Data Serialization
- Performance
- PoolingEnemies
- ~~Pooling VFX~~ (No VFX implemented)
- Optimized and simple grid system
- Compare NavMesh with other Pathfinding algorithms (DFS, BFS, Dijkstra, AStar)
- Check if Dynamic baking is needed
- Event Driven
- Assemble Definitions
- Agnostic to: Input System/~~path finding~~/persistent/serialization layer
- Optional:
	- Replayability
	- Meta progression
	- Starts from specific wave”
	- Addressables
		- Check for being remote could cause issues for playtest
		- Create remove host Google
		- Test Build in different locations 
	- Integrate Spreadsheet to update SOs

### Dev Log

- **Grid System** - I could have use Tile Map grid system for example as base but I wanted to do a slim version but still expandable 
- **Navmesh** - PoC navmesh - tested possibility of using navmesh. I wanted to give it a try to see if the movement would be more organic. Also try not to reinvent the wheel. 
I checked what pathfinding algorithm Unity uses, which is the A* (star) , the one that is most commonly used and have better performance I felt like some pathfinding algorithms can cause a too straight route. Thats the case like in the game Fieldrunners is too mechanical and flat movement.
 Although it was a quick implementation. If not balanced or the spawning is too intense, they can start getting stuck on each other if not killed faster enough. A grid shaped navmesh  baked could be used to improve the behavior. 
The Navmesh still can be used for Air unity.
- **Balance** - Created a spreadsheet to simulate some data about the waves, enemys, score, Towers - the system will consist a summon cost per wave that will be fulfilled by summoning random enemies with specific costs and implement a exponential progression (Growth) https://docs.google.com/spreadsheets/d/1nr-SldvAJsId-6262lTEuEvSknqsuxypzJ87Age02sg/edit?usp=sharing
- **Scalable Attributes for Towers **- same spreadsheet Tab: Scalable Tower Stats
Based on Level Upgrade, Some stats are special traits that will be unlocked on certain level upgrade
- **Unit Tests** - Starting separating the game logic that I had from Spreadsheets simulations into a Helper class which made than good target for unit Tests (probably I will have time for this though) also helped to decrease the complexity and size of system code
- **DOTS/ECS **- As much as I develop I think a full DOTS/ECS implementation would be interesting. There are so many calculations and physics that could be processed in parallel. As an infinite game it is gonna start bottle necking. Anyway, the plan is to implement a custom pathfinding with Job system and burst compiler if available time.
- **Forecast** - *One thing to optimize* - Start Calculating and preparing the next wave simulation while the previous one is running. So when I have to start a new one I just have to check if it is ready. Even further, a set of waves could be generated, some aspects like enemy composition randomized and finally serialized if needed for a remote service side for example, removing a lot of processing from clients.


###Known issues

New Input system - mouse event doesn't trigger on Editor:  Disable “Simulate Touch Input from  Mouse or Pen” on the Input Debug.


[Issue reference](https://forum.unity.com/threads/need-help-unity-input-system-mouse-totally-not-detected-clicks-not-happening.1369047/ "Issue reference")


## References

Here some references that I used to implement the game. 

- Wave System https://www.youtube.com/watch?v=7T-MTo8Uaio
- Growth Formula: https://www.cuemath.com/exponential-growth-formula/
- Unity NavMesh: https://docs.unity3d.com/550/Documentation/Manual/nav-InnerWorkings.html
- Adressables: https://www.youtube.com/watch?v=KJbNsaj1c1o
- AStar Pathfinding implementation with jobs and bust compiler: https://www.youtube.com/watch?v=OlIJCKJtk8o

