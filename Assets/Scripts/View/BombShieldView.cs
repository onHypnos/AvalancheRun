using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShieldView : MonoBehaviour
{
    private Vector3 _temp;
    private Rigidbody _tempRig;
    [SerializeField] private float _force = 5000;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            _temp = (other.transform.position - transform.position).normalized;
            //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(_force, transform.position, transform.localScale.x+10);
            other.gameObject.GetComponent<Rigidbody>();
            _tempRig = other.gameObject.GetComponent<Rigidbody>();
            _tempRig.velocity = Vector3.zero;
            _tempRig.AddForce(Vector3.up * _force, ForceMode.Force);
            Debug.Log($"Hit {other.name}");
        }
    }
}
