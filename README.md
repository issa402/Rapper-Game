# Rapper Battle Royale

A Unity-based battle royale game featuring AI-controlled rappers, inspired by simulationvoid's square battle concept. The game features colorful squares (rappers) competing in a shrinking arena with weapons and collectibles.

![Game Concept](https://i.imgur.com/example.jpg) <!-- Replace with actual screenshot when available -->

## Game Overview

In this unique battle royale experience, AI-controlled rappers (represented as colored squares) compete against each other in an automated arena with a purple border that shrinks over time. Each rapper moves independently, picking up weapons and engaging in combat without player intervention.

## Features

- Fully automated AI-controlled rappers
- Dynamic weapon pickup system
- Multiple weapon types (guns and knives)
- Projectile-based combat with visual effects
- Health system with visual health bars
- Apple collectibles for health restoration
- Shrinking arena with purple borders
- Color-coded rappers (blue, red, green, yellow)

## Visual Style

The game adopts a minimalist aesthetic with a focus on bright colors and simple shapes:
- Rappers are represented as colored squares
- The arena has a distinct purple border
- Weapons are simple geometric shapes
- Gun projectiles have yellow trails
- Apples provide health boosts
- Health bars show current rapper status

## Technical Implementation

The game is built using Unity and C#, featuring:

- AI movement and combat logic
- Weapon system with inheritance (abstract Weapon class)
- Projectile physics with visual effects
- Collectible items with healing properties
- Arena management with shrinking boundaries
- Health visualization system
- Audio management for sound effects

## Project Structure

- `RapperController.cs`: Handles rapper AI behavior and movement
- `Weapon.cs`: Abstract base class for all weapons
- `Gun.cs`: Implementation of ranged weapons with visual effects
- `Knife.cs`: Implementation of melee weapons with attack animations
- `Bullet.cs`: Projectile behavior and collision
- `AppleCollectible.cs`: Health restoration items
- `GameManager.cs`: Core game loop and state management
- `ArenaManager.cs`: Handles arena boundary and shrinking logic
- `HealthBar.cs`: Visual representation of rapper health
- `AudioManager.cs`: Sound effect and music handling

## Setup Guide

See `PREFAB_SETUP.md` for detailed instructions on setting up the Unity prefabs and scene configuration to match the visual style of the reference.

## Getting Started

1. Clone this repository
2. Open the project in Unity (2020.3 or newer recommended)
3. Follow the setup instructions in PREFAB_SETUP.md
4. Press Play to watch the AI rappers battle it out!

## Contributing

Feel free to submit issues and enhancement requests!

## License

MIT License