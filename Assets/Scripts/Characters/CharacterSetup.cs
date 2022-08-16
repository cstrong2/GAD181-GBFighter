using System.Linq;
using Attributes;
using Events;
using Interfaces;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterSetup : MonoBehaviour, IDamageable
{
    [Header("Character Stats")] 
    public int maxHealth;
    [SerializeField] private int currentHealth;
    
    [Header("Character Rig and Model")]
    [SerializeField] private CharacterData cData;
    [SerializeField][ReadOnly] private GameObject armature;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar avatar;
    
    [Header("Player Information")] 
    public string charInstanceName;
    [SerializeField] private PlayerData pData;
    [SerializeField] private int playerID;
    [SerializeField] public PlayerInput playerInput;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if (currentHealth <= 0)
                GameEvents.OnPlayerDiedEvent?.Invoke(playerID);
        }
    }

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
        var armatureInstance = Instantiate(armature, transform);
        var animators = armatureInstance.GetComponentsInChildren<Animator>().ToList();
        Debug.Log(animators[0]);
        for (int i = 0; i < animators.Count; i++)
        {
            if (i >= 1)
                DestroyImmediate(animators[i]);
        }
        
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
        CurrentHealth = maxHealth;
    }
    
    private void AssignPlayerData(PlayerData playerData)
    {
        PlayerID = playerData.PlayerID;
        charInstanceName = playerData.PlayerLabelShort;
    }

    public void DoDamage(int damageAmount)
    {
        int alteredHealth = this.CurrentHealth += damageAmount;
        this.CurrentHealth = alteredHealth >= maxHealth ? maxHealth : alteredHealth;
        float healthAsPercent = (float)this.CurrentHealth / (float)maxHealth;
        GameEvents.OnCharacterDamagedEvent?.Invoke(playerID, healthAsPercent);
    }
    
    ////    TODO: This Update is a TESTING setup only. DELETE IT. This will run on all characters in the scene for testing purposes.
//    private void Update()
//    {
//        if (Keyboard.current.qKey.wasPressedThisFrame)
//        {
//            DoDamage(-20);
//        }
//    }
}
