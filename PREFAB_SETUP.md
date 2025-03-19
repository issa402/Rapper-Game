# Unity Prefab Setup Guide

This guide will help you set up the necessary prefabs for the Rapper Battle Royale game to match the reference image.

## Required Prefabs

### 1. Rapper Prefab

Create a new prefab for the rapper character:

1. Create a cube in your scene (this will be a colored square like in the reference)
2. Add the following components:
   - RapperController script
   - Rigidbody (Use gravity: false, Is Kinematic: false)
   - Box Collider (Is Trigger: true)
3. Set up the following properties in the RapperController:
   - Movement Speed: 5
   - Rotation Speed: 100
   - Health: 100
   - Max Health: 100
   - Rapper Color: Set different colors for each rapper (blue, red, green, yellow)

### 2. Health Bar Prefab

1. Create a new empty GameObject
2. Add the HealthBar script
3. Create two child objects:
   - Background (scale 1, 0.2, 0.1)
   - Fill (scale 1, 0.2, 0.1) - This will be scaled based on health
4. Add SpriteRenderer to both with appropriate sprites
5. Set the references in the HealthBar script

### 3. Weapon Prefabs

#### Gun Prefab
1. Create a new cube
2. Scale it to (0.3, 0.3, 1)
3. Add the Gun script
4. Configure properties:
   - Fire Rate: 0.5
   - Damage: 20
   - Range: 10
   - Bullet Color: Yellow (to match reference image)
5. Set up references to bullet prefab and effects

#### Knife Prefab
1. Create a new cube
2. Scale it to (0.2, 0.8, 0.2)
3. Add the Knife script
4. Configure properties:
   - Damage: 35
   - Attack Range: 1.5
   - Knife Color: Silver/Gray

#### Bullet Prefab
1. Create a sphere
2. Scale it to (0.1, 0.1, 0.1)
3. Add the Bullet script
4. Add a Rigidbody (Use gravity: false)
5. Add a Sphere Collider (Is Trigger: true)
6. Add a Trail Renderer for visual effect (yellow color)

### 4. Apple Collectible Prefab

1. Create a new sphere
2. Scale it to (0.3, 0.3, 0.3)
3. Add the AppleCollectible script
4. Add a Sphere Collider (Is Trigger: true)
5. Set Material color to red
6. Configure properties:
   - Health Boost: 25
   - Rotation Speed: 50
   - Bob Height: 0.2

### 5. Arena Setup

1. Create a plane for the ground
2. Add the ArenaManager script to an empty GameObject
3. Create the arena border:
   - Create a new empty GameObject
   - Add four cubes as children (top, right, bottom, left borders)
   - Scale and position them to form a square border
   - Set material color to purple (#8000FF) to match reference image
4. Configure the ArenaManager:
   - Set the Border reference
   - Border Color: Purple
   - Initial Size: 20
   - Shrink Interval: 30
   - Shrink Amount: 0.1

### 6. Game Manager

1. Create an empty GameObject
2. Add the GameManager script
3. Configure properties:
   - Number of Rappers: 4
   - Rapper Colors: Set array with Blue, Red, Green, Yellow
   - References to prefabs (Rapper, Gun, Knife, Apple)
   - Weapon Spawn Interval: 5
   - Max Weapons: 15

### 7. Audio Manager

1. Create an empty GameObject
2. Add the AudioManager script
3. Add AudioSource components for:
   - Background Music
   - Sound Effects
4. Configure audio clips:
   - Background Music
   - Weapon Pickup
   - Gunshot
   - Hit Impact
   - Death
   - Apple Pickup
   - Arena Shrink

## Scene Setup

1. Create a new scene
2. Add all managers to the scene
3. Set up the arena with purple borders
4. Position the camera above the arena for a top-down view
5. Add appropriate lighting
6. Place reference objects for weapon and apple spawn points

## Testing

1. Enter Play mode
2. Verify that:
   - Rappers spawn as colored squares
   - Weapons appear periodically
   - Apple collectibles spawn
   - Arena border is purple and shrinks over time
   - Combat system functions with visual effects
   - Health bars display properly
   - Audio plays correctly

## Common Issues

1. If rappers don't move:
   - Check RapperController settings
   - Verify Rigidbody configuration

2. If weapons don't spawn:
   - Check GameManager settings
   - Verify weapon prefab references

3. If arena doesn't shrink:
   - Check ArenaManager settings
   - Verify border references

4. If colors don't match reference:
   - Ensure rapper colors are set correctly
   - Check that border color is set to purple
   - Verify bullet color is yellow

## Visual Style Reference

To match the reference image:
- Rappers should be simple colored squares (blue, red, green, yellow)
- Arena border should be purple
- Gun projectiles should be yellow with a trail
- Knives should be gray/silver
- Apples should be red
- The background should be dark/black
- Use simple geometric shapes for all game elements