using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralGeneration;

namespace Algorithms {

    public class Graphs : MonoBehaviour
    {
        public static Graphs instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        private Dictionary<GameObject, Dictionary<GameObject, int>> graph;
        private Dictionary<string, Dictionary<string, string>> test;

        private void Start() {
            graph = DungeonManager.instance.graph;
            test = DungeonManager.instance.test;
        }

        public void AddNewNodes(GameObject node, GameObject[] neighbours) {
            if (NodeExists(node)) {
                Debug.LogWarning($"Node: {node.name} already exists");
                return;
            }  

            Dictionary<GameObject, int> temp = new Dictionary<GameObject, int>();

            foreach(GameObject room in neighbours) {
                temp.Add(room, 1);
            }   

            graph.Add(node, temp);
            Test();     
        }

        private bool NodeExists(GameObject node) {
            return graph.ContainsKey(node);
        }

        private void Test() {
            foreach(var x in graph.Values) {
                foreach (var y in x.Keys) {
                    print(y.name);
                }
            }
        }

        private void TestDict() {
            Dictionary<GameObject, int> temp = new Dictionary<GameObject, int>();
            Dictionary<string, string> neighbours = new Dictionary<string, string>();
            neighbours.Add("a", "b");
            test.Add("one", neighbours);

            // How to check for the values and keys of a two dimensional dict
            foreach(var x in test.Values) {
                foreach(var y in x) {
                    print(y.Key);
                }
            }   

            // graph.Add(node, temp); 
        }

    }
}
