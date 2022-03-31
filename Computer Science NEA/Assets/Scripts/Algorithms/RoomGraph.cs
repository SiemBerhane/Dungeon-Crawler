using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralGeneration;

public class RoomGraph : MonoBehaviour
{
    [SerializeField] private Transform colliderCheckerParent;
    [SerializeField] private float delay;
    private int colliderChildCount;

    public Dictionary<GameObject, Dictionary<GameObject, int>> graph;

    private void Start()
    {
        EventManager.instance.onAllRoomsSpawned += InitiateGraph;
    }

    // Get the closed rooms first before continuing with the rest
    private void InitiateGraph()
    {
        StartCoroutine(GetClosedRooms());
    }

    private IEnumerator GetClosedRooms()
    {
        yield return new WaitForSeconds(delay);
        GetColliderChildren();
    }

    private void GetColliderChildren()
    {
        colliderChildCount = colliderCheckerParent.childCount;
        AddRoomsToGraph();
    }

    public void AddRoomsToGraph()
    {
        graph = DungeonManager.instance.graph;
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        // Adds the rooms to the graph
        foreach (GameObject room in rooms)
        {
            Dictionary<GameObject, int> temp = FindAdjRooms(room);
            graph.Add(room, temp);
        }

        CalculateDifficulty(rooms);

        // Testing: Just displays the graph
        /*foreach (GameObject room in graph.Keys)
        {
            foreach (GameObject r in graph[room].Keys)
            {
                print($"Room: {room}, AdjRooms: {r.name}");
            }
        }*/
    }

    private void CalculateDifficulty(GameObject[] rooms) {
        // Will add this later

        foreach (GameObject room in rooms) {
            SpawnEnemies(room);
        }
    }

    private void SpawnEnemies(GameObject room) {
        if (room.GetComponent<EnemySpawner>() != null) {
            room.GetComponent<EnemySpawner>().SpawnEnemies(1);
        }
    }

    private Dictionary<GameObject, int> FindAdjRooms(GameObject room)
    {
        Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>();
        Transform spawnpointParent = room.transform.GetChild(0);
        
        // Loops through each child and gets the room adjacent to it
        foreach (Transform child in spawnpointParent)
        {
            OpeningsManager opMan = child.GetComponent<OpeningsManager>();
            GameObject adjRoom = opMan.GetAdjacentRoom();

            if (adjRoom is null) {
                continue; 
            }

            dict.Add(adjRoom, 0);
        }

        return dict;
    }


    private void OnDestroy()
    {
        EventManager.instance.onAllRoomsSpawned -= InitiateGraph;
    }
}
