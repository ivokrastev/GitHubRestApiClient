using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubRestApiClient
{
    public enum WorkflowRunStatus
    {
        Requested,
        In_Progress,
        Completed,
        Queued,
        Waiting,
        Pending
    }
}
