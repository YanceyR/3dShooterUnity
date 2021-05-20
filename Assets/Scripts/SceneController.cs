using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject minimapPrefab;


    private GameObject player;
    private GameObject enemy;
    private GameObject minimap;


    private Vector3 playerStartingPosition = new Vector3(0, 2, 16);

    private GameObject CreatePlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = playerStartingPosition;
        float randomAngle = Random.Range(0, 360);
        player.transform.Rotate(0, randomAngle, 0);
        return player;
    }

    private GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = new Vector3(0, 3, -16);
        float randomAngle = Random.Range(0, 360);
        enemy.transform.Rotate(0, randomAngle, 0);

        WanderingAI wanderingEnemy = enemy.GetComponent<WanderingAI>();
        wanderingEnemy.player = player;

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

    // Start is called before the first frame update
    void Start()
    {
        player = CreatePlayer();
        enemy = CreateEnemy();
        minimap = CreateMinimap(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            enemy = CreateEnemy();
        }
    }


}
