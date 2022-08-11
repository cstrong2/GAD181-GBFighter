using System;
using Attributes;
using Core;
using ScriptableObjects;
using UnityEditor.Animations;
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

    public PlayerData PData { get; set; }

    public int PlayerID { get; set; }

    public Transform SpawnPosition { get; set; }
    

    private void OnEnable()
    {
        
        if (PlayersManager.Instance && GameManager.Instance)
        {
            
            if(PData && CData)
            {
                var playerInstance = PlayersManager.Instance.Players[PData.PlayerID];
                
                if (playerInput == null) 
                    playerInput = playerInstance.GetComponent<PlayerInput>();
                
                charInstanceName = PData.PlayerLabelShort;
            }

        }
        else
        {
            Debug.Log("No player manager or game manager was found");
            if (playerInput == null)
                playerInput = GetComponent<PlayerInput>();
        }
        if(CData)
        {
            Debug.Log(armature + " should instantiate");
            Instantiate(armature, transform);
            this.GetComponent<Transform>().position = SpawnPosition.position;
        }
        
        var animators = GetComponentsInChildren<Animator>();
        Debug.Log(animators.Length);
        
        if (animators.Length > 0)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                if (i > 0)
                    Destroy(animators[i]);
            }
        }
    }
    

    public void AssignCharData(CharacterData characterData)
    {
        animator = GetComponentsInChildren<Animator>()[0];
        avatar = CData.CharAvatar;
        animator.avatar = avatar;
        armature = CData.CharPrefab;
        maxHealth = CData.MaxHealth;
        currentHealth = maxHealth;
    }

}
