using System.Linq;
using Attributes;
using Events;
using Interfaces;
using Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSetup : MonoBehaviour, IDamageable
{
    [Header("Character Stats")]
    public int maxHealth;
    [SerializeField] private int currentHealth;

    [Header("Character Rig and Model")]
    [SerializeField] private CharacterData cData;
    [SerializeField][ReadOnly] private GameObject armature;
    GameObject _armatureInstance;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar avatar;
    [SerializeField] private GameObject playerIndicator;

    [Header("Player Information")]
    public string charInstanceName;
    [SerializeField] private PlayerData pData;
    [SerializeField] private int playerID;
    [SerializeField] public PlayerInput playerInput;

    [Header("Audio")] 
    [SerializeField] private AudioClipList hitSounds;
    [SerializeField] private AudioClipList attackSounds;
    [SerializeField] private AudioClipList painSounds;
    private Attack attack;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            Debug.Log($"currentHealth was altered and is now {CurrentHealth}");
            if (CurrentHealth <= 0)
            {
                Debug.Log($"currentHealth hit {CurrentHealth} and this player {playerID}");
                GameEvents.OnPlayerDiedEvent?.Invoke(playerID);
            }
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

    public PlayerData PData
    {
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
        GameEvents.OnGameOverUIEvent += Clear;
    }

    private void OnDisable()
    {
        GameEvents.OnFightSceneHasLoadedEvent -= SpawnCharacter;
        GameEvents.OnGameOverUIEvent -= Clear;
    }

    private void SpawnCharacter()
    {
        _armatureInstance = Instantiate(armature, transform);
        var animators = _armatureInstance.GetComponentsInChildren<Animator>().ToList();
        Debug.Log(animators.Count + " animators");

        if (animators.Count > 0)
            for (int i = 0; i < animators.Count; i++)
            {
                if (i >= 0)
                {
                    DestroyImmediate(animators[i]);
                    break;
                }
            }

        this.GetComponent<Transform>().position = SpawnPosition.position;
        
        animator = this.GetComponent<Animator>();
        
        if(animator)
        {
            animator.avatar = avatar;
            animator.enabled = true;
        }
        
        playerIndicator.SetActive(true);
        
        attack = gameObject.AddComponent<Attack>();
        
        if(attack)
        {
            attack.attackSounds = attackSounds;
            attack.hitSounds = hitSounds;
        }
    }

    public void AssignCharData(CharacterData characterData)
    {
        avatar = CData.CharAvatar;
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
        CurrentHealth -= damageAmount;
        Debug.Log(playerID + " took damage of" + damageAmount);
        float healthAsPercent = (float)this.CurrentHealth / (float)maxHealth;
        GameEvents.OnCharacterDamagedEvent?.Invoke(playerID, healthAsPercent);
        GameEvents.OnAudioCollisionEvent?.Invoke(painSounds.GetRandomClip());
    }

    void Clear()
    {
        Destroy(_armatureInstance);
        playerIndicator.SetActive(false);
        Destroy(attack);
    }
}
