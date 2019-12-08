using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Gun,
    SubmachineGun,
    RocketLauncher,
};

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] public WeaponType weaponType;
    [SerializeField] public float damage;
    [SerializeField] public int magazinSize;
    [SerializeField] public int loadStatus;
    [SerializeField] public float fireDistance;

    [SerializeField] public GameObject weaponPrefab;
    // how many seconds are between every shot
    [SerializeField] public float fireRate;
    // Use this for initialization
    void Start()
    {
        
    }
    // Update is called once per frame
        void Update()
    {
       
    }
}
