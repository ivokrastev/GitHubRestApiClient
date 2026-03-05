using GitHubRestApiClient;

var client = new GitHubApiClient();

Console.WriteLine("Searching for workflows...");
var workflows = UsageHelpers.HelperMethods.SearchForWorkflows(client).Result;
var w = workflows.Where(w => w.Name.StartsWith("Build and Test")).Single();
Console.WriteLine($"Searching for {w.Name} workflow runs...");
UsageHelpers.HelperMethods.DownloadWorkflowRunsArtifacts(client, w.Id).Wait();