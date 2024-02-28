/* This code example demonstrates how to pair the LLamaSharp package 
 * with Microsoft's KernelMemory package to analyze the content of 
 * various documents (PDF, Markdown, text files, etc.) and answer
 * questions about them in an interactive chat session with a LLM.
 * 
 * Unlike the other code example in this folder, this code example wraps
 * document ingestion and Q&A functionality in a class that also stores
 * ingested document information to disk, meaning that subsequent launches
 * of this application are very fast since the documents do not need
 * to be re-analyzed every time the application starts.
 */

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