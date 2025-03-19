using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Rapper Settings")]
    public GameObject rapperPrefab;
    public int numberOfRappers = 4;
    public Transform[] spawnPoints;
    public Color[] rapperColors = { Color.blue, Color.red, Color.green, Color.yellow }; // Match colors in image
    public string[] rapperNames;
    public AudioClip[] rapperSongs;
    public Sprite[] rapperSprites;
    
    [Header("Weapon Settings")]
    public GameObject gunPrefab;
    public GameObject knifePrefab;
    public float weaponSpawnInterval = 5f;
    public int maxWeapons = 15;
    public Transform[] weaponSpawnPoints;
    
    [Header("Collectible Settings")]
    public GameObject applePrefab; // Add apple collectible
    public float appleSpawnInterval = 8f;
    
    [Header("Arena Settings")]
    public float arenaSize = 20f;
    public float shrinkBorderInterval = 30f;
    public float shrinkAmount = 0.1f;
    public GameObject arenaBorder;
    public Color borderColor = new Color(0.5f, 0f, 1f); // Purple color for border
    
    // Runtime variables
    private List<RapperController> activeRappers;
    private List<Weapon> activeWeapons;
    private float nextWeaponSpawnTime;
    private bool gameOver = false;
    private Vector3 originalBorderScale;
    
    private AudioManager audioManager;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        activeRappers = new List<RapperController>();
        activeWeapons = new List<Weapon>();
    }
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
        // Store original border scale
        if (arenaBorder != null)
        {
            originalBorderScale = arenaBorder.transform.localScale;
            
            // Set border color to match image
            SpriteRenderer borderRenderer = arenaBorder.GetComponent<SpriteRenderer>();
            if (borderRenderer != null)
            {
                borderRenderer.color = borderColor;
            }
        }
        
        StartGame();
    }
    
    void Update()
    {
        if (gameOver) return;
        
        // Spawn weapons periodically
        if (Time.time >= nextWeaponSpawnTime && activeWeapons.Count < maxWeapons)
        {
            SpawnWeapon();
            nextWeaponSpawnTime = Time.time + weaponSpawnInterval;
        }
        
        // Check win condition
        CheckWinCondition();
    }
    
    private void StartGame()
    {
        // Spawn rappers
        for (int i = 0; i < numberOfRappers; i++)
        {
            SpawnRapper(i);
        }
        
        // Initial weapon spawns
        for (int i = 0; i < maxWeapons / 2; i++)
        {
            SpawnWeapon();
        }
        
        // Start arena shrinking
        StartCoroutine(ShrinkArenaRoutine());
        
        // Start spawning collectibles
        StartCoroutine(SpawnCollectiblesRoutine());
        
        nextWeaponSpawnTime = Time.time + weaponSpawnInterval;
    }
    
    private void SpawnRapper(int index)
    {
        Vector3 spawnPosition;
        
        // Use spawn points if available, otherwise random positions
        if (spawnPoints != null && spawnPoints.Length > index)
        {
            spawnPosition = spawnPoints[index].position;
        }
        else
        {
            spawnPosition = new Vector3(
                Random.Range(-arenaSize, arenaSize),
                0f,
                Random.Range(-arenaSize, arenaSize)
            );
        }
        
        GameObject rapperObj = Instantiate(rapperPrefab, spawnPosition, Quaternion.identity);
        RapperController rapper = rapperObj.GetComponent<RapperController>();
        
        // All rappers are AI-controlled
        rapper.isPlayer = false;
        
        // Set rapper color if renderer exists
        Renderer rapperRenderer = rapperObj.GetComponent<Renderer>();
        if (rapperRenderer != null && index < rapperColors.Length)
        {
            rapperRenderer.material.color = rapperColors[index];
        }
        
        // Set rapper name if available
        if (rapperNames != null && index < rapperNames.Length)
        {
            rapper.rapperName = rapperNames[index];
            rapperObj.name = rapperNames[index];
        }
        else
        {
            rapperObj.name = "Rapper " + (index + 1);
        }
        
        // Set sprite if available
        SpriteRenderer spriteRenderer = rapperObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && rapperSprites != null && index < rapperSprites.Length)
        {
            spriteRenderer.sprite = rapperSprites[index];
        }
        
        activeRappers.Add(rapper);
    }
    
    private void SpawnWeapon()
    {
        Vector3 spawnPosition;
        
        // Use spawn points if available, otherwise random positions
        if (weaponSpawnPoints != null && weaponSpawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, weaponSpawnPoints.Length);
            spawnPosition = weaponSpawnPoints[randomIndex].position;
        }
        else
        {
            spawnPosition = new Vector3(
                Random.Range(-arenaSize, arenaSize),
                0f,
                Random.Range(-arenaSize, arenaSize)
            );
        }
        
        // Randomly choose between gun and knife
        GameObject weaponPrefab = Random.value > 0.5f ? gunPrefab : knifePrefab;
        
        // Fallback to original system if prefabs aren't set
        if (weaponPrefab == null)
        {
            Debug.LogWarning("Weapon prefab not assigned, using backup system");
            return;
        }
        
        GameObject weaponObj = Instantiate(weaponPrefab, spawnPosition, Quaternion.identity);
        Weapon weapon = weaponObj.GetComponent<Weapon>();
        
        if (weapon != null)
        {
            activeWeapons.Add(weapon);
        }
    }
    
    IEnumerator ShrinkArenaRoutine()
    {
        while (!gameOver && arenaBorder != null)
        {
            yield return new WaitForSeconds(shrinkBorderInterval);
            
            // Shrink arena
            Vector3 newScale = arenaBorder.transform.localScale * (1f - shrinkAmount);
            arenaBorder.transform.localScale = newScale;
            
            // Update safe area for rappers
            arenaSize *= (1f - shrinkAmount);
            
            // Check if any rappers are now outside the arena and damage them
            foreach (RapperController rapper in activeRappers)
            {
                if (!IsInSafeZone(rapper.transform.position))
                {
                    rapper.TakeDamage(10f);
                }
            }
        }
    }
    
    IEnumerator SpawnCollectiblesRoutine()
    {
        while (!gameOver && applePrefab != null)
        {
            yield return new WaitForSeconds(appleSpawnInterval);
            
            // Spawn an apple within the current arena bounds
            Vector3 spawnPos = new Vector3(
                Random.Range(-arenaSize, arenaSize),
                0f,
                Random.Range(-arenaSize, arenaSize)
            );
            
            Instantiate(applePrefab, spawnPos, Quaternion.identity);
        }
    }
    
    private bool IsInSafeZone(Vector3 position)
    {
        // Check if position is within current arena bounds
        return Mathf.Abs(position.x) <= arenaSize && Mathf.Abs(position.z) <= arenaSize;
    }
    
    public void RapperDied(RapperController rapper)
    {
        activeRappers.Remove(rapper);
        
        if (audioManager != null)
        {
            audioManager.PlayDeath();
        }
        
        // Cleanup after delay
        Destroy(rapper.gameObject, 3f);
    }
    
    private void CheckWinCondition()
    {
        if (activeRappers.Count <= 1)
        {
            gameOver = true;
            Debug.Log("Game Over!");
            
            if (activeRappers.Count == 1)
            {
                string winnerName = activeRappers[0].rapperName;
                if (string.IsNullOrEmpty(winnerName))
                {
                    winnerName = activeRappers[0].gameObject.name;
                }
                Debug.Log($"Winner: {winnerName}");
                
                // Display winner UI here if needed
            }
            else
            {
                Debug.Log("No survivors!");
            }
        }
    }
}