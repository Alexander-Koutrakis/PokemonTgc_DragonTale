using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem {


// create a new binaryformater to store the data for each deck

 public static void SaveDeck(Deck masterDeckData, Deck Deck1Data, Deck Deck2Data, Deck Deck3Data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/deck.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        DeckData data = new DeckData(masterDeckData, Deck1Data, Deck2Data, Deck3Data);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("game saved");
    }


//deserialize a binaryFormatter in order to translate out data from binary
    public static DeckData LoadDeck()
    {
        string path = Application.persistentDataPath + "/deck.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DeckData data = formatter.Deserialize(stream) as DeckData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }


    }
}
