using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class InkDialogueVariables
{
    private Dictionary<string, Ink.Runtime.Object> variables;

    public InkDialogueVariables(Story story)
    {
        variables = new Dictionary<string, Ink.Runtime.Object>();

        foreach (string name in story.variablesState)
        {
            Ink.Runtime.Object value = story.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            // Debug.Log($"Loaded Ink variable: {name} with value: {value}");
        }
    }

    public void SyncVariablesAndStartListening(Story story)
    {
        SyncVariablesToStory(story);
        story.variablesState.variableChangedEvent += UpdateVariableState;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= UpdateVariableState;
    }

    public void UpdateVariableState(string name, Ink.Runtime.Object value)
    {
        if (!variables.ContainsKey(name))
        {
            // Debug.LogWarning($"Variable {name} does not exist in InkDialogueVariables.");
            return;
        }

        variables[name] = value;
        // Debug.Log($"Updated Ink variable: {name} to value: {value}");
        
    }
    
    private void SyncVariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> kvp in variables)
        {
            story.variablesState.SetGlobal(kvp.Key, kvp.Value);
        }
    }
}
