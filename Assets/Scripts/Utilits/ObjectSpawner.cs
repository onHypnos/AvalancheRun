using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner current;
    [SerializeField]private GameObject _coinObject;    
    
    private void Awake()
    {
        //DontDestroyOnLoad(this);
        current = this;
    }


    public GameObject CreateCoin(Vector3 position) => Instantiate(_coinObject, position, Quaternion.identity);
    public GameObject CreateObject(Vector3 position, GameObject example)
    {
        return Instantiate(example, position, Quaternion.identity);
    }

    public void CreateObjectsInTime(GameObject[] objects, ObjectSpawnerView spawnerView, float deltaTime)
    {
        StartCoroutine(Utilits.CreatingObjects(objects, spawnerView.transform, deltaTime)); 
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
