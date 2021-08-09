using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float secondsBetweenEnemies;
    [SerializeField] private Vector2 forceRange;
    [SerializeField] private int forceMultiplication;
    [SerializeField] private float adjustViewPortTransition;
    /*[SerializeField] private float destroyTimer = 5f;*/
    /*[SerializeField] private Vector3 spawnPoint;*/
    /*[SerializeField] private Vector3 rotation = new Vector3(-90f, 0f, 0f);*/

    private Camera mainCamera;
    private float timer;
    private Vector3 rotation;

    private float faceY;
    private float faceX;
    private float faceZ;

    /*private string destroyEnemy = "Enemy";*/

    void Start()
    {
        mainCamera = Camera.main;
        transform.position = transform.position;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            SpawnEnemy();

            timer += secondsBetweenEnemies;
        }

        /*Invoke(nameof(DestroyEnemy), destroyTimer);*/
    }

    private void SpawnEnemy()
    {

        int side = Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;

        switch (side)
        {
            // Left
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;

                faceY = 90f;
                faceZ = -90f;
                faceX = Random.Range(-90f, 90f);

                rotation = new Vector3(faceX, faceY, faceZ);

                break;

            // Right
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;

                faceY = -90f;
                faceZ = 90f;
                faceX = Random.Range(-90f, 90f);

                rotation = new Vector3(faceX, faceY, faceZ);

                break;

            // Bottom
            case 2:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;

                faceY = Random.Range(0, 2) == 0 ? -90 : 90;
                
                if (faceY == 90f)
                {
                    faceZ = -90f;
                }
                else
                {
                    faceZ = 90f;
                }
                
                faceX = Random.Range(-90f, -15f);

                rotation = new Vector3(faceX, faceY, faceZ);

                break;

            // Top
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;

                faceY = Random.Range(0, 2) == 0 ? -90 : 90;

                if (faceY == 90f)
                {
                    faceZ = 270f;
                }
                else
                {
                    faceZ = 90f;
                }

                faceX = Random.Range(15f, 90f);

                rotation = new Vector3(faceX, faceY, faceZ);

                break;
        }

        

        /*float faceY = Random.Range(0, 2) == 0 ? -90 : 90;

        Debug.Log(faceY);

        if (faceY == 90f)
        {
            faceZ = -90f;
            faceX = Random.Range(-90f, 90f);
        } 
        else
        {
            faceZ = 90f;
            faceX = Random.Range(-90f, 90f);
        }

        rotation = new Vector3(faceX, faceY, faceZ);*/

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = -0.9f;

        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        if (selectedEnemy.name == "PA_Warrior")
        {
            worldSpawnPoint.z = -0.6f;
        }

        GameObject instance = Instantiate(
            selectedEnemy,
            worldSpawnPoint,
            Quaternion.Euler(rotation));
        
        Rigidbody rb = instance.GetComponent<Rigidbody>();

        rb.AddRelativeForce(Vector3.forward * Random.Range(forceRange.x, forceRange.y) * forceMultiplication * Time.deltaTime );
    }
}
