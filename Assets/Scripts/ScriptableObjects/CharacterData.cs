using System.Collections.Generic;
using Player;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterData_", menuName = "Characters/New Character Data", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [Header("Character Stats")]
        [SerializeField] private Sprite charImage;
        [SerializeField] private new string name;
        [SerializeField] private int charID;
        [SerializeField] private float moveSpeed;
        
        [SerializeField] private int maxHealth;
        
        [Header("Character Animation Data")]
        [SerializeField] private Animator charAnimator;
        [SerializeField] private Avatar charAvatar;
        [SerializeField] private Mesh charArmatureMesh;
        
        [Header("Character Prefab")]
        [SerializeField]
        private GameObject charPrefab;
        
        public List<Action> actionList;
        
        #region Properties
        
        public string Name => name;
        public int CharID => charID;
        public float MoveSpeed => moveSpeed;
        public int MaxHealth => maxHealth;
        public Animator CharAnimator => charAnimator;
        public Avatar CharAvatar => charAvatar;
        public Mesh CharArmatureMesh => charArmatureMesh;
        public GameObject CharPrefab => charPrefab;
        public Sprite CharImage => charImage;
        
        #endregion
        
        

    }
}