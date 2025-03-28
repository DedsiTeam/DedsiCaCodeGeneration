namespace DedsiCaCodeGeneration;

public static class FileHelper
{
    public static void DirectoryCreate(string path, bool isExistDelete = true)
    {
        if (Directory.Exists(path) && isExistDelete)
        {
            Directory.Delete(path, true);
        }
        Directory.CreateDirectory(path);
    }
    
    public static void FileCreate(string filePath, string fileName, string fileContent, bool isExistDelete = true)
    {
        DirectoryCreate(filePath, isExistDelete);
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);   
        }

        File.WriteAllText(filePath + "\\" + fileName, fileContent);
    }
}