using GitHubRestApiClient;

var client = new GitHubApiClient();

Console.WriteLine("Searching for workflows...");
var workflows = await client.ListRepositoryWorkflows();
foreach (var workflow in workflows)
{
    Console.WriteLine("\t" + workflow.Name);
    Console.WriteLine("\t" + workflow.Id);
    Console.WriteLine("\t" + workflow.Path);
    Console.WriteLine("\t" + workflow.UpdatedAt);
    Console.WriteLine("\t=================");
}
Console.WriteLine($"Total workflows found: {workflows.Count}");
var w = workflows.Where(w => w.Name.StartsWith("Build and Test")).Single();
Console.WriteLine($"Searching for {w.Name} workflow runs...");
var workflowRuns = await client.ListWorkflowRuns(w.Id);
foreach (var run in workflowRuns)
{
    if (run.Status != WorkflowRunStatus.Completed) continue;
    Console.WriteLine("\t" + run.Conclusion);
    Console.WriteLine("\t" + run.Id);
    Console.WriteLine("\t" + run.Name);
    Console.WriteLine("\t" + run.TriggeringEvent);
    Console.WriteLine("\t" + run.DisplayTitle);
    Console.WriteLine("\t" + run.Status);
    Console.WriteLine("\t" + run.RunStartedAt);
    Console.WriteLine("\t" + run.ArtifactsUrl);
    Console.WriteLine("\t=================");
    var r = await client.ListWorkflowRunArtifacts(run.Id);
    foreach (var artifact in r)
    {
        string dowloadsDirectory = "Downloads";
        string filename = $"{artifact.Name}.{artifact.Id}.zip";
        Stream streamToReadFrom = await client.DownloadArtifact(artifact);
        GitHubApiClient.SaveStreamAsFile(dowloadsDirectory, streamToReadFrom, filename);

        Console.WriteLine($"\t\t{filename} exists: {File.Exists($@"{dowloadsDirectory}/{filename}")}");
        Console.WriteLine($"\t\tSize {File.ReadAllBytes($@"{dowloadsDirectory}/{filename}").Length}");
    }
}
Console.WriteLine($"Total workflows runs found: {workflowRuns.Count}");