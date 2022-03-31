using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Algorithms;


namespace ProceduralGeneration
{
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        [Tooltip("Can't be bigger than 10 for now, messes up the hash map")] public int maxNumOfRooms;
        [HideInInspector] public int currentNumOfRooms;
        [HideInInspector] public int numOfEndRooms = 0;

        [SerializeField] private RoomGraph graphScript;

        public GameObject[] roomHashTable;
        public Dictionary<string, GameObject> roomsWithOneDoor = new Dictionary<string, GameObject>();

        // Graphs
        public Dictionary<GameObject, Dictionary<GameObject, int>> graph = new Dictionary<GameObject, Dictionary<GameObject, int>>();
        public Dictionary<string, Dictionary<string, string>> test = new Dictionary<string, Dictionary<string, string>>();


        public GameObject[] roomsWithOneDoorArray;
        public GameObject[] roomsWithLeftDoor;
        public GameObject[] roomsWithRightDoor;
        public GameObject[] roomsWithTopDoor;
        public GameObject[] roomsWithBottomDoor;

        [SerializeField] private GameObject startRoom;
        [SerializeField] private GameObject placeHolder;
        [SerializeField] private ClosedRoomManager closedRoomManager;

        private List<GameObject> roomsSpawned = new List<GameObject>();

        private bool allRoomsSpawned = false;

        void Start()
        {
            EventManager.instance.onRoomValidationOver += GetNewRooms;
            EventManager.instance.onAllRoomsSpawned += StartFinalRoom;

            roomHashTable = new GameObject[maxNumOfRooms * 2];
            AddElementsToDictionary();
            SpawnRooms(startRoom);
        }

        // Can't add elements to a dictionary in the inspector so I manually added them from an array
        private void AddElementsToDictionary()
        {
            roomsWithOneDoor.Add("Bottom", roomsWithOneDoorArray[0]);
            roomsWithOneDoor.Add("Top", roomsWithOneDoorArray[1]);
            roomsWithOneDoor.Add("Right", roomsWithOneDoorArray[2]);
            roomsWithOneDoor.Add("Left", roomsWithOneDoorArray[3]);
        }

        bool istrue = true;

        private void Update()
        {
            // For test purposes
            if (currentNumOfRooms == maxNumOfRooms && istrue && !LinkedLists.instance.isEmpty())
            {
                LinkedLists.instance.DisplayList();
                istrue = false;
            }

            if (currentNumOfRooms >= maxNumOfRooms && !allRoomsSpawned)
            {
                allRoomsSpawned = true;
            }
        }

        // Gets all the rooms and starts the process of spawning rooms at their spawnpoints
        private void GetNewRooms()
        {
            GameObject[] rooms = GetAllRooms();
            for (int i = 0; i < rooms.Length; i++)
            {
                if (!roomsSpawned.Contains(rooms[i]) && currentNumOfRooms <= maxNumOfRooms)
                {
                    SpawnRooms(rooms[i]);
                }
            }

            if (currentNumOfRooms >= maxNumOfRooms && !allRoomsSpawned)
            {
                print("All rooms have been spawned!");
                // EventManager.instance.AllRoomsSpawned();
                StartCoroutine(GetFinalRoom());
                closedRoomManager.GetSpawnPoints(rooms);
                allRoomsSpawned = true;
            }
        }

        private void SpawnRooms(GameObject room)
        {
            roomsSpawned.Add(room);
            RoomManager RM = room.GetComponentInChildren<RoomManager>();
            RM.GetOpenings();
        }

        private void StartFinalRoom()
        {
            StartCoroutine(GetFinalRoom());
        }

        private IEnumerator GetFinalRoom()
        {
            // Waits for all the end rooms to spawn before continuing
            yield return new WaitForSeconds(.1f);
            int roomNum = maxNumOfRooms - 1;
            GameObject[] allRooms = GetAllRooms();
            List<GameObject> lastRooms = new List<GameObject>();

            // Gets all the rooms that has a valid "last room" name and makes one of them the boss room
            foreach (GameObject room in allRooms)
            {
                if (roomNum.ToString() == room.name[0].ToString() && room.name[room.name.Length - 1] != 'D')
                {
                    lastRooms.Add(room);
                }
            }
            lastRooms[0].name = roomNum.ToString() + 'E';

            // graphScript.AddRoomsToGraph();
        }


        public void RemoveRoomFromHashTable(GameObject room)
        {
            int index = HashingAlgorithm.instance.HashKey(room.name);
            if (roomHashTable[index] == room)
            {
                roomHashTable[index] = placeHolder;
                // print($"{room.name} has been removed from the hash table");
            }
            else
            {
                LinkedLists.instance.Remove(room);
                // print($"{room.name} has been removed from the linked list");
            }
        }

        public GameObject[] GetAllRooms()
        {
            return GameObject.FindGameObjectsWithTag("Room");
        }

        public void IncreaseNumOfRooms()
        {
            currentNumOfRooms++;
        }

        public void DecreaseNumOfRooms()
        {
            currentNumOfRooms--;
        }

        public int GetNumOfRooms()
        {
            return currentNumOfRooms;
        }

        public void IncreaseNumOfEndRooms()
        {
            numOfEndRooms++;
        }

        public void DecreseNumOfEndRooms()
        {
            numOfEndRooms--;
        }

        public int GetNumOfEndRooms()
        {
            return numOfEndRooms;
        }

        private void OnDestroy()
        {
            EventManager.instance.onRoomValidationOver -= GetNewRooms;
            EventManager.instance.onAllRoomsSpawned -= StartFinalRoom;
        }
    }
}
