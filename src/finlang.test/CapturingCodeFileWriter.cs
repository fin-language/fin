// from StateSmith project
using finlang.Output;

namespace finlang.test;

/// <summary>
/// Captures what would normally be written to file to memory instead.
/// </summary>
public class CapturingCodeFileWriter : ICodeFileWriter
{
    /// <summary>
    /// indexed by ABSOLUTE filepath (key)
    /// </summary>
    public HashList<string, Capture> captures = new();
    public Capture? lastCapture = null;
    public string LastCode => lastCapture?.code ?? "";
    public int captureCount = 0;

    public void WriteFile(string filePath, string code)
    {
        Capture capture = new(filePath, code);
        lastCapture = capture;
        captures.Add(filePath, capture);
        captureCount++;
    }

    /// <summary>
    /// Finds captures for a file name (not a file path!)
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public List<Capture> GetCapturesForFileName(string fileName)
    {
        List<Capture> matchedCaptures = [];
        foreach (var absoluteFilePath in captures.GetKeys())
        {
            if (Path.GetFileName(absoluteFilePath) == fileName)
            {
                matchedCaptures.AddRange(captures.GetValues(absoluteFilePath));
            }
        }

        return matchedCaptures;
    }

    public class Capture
    {
        public string fileName;
        public string code;

        public Capture(string fileName, string code)
        {
            this.fileName = fileName;
            this.code = code;
        }
    }
}
