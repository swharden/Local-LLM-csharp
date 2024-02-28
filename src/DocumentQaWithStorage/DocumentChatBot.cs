using LLamaSharp.KernelMemory;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory;
using System.Diagnostics;
using LLama.Common;
using Microsoft.KernelMemory.ContentStorage.DevTools;
using Microsoft.KernelMemory.FileSystem.DevTools;
using Microsoft.KernelMemory.MemoryStorage.DevTools;

namespace DocumentQaWithStorage;

/// <summary>
/// This class is a wrapper for KernelMemory and LLamaSharp which
/// allows documents to be ingested and their information to be stored
/// locally such that the information can be recalled when the program
/// restarts instead of requiring slow re-analysis of documents.
/// </summary>
public class DocumentChatBot
{
    public string StorageFolder => Path.GetFullPath($"./storage-{nameof(DocumentChatBot)}");
    public bool StorageFolderExists => Directory.Exists(StorageFolder) && Directory.GetDirectories(StorageFolder).Length > 0;

    readonly IKernelMemory Memory;

    public DocumentChatBot(string modelPath)
    {
        if (!File.Exists(modelPath))
            throw new FileNotFoundException(modelPath);

        InferenceParams infParams = new() { AntiPrompts = ["\n\n"] };

        LLamaSharpConfig lsConfig = new(modelPath) { DefaultInferenceParams = infParams };

        SearchClientConfig searchClientConfig = new()
        {
            MaxMatchesCount = 1,
            AnswerTokens = 500,
        };

        TextPartitioningOptions parseOptions = new()
        {
            MaxTokensPerParagraph = 300,
            MaxTokensPerLine = 100,
            OverlappingTokens = 30
        };

        SimpleFileStorageConfig storageConfig = new()
        {
            Directory = StorageFolder,
            StorageType = FileSystemTypes.Disk,
        };

        SimpleVectorDbConfig vectorDbConfig = new()
        {
            Directory = StorageFolder,
            StorageType = FileSystemTypes.Disk,
        };

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Kernel memory folder: {StorageFolder}");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Memory = new KernelMemoryBuilder()
            .WithSimpleFileStorage(storageConfig)
            .WithSimpleVectorDb(vectorDbConfig)
            .WithLLamaSharpDefaults(lsConfig)
            .WithSearchClientConfig(searchClientConfig)
            .With(parseOptions)
            .Build();
    }

    public async Task ImportFiles(string folderPath, string searchPattern)
    {
        string[] filePaths = Directory.GetFiles(folderPath, searchPattern);
        for (int i = 0; i < filePaths.Length; i++)
        {
            await ImportFile(filePaths[i]);
        }
    }

    public async Task ImportFile(string filePath)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Importing file: {Path.GetFileName(filePath)}");
        await Memory.ImportDocumentAsync(filePath, steps: Constants.PipelineWithoutSummary);
        Console.WriteLine($"Completed in {sw.Elapsed}\n");
    }

    public async Task ImportText(string text)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Importing text ({text.Length} bytes)...");
        await Memory.ImportTextAsync(text, steps: Constants.PipelineWithoutSummary);
        Console.WriteLine($"Completed in {sw.Elapsed}\n");
    }

    public async Task AskAsync(string question)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"Generating answer...");

        MemoryAnswer answer = await Memory.AskAsync(question);
        Console.WriteLine($"Answer generated in {sw.Elapsed}");

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Answer: {answer.Result}");
        foreach (var source in answer.RelevantSources)
        {
            Console.WriteLine($"Source: {source.SourceName}");
        }
        Console.WriteLine();
    }

    public async Task ShowAnswer(string question)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"Generating answer...");

        MemoryAnswer answer = await Memory.AskAsync(question);
        Console.WriteLine($"Answer generated in {sw.Elapsed}");

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Answer: {answer.Result}");
        foreach (var source in answer.RelevantSources)
        {
            Console.WriteLine($"Source: {source.SourceName}");
        }
    }
}
