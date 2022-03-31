using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralGeneration
{
    public class IsRoomClosed : MonoBehaviour
    {
        private DungeonManager dungeon;
        private bool isDestroyed = false;

        private void Start()
        {
            dungeon = DungeonManager.instance;
        }

        public IEnumerator WaitForCollision(float delay, GameObject spawnPoint)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (isDestroyed)
            {
                yield break;
            }
            SpawnRoom(spawnPoint);
        }

        private void SpawnRoom(GameObject spawnPoint)
        {
            OpeningsManager opening = spawnPoint.GetComponent<OpeningsManager>();
            opening.SpawnClosedRoom();
        }

        public void DestroyObject()
        {
            isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
