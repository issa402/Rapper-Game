# Unity Prefab Setup Guide

This guide will help you set up the necessary prefabs for the Rapper Battle Royale game.

## Required Prefabs

### 1. Rapper Prefab

Create a new prefab for the rapper character:

1. Create a cube in your scene
2. Add the following components:
   - RapperController script
   - Rigidbody (Use gravity: false, Is Kinematic: true)
   - Box Collider (Is Trigger: true)
3. Set up the following properties in the RapperController:
   - Movement Speed: 5
   - Rotation Speed: 100
   - Health: 100

### 2. Weapon Prefabs

#### Gun Prefab
1. Create a new cube
2. Scale it to (0.3, 0.3, 1)
3. Add the Gun script
4. Configure properties:
   - Fire Rate: 0.5
   - Damage: 20
   - Range: 10

#### Knife Prefab
1. Create a new cube
2. Scale it to (0.2, 0.8, 0.2)
3. Add the Knife script
4. Configure properties:
   - Damage: 35
   - Attack Range: 1.5

#### Bullet Prefab
1. Create a sphere
2. Scale it to (0.1, 0.1, 0.1)
3. Add the Bullet script
4. Add a Rigidbody (Use gravity: false)
5. Add a Sphere Collider (Is Trigger: true)

### 3. Arena Setup

1. Create a plane for the ground
2. Add walls using cubes:
   - Scale each wall appropriately
   - Add Box Colliders
   - Position them around the arena

### 4. Game Manager

1. Create an empty GameObject
2. Add the GameManager script
3. Configure properties:
   - Number of Rappers: 10
   - Arena Size: 20
   - Weapon Spawn Interval: 5
   - Max Weapons: 15

### 5. Audio Manager

1. Create an empty GameObject
2. Add the AudioManager script
3. Add AudioSource components for:
   - Background Music
   - Weapon Pickup
   - Shooting
   - Hit Impact
   - Death

## Scene Setup

1. Create a new scene
2. Add all prefabs to the scene
3. Position the camera above the arena
4. Add appropriate lighting

## Testing

1. Enter Play mode
2. Verify that:
   - Rappers spawn correctly
   - Weapons appear periodically
   - AI movement works
   - Combat system functions
   - Audio plays correctly

## Common Issues

1. If rappers don't move:
   - Check RapperController settings
   - Verify Rigidbody configuration

2. If weapons don't spawn:
   - Check GameManager settings
   - Verify weapon prefab references

3. If collisions don't work:
   - Verify collider settings
   - Check collision matrix settings

## Additional Notes

- Adjust values in the inspector to balance gameplay
- Use layers to manage collision detection
- Tag important objects appropriately