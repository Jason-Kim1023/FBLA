using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    //All utility stuff running for things like the dialogue factory
    public static Dialouge generateDialoge(List<string> textList, string speaker)
    {
        DialougeFactory df = new DialougeFactory();

        if (textList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < textList.Count; i++)
        {
            df.addNewDialouge(textList[i], speaker);
        }

        return df.build();
    }
}
