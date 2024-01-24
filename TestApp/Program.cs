using GitHubRestApiClient;

if (args.Length != 3)
    throw new ArgumentException($"Exatly 3 argumets are required: [GitHubIdentity] [RepositoryName] [AccessToken]");

string repositoryName = args[1];
var client = new GitHubApiClient(args[0], args[2]);

var workflows = await client.ListRepositoryWorkflows("DevelopmentPractice");
Console.WriteLine("Searching for workflows...");
foreach (var workflow in workflows)
{
    Console.WriteLine("\t" + workflow.Name);
    Console.WriteLine("\t" + workflow.Id);
    Console.WriteLine("\t" + workflow.Path);
    Console.WriteLine("\t" + workflow.UpdatedAt);
    Console.WriteLine("\t=================");
}
Console.WriteLine($"Total workflows found: {workflows.Count}");

Console.WriteLine("Searching for workflow runs...");
var workflowRuns = await client.ListWorkflowRuns(repositoryName, workflows.First().Id);
foreach (var run in workflowRuns)
{
    Console.WriteLine("\t" + run.Id);
    Console.WriteLine("\t" + run.Name);
    Console.WriteLine("\t" + run.TriggeringEvent);
    Console.WriteLine("\t" + run.DisplayTitle);
    Console.WriteLine("\t" + run.Status);
    Console.WriteLine("\t" + run.RunStartedAt);
    Console.WriteLine("\t=================");
}
Console.WriteLine($"Total workflows runs found: {workflowRuns.Count}");