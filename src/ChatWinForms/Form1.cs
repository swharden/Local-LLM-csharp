/*  This code example demonstrates how to use the LLamaSharp package
    to engage in an AI chat session with a large language model
    using Windows Forms. Note that ChatBot is a class in this project
    that makes it easy to asynchronously interact with a chat session.
*/

namespace ChatWinForms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        string path = GetModelPath();
        Text = Path.GetFileNameWithoutExtension(path);

        ChatBot chatter = new(path);
        chatter.TextGenerated += (s, e) => { rtbOutput.AppendText(e); Application.DoEvents(); };
        chatter.ResponseStarted += (s, e) => EnableInputs(false);
        chatter.ResponseEnded += (s, e) => EnableInputs(true);

        btnSend.Click += async (s, e) => await chatter.AskAsync(tbInput.Text!, (float)nudTemperature.Value);
    }

    public static string GetModelPath()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "GGUF files (*.gguf)|*.gguf|All files (*.*)|*.*",
            Title = "Select a GGUF model",
        };

        DialogResult result = dialog.ShowDialog();

        return result == DialogResult.OK
            ? dialog.FileName
            : throw new FileNotFoundException();
    }

    public void EnableInputs(bool enabled)
    {
        if (enabled == false)
            rtbOutput.Clear();

        btnSend.Enabled = enabled;
        nudTemperature.Enabled = enabled;
        tbInput.Enabled = enabled;
        Application.DoEvents();
    }
}
