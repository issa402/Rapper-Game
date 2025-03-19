# Rapper Battle Royale

A Unity-based battle royale game featuring AI-controlled rappers, inspired by simulationvoid's square battle concept.

## Game Overview

In this unique battle royale experience, AI-controlled rappers compete against each other in an automated arena. Each rapper moves independently, picking up weapons and engaging in combat without player intervention.

## Features

- Fully automated AI-controlled rappers
- Dynamic weapon pickup system
- Multiple weapon types (guns and knives)
- Projectile-based combat system
- Audio management system

## Technical Implementation

The game is built using Unity and C#, featuring:

- AI movement and combat logic
- Weapon system with inheritance (abstract Weapon class)
- Projectile physics
- Collision detection
- Game state management

## Project Structure

- `RapperController.cs`: Handles rapper AI behavior and movement
- `Weapon.cs`: Abstract base class for all weapons
- `Gun.cs`: Implementation of ranged weapons
- `Knife.cs`: Implementation of melee weapons
- `Bullet.cs`: Projectile behavior and collision
- `GameManager.cs`: Core game loop and state management
- `AudioManager.cs`: Sound effect and music handling

## Setup Guide

See `PREFAB_SETUP.md` for detailed instructions on setting up the Unity prefabs and scene configuration.

## Contributing

Feel free to submit issues and enhancement requests!

## License

MIT License