using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterDB_", menuName = "Characters/New Database", order = +1)]
    public class CharacterDatabase : ScriptableObject
    {
        public List<CharacterData> charactersList;
        
        public CharacterData GetCharByID(int id)
        {
            return charactersList.Find(m => m.CharID == id);
        }
    }
}