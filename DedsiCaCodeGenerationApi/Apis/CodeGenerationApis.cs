using Scriban;
using System.Text.RegularExpressions;

namespace DedsiCaCodeGenerationApi.Apis;

public static class CodeGenerationApis
{
    public static RouteGroupBuilder MapCodeGenerationApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/code-generation");

        api.MapPost("/template/generate", GenerateCodeAsync).WithDescription("按照模板生成代码");

        return api;
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static async Task<CodeGenerationResultDto> GenerateCodeAsync(CodeGenerationInputDto input)
    {
        var templateFilePath = "./TemplateFiles";

        var humpDomainName = Regex.Replace(input.DomainName, "^[A-Z]", match => match.Value.ToLower()); ;
        var inputData = new { projectname = input.ProjectName, domainname = input.DomainName, humpdomainname = humpDomainName };

        // Domain
        var text = await File.ReadAllTextAsync($"{templateFilePath}\\Domain");
        var Domain = Template.Parse(text).Render();

        // CreateCommandHandler
        text = await File.ReadAllTextAsync($"{templateFilePath}\\CommandHandlers\\CreateCommandHandler");
        var CreateCommandHandler = Template.Parse(text).Render(inputData);

        // UpdateCommandHandler
        text = await File.ReadAllTextAsync($"{templateFilePath}\\CommandHandlers\\UpdateCommandHandler");
        var UpdateCommandHandler = Template.Parse(text).Render(inputData);

        // DeleteCommandHandler
        text = await File.ReadAllTextAsync($"{templateFilePath}\\CommandHandlers\\DeleteCommandHandler");
        var DeleteCommandHandler = Template.Parse(text).Render(inputData);

        // EfCoreRepository
        text = await File.ReadAllTextAsync($"{templateFilePath}\\Repositories\\EfCoreRepository");
        var EfCoreRepository = Template.Parse(text).Render(inputData);

        // EfCoreQuery
        text = await File.ReadAllTextAsync($"{templateFilePath}\\Queries\\EfCoreQuery");
        var EfCoreQuery = Template.Parse(text).Render(inputData);

        text = await File.ReadAllTextAsync($"{templateFilePath}\\Dtos");
        var Dtos = Template.Parse(text).Render(inputData);

        text = await File.ReadAllTextAsync($"{templateFilePath}\\Controller");
        var Controller = Template.Parse(text).Render(inputData);

        return new CodeGenerationResultDto(
            Domain,
            CreateCommandHandler,
            UpdateCommandHandler,
            DeleteCommandHandler,
            EfCoreRepository,
            EfCoreQuery,
            Dtos,
            Controller
        );
    }
}

/// <summary>
/// 代码生成入参
/// </summary>
/// <param name="ProjectName">项目名称</param>
/// <param name="DomainName">领域名称</param>
public record CodeGenerationInputDto(string ProjectName, string DomainName);
public record CodeGenerationResultDto(
    string Domain,
    string CreateCommandHandler,
    string UpdateCommandHandler,
    string DeleteCommandHandler,
    string EfCoreRepository,
    string EfCoreQuery,
    string Dtos,
    string Controller
);
