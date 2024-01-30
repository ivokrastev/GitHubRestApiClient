namespace GitHubRestApiClient
{
    public class Artifact
    {
        public long Id { get; }
        public string Name { get; }

        public Artifact(global::Artifact a)
        {
            Name = a.Name;
            Id = a.Id;
        }
    }
}
