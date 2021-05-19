using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private GameObject player;
    private GameObject enemy;

    private Vector3 playerStartingPosition = new Vector3(0, 2, 16);


    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            CreateEnemy();

            WanderingAI wanderingEnemy = enemy.GetComponent<WanderingAI>();
            wanderingEnemy.player = player;
        }
    }

    private void CreatePlayer()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = playerStartingPosition;
        float randomAngle = Random.Range(0, 360);
        player.transform.Rotate(0, randomAngle, 0);
    }

    private void CreateEnemy()
    {
        enemy = Instantiate(enemyPrefab);
        enemy.transform.position = new Vector3(0, 3, -16);
        float randomAngle = Random.Range(0, 360);
        enemy.transform.Rotate(0, randomAngle, 0);
    }
}
