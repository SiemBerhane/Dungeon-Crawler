using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithms;

namespace ProceduralGeneration
{
    public class OpeningsManager : MonoBehaviour
    {
        private bool isOpeningAvailable;
        private GameObject adjacentRoom;
        private List<GameObject> adjRoom = new List<GameObject>();

        private enum DoorDirection
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private DoorDirection doorDirection;

        // Start is called before the first frame update
        void Start()
        {
            isOpeningAvailable = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            // Stops rooms from spawning other rooms in places where there are already rooms
            if (col.CompareTag("Room"))
            {
                adjacentRoom = col.gameObject;
                // print($"hahah: {adjacentRoom.name}, {transform.parent.parent.name}");
                isOpeningAvailable = false;
            }
        }

        public void SpawnRooms()
        {
            int maxNumOfRooms = DungeonManager.instance.maxNumOfRooms;

            if (!isOpeningAvailable || DungeonManager.instance.currentNumOfRooms >= maxNumOfRooms)
            {
                EventManager.instance.OpeningsValidationOver(this);
                return;
            }

            GetOpeningDirection();
            GameObject[] roomArray = WhichArray();
            int index = Random.Range(0, roomArray.Length);

            // Key used to index each room
            char type = GetRoomType(roomArray[index]);
            string key = DungeonManager.instance.GetNumOfRooms().ToString() + type;
            DungeonManager.instance.IncreaseNumOfRooms();

            GameObject lastPlacedRoom = Instantiate(roomArray[index], this.transform.position, Quaternion.identity);
            lastPlacedRoom.name = key;

            // Make the room a child to the parent variable
            GameObject parent = GameObject.FindGameObjectWithTag("RoomParent");
            lastPlacedRoom.transform.parent = parent.transform;

            // AddRoomToHashTable(key, lastPlacedRoom);

            EventManager.instance.OpeningsValidationOver(this);
        }

        public void SpawnClosedRoom()
        {
            Dictionary<string, GameObject> roomsDict = DungeonManager.instance.roomsWithOneDoor;

            // Create a key for the room
            char type = 'D';
            string key = DungeonManager.instance.GetNumOfEndRooms().ToString() + type;

            GetOpeningDirection();

            GameObject lastPlacedRoom = Instantiate(roomsDict[doorDirection.ToString()], this.transform.position, Quaternion.identity);
            lastPlacedRoom.name = key;

            // Make the room a child to the parent variable
            GameObject parent = GameObject.FindGameObjectWithTag("RoomParent");
            lastPlacedRoom.transform.parent = parent.transform;

            // AddRoomToHashTable(key, lastPlacedRoom);
            DungeonManager.instance.IncreaseNumOfEndRooms();
        }

        public GameObject GetAdjacentRoom()
        {
            // print($"adj room: {adjacentRoom.name}, parent: {transform.parent.parent.name}");
            return adjacentRoom;
        }

        private char GetRoomType(GameObject room)
        {
            // Three levels of difficulty, A-C
            // Decide difficulty based on how many rooms placed/ distance from spawn room and distance from a boss/loot room
            // Could use graphs...

            // Loot rooms - D
            // Boss room - E
            return 'A';
        }

        private void AddRoomToHashTable(string roomType, GameObject room)
        {

            var hashMap = DungeonManager.instance.roomHashTable;
            int index = GetHashKey(room.name);

            if (hashMap[index] != null)
            {
                Node n = LinkedLists.instance.Append(room, roomType);
            }
            hashMap[index] = room;

        }

        private int GetHashKey(string key)
        {
            return HashingAlgorithm.instance.HashKey(key);
        }

        private void GetOpeningDirection()
        {
            if (transform.localPosition.x > 0)
            {
                doorDirection = DoorDirection.Right;

            }
            else if (transform.localPosition.x < 0)
            {
                doorDirection = DoorDirection.Left;

            }
            else if (transform.localPosition.y > 0)
            {
                doorDirection = DoorDirection.Top;

            }
            else if (transform.localPosition.y < 0)
            {
                doorDirection = DoorDirection.Bottom;

            }
        }

        private GameObject[] WhichArray()
        {
            switch (doorDirection)
            {

                case DoorDirection.Left:
                    return DungeonManager.instance.roomsWithRightDoor;

                case DoorDirection.Right:
                    return DungeonManager.instance.roomsWithLeftDoor;

                case DoorDirection.Top:
                    return DungeonManager.instance.roomsWithBottomDoor;

                default:
                    return DungeonManager.instance.roomsWithTopDoor;
            }
        }
    }
}