using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public event Action<GameObject> onRoomCollision;
    public event Action onTooManyRooms;
    public event Action onRoomInvalid;
    public event Action onRoomValidationOver;
    public event Action<ProceduralGeneration.OpeningsManager> onOpeningsValidationOver;
    public event Action onAllRoomsSpawned;

    public void AllRoomsSpawned() {
        if (onAllRoomsSpawned != null) {
            onAllRoomsSpawned();
        }
    }

    public void OpeningsValidationOver(ProceduralGeneration.OpeningsManager OM) {
        if (onOpeningsValidationOver != null) {
            onOpeningsValidationOver(OM);
        }
    }

    public void RoomValidationOver() {
        if (onRoomValidationOver != null) {
            onRoomValidationOver();
        }
    }

    public void RoomInvalid() {
        if (onRoomInvalid != null) {
            onRoomInvalid();
        }
    }

    public void RoomCollision(GameObject obj) {
        if (onRoomCollision != null) {
            onRoomCollision(obj);
        }
    }

    public void TooManyRooms() {
        if (onTooManyRooms != null) {
            onTooManyRooms();
        }
    }
}
