namespace GitHubRestApiClient
{
    public class Artifact
    {
        public long Id { get; }
        public string Name { get; }
        public string DownloadUrl { get; }

        public Artifact(global::Artifact a)
        {
            Name = a.Name;
            Id = a.Id;
            DownloadUrl = a.ArchiveDownloadUrl;
        }
    }
}
