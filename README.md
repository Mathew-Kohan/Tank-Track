# Tank Track

I created this project from scratch. All scripts are written by myself. It is a 2D side view mini tank game. This project is compatible with the touch screens. 
Only one scene is developed. The sprites used in the project were downloaded from Kenney.nl.
below, I have explained the items in the scene and the way of implementing them.

1. The player can shoot a projectile using a trajectory.  [Trajectory.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/Player/Trajectory.cs)
2. I used IDamageable interface for the game health system.   [IDamageable.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/IDamageable.cs)
3. HUD for showing remaining projectiles and health bar and coin amount. HUD is driven by UIManager and I used the Singleton design pattern.  [UIManager.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/UI/UIManager.cs)  [Healthbaer.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/UI/HealthBar.cs) 
4. Used Abstract class for exploding items like tank projectiles and mines.  [Explode.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/Explode.cs)
5. Two types of power-ups are designed and built in the game, i.e.  health upgrades and increased projectile. [PowerUp.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/PowerUp/PowerUp.cs)
6. Simple enemy AI system for tracking and shooting the player.  [Enemy.cs](https://github.com/Mathew-Kohan/Tank-Track/blob/main/Assets/Scripts/Enemy/Enemy.cs)
