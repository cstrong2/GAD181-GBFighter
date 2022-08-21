using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public CharacterData GetRandomCharacter()
        {
            return charactersList[Random.Range(0, charactersList.Count)];
        }
        
        public int GetRandomCharacterID()
        {
            return charactersList[Random.Range(0, charactersList.Count)].CharID;
        }
    }
}