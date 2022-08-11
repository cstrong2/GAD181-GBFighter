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
    [SerializeField][ReadOnly] private AnimatorController animatorController;
    [SerializeField] private Animator animator;
    [SerializeField] private Avatar avatar;
    private Transform spawnPosition;
    
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

    public PlayerData PData
    {
        get => pData;
        set => pData = value;
    }

    public int PlayerID
    {
        get => playerID;
        set => playerID = value;
    }

    public Transform SpawnPosition
    {
        get => spawnPosition;
        set => spawnPosition = value;
    }

    private void Awake()
    {
//        DestroyImmediate(GetComponent<PlayerInput>());
        
        if (PlayersManager.Instance && GameManager.Instance)
        {
//            PData = playerInstance.playerInstanceData;
//            playerID = PData.PlayerID;
//            CData = GameManager.Instance.GetCharByID(PData.CurrentCharacterID);
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
        
    }

    private void Start()
    {
        
//        var playersParent = GameObject.Find("PlayersParent");
//        if (!playersParent) {
//            playersParent = new GameObject();
//            playersParent.name = "PlayersParent";
//        }
//
//        this.transform.parent = playersParent.transform;
        
        if(CData)
        {
            Instantiate(armature, transform);
            this.GetComponent<Transform>().position = SpawnPosition.position;
        }
        
        var animators = GetComponentsInChildren<Animator>();
        Debug.Log(animators.Length);
        for (int i = 0; i < animators.Length; i++)
        {
            if (i > 0)
                Destroy(animators[i]);
        }
       
    }

    public void AssignCharData(CharacterData characterData)
    {
        animator = GetComponentsInChildren<Animator>()[0];
        avatar = CData.CharAvatar;
        animator.avatar = avatar;
        animator.runtimeAnimatorController = CData.CharAnimatorController;
        armature = CData.CharPrefab;
        maxHealth = CData.MaxHealth;
        currentHealth = maxHealth;
    }

}
