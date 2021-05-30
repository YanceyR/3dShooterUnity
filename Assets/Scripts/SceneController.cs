using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private bool spawnPlayer = true;
    [SerializeField] private bool spawnEnemy = true;
    [SerializeField] private bool enableMinimap = true;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject minimapPrefab;
    [SerializeField] private GameObject enviroment;

    private GameObject player;
    private GameObject enemy;
    private GameObject minimap;

    // HACK: figure out a better way
    private int spawnEnemyX = 30;
    private int spawnEnemyZ = 20;
    private int spawnPlayerX = 0;
    private int spawnPlayerZ = 16;
    
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPlayer)
        {
            player = CreatePlayer();
        }

        if (spawnEnemy)
        {
            enemy = CreateEnemy(player);
        }

        if (enableMinimap)
        {
            minimap = CreateMinimap(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemy && enemy == null)
        {
            enemy = CreateEnemy(player);
        }
    }

    private GameObject CreatePlayer()
    {
        GameObject player = Instantiate(playerPrefab, enviroment.transform);
        float height = player.transform.localScale.y;
        float enviromentHeightFactor = enviroment.transform.localScale.y;
        float posY = height / 2 * enviromentHeightFactor;
        float randomAngle = Random.Range(0, 360);

        player.transform.position = new Vector3(spawnPlayerX, posY, spawnPlayerZ);
        player.transform.Rotate(0, randomAngle, 0);

        return player;
    }

    private GameObject CreateEnemy(GameObject player)
    {
        GameObject enemy = Instantiate(enemyPrefab, enviroment.transform);
        float height = enemy.transform.localScale.y;
        float enviromentHeightFactor = enviroment.transform.localScale.y;
        float posX = CoinToss() ? spawnEnemyX : spawnEnemyX * -1;
        float posY = height / 2 * enviromentHeightFactor;
        float posZ = CoinToss() ? spawnEnemyZ : spawnEnemyZ * -1;
        float randomAngle = Random.Range(0, 360);

        enemy.transform.position = new Vector3(posX, posY, posZ);
        enemy.transform.Rotate(0, randomAngle, 0);

        WanderingAI searchingEnemy = enemy.GetComponent<WanderingAI>();

        if (searchingEnemy)
        {
            searchingEnemy.setPlayer(player);
        }

        return enemy;
    }

    private GameObject CreateMinimap(GameObject trackingTarget)
    {
        GameObject minimap = Instantiate(minimapPrefab);

        if (trackingTarget)
        {
            TrackTarget trackingMinimap = minimap.GetComponent<TrackTarget>();
            trackingMinimap.target = trackingTarget;
        }

        return minimap;
    }

    // Utils
    bool CoinToss()
    {
        float coinToss = (Random.Range(0.1f, 1.9f));
        return coinToss >= 1;
    }

}
