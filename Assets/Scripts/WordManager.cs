using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    string[] currentWord = new []{"","","","",""};
    [SerializeField] private Transform ceiling;
    [SerializeField] private GameObject chairPrefab;
    public void changeLetter(int index, string letter)
    {
        currentWord[index] = letter;
    
        //join the array into a single string
        if (string.Join("", currentWord) == "CHAIR")
        {
            Instantiate(chairPrefab,ceiling.position,Quaternion.identity);
        }
    }
}
