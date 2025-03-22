using System.Text.Json;

namespace Client.Core;

public class LocalDataService
{
    private string _dataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private string _appName => "MythicStone";


    public LocalDataService()
    {
        CreateFolder();
    }


    private void CreateFolder()
    {
        var path = Path.Combine(_dataPath, _appName);
        Console.WriteLine(path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(_dataPath, _appName, fileName);
    }

    public T Parse<T>(string file_path)
    {
        if (!Exists(file_path))
        {
            return default;
        }

        var content = File.ReadAllText(GetFilePath(file_path));

        return JsonSerializer.Deserialize<T>(content);
    }

    public void Save<T>(string file, T value)
    {
        var path = GetFilePath(file);
        var json = JsonSerializer.Serialize(value);
        File.WriteAllText(path, json);
    }


    public bool Exists(string file)
    {
        return File.Exists(GetFilePath(file));
    }
}