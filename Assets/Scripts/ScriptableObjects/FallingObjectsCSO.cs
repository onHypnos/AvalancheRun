using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelDate/FallingObjects", fileName = "NewFallingObjectsContainer")]
public class FallingObjectsCSO : ScriptableObject
{
    [Tooltip("Пул объектов")]
    [SerializeField] private GameObject[] _objects;
    public GameObject[] Objects => _objects;    
    

}
