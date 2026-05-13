Wind Warrior 
Wind Warrior is a 2D Action-Platformer prototype developed with Unity and C#. This project was created to explore core platformer mechanics, taking inspiration from the combat and movement styles of games like Dead Cells.

Project Goals & Features
The main focus of this project was to implement and polish fundamental 2D systems:

Combat Basics: A simple combo system with basic sword attack animations.

Physics-Based Movement: Player movement including jumping, gravity-sensitive falling, and a rolling mechanic for dodging.

Character Controller: Managed via a PlayerState system to handle transitions between animations like Running, Jumping, and Attacking.

Platforming Mechanics: Implementation of one-way platforms (letting the player jump through or fall down) and hazard detection.

Technical Implementation
Stable Grounding: Uses Physics2D.OverlapCircle to ensure the player correctly detects the ground and platforms.

Game Management: A simple GameManager (Singleton) to handle player health and scene reloading upon death.

Camera: Basic Cinemachine setup for smooth player following and screen shake effects.

