using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject Character;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject avoidPrefab;
    public GameObject winPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;

        // Vector3 charPos = new Vector3(18.24f,1.54f,-0.29f);
        // Instantiate(Character, charPos, Quaternion.identity, environmentRoot);

        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if(letter == 'x'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if(letter == 's'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if(letter == 'b'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if(letter == '?'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if(letter == 'a'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(avoidPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                if(letter == 'w'){
                    Vector3 newPos = new Vector3(column, row, 0f);
                    Instantiate(winPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                // column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        Character.transform.position = new Vector3(18.24f,1.54f,-0.29f);
        LoadLevel();
    }
}
