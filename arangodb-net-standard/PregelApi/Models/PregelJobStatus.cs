using Newtonsoft.Json;
using System;
using System.Data.Common;
using System.Text;

namespace ArangoDBNetStandard.PregelApi.Models
{
    public class PregelJobStatus
    {
        /// <summary>
        /// ID of the Pregel job.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// An algorithm used by the job.
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// The date and time when the job was created.
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// The date and time when the job results expire. 
        /// </summary>
        /// <remarks>
        /// The expiration date is only meaningful for jobs 
        /// that were completed, canceled or resulted in an error. 
        /// Such jobs are cleaned up by the garbage collection
        /// when they reach their expiration date/time.
        /// </remarks>
        public string Expires { get; set; }

        /// <summary>
        /// The TTL (time to live) value for the job results,
        /// specified in seconds. The TTL is used to calculate
        /// the expiration date for the job’s results.
        /// </summary>
        public int TTL { get; set; }

        /// <summary>
        /// State of the job execution.
        /// </summary>
        /// <remarks>
        /// The following values can be returned:
        /// "loading": Job is loading. Introduced in v3.10.
        /// "running": Algorithm is executing normally.
        /// "storing": The algorithm finished, but the results 
        /// are still being written back into the collections. 
        /// Occurs only if the store parameter is set to true.
        /// "done": The execution is done.In version 3.7.1 and 
        /// later, this means that storing is also done. 
        /// In earlier versions, the results may not be written
        /// back into the collections yet.This event is announced
        /// in the server log (requires at least info log level 
        /// for the pregel log topic).
        /// "canceled": The execution was permanently canceled,
        /// either by the user or by an error.
        /// "fatal error": The execution has failed and cannot recover.
        /// "in error" (currently unused): The execution is in an 
        /// error state.This can be caused by DB-Servers being not
        /// reachable or being non responsive. The execution might
        /// recover later, or switch to "canceled" if it was not 
        /// able to recover successfully.
        /// "recovering" (currently unused): The execution is actively 
        /// recovering, will switch back to running if the recovery
        /// was successful.
        /// </remarks>
        public string State { get; set; }

        /// <summary>
        /// The number of global supersteps executed.
        /// </summary>
        public int GSS { get; set; }

        /// <summary>
        /// Total runtime of the execution up to now
        /// (if the execution is still ongoing).
        /// </summary>
        public decimal TotalRuntime { get; set; }

        /// <summary>
        /// Startup runtime of the execution. The startup
        /// time includes the data loading time and can be 
        /// substantial. The startup time will be reported
        /// as 0 if the startup is still ongoing.
        /// </summary>
        public decimal StartupTime { get; set; }

        /// <summary>
        /// Algorithm execution time. The computation
        /// time will be reported as 0 if the 
        /// computation still ongoing.
        /// </summary>
        public decimal ComputationTime { get; set; }

        /// <summary>
        /// Time for storing the results if the job
        /// includes results storage. The storage time 
        /// be reported as 0 if storing the results is 
        /// still ongoing.
        /// </summary>
        public decimal StorageTime { get; set; }

        /// <summary>
        /// Database in which the job is executing.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SendCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReceivedCount { get; set; }

        /// <summary>
        /// Total number of vertices processed.
        /// </summary>
        public int VertexCount { get; set; }

        /// <summary>
        /// Total number of edges processed.
        /// </summary>
        public int EdgeCount { get; set; }

        /// <summary>
        /// Statistics about the Pregel execution.
        /// The value will only be populated once 
        /// the algorithm has finished.
        /// </summary>
        public object Reports { get; set; }

        /// <summary>
        /// Additional Pregel job run details
        /// </summary>
        public PregelJobDetail Detail { get; set; }
    }
}