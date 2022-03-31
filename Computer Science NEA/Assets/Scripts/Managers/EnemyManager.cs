using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Stores all the enemies so they can be accessed 

    public static EnemyManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private GameObject[] LevelAEnemies;
    [SerializeField] private GameObject[] LevelBEnemies;
    [SerializeField] private GameObject[] LevelCEnemies;

    public GameObject GetEnemy(char enemyList) {
        int index;
        GameObject enemy;
        
        switch (enemyList) {
            case 'A':
                index = Random.Range(0, LevelAEnemies.Length - 1);
                enemy = LevelAEnemies[index];
            break;

            case 'B':
                index = Random.Range(0, LevelBEnemies.Length - 1);
                enemy = LevelBEnemies[index];
            break;
                
            case 'C':
                index = Random.Range(0, LevelCEnemies.Length - 1);
                enemy = LevelCEnemies[index];
            break;

            default:
                enemy = null;
            break;
        }

        return enemy;
    }

    public GameObject GetEnemyA() {
        int index = Random.Range(0, LevelAEnemies.Length - 1);
        return LevelAEnemies[index];
    }

    public GameObject GetEnemyB() {
        int index = Random.Range(0, LevelBEnemies.Length - 1);
        return LevelBEnemies[index];
    }

    public GameObject GetEnemyC() {
        int index = Random.Range(0, LevelCEnemies.Length - 1);
        return LevelCEnemies[index];
    }
    
}
