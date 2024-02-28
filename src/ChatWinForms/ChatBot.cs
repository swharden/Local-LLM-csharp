using LLama.Common;
using LLama;

namespace ChatWinForms;

/// <summary>
/// This class wraps a LLamaSharp chat session and exposes events
/// to make it easier to engage in chat with a model while invoking
/// events to update the GUI at various points along the way.
/// </summary>
internal class ChatBot
{
    readonly ChatSession Session;

    public EventHandler<string> TextGenerated = delegate { };
    public EventHandler ResponseStarted = delegate { };
    public EventHandler ResponseEnded = delegate { };

    public ChatBot(string modelPath)
    {
        if (!File.Exists(modelPath))
            throw new FileNotFoundException(modelPath);

        ModelParams modelParams = new(modelPath);
        LLamaWeights weights = LLamaWeights.LoadFromFile(modelParams);

        LLamaContext context = weights.CreateContext(modelParams);
        InteractiveExecutor ex = new(context);
        Session = new ChatSession(ex);

        var hideWords = new LLamaTransforms.KeywordTextOutputStreamTransform(["User:", "Bot: "]);
        Session.WithOutputTransform(hideWords);
    }

    public async Task AskAsync(string userInput, float temperature = 0.6f)
    {
        ResponseStarted.Invoke(this, EventArgs.Empty);

        InferenceParams infParams = new()
        {
            Temperature = temperature,
            AntiPrompts = ["User:"]
        };

        // Get a question from the user
        ChatHistory.Message msg = new(AuthorRole.User, "Question: " + userInput);

        // Display answer text as it is being generated
        await foreach (string text in Session.ChatAsync(msg, infParams))
        {
            TextGenerated.Invoke(this, text);
        }

        ResponseEnded.Invoke(this, EventArgs.Empty);
    }
}
