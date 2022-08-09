using Attributes;
using Core;
using ScriptableObjects;
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
    [SerializeField][ReadOnly] private Animator animator;
    private Transform spawnLocation;
    [Header("Player Information")] 
    [SerializeField] private PlayerData pData;
    [SerializeField] private int playerID;
    [SerializeField] public PlayerInput playerInput;

    public CharacterData CData
    {
        get => cData; 
        set => cData = value;
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

    private void Awake()
    {
        
        if (PlayersManager.Instance && GameManager.Instance)
        {
            var playerInstance = PlayersManager.Instance.Players[0];
            PData = playerInstance.playerInstanceData;
            playerID = PData.PlayerID;
            CData = GameManager.Instance.GetCharByID(PData.CurrentCharacterID);
            charInstanceName = CData.Name + "-P" + playerID;
            playerInput = playerInstance.GetComponent<PlayerInput>();
            
        }
        else
        {
            Debug.Log("No player manager or game manager was found");
            if (playerInput == null)
                playerInput = GetComponent<PlayerInput>();
        }
       

        
        armature = CData.CharPrefab;
        //TODO: REMOVE THIS This is temp code for interim release   

        #region InterimCodeForWeek1ToDelete

        Debug.Log(PlayersManagerInterim.PMInstance.SpawnLocations[PlayerID]);
        if (PlayersManagerInterim.PMInstance)
        {
            spawnLocation = PlayersManagerInterim.PMInstance.SpawnLocations[PlayerID];
            Instantiate(armature, transform);
            this.gameObject.SetActive(false);
            this.GetComponent<Transform>().position = spawnLocation.position;
            this.gameObject.SetActive(true);

        }
        else
        {
            Instantiate(armature, transform);
        }

        #endregion 
        
        // Apply the animator and configure it
        animator = GetComponent<Animator>();
        animator.avatar = CData.CharAvatar;
        
        maxHealth = CData.MaxHealth;
        currentHealth = maxHealth;

    }

    private void Start()
    {
        var playersParent = GameObject.Find("PlayersParent");
        if (!playersParent) {
            playersParent = new GameObject();
            playersParent.name = "PlayersParent";
        }
        this.transform.parent = playersParent.transform;

       
    }
}
