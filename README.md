🧟 Zombie Game Project
A desktop game developed using C# where the player must survive waves of zombies. The game features multiple levels of increasing difficulty, where the number and strength of zombies grow with each new level.

🚀 Features
Multi-Level Gameplay: The game starts at level 1 and continues up to level 5, with increasing difficulty at each level.
Zombie Health Scaling: As levels progress, the zombies become stronger and require more shots to defeat.
Level Reset on Death: If the player dies, the game restarts from level 1, regardless of the current level.
Ammo and Power-ups: Throughout the game, ammo and power-ups will appear, allowing the player to replenish their resources.
Background Music and Sound Effects: Background music plays throughout the game, and sound effects are included for shooting and zombie deaths.
Score Tracking: Keep track of your kills and scores as you progress through levels.

🛠 Technologies Used
C# .NET Framework: Core game logic and processing.
Windows Forms: User interface and game window.
WMPLib & SoundPlayer: Used for handling background music and sound effects.
Git and GitHub: Version control and collaboration.

📂 Project Structure
Form1.cs: Main game logic, including player movement, shooting, and interactions with zombies.
Form1.Designer.cs: Design and layout of the game window.
FormLabel.cs: A separate form to display the score and allow players to restart or quit.
Enemy.cs: Class representing a generic zombie in the game.
SpeedyZombie.cs: Specialized zombie with enhanced speed attributes.
Resources: Contains images, sound files, and other game assets.

🎮 How to Play
Launch the game by running the ZombieApp.exe located in the bin/Debug folder.
Navigate using the arrow keys (left, right, up, down).
Shoot by pressing the Space key.
Collect Ammo by moving over the ammo boxes.
Advance Levels by killing 6 zombies in each level.

📈 Future Enhancements
Improved AI: Add more intelligent behavior to zombies.
Boss Fights: Introduce boss zombies with unique abilities.
Player Upgrades: Health, speed, and weapon upgrades.
UI Enhancements: Improved menus and statistics display.

💻 Author
Name: Gal Lugassi
GitHub: github.com/gallugassi3
LinkedIn: linkedin.com/in/gal-lugassi-678357262

📜 License
This project is licensed under the MIT License - see the LICENSE file for details.
