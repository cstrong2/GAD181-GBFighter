using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterDB_", menuName = "Characters/New Database", order = +1)]
    public class CharacterDatabase : ScriptableObject
    {
        public List<CharacterData> charactersList;
    }
}