using System.Text.RegularExpressions;
using DedsiCaCodeGeneration;
using Scriban;

Console.WriteLine("请输入项目名称：");
var projectName = Console.ReadLine();

Console.WriteLine("请输入领域名称：");
var domainName = Console.ReadLine();
var humpDomainName = Regex.Replace(domainName, "^[A-Z]", match => match.Value.ToLower());;

var inputData = new { projectname = projectName, domainname = domainName, humpdomainname = humpDomainName };

var templateFilePath = "./TemplateFiles";
var outputFilePath = $"./Result/{projectName}/{domainName}s";
FileHelper.DirectoryCreate(outputFilePath);
Console.WriteLine("创建目录成功！");

await CreateDomainFileAsync();
await CreateCommandHandlerAsync();
await CreateEfCoreRepositoryAsync();
await CreateQueryAsync();
await CreateDtoAsync();
await CreateControllerAsync();
return;


async Task CreateDomainFileAsync()
{
    var actionName = "Domain";
    
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}");
    
    var template = Template.Parse(text);
    var result = template.Render();
    
    FileHelper.FileCreate($"{outputFilePath}\\{actionName}\\{domainName}s",$"{domainName}.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
}

async Task CreateCommandHandlerAsync()
{
    var actionName = "CommandHandlers";
    var newOutputFilePath = $"{outputFilePath}\\UseCases\\{actionName}";

    #region CreateCommandHandler
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\CreateCommandHandler");
    var template = Template.Parse(text);
    var result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"Create{domainName}CommandHandler.cs", result);
    
    Console.WriteLine($"Create {actionName} CreateCommandHandler File Ok!");
    #endregion
    
    #region UpadateCommandHandler
    text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\UpdateCommandHandler");
    template = Template.Parse(text);
    result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"Update{domainName}CommandHandler.cs", result, false);
    
    Console.WriteLine($"Create {actionName} UpdateCommandHandler File Ok!");
    #endregion
    
    #region UpadateCommandHandler
    text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\DeleteCommandHandler");
    template = Template.Parse(text);
    result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"Delete{domainName}CommandHandler.cs", result, false);
    
    Console.WriteLine($"Create {actionName} DeleteCommandHandler File Ok!");
    #endregion
}

async Task CreateEfCoreRepositoryAsync()
{
    var actionName = "Repositories";
    var newOutputFilePath = $"{outputFilePath}\\Infrastructures\\{domainName}s";
    
    #region CreateCommandHandler
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\EfCoreRepository");
    var template = Template.Parse(text);
    var result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Repository.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateQueryAsync()
{
    var actionName = "Queries";
    var newOutputFilePath = $"{outputFilePath}\\UseCases\\{actionName}";
    
    #region EfCoreQuery
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}\\EfCoreQuery");
    var template = Template.Parse(text);
    var result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Query.cs", result, false);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateDtoAsync()
{
    var actionName = "Dtos";
    var newOutputFilePath = $"{outputFilePath}\\Shareds\\{domainName}s";
    
    #region Dtos
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}");
    var template = Template.Parse(text);
    var result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}Dto.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}

async Task CreateControllerAsync()
{
    var actionName = "Controller";
    var newOutputFilePath = $"{outputFilePath}\\HttpApis\\{domainName}s";

    #region Dtos
    var text = await File.ReadAllTextAsync($"{templateFilePath}\\{actionName}");
    var template = Template.Parse(text);
    var result = template.Render(inputData);

    FileHelper.FileCreate(newOutputFilePath, $"{domainName}{actionName}.cs", result);
    
    Console.WriteLine($"Create {actionName} File Ok!");
    #endregion
}