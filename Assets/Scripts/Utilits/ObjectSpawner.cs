using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner current;
    [SerializeField]private GameObject _coinObject;
    // Use this for initialization
    public void Awake()
    {
        DontDestroyOnLoad(this);
        current = this;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public GameObject CreateCoin(Vector3 position) => Instantiate(_coinObject, position, Quaternion.identity);
    
}
