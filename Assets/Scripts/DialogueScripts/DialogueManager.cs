using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextAsset riddleJSON;

    public RiddlesList riddleList = new RiddlesList();

    private Story currentStory;

    public bool dialogueIsPlaying;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager.");
        }

        instance = this;

        riddleList = JsonUtility.FromJson<RiddlesList>(riddleJSON.text);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (InputManager.GetInstance().GetInteractPressed())
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        RiddlesObject riddles = RandomizeRiddle();
        currentStory.variablesState["riddle"] = riddles.riddle;
        currentStory.variablesState["answer"] = riddles.answer;

        ContinueStory();

    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    public void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }


    [System.Serializable]
    public class RiddlesObject
    {
        public string riddle;
        public string answer;
    }

    [System.Serializable]

    public class RiddlesList
    {
        public RiddlesObject[] AllRiddles;
    }


    RiddlesObject RandomizeRiddle()
    {
        return riddleList.AllRiddles[Random.Range(0, riddleList.AllRiddles.Length)];
    }
}
