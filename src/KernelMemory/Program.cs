using Microsoft.KernelMemory;
using Microsoft.KernelMemory.AI;
using Microsoft.KernelMemory.AI.LlamaSharp;

string modelPath = @"C:\Users\scott\Documents\important\LLM-models\llama-2-7b-chat.Q5_K_M.gguf";
LlamaSharpConfig lsConfig = new() { ModelPath = modelPath };
LlamaSharpTextGenerator txtGen = new(lsConfig, loggerFactory: null);
TextGenerationOptions options = new()
{
    MaxTokens = 1000,
    Temperature = 0.0,
    StopSequences = ["Question"]
};

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nQuestion: ");
    string prompt = Console.ReadLine() ?? string.Empty;

    Console.ForegroundColor = ConsoleColor.Yellow;
    await foreach (string token in txtGen.GenerateTextAsync(prompt, options))
    {
        Console.Write(token);
    }
}
