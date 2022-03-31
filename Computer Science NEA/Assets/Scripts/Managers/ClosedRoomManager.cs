using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralGeneration
{
    public class ClosedRoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject roomChecker;
        [SerializeField] private float delay;

        private bool isDestroyed = false;
        private LayerMask startRoom;

        private void Start()
        {
            startRoom = LayerMask.GetMask("StartRoom");
        }

        public void GetSpawnPoints(GameObject[] rooms)
        {
            foreach (GameObject room in rooms)
            {

                // Checks the rooms layer
                if ((startRoom & (1 << room.layer)) == 0)
                {

                    Transform child = room.transform.GetChild(0);

                    // Spawns a checker at each room opening
                    foreach (Transform spawnpoint in child.transform)
                    {

                        GameObject obj = Instantiate(roomChecker, spawnpoint.position, Quaternion.identity);
                        obj.transform.parent = GameObject.FindGameObjectWithTag("CollisionParent").transform;

                        StartCoroutine(obj.GetComponent<IsRoomClosed>().WaitForCollision(delay, spawnpoint.gameObject));
                    }
                }
            }
            print("All end rooms have been spawned");
            EventManager.instance.AllRoomsSpawned();
        }

        public void DestroyObject()
        {
            isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
