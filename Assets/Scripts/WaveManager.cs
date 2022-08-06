using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject basicEnemyPrefab;
    [SerializeField]
    private GameObject advancedEnemyPrefab;
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
    [SerializeField]
    private float pSpawnMinX;
    [SerializeField]
    private float pSpawnMaxX;
    [SerializeField]
    private float pSpawnMinY;
    [SerializeField]
    private float pSpawnMaxY;
    [SerializeField]
    private int waveCounter;
    [SerializeField]
    private int difficultyIncrements = 5;

    private List<EnemyController> enemies = new List<EnemyController>();
    private GameObject activePowerUp = null;

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
            HandlePowerUps();
        }
    }

    public IEnumerator SpawnEnemies()
    {
        int counter = 1;
        foreach (Transform t in enemyLocations)
        {
            if (counter <= 3 || waveCounter >= difficultyIncrements)
            {
                EnemyController newEnemy = null;

                if ((waveCounter >= 2*difficultyIncrements && waveCounter < 3*difficultyIncrements && (counter == 2 || counter == 3)) || (waveCounter >= 3*difficultyIncrements && waveCounter < 4*difficultyIncrements && (counter == 1 || counter == 4 || counter == 5)) || waveCounter >= 4*difficultyIncrements)
                {
                    newEnemy = GameObject.Instantiate(advancedEnemyPrefab, this.transform.position, this.transform.rotation).GetComponent<EnemyController>();
                }
                else
                {
                    newEnemy = GameObject.Instantiate(basicEnemyPrefab, this.transform.position, this.transform.rotation).GetComponent<EnemyController>();
                }

                if (newEnemy != null)
                {
                    newEnemy.SetTargetLocation(t.position);

                    enemies.Add(newEnemy);

                    newEnemy.shipDestroyedDelegate += RemoveEnemyOnDestroy;

                    counter++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void RemoveEnemyOnDestroy(GameObject enemyObjectRef)
    {
        EnemyController enemyRef = enemyObjectRef.GetComponent<EnemyController>();
        enemies.Remove(enemyRef);
        scoreTracker.UpdateScore(enemyRef.ScoreValue);
    }

    public void HandlePowerUps()
    {
        bool spawnPowerUp = false;
        if (waveCounter % 2 == 0)
        {
            int val = Random.Range(0, 3);
            if (val != 0)
            {
                spawnPowerUp = true;
            }
        }
        if (activePowerUp == null && spawnPowerUp == true)
        {
            int i = Random.Range(0, 2);

            float x = Random.Range(pSpawnMinX, pSpawnMaxX);
            float y = Random.Range(pSpawnMinY, pSpawnMaxY);
            Vector3 spawnPosition = new Vector3(x, y, 0);

            activePowerUp = GameObject.Instantiate(powerUpPrefabs[i], spawnPosition, Quaternion.identity);
        }
    }
}
