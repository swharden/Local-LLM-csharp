/* This code example demonstrates how to pair the LLamaSharp package 
 * with Microsoft's KernelMemory package to analyze the content of 
 * various documents (PDF, Markdown, text files, etc.) and answer
 * questions about them in an interactive chat session with a LLM.
 */

using LLamaSharp.KernelMemory;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory;
using System.Diagnostics;

// Setup the kernel memory with the LLM model
string modelPath = @"C:\Users\scott\Documents\important\LLM-models\llama-2-7b-chat.Q5_K_M.gguf";

LLama.Common.InferenceParams infParams = new() { AntiPrompts = ["\n\n"] };

LLamaSharpConfig lsConfig = new(modelPath) { DefaultInferenceParams = infParams };

SearchClientConfig searchClientConfig = new()
{
    MaxMatchesCount = 1,
    AnswerTokens = 100,
};

TextPartitioningOptions parseOptions = new()
{
    MaxTokensPerParagraph = 300,
    MaxTokensPerLine = 100,
    OverlappingTokens = 30
};

Console.ForegroundColor = ConsoleColor.DarkGray;
IKernelMemory memory = new KernelMemoryBuilder()
    .WithLLamaSharpDefaults(lsConfig)
    .WithSearchClientConfig(searchClientConfig)
    .With(parseOptions)
    .Build();

// Ingest documents (format is automatically detected from the filename)
string documentFolder = "../../../../../data";
string[] documentPaths = Directory.GetFiles(documentFolder, "*.txt");
for (int i = 0; i < documentPaths.Length; i++)
{
    string path = documentPaths[i];
    Stopwatch sw = Stopwatch.StartNew();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Importing {i + 1} of {documentPaths.Length}: {Path.GetFileName(path)}");
    await memory.ImportDocumentAsync(path, steps: Constants.PipelineWithoutSummary);
    Console.WriteLine($"Completed in {sw.Elapsed}\n");
}

// Allow the user to ask questions
while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nQuestion: ");
    string question = Console.ReadLine() ?? string.Empty;

    Stopwatch sw = Stopwatch.StartNew();
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine($"Generating answer...");

    MemoryAnswer answer = await memory.AskAsync(question);
    Console.WriteLine($"Answer generated in {sw.Elapsed}");

    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine($"Answer: {answer.Result}");
    foreach (var source in answer.RelevantSources)
    {
        Console.WriteLine($"Source: {source.SourceName}");
    }
}