using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject[] powerUpPrefabs;
    [SerializeField]
    private TextMeshProUGUI waveTextAsset;
    [SerializeField]
    private Transform[] enemyLocations;
    [SerializeField]
    private ScoreTracker scoreTracker;
    [SerializeField]
    private float spawnDelay = 1.0f;

    private int waveCounter;
    private List<EnemyController> enemies = new List<EnemyController>();

    // Public accessor for enemies list
    public List<EnemyController> Enemies { get { return enemies; } }

    // Public accessor for waveCounter
    public int WaveCounter { get { return waveCounter; } }

    // Start is called before the first frame update
    void Start()
    {
        waveCounter = 1;

        waveTextAsset.text = waveCounter.ToString();

        // Spawn first wave
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        // When all enemies are destroyed, increment waves, update text, spawn new enemies and/or power-ups
        if (enemies.Count == 0)
        {
            waveCounter++;
            waveTextAsset.text = waveCounter.ToString();

            // Spawn new enemies
            StartCoroutine(SpawnEnemies());

            // Spawn power-ups
        }
    }

    public IEnumerator SpawnEnemies()
    {
        foreach (Transform t in enemyLocations)
        {
            EnemyController newEnemy = GameObject.Instantiate(enemyPrefab, this.transform.position, this.transform.rotation).GetComponent<EnemyController>();

            newEnemy.SetTargetLocation(t.position);
            
            enemies.Add(newEnemy);

            newEnemy.enemyDestroyedEvent += RemoveEnemyOnDestroy;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void RemoveEnemyOnDestroy(EnemyController enemyRef)
    {
        enemies.Remove(enemyRef);
        scoreTracker.UpdateScore(enemyRef.ScoreValue);
    }
}
