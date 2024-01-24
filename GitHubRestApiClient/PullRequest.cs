namespace GitHubRestApiClient
{
    public class PullRequest
    {
        public long Id { get; }
        public string Title { get; }

        public PullRequest(Octokit.PullRequest pullRequest)
        {
            Id = pullRequest.Id;
            Title = pullRequest.Title;
        }
    }
}