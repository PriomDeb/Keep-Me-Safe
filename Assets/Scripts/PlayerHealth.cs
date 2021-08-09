using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject enemyGameObject;

    public void Crash()
    {
        gameObject.SetActive(false);

        enemyGameObject.SetActive(false);
    }
}
