using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{

    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questID) => StartQuest(questID));
        story.BindExternalFunction("AdvanceQuest", (string questID) => AdvanceQuest(questID));
        story.BindExternalFunction("FinishQuest", (string questID) => FinishQuest(questID));
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("FinishQuest");
    }

    private void StartQuest(string questID)
    {
        GameEventsManager.instance.questEvents.StartQuest(questID);
    }

    private void AdvanceQuest(string questID)
    {
        GameEventsManager.instance.questEvents.AdvanceQuest(questID);
    }
    
    private void FinishQuest(string questID)
    {
        GameEventsManager.instance.questEvents.FinishQuest(questID);
    }
}
