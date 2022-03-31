using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProceduralGeneration
{
    public class RoomManager : MonoBehaviour
    {
        public bool validationOver;

        private int numOfOpenings;
        private OpeningsManager[] listOfOpenings;

        private void Start()
        {
            EventManager.instance.onOpeningsValidationOver += ValidationOver;
            // Create event that will destroy spawnpoints to save memory :)
        }

        public void GetOpenings()
        {
            listOfOpenings = transform.GetComponentsInChildren<OpeningsManager>();
            numOfOpenings = listOfOpenings.Length;

            CheckOpenings();
        }

        public void CheckOpenings()
        {
            foreach (OpeningsManager opening in listOfOpenings)
            {
                opening.Invoke("SpawnRooms", 0.1f);
            }
        }


        private void ValidationOver(OpeningsManager opening)
        {
            if (numOfOpenings == 0)
            {
                EventManager.instance.RoomValidationOver();
            }
            else if (numOfOpenings == 1)
            {
                numOfOpenings--;
                EventManager.instance.RoomValidationOver();
            }
            else
            {
                numOfOpenings--;
            }

            // opening.enabled = false;
            return;
        }

        private void OnDestroy()
        {
            EventManager.instance.onOpeningsValidationOver -= ValidationOver;
        }
    }
}
