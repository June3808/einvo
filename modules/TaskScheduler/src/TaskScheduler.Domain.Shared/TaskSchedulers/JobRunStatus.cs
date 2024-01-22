using System;
using System.Collections.Generic;
using System.Text;

namespace TaskScheduler
{
    public enum JobRunStatus
    {
        Waiting = 0,
        Executing = 1,
        Error = 2,
        Finished = 3,
        Vetoed = 4
    }
}
