using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithms;
using ProceduralGeneration;

public class RoomCollision : MonoBehaviour
{
    private LayerMask mask;
    private string[] rooms = new string[2];

    private void Start() {
        mask = LayerMask.GetMask("Room");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Room")) {
            FirstPlacedRoom(this.gameObject, col.gameObject);
        }
        else if (col.CompareTag("ClosedRoomCheck")) {
            col.GetComponent<IsRoomClosed>().DestroyObject();
        }
    }

    private void FirstPlacedRoom(GameObject room1, GameObject room2) {

        int room1Id = room1.name[0];
        int room2Id = room2.name[0];

        if (room1Id < room2Id) {            
            DestroyRoom(room2);
        }
        else {            
            DestroyRoom(room1);
        }
    }

    private void DestroyRoom(GameObject room) {
        if (room == null) {
            print($"{room.name} has already been destroyed");
            return;
        }
        // print($"{room} has been destroyed");
        // DungeonManager.instance.RemoveRoomFromHashTable(room);
        Destroy(room);
        DungeonManager.instance.DecreaseNumOfRooms();
    }

}
