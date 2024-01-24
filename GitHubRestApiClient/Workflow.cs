namespace GitHubRestApiClient
{
    public class Workflow
    {
        public string Name { get; }
        public long Id { get; }
        public string Path { get; }
        public DateTimeOffset UpdatedAt { get; }

        internal Workflow(Octokit.Workflow workflow)
        {
            Name = workflow.Name;
            Id = workflow.Id;
            Path = workflow.Path;
            UpdatedAt = workflow.UpdatedAt;
        }
    }
}
