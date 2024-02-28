/* This code example demonstrates Microsoft's KernelMemory package
 * and the "LLamaSharp text generator" it comes with. Although
 * it technically works, it's stateless (resetting context every time),
 * and is pretty limited. For most applications users will be best
 * served by interacting with the LLamaSharp classes directly and
 * avoiding Microsoft's repackaged wrappers around their functionality.
 */

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
