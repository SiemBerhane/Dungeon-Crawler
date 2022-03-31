using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralGeneration;

namespace Algorithms {
    public class HashingAlgorithm : MonoBehaviour
    {
        public static HashingAlgorithm instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public int HashKey(string key) {
            int sumOfKey = 0;

            foreach(Char chr in key) {
                sumOfKey += (int)chr;
            }

            return sumOfKey % DungeonManager.instance.roomHashTable.Length;
        }

    }

}

