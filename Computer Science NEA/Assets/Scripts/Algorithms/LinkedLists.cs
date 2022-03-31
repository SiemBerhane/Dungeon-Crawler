using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Algorithms {

    // If there's a collision in the hash table, the data will be stored in a linked list
    public class LinkedLists : MonoBehaviour
    {
        public static LinkedLists instance;

        private void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        
        Node head;

        public Node TraverseList(GameObject data) {
            Node temp = head;

            while(temp != null) {
                if (temp.data == data) {
                    print("Room was found");
                    return temp;
                }
                temp = temp.next;
            }

            // Room wasn't found in the list
            return null;
        }

        public void DisplayList() {
            Node temp = head;
            if (temp == null)
            {
                Debug.LogWarning("Linked list is empty");
            }

            while (temp.next != null) {
                print(temp.data.name);
                temp = temp.next;
            }
        }

        public bool isEmpty() {
            Node temp = head;
            int counter = 0;

            if (temp != null) {
                while (temp.next != null) {
                    counter++;
                    temp = temp.next;
                }
            }
            
            return counter < 1;
        }

        public Node Append(GameObject data, string roomType) {
            Node newNode = new Node(data, roomType);
            if (head == null) {
                head = newNode;
                print($"{data.name} is the head of the linked list");
            }
            else {
                Node current = head;
                while (current.next != null) {
                    current = current.next;
                }

                current.next = newNode;
            }

            return newNode;
        }

        public void Remove(GameObject data) {
            Node temp = head;
            Node prev;

            if (temp.data == data && temp != null) {
                head = temp.next;
            }

            while (temp.next != null) {
                prev = temp;
                if (temp.data == data) {
                    prev.next = temp.next;
                }
                temp = temp.next;
            }
        }
    }

    public class Node {
        public Node next = null;
        public GameObject data;
        public string roomType;

        public Node(GameObject d, string rt) {
            this.data = d;
            this.roomType = rt;
        }
    }

}
