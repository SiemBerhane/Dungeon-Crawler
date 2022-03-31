using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int numOfEnemies;
    private int currentNumOfEnemies; 

    public void SpawnEnemies(int numOfEnemies) {

        // Renderers and colliders can be used to find the dimensions of objects.
        // TODO: get enemies spawning in random locations in the room
        Renderer rend = GetComponent<Renderer>();
         
        for (int i = 0; i < numOfEnemies; i++)
        {
            string objName = this.transform.name;
            char lastChar = objName[objName.Length - 1];

            GameObject enemy = EnemyManager.instance.GetEnemy(lastChar);
           
           if (enemy == null) {
                return;
            }

            GameObject spawnedEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
            numOfEnemies++;
        }
    }
}
