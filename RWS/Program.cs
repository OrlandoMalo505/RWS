using RWS;
using System;


Repo repo = new();
Console.WriteLine("Enter the domain in format {https://Domain},for example: 'https://upt.al/' :");
var domain = Console.ReadLine();
var res = repo.ExtractHref(domain);
var links = new List<string>();
Console.WriteLine("***************************");
Console.WriteLine("These are all the pages of the website:");
foreach (var i in res)
{

    if (domain.EndsWith("/"))
    {
        Console.WriteLine(domain.Remove(domain.Length - 1) + i);
        links.Add(domain.Remove(domain.Length - 1) + i);
    }
    else
    {
        Console.WriteLine(domain + i);
        links.Add(domain + i);
    }
}
Console.WriteLine("***************************");
Console.WriteLine("Do want to save these URLs in a file? Press yes or no:");
var answer = Console.ReadLine();
if (answer.ToUpper() == "YES")
{
    Console.WriteLine("The file will be saved at bin\\Debug\\net6.0");

    await repo.SaveURLS(links);
    Console.WriteLine("File is saved.");
}
Console.WriteLine("***************************");
Console.WriteLine("Do you want to get the html content for any of the pages? Press yes or no.");
var ans = Console.ReadLine();
if (ans.ToUpper() == "YES")
{
    Console.WriteLine("Enter the page URL:");
    var url = Console.ReadLine();
    var content = repo.CallUrl(url).Result;
    Console.WriteLine("The content will be saved at: bin\\Debug\\net6.0");
    await repo.WriteToFile(content, url);
}

Console.WriteLine("***************************");
Console.WriteLine("Do you want to get the paragraph content for any of the pages? Press yes or no.");
var a = Console.ReadLine();
if (a.ToUpper() == "YES")
{
    Console.WriteLine("Enter the page URL:");
    var url = Console.ReadLine();
    var content = repo.ExtractParagraphs(url);
    Console.WriteLine("The content will be saved at: bin\\Debug\\net6.0");
    await repo.SaveElements(content, url, "paragraph");
}

Console.WriteLine("***************************");
Console.WriteLine("Do you want to get the li elements for any of the pages? Press yes or no.");
var answw = Console.ReadLine();
if (answw.ToUpper() == "YES")
{
    Console.WriteLine("Enter the page URL:");
    var url = Console.ReadLine();
    var content = repo.ExtractLiElements(url);
    Console.WriteLine("The content will be saved at: bin\\Debug\\net6.0");
    await repo.SaveElements(content, url, "li");
}

Console.WriteLine("***************************");
Console.WriteLine("Do you want to get the html content for all the pages of the website? Press yes or no.");
var answ = Console.ReadLine();
if (answ.ToUpper() == "YES")
{
    Console.WriteLine("The content will be saved at: bin\\Debug\\net6.0 ");
    Console.WriteLine("Saving, please wait...");
    foreach (var url in links)
    {
        var content = repo.CallUrl(url).Result;
        await repo.WriteToFile(content, url);
    }
}


