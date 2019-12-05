using UnityEngine;

[CreateAssetMenu(fileName ="Player", menuName = "Game Objects/Characters")]
public class Player : ScriptableObject
{
    [SerializeField] public float walkSpeed;
    [SerializeField] public Weapon[] weapons;

}


