// https://github.com/scriban/scriban

using Scriban;

var text = await File.ReadAllTextAsync("./TemplateFiles/Domain");

var template = Template.Parse(text);
var result = template.Render(new { projectname = "World", domainname = "Dedsi" });
Console.WriteLine(result);