// https://github.com/scriban/scriban

using DedsiCaCodeGeneration;
using Scriban;

Console.WriteLine("请输入项目名称：");
var projectName = Console.ReadLine();

Console.WriteLine("请输入领域名称：");
var domainName = Console.ReadLine();

var templateFilePath = "D:\\Github\\CaCodeGeneration\\DedsiCaCodeGeneration\\TemplateFiles";
var outputFilePath = "C:\\Users\\Cohen\\Desktop\\" + domainName;
FileHelper.DirectoryCreate(outputFilePath);
Console.WriteLine("创建目录成功！");

await CreateDomainFile();
await CreateCommandHandlerAsync();
await CreateEfCoreRepositoryAsync();
await CreateQueryAsync();
await CreateDtoAsync();
await CreateControllerAsync();
return;


async Task CreateDomainFile()
{
    var actionName = "Domain";
    
    var text = await File.ReadAllTextAsync(templateFilePath + "\\" + actionName);
    
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });
    
    FileHelper.FileCreate(outputFilePath + "\\" + actionName,$"{domainName}.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
}

async Task CreateCommandHandlerAsync()
{
    var actionName = "CommandHandlers";
    var newOutputFilePath = $"{outputFilePath}\\{actionName}";

    #region CreateCommandHandler
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\CreateCommandHandler");
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"Create{domainName}CommandHandler.cs", result);
    
    Console.WriteLine($"Create {actionName} CreateCommandHandler Ok!");
    #endregion
    
    #region UpadateCommandHandler
    text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\UpdateCommandHandler");
    template = Template.Parse(text);
    result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"Update{domainName}CommandHandler.cs", result, false);
    
    Console.WriteLine($"Create {actionName} UpdateCommandHandler Ok!");
    #endregion
    
    #region UpadateCommandHandler
    text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\DeleteCommandHandler");
    template = Template.Parse(text);
    result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"Delete{domainName}CommandHandler.cs", result, false);
    
    Console.WriteLine($"Create {actionName} DeleteCommandHandler Ok!");
    #endregion
}

async Task CreateEfCoreRepositoryAsync()
{
    var actionName = "Repositories";
    var newOutputFilePath = $"{outputFilePath}\\{actionName}";
    
    #region CreateCommandHandler
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\EfCoreRepository");
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Repository.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateQueryAsync()
{
    var actionName = "Queries";
    var newOutputFilePath = $"{outputFilePath}\\{actionName}";
    
    #region EfCoreQuery
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\EfCoreQuery");
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Query.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateDtoAsync()
{
    var actionName = "Dtos";
    var newOutputFilePath = $"{outputFilePath}\\{actionName}";
    
    #region Dtos
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}");
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Dto.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateControllerAsync()
{
    var actionName = "Controller";
    var newOutputFilePath = $"{outputFilePath}\\{actionName}";

    #region Dtos
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}");
    var template = Template.Parse(text);
    var result = template.Render(new { projectname = projectName, domainname = domainName });

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}{actionName}.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}