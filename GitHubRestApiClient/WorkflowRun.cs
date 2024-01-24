using System.Collections.Immutable;

namespace GitHubRestApiClient
{
    public class WorkflowRun
    {
        public long Id { get; }
        public long RunNumber { get; }
        public string ArtifactsUrl { get; }
        public string DisplayTitle { get; }
        public string TriggeringEvent { get; }
        public Commit HeadCommit { get; }
        public string Name { get; }
        public IReadOnlyList<PullRequest> PullRequests { get; }
        public DateTimeOffset RunStartedAt { get; }
        public WorkflowRunStatus Status { get; }

        internal WorkflowRun(Octokit.WorkflowRun workflowRun)
        {
            Name = workflowRun.Name;
            Id = workflowRun.Id;
            RunNumber = workflowRun.RunNumber;
            DisplayTitle = workflowRun.DisplayTitle;
            Status = (WorkflowRunStatus) Enum.Parse(typeof(WorkflowRunStatus), workflowRun.Status.StringValue, ignoreCase: true);
            RunStartedAt = workflowRun.RunStartedAt;
            TriggeringEvent = workflowRun.Event;
            ArtifactsUrl = workflowRun.ArtifactsUrl;
            HeadCommit = new Commit(workflowRun.HeadCommit);
            PullRequests = workflowRun.PullRequests.Select(pr => new PullRequest(pr)).ToImmutableArray();
        }
    }
}
