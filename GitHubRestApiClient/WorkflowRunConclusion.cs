using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubRestApiClient
{
    public enum WorkflowRunConclusion
    {
        Success,
        Failure,
        Neutral,
        Cancelled,
        Timed_Out,
        Action_Required,
        Stale,
        Startup_Failure,
        Skipped,
        Unknown,
    }
}
