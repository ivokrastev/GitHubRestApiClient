using Octokit;
using System.Collections.Immutable;

namespace GitHubRestApiClient
{
    public class GitHubApiClient
    {
        private string GitHubIdentity { get; }
        private string AccessToken { get; }

        public GitHubClient ApiClient { get; }

        public GitHubApiClient(string usernameOrOrganization, string accessToken)
        {
            AccessToken = accessToken;
            GitHubIdentity = usernameOrOrganization;
            Credentials credentials = new Credentials(AccessToken);
            ApiClient = new GitHubClient(new ProductHeaderValue(GitHubIdentity)) { Credentials = credentials };
        }

        public async Task<IReadOnlyCollection<Workflow>> ListRepositoryWorkflows(string repositoryName)
        {
            WorkflowsResponse response = await ApiClient.Actions.Workflows.List(GitHubIdentity, repositoryName);
            return response.Workflows
                .Select(w => new Workflow(w))
                .ToImmutableArray();
        }

        public async Task<IReadOnlyCollection<WorkflowRun>> ListWorkflowRuns(string repositoryName, long workflowId)
        {
            WorkflowRunsResponse response = await ApiClient.Actions.Workflows.Runs.ListByWorkflow(GitHubIdentity, repositoryName, workflowId);
            return response.WorkflowRuns
                .Select(r => new WorkflowRun(r))
                .ToImmutableArray();
        }
    }
}
