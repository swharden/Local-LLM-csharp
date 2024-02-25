using DocumentQaWithStorage;

string modelPath = @"C:\Users\scott\Documents\important\LLM-models\llama-2-7b-chat.Q5_K_M.gguf";
DocumentChatBot chat = new(modelPath);

// only import files if they have not been imported before
if (!chat.StorageFolderExists)
{
    // information learned from documents is saved to disk
    await chat.ImportFiles("../../../../../data/", "*.*");
}

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nQuestion: ");
    string question = Console.ReadLine() ?? string.Empty;

    await chat.ShowAnswer(question);
}