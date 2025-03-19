using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    // Prefabs
    public GameObject rapperPrefab;
    public GameObject[] weaponPrefabs;
    
    // Game settings
    public int numberOfRappers = 10;
    public float arenaSize = 20f;
    public float weaponSpawnInterval = 5f;
    public int maxWeapons = 15;
    
    // Runtime variables
    private List<RapperController> activeRappers;
    private List<Weapon> activeWeapons;
    private float nextWeaponSpawnTime;
    private bool gameOver = false;
    
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
            SpawnRapper();
        }
        
        // Initial weapon spawns
        for (int i = 0; i < maxWeapons / 2; i++)
        {
            SpawnWeapon();
        }
        
        nextWeaponSpawnTime = Time.time + weaponSpawnInterval;
    }
    
    private void SpawnRapper()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-arenaSize, arenaSize),
            0f,
            Random.Range(-arenaSize, arenaSize)
        );
        
        GameObject rapperObj = Instantiate(rapperPrefab, randomPosition, Quaternion.identity);
        RapperController rapper = rapperObj.GetComponent<RapperController>();
        
        // All rappers are AI-controlled
        rapper.isPlayer = false;
        
        activeRappers.Add(rapper);
    }
    
    private void SpawnWeapon()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-arenaSize, arenaSize),
            0f,
            Random.Range(-arenaSize, arenaSize)
        );
        
        GameObject weaponPrefab = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];
        GameObject weaponObj = Instantiate(weaponPrefab, randomPosition, Quaternion.identity);
        
        Weapon weapon = weaponObj.GetComponent<Weapon>();
        activeWeapons.Add(weapon);
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
                Debug.Log($"Winner: Rapper {activeRappers[0].gameObject.name}");
            }
            else
            {
                Debug.Log("No survivors!");
            }
        }
    }
}