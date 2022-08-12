using Attributes;
using Events;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSetup : MonoBehaviour
{
    [Header("Character Stats")] 
    [SerializeField][ReadOnly] private string charInstanceName;
    [SerializeField][ReadOnly] private int maxHealth;
    public int currentHealth;
    
    [Header("Character Rig and Model")]
    [SerializeField] private CharacterData cData;
    [SerializeField][ReadOnly] private GameObject armature;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar avatar;
    
    [Header("Player Information")] 
    [SerializeField] private PlayerData pData;
    [SerializeField] private int playerID;
    [SerializeField] public PlayerInput playerInput;

    public CharacterData CData
    {
        get => cData;
        set
        {
            cData = value;
            AssignCharData(CData);
        }
    }

    public PlayerData PData {
        get => pData;
        set
        {
            pData = value;
            AssignPlayerData(PData);
        }
    }
    
    public int PlayerID { get => playerID; set => playerID = value; }

    public Transform SpawnPosition { get; set; }
    

    private void OnEnable()
    {
        GameEvents.OnFightSceneHasLoadedEvent += SpawnCharacter;
    }
    
    private void OnDisable()
    {
        GameEvents.OnFightSceneHasLoadedEvent -= SpawnCharacter;
    }

    private void SpawnCharacter()
    {
        Instantiate(armature, transform);
        this.GetComponent<Transform>().position = SpawnPosition.position;
        animator = this.AddComponent<Animator>();
        animator.runtimeAnimatorController = CData.CharAnimatorController;
        animator.avatar = avatar;
        animator.enabled = true;
    }
    
    public void AssignCharData(CharacterData characterData)
    {
        avatar = CData.CharAvatar;
//        animator = GetComponentInChildren<Animator>();
//        animator.enabled = false;
        
        armature = CData.CharPrefab;
        maxHealth = CData.MaxHealth;
        currentHealth = maxHealth;
    }
    
    private void AssignPlayerData(PlayerData playerData)
    {
        PlayerID = playerData.PlayerID;
        charInstanceName = playerData.PlayerLabelShort;
    }
    
}
