using UnityEngine;

// Memunculkan musuh acak dari sisi kanan. 10 detik per musuh = 6 musuh/menit (Bagian A, level 1).
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // isi: Enemy_Runner, Enemy_Grunt, Enemy_Tank
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private float spawnX = 9.5f;
    [SerializeField] private float spawnYRange = 2f;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;

        timer = spawnInterval;
        Spawn();
    }

    private void Spawn()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float y = Random.Range(-spawnYRange, spawnYRange);
        Instantiate(prefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
    }
}
