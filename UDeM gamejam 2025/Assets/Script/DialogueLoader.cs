using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DialogueLoader : MonoBehaviour
{
    public List<DialogueLine> LoadDialogueFromJSON(string fileName)
    {
        TextAsset file = Resources.Load<TextAsset>(fileName);
        DialogueLine[] allLines = JsonUtilityWrapper.FromJsonArray<DialogueLine>(file.text);
        List<DialogueLine> result = new List<DialogueLine>();

        foreach (DialogueLine line in allLines)
        {
            if (line.requires == null || line.requires.Count == 0 || AllRequirementsMet(line.requires))
                result.Add(line);
        }

        return result;
    }

    bool AllRequirementsMet(List<string> flags)
    {
        return flags.All(flag => GameData.GetFlag(flag));
    }
}
