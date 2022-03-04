using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeAndClean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        List <string> ItemList = new List<string>();

        ItemList.Add("Hello");
        ItemList.Add("Bye");
        ItemList.Add("asdf");
        ItemList.Add("fdas");
        ItemList.Add("asds");
        ItemList.Add("aabb");
        for (int i = 0; i < ItemList.Count; i++) {

            PrintInput(ItemList[i]);
        }
    }

    public void PrintInput(string word) {
        int con = 0;
        //Make a dictionary for the characters in the word
        Dictionary<char, int> wordChars = new Dictionary<char, int>();

        for (int i = 0; i < word.Length; i++) {
            //Check if the character is already in the dictionary 
            if (wordChars.ContainsKey(word[i])) {

                int pos = wordChars[word[i]];
                wordChars.Remove(word[i]);
                wordChars.Add(word[i], pos + 1);

            } else {
                //if is not in the dictionary, add it
                wordChars.Add(word[i], 1);

            }
        }

        foreach (KeyValuePair<char, int> it in wordChars) {
            if (it.Value == 2 && con == 0) {
                Debug.Log(word);
                Debug.Log(con);
                con++;
            }
        }
        

    }
}
