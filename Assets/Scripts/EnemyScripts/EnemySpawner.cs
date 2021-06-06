using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundHeavy;

public class EnemySpawner : MonoBehaviour
{
    public int spawnAmountOnDeath = 1;

    private SceneController sceneController;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject sceneControllerObject = GameObject.FindWithTag(Tag.SceneControllerTag);
            sceneController = sceneControllerObject.GetComponent<SceneController>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex + "SceneController.cs script REQUIRED on GameObject tagged SceneController.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        for (int i = 0; i < spawnAmountOnDeath; i++)
        {
            sceneController.spawnEnemy();
        }
    }
}
