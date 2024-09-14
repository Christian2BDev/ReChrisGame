using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    public void SpawnEnemies()
    {
        //First despawn all enemies from previous waves.
        
        //Spawn all new enemies outside the players camera view
        for(int i = 0; i < (GameState.wavesSurvived + 5) * 1.1; i++)
        {
            Vector2 randomSpawnPoint = Vector2.zero;
            float height = Camera.main.orthographicSize * 2;
            float width = height * Camera.main.aspect;
            float randNum = Random.Range(0, 5);
            float randomXDeviation = Random.Range(4, 10);
            float randomYDeviation = Random.Range(4, 10);
            if (randNum == 0)
            {
                //Take point above camera viewport
                Vector2 point = new Vector2((height / 2) + randomXDeviation, (width / 2) + randomYDeviation);
                randomSpawnPoint = (Vector2)Camera.main.transform.position + point;
            }
            else if(randNum == 1)
            {
                //Take point under camera viewport
                Vector2 point = new Vector2((height / 2) - randomXDeviation, (width / 2) + randomYDeviation);
                randomSpawnPoint = (Vector2)Camera.main.transform.position + point;
            }
            else if (randNum == 2)
            {
                //Take point at the right camera viewport
                Vector2 point = new Vector2((height / 2) + randomXDeviation, (width / 2) + randomYDeviation);
                randomSpawnPoint = (Vector2)Camera.main.transform.position + point;
            }
            else if (randNum == 3)
            {
                //Take point at the left camera viewport
                Vector2 point = new Vector2((height / 2) + randomXDeviation, (width / 2) - randomYDeviation);
                randomSpawnPoint = (Vector2)Camera.main.transform.position + point;
            }
            Instantiate(enemyPrefab, (Vector3)randomSpawnPoint, enemyPrefab.transform.rotation);
        }
    }
}
