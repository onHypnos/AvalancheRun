using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner current;
    [SerializeField]private GameObject _coinObject;
    
    public void Awake()
    {
        DontDestroyOnLoad(this);
        current = this;
    }


    public GameObject CreateCoin(Vector3 position) => Instantiate(_coinObject, position, Quaternion.identity);
    
}
