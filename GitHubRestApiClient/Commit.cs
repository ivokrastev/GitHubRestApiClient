namespace GitHubRestApiClient
{
    public class Commit
    {
        public string Sha { get; }
        public string AuthorEmail { get; }
        public string Label { get; }
        public string Message { get; }

        public Commit(Octokit.Commit commit) 
        { 
            Sha = commit.Sha;
            AuthorEmail = commit.Author.Email;
            Label = commit.Label;
            Message = commit.Message;
        }
    }
}