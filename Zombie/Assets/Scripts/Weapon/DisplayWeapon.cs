using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWeapon : MonoBehaviour
{
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public float damage;
    [SerializeField] public int magazinSize;
    [SerializeField] public int loadStatus;
    [SerializeField] public float fireDistance;

    [SerializeField] public GameObject weaponPrefab;
    // how many seconds are between every shot
    [SerializeField] public float fireRate;
    // Start is called before the first frame update
    void Start()
    {

    }
}
