using Octokit;
using System.Collections.Immutable;

namespace GitHubRestApiClient
{
    /// <summary>
    /// GitHub REST API client. It should be used from within GitHub Workflow environment.
    public class GitHubApiClient
    {
        private string GitHubIdentity { get; }
        private string AccessToken { get; }

        /// <summary>
        /// When running in GitHub workflow, we're using a token with access permissions only for the
        /// repository where the workflow is defined. When using ${{secrets.GITHUB_TOKEN}},
        /// the only repo that can be accessed is the one where the workflow yml file exists.
        /// </summary>
        private string RepoName { get; }

        public GitHubClient ApiClient { get; }

        /// <summary>
        /// GitHub REST API client. It should be used from within GitHub Workflow environment. The workflow MUST define
        /// a variable AUTH_TOKEN at the top level of the YAML file, so it is accessible throughout the entire workflow.
        /// env:
        ///     AUTH_TOKEN: ${{secrets.GITHUB_TOKEN}}.
        /// </summary>
        public GitHubApiClient()
        {
#if DEBUG
            AccessToken = "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";
            GitHubIdentity = "ivokrastev";
            RepoName = "GitHubRestApiClient";
            if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(GitHubIdentity) || string.IsNullOrEmpty(RepoName))
                throw new ApplicationException("Local debugging requires to specify AccessToken, GitHubIdentity and RepoName.");
#else
            // In GitHub this is built and run in Release mode. The properties AccessToken, GitHubIdentity and RepoName will be 
            // initialized from workflow-level environment variables. For github workflows, GITHUB_REPOSITORY is a default environment
            // variable, so there is no need to declare and set it. What MUST be declared in the workflow is an environment 
            // varible - AUTH_TOKEN: ${{secrets.GITHUB_TOKEN}}. It must be defined at the top level of the YAML file, so it is
            // accessible throughout the entire workflow.
            AccessToken = Environment.GetEnvironmentVariable("AUTH_TOKEN");
            string[] repoData = Environment.GetEnvironmentVariable("GITHUB_REPOSITORY").Split('/');
            GitHubIdentity = repoData.First();
            RepoName = repoData.Last();
#endif
            Credentials credentials = new Credentials(AccessToken);
            ApiClient = new GitHubClient(new ProductHeaderValue(GitHubIdentity)) { Credentials = credentials };
        }

        public async Task<IReadOnlyCollection<Workflow>> ListRepositoryWorkflows()
        {
            WorkflowsResponse response = await ApiClient.Actions.Workflows.List(GitHubIdentity, RepoName);
            return response.Workflows
                .Select(w => new Workflow(w))
                .ToArray();
        }

        public async Task<IReadOnlyCollection<WorkflowRun>> ListWorkflowRuns(long workflowId)
        {
            WorkflowRunsResponse response = await ApiClient.Actions.Workflows.Runs.ListByWorkflow(GitHubIdentity, RepoName, workflowId);
            return response.WorkflowRuns
                .Select(r => new WorkflowRun(r))
                .ToArray();
        }

        public async Task<IReadOnlyCollection<Artifact>> ListWorkflowRunArtifacts(long runId)
        {
            //ApiClient.Actions.Artifacts.DownloadArtifact
            ListArtifactsResponse response = await ApiClient.Actions.Artifacts.ListWorkflowArtifacts(GitHubIdentity, RepoName, runId);
            return response.Artifacts
                .Where(a => a.WorkflowRun.Id == runId)
                .Select(a => new Artifact(a))
                .ToArray();
        }

        public async Task<Stream> DownloadArtifact(long artifactId)
        {
            return await ApiClient.Actions.Artifacts.DownloadArtifact(GitHubIdentity, RepoName, artifactId, "zip");
        }
    }
}
