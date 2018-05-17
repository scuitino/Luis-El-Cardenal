using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawnedItem : MonoBehaviour {

    // to know which object is the spawner
    public GameObject _spawner;

    // set reference to the spawner
    public void SetSpawner(GameObject aSpawner)
    {
        _spawner = aSpawner;
    }

    // to destroy the spawner of this item
    public void DestroySpawner()
    {
        Destroy(_spawner);
    }
}
