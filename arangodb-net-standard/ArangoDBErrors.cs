using System;
using System.Collections.Generic;
using System.Text;

namespace ArangoDBNetStandard
{
    /// <summary>
    /// Enum of error numbers and their meanings
    /// </summary>
    public enum ArangoDBErrors
    {
        /// <summary>
        /// no error: No error has occurred.
        /// </summary>			
        ERROR_NO_ERROR = 0,
        /// <summary>
        /// failed: Will be raised when a general error occurred.
        /// </summary>			
        ERROR_FAILED = 1,
        /// <summary>
        /// system error: Will be raised when operating system error occurred.
        /// </summary>			
        ERROR_SYS_ERROR = 2,
        /// <summary>
        /// out of memory: Will be raised when there is a memory shortage.
        /// </summary>			
        ERROR_OUT_OF_MEMORY = 3,
        /// <summary>
        /// internal error: Will be raised when an internal error occurred.
        /// </summary>			
        ERROR_INTERNAL = 4,
        /// <summary>
        /// illegal number: Will be raised when an illegal representation of a number was given.
        /// </summary>			
        ERROR_ILLEGAL_NUMBER = 5,
        /// <summary>
        /// numeric overflow: Will be raised when a numeric overflow occurred.
        /// </summary>			
        ERROR_NUMERIC_OVERFLOW = 6,
        /// <summary>
        /// illegal option: Will be raised when an unknown option was supplied by the user.
        /// </summary>			
        ERROR_ILLEGAL_OPTION = 7,
        /// <summary>
        /// dead process identifier: Will be raised when a PID without a living process was found.
        /// </summary>			
        ERROR_DEAD_PID = 8,
        /// <summary>
        /// not implemented: Will be raised when hitting an unimplemented feature.
        /// </summary>			
        ERROR_NOT_IMPLEMENTED = 9,
        /// <summary>
        /// bad parameter: Will be raised when the parameter does not fulfill the requirements.
        /// </summary>			
        ERROR_BAD_PARAMETER = 10,
        /// <summary>
        /// forbidden: Will be raised when you are missing permission for the operation.
        /// </summary>			
        ERROR_FORBIDDEN = 11,
        /// <summary>
        /// out of memory in mmap: Will be raised when there is a memory shortage.
        /// </summary>			
        ERROR_OUT_OF_MEMORY_MMAP = 12,
        /// <summary>
        /// csv is corrupt: Will be raised when encountering a corrupt csv line.
        /// </summary>			
        ERROR_CORRUPTED_CSV = 13,
        /// <summary>
        /// file not found: Will be raised when a file is not found.
        /// </summary>			
        ERROR_FILE_NOT_FOUND = 14,
        /// <summary>
        /// cannot write file: Will be raised when a file cannot be written.
        /// </summary>			
        ERROR_CANNOT_WRITE_FILE = 15,
        /// <summary>
        /// cannot overwrite file: Will be raised when an attempt is made to overwrite an existing file.
        /// </summary>			
        ERROR_CANNOT_OVERWRITE_FILE = 16,
        /// <summary>
        /// type error: Will be raised when a type error is encountered.
        /// </summary>			
        ERROR_TYPE_ERROR = 17,
        /// <summary>
        /// lock timeout: Will be raised when there's a timeout waiting for a lock.
        /// </summary>			
        ERROR_LOCK_TIMEOUT = 18,
        /// <summary>
        /// cannot create directory: Will be raised when an attempt to create a directory fails.
        /// </summary>			
        ERROR_CANNOT_CREATE_DIRECTORY = 19,
        /// <summary>
        /// cannot create temporary file: Will be raised when an attempt to create a temporary file fails.
        /// </summary>			
        ERROR_CANNOT_CREATE_TEMP_FILE = 20,
        /// <summary>
        /// canceled request: Will be raised when a request is canceled by the user.
        /// </summary>			
        ERROR_REQUEST_CANCELED = 21,
        /// <summary>
        /// intentional debug error: Will be raised intentionally during debugging.
        /// </summary>			
        ERROR_DEBUG = 22,
        /// <summary>
        /// IP address is invalid: Will be raised when the structure of an IP address is invalid.
        /// </summary>			
        ERROR_IP_ADDRESS_INVALID = 25,
        /// <summary>
        /// file exists: Will be raised when a file already exists.
        /// </summary>			
        ERROR_FILE_EXISTS = 27,
        /// <summary>
        /// locked: Will be raised when a resource or an operation is locked.
        /// </summary>			
        ERROR_LOCKED = 28,
        /// <summary>
        /// deadlock detected: Will be raised when a deadlock is detected when accessing collections.
        /// </summary>			
        ERROR_DEADLOCK = 29,
        /// <summary>
        /// shutdown in progress: Will be raised when a call cannot succeed because a server shutdown is already in progress.
        /// </summary>			
        ERROR_SHUTTING_DOWN = 30,
        /// <summary>
        /// only enterprise version: Will be raised when an Enterprise Edition feature is requested from the Community Edition.
        /// </summary>			
        ERROR_ONLY_ENTERPRISE = 31,
        /// <summary>
        /// resource limit exceeded: Will be raised when the resources used by an operation exceed the configured maximum value.
        /// </summary>			
        ERROR_RESOURCE_LIMIT = 32,
        /// <summary>
        /// icu error: %s: will be raised if icu operations failed
        /// </summary>			
        ERROR_ARANGO_ICU_ERROR = 33,
        /// <summary>
        /// cannot read file: Will be raised when a file cannot be read.
        /// </summary>			
        ERROR_CANNOT_READ_FILE = 34,
        /// <summary>
        /// incompatible server version: Will be raised when a server is running an incompatible version of ArangoDB.
        /// </summary>			
        ERROR_INCOMPATIBLE_VERSION = 35,
        /// <summary>
        /// disabled: Will be raised when a requested resource is not enabled.
        /// </summary>			
        ERROR_DISABLED = 36,
        /// <summary>
        /// malformed json: Will be raised when a JSON string could not be parsed.
        /// </summary>			
        ERROR_MALFORMED_JSON = 37,
        /// <summary>
        /// startup ongoing: Will be raised when a call cannot succeed because the server startup phase is still in progress.
        /// </summary>			
        ERROR_STARTING_UP = 38,
        /// <summary>
        /// bad parameter: Will be raised when the HTTP request does not fulfill the requirements.
        /// </summary>			
        ERROR_HTTP_BAD_PARAMETER = 400,
        /// <summary>
        /// unauthorized: Will be raised when authorization is required but the user is not authorized.
        /// </summary>			
        ERROR_HTTP_UNAUTHORIZED = 401,
        /// <summary>
        /// forbidden: Will be raised when the operation is forbidden.
        /// </summary>			
        ERROR_HTTP_FORBIDDEN = 403,
        /// <summary>
        /// not found: Will be raised when an URI is unknown.
        /// </summary>			
        ERROR_HTTP_NOT_FOUND = 404,
        /// <summary>
        /// method not supported: Will be raised when an unsupported HTTP method is used for an operation.
        /// </summary>			
        ERROR_HTTP_METHOD_NOT_ALLOWED = 405,
        /// <summary>
        /// request not acceptable: Will be raised when an unsupported HTTP content type is used for an operation, or if a request is not acceptable for a leader or follower.
        /// </summary>			
        ERROR_HTTP_NOT_ACCEPTABLE = 406,
        /// <summary>
        /// request timeout: Will be raised when a timeout occured.
        /// </summary>			
        ERROR_HTTP_REQUEST_TIMEOUT = 408,
        /// <summary>
        /// conflict: Will be raised when a conflict occurs in an HTTP operation.
        /// </summary>			
        ERROR_HTTP_CONFLICT = 409,
        /// <summary>
        /// content permanently deleted: Will be raised when the requested content has been permanently deleted.
        /// </summary>			
        ERROR_HTTP_GONE = 410,
        /// <summary>
        /// precondition failed: Will be raised when a precondition for an HTTP request is not met.
        /// </summary>			
        ERROR_HTTP_PRECONDITION_FAILED = 412,
        /// <summary>
        /// internal server error: Will be raised when an internal server is encountered.
        /// </summary>			
        ERROR_HTTP_SERVER_ERROR = 500,
        /// <summary>
        /// not implemented: Will be raised when an API is called this is not implemented in general, or not implemented for the current setup.
        /// </summary>			
        ERROR_HTTP_NOT_IMPLEMENTED = 501,
        /// <summary>
        /// service unavailable: Will be raised when a service is temporarily unavailable.
        /// </summary>			
        ERROR_HTTP_SERVICE_UNAVAILABLE = 503,
        /// <summary>
        /// gateway timeout: Will be raised when a service contacted by ArangoDB does not respond in a timely manner.
        /// </summary>			
        ERROR_HTTP_GATEWAY_TIMEOUT = 504,
        /// <summary>
        /// invalid JSON object: Will be raised when a string representation of a JSON object is corrupt.
        /// </summary>			
        ERROR_HTTP_CORRUPTED_JSON = 600,
        /// <summary>
        /// superfluous URL suffices: Will be raised when the URL contains superfluous suffices.
        /// </summary>			
        ERROR_HTTP_SUPERFLUOUS_SUFFICES = 601,
        /// <summary>
        /// illegal state: Internal error that will be raised when the datafile is not in the required state.
        /// </summary>			
        ERROR_ARANGO_ILLEGAL_STATE = 1000,
        /// <summary>
        /// read only: Internal error that will be raised when trying to write to a read-only datafile or collection.
        /// </summary>			
        ERROR_ARANGO_READ_ONLY = 1004,
        /// <summary>
        /// duplicate identifier: Internal error that will be raised when a identifier duplicate is detected.
        /// </summary>			
        ERROR_ARANGO_DUPLICATE_IDENTIFIER = 1005,
        /// <summary>
        /// corrupted datafile: Will be raised when a corruption is detected in a datafile.
        /// </summary>			
        ERROR_ARANGO_CORRUPTED_DATAFILE = 1100,
        /// <summary>
        /// illegal or unreadable parameter file: Will be raised if a parameter file is corrupted or cannot be read.
        /// </summary>			
        ERROR_ARANGO_ILLEGAL_PARAMETER_FILE = 1101,
        /// <summary>
        /// corrupted collection: Will be raised when a collection contains one or more corrupted data files.
        /// </summary>			
        ERROR_ARANGO_CORRUPTED_COLLECTION = 1102,
        /// <summary>
        /// filesystem full: Will be raised when the filesystem is full.
        /// </summary>			
        ERROR_ARANGO_FILESYSTEM_FULL = 1104,
        /// <summary>
        /// database directory is locked: Will be raised when the database directory is locked by a different process.
        /// </summary>			
        ERROR_ARANGO_DATADIR_LOCKED = 1107,
        /// <summary>
        /// conflict: Will be raised when updating or deleting a document and a conflict has been detected.
        /// </summary>			
        ERROR_ARANGO_CONFLICT = 1200,
        /// <summary>
        /// document not found: Will be raised when a document with a given identifier is unknown.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_NOT_FOUND = 1202,
        /// <summary>
        /// collection or view not found: Will be raised when a collection or View with the given identifier or name is unknown.
        /// </summary>			
        ERROR_ARANGO_DATA_SOURCE_NOT_FOUND = 1203,
        /// <summary>
        /// parameter 'collection' not found: Will be raised when the collection parameter is missing.
        /// </summary>			
        ERROR_ARANGO_COLLECTION_PARAMETER_MISSING = 1204,
        /// <summary>
        /// illegal document identifier: Will be raised when a document identifier is corrupt.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_HANDLE_BAD = 1205,
        /// <summary>
        /// duplicate name: Will be raised when a name duplicate is detected.
        /// </summary>			
        ERROR_ARANGO_DUPLICATE_NAME = 1207,
        /// <summary>
        /// illegal name: Will be raised when an illegal name is detected.
        /// </summary>			
        ERROR_ARANGO_ILLEGAL_NAME = 1208,
        /// <summary>
        /// no suitable index known: Will be raised when no suitable index for the query is known.
        /// </summary>			
        ERROR_ARANGO_NO_INDEX = 1209,
        /// <summary>
        /// unique constraint violated: Will be raised when there is a unique constraint violation.
        /// </summary>			
        ERROR_ARANGO_UNIQUE_CONSTRAINT_VIOLATED = 1210,
        /// <summary>
        /// index not found: Will be raised when an index with a given identifier is unknown.
        /// </summary>			
        ERROR_ARANGO_INDEX_NOT_FOUND = 1212,
        /// <summary>
        /// cross collection request not allowed: Will be raised when a cross-collection is requested.
        /// </summary>			
        ERROR_ARANGO_CROSS_COLLECTION_REQUEST = 1213,
        /// <summary>
        /// illegal index identifier: Will be raised when a index identifier is corrupt.
        /// </summary>			
        ERROR_ARANGO_INDEX_HANDLE_BAD = 1214,
        /// <summary>
        /// document too large: Will be raised when the document cannot fit into any datafile because of it is too large.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_TOO_LARGE = 1216,
        /// <summary>
        /// collection must be unloaded: Will be raised when a collection should be unloaded, but has a different status.
        /// </summary>			
        ERROR_ARANGO_COLLECTION_NOT_UNLOADED = 1217,
        /// <summary>
        /// collection type invalid: Will be raised when an invalid collection type is used in a request.
        /// </summary>			
        ERROR_ARANGO_COLLECTION_TYPE_INVALID = 1218,
        /// <summary>
        /// parsing attribute name definition failed: Will be raised when parsing an attribute name definition failed.
        /// </summary>			
        ERROR_ARANGO_ATTRIBUTE_PARSER_FAILED = 1220,
        /// <summary>
        /// illegal document key: Will be raised when a document key is corrupt.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_KEY_BAD = 1221,
        /// <summary>
        /// unexpected document key: Will be raised when a user-defined document key is supplied for collections with auto key generation.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_KEY_UNEXPECTED = 1222,
        /// <summary>
        /// server database directory not writable: Will be raised when the server's database directory is not writable for the current user.
        /// </summary>			
        ERROR_ARANGO_DATADIR_NOT_WRITABLE = 1224,
        /// <summary>
        /// out of keys: Will be raised when a key generator runs out of keys.
        /// </summary>			
        ERROR_ARANGO_OUT_OF_KEYS = 1225,
        /// <summary>
        /// missing document key: Will be raised when a document key is missing.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_KEY_MISSING = 1226,
        /// <summary>
        /// invalid document type: Will be raised when there is an attempt to create a document with an invalid type.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_TYPE_INVALID = 1227,
        /// <summary>
        /// database not found: Will be raised when a non-existing database is accessed.
        /// </summary>			
        ERROR_ARANGO_DATABASE_NOT_FOUND = 1228,
        /// <summary>
        /// database name invalid: Will be raised when an invalid database name is used.
        /// </summary>			
        ERROR_ARANGO_DATABASE_NAME_INVALID = 1229,
        /// <summary>
        /// operation only allowed in system database: Will be raised when an operation is requested in a database other than the system database.
        /// </summary>			
        ERROR_ARANGO_USE_SYSTEM_DATABASE = 1230,
        /// <summary>
        /// invalid key generator: Will be raised when an invalid key generator description is used.
        /// </summary>			
        ERROR_ARANGO_INVALID_KEY_GENERATOR = 1232,
        /// <summary>
        /// edge attribute missing or invalid: will be raised when the _from or _to values of an edge are undefined or contain an invalid value.
        /// </summary>			
        ERROR_ARANGO_INVALID_EDGE_ATTRIBUTE = 1233,
        /// <summary>
        /// index creation failed: Will be raised when an attempt to create an index has failed.
        /// </summary>			
        ERROR_ARANGO_INDEX_CREATION_FAILED = 1235,
        /// <summary>
        /// collection type mismatch: Will be raised when a collection has a different type from what has been expected.
        /// </summary>			
        ERROR_ARANGO_COLLECTION_TYPE_MISMATCH = 1237,
        /// <summary>
        /// collection not loaded: Will be raised when a collection is accessed that is not yet loaded.
        /// </summary>			
        ERROR_ARANGO_COLLECTION_NOT_LOADED = 1238,
        /// <summary>
        /// illegal document revision: Will be raised when a document revision is corrupt or is missing where needed.
        /// </summary>			
        ERROR_ARANGO_DOCUMENT_REV_BAD = 1239,
        /// <summary>
        /// incomplete read: Will be raised by the storage engine when a read cannot be completed.
        /// </summary>			
        ERROR_ARANGO_INCOMPLETE_READ = 1240,
        /// <summary>
        /// server database directory is empty: Will be raised when encountering an empty server database directory.
        /// </summary>			
        ERROR_ARANGO_EMPTY_DATADIR = 1301,
        /// <summary>
        /// operation should be tried again: Will be raised when an operation should be retried.
        /// </summary>			
        ERROR_ARANGO_TRY_AGAIN = 1302,
        /// <summary>
        /// engine is busy: Will be raised when storage engine is busy.
        /// </summary>			
        ERROR_ARANGO_BUSY = 1303,
        /// <summary>
        /// merge in progress: Will be raised when storage engine has a datafile merge in progress and cannot complete the operation.
        /// </summary>			
        ERROR_ARANGO_MERGE_IN_PROGRESS = 1304,
        /// <summary>
        /// storage engine I/O error: Will be raised when storage engine encounters an I/O error.
        /// </summary>			
        ERROR_ARANGO_IO_ERROR = 1305,
        /// <summary>
        /// no response: Will be raised when the replication applier does not receive any or an incomplete response from the leader.
        /// </summary>			
        ERROR_REPLICATION_NO_RESPONSE = 1400,
        /// <summary>
        /// invalid response: Will be raised when the replication applier receives an invalid response from the leader.
        /// </summary>			
        ERROR_REPLICATION_INVALID_RESPONSE = 1401,
        /// <summary>
        /// leader error: Will be raised when the replication applier receives a server error from the leader.
        /// </summary>			
        ERROR_REPLICATION_LEADER_ERROR = 1402,
        /// <summary>
        /// leader incompatible: Will be raised when the replication applier connects to a leader that has an incompatible version.
        /// </summary>			
        ERROR_REPLICATION_LEADER_INCOMPATIBLE = 1403,
        /// <summary>
        /// leader change: Will be raised when the replication applier connects to a different leader than before.
        /// </summary>			
        ERROR_REPLICATION_LEADER_CHANGE = 1404,
        /// <summary>
        /// loop detected: Will be raised when the replication applier is asked to connect to itself for replication.
        /// </summary>			
        ERROR_REPLICATION_LOOP = 1405,
        /// <summary>
        /// unexpected marker: Will be raised when an unexpected marker is found in the replication log stream.
        /// </summary>			
        ERROR_REPLICATION_UNEXPECTED_MARKER = 1406,
        /// <summary>
        /// invalid applier state: Will be raised when an invalid replication applier state file is found.
        /// </summary>			
        ERROR_REPLICATION_INVALID_APPLIER_STATE = 1407,
        /// <summary>
        /// invalid transaction: Will be raised when an unexpected transaction id is found.
        /// </summary>			
        ERROR_REPLICATION_UNEXPECTED_TRANSACTION = 1408,
        /// <summary>
        /// shard synchronization attempt timeout exceeded: Will be raised when the synchronization of a shard takes longer than the configured timeout.
        /// </summary>			
        ERROR_REPLICATION_SHARD_SYNC_ATTEMPT_TIMEOUT_EXCEEDED = 1409,
        /// <summary>
        /// invalid replication applier configuration: Will be raised when the configuration for the replication applier is invalid.
        /// </summary>			
        ERROR_REPLICATION_INVALID_APPLIER_CONFIGURATION = 1410,
        /// <summary>
        /// cannot perform operation while applier is running: Will be raised when there is an attempt to perform an operation while the replication applier is running.
        /// </summary>			
        ERROR_REPLICATION_RUNNING = 1411,
        /// <summary>
        /// replication stopped: Special error code used to indicate the replication applier was stopped by a user.
        /// </summary>			
        ERROR_REPLICATION_APPLIER_STOPPED = 1412,
        /// <summary>
        /// no start tick: Will be raised when the replication applier is started without a known start tick value.
        /// </summary>			
        ERROR_REPLICATION_NO_START_TICK = 1413,
        /// <summary>
        /// start tick not present: Will be raised when the replication applier fetches data using a start tick, but that start tick is not present on the logger server anymore.
        /// </summary>			
        ERROR_REPLICATION_START_TICK_NOT_PRESENT = 1414,
        /// <summary>
        /// wrong checksum: Will be raised when a new born follower submits a wrong checksum
        /// </summary>			
        ERROR_REPLICATION_WRONG_CHECKSUM = 1416,
        /// <summary>
        /// shard not empty: Will be raised when a shard is not empty and the follower tries a shortcut
        /// </summary>			
        ERROR_REPLICATION_SHARD_NONEMPTY = 1417,
        /// <summary>
        /// replicated log {} not found: Will be raised when a specific replicated log is not found
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_NOT_FOUND = 1418,
        /// <summary>
        /// not the log leader: Will be raised when a participant of a replicated log is ordered to do something only the leader can do
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_NOT_THE_LEADER = 1419,
        /// <summary>
        /// not a log follower: Will be raised when a participant of a replicated log is ordered to do something only a follower can do
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_NOT_A_FOLLOWER = 1420,
        /// <summary>
        /// follower rejected append entries request: Will be raised when a follower of a replicated log rejects an append entries request
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_APPEND_ENTRIES_REJECTED = 1421,
        /// <summary>
        /// a resigned leader instance rejected a request: Will be raised when a leader instance of a replicated log rejects a request because it just resigned. This can also happen if the term changes (due to a configuration change), even if the leader stays the same.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_LEADER_RESIGNED = 1422,
        /// <summary>
        /// a resigned follower instance rejected a request: Will be raised when a follower instance of a replicated log rejects a request because it just resigned. This can also happen if the term changes (due to a configuration change), even if the server stays a follower.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_FOLLOWER_RESIGNED = 1423,
        /// <summary>
        /// the replicated log of the participant is gone: Will be raised when a participant instance of a replicated log is no longer available.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_PARTICIPANT_GONE = 1424,
        /// <summary>
        /// an invalid term was given: Will be raised when a participant tries to change its term but found a invalid new term.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_INVALID_TERM = 1425,
        /// <summary>
        /// log participant unconfigured: Will be raised when a participant is currently unconfigured, i.e. neither a leader nor a follower.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_LOG_UNCONFIGURED = 1426,
        /// <summary>
        /// replicated state {id:} of type {type:} not found: Will be raised when a specific replicated state was not found.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_STATE_NOT_FOUND = 1427,
        /// <summary>
        /// replicated state {id:} of type {type:} is unavailable: Will be raised when a specific replicated state was accessed but is not (yet) available.
        /// </summary>			
        ERROR_REPLICATION_REPLICATED_STATE_NOT_AVAILABLE = 1428,
        /// <summary>
        /// not a follower: Will be raised when an operation is sent to a non-following server.
        /// </summary>			
        ERROR_CLUSTER_NOT_FOLLOWER = 1446,
        /// <summary>
        /// follower transaction intermediate commit already performed: Will be raised when a follower transaction has already performed an intermediate commit and must be rolled back.
        /// </summary>			
        ERROR_CLUSTER_FOLLOWER_TRANSACTION_COMMIT_PERFORMED = 1447,
        /// <summary>
        /// creating collection failed due to precondition: Will be raised when updating the plan on collection creation failed.
        /// </summary>			
        ERROR_CLUSTER_CREATE_COLLECTION_PRECONDITION_FAILED = 1448,
        /// <summary>
        /// got a request from an unknown server: Will be raised on some occasions when one server gets a request from another, which has not (yet?) been made known via the Agency.
        /// </summary>			
        ERROR_CLUSTER_SERVER_UNKNOWN = 1449,
        /// <summary>
        /// too many shards: Will be raised when the number of shards for a collection is higher than allowed.
        /// </summary>			
        ERROR_CLUSTER_TOO_MANY_SHARDS = 1450,
        /// <summary>
        /// could not create collection in plan: Will be raised when a Coordinator in a cluster cannot create an entry for a new collection in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_COLLECTION_IN_PLAN = 1454,
        /// <summary>
        /// could not create collection: Will be raised when a Coordinator in a cluster notices that some DB-Servers report problems when creating shards for a new collection.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_COLLECTION = 1456,
        /// <summary>
        /// timeout in cluster operation: Will be raised when a Coordinator in a cluster runs into a timeout for some cluster wide operation.
        /// </summary>			
        ERROR_CLUSTER_TIMEOUT = 1457,
        /// <summary>
        /// could not remove collection from plan: Will be raised when a Coordinator in a cluster cannot remove an entry for a collection in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_REMOVE_COLLECTION_IN_PLAN = 1458,
        /// <summary>
        /// could not create database in plan: Will be raised when a Coordinator in a cluster cannot create an entry for a new database in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_DATABASE_IN_PLAN = 1460,
        /// <summary>
        /// could not create database: Will be raised when a Coordinator in a cluster notices that some DB-Servers report problems when creating databases for a new cluster wide database.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_DATABASE = 1461,
        /// <summary>
        /// could not remove database from plan: Will be raised when a Coordinator in a cluster cannot remove an entry for a database in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_REMOVE_DATABASE_IN_PLAN = 1462,
        /// <summary>
        /// could not remove database from current: Will be raised when a Coordinator in a cluster cannot remove an entry for a database in the Current hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_REMOVE_DATABASE_IN_CURRENT = 1463,
        /// <summary>
        /// no responsible shard found: Will be raised when a Coordinator in a cluster cannot determine the shard that is responsible for a given document.
        /// </summary>			
        ERROR_CLUSTER_SHARD_GONE = 1464,
        /// <summary>
        /// cluster internal HTTP connection broken: Will be raised when a Coordinator in a cluster loses an HTTP connection to a DB-Server in the cluster whilst transferring data.
        /// </summary>			
        ERROR_CLUSTER_CONNECTION_LOST = 1465,
        /// <summary>
        /// must not specify _key for this collection: Will be raised when a Coordinator in a cluster finds that the _key attribute was specified in a sharded collection the uses not only _key as sharding attribute.
        /// </summary>			
        ERROR_CLUSTER_MUST_NOT_SPECIFY_KEY = 1466,
        /// <summary>
        /// got contradicting answers from different shards: Will be raised if a Coordinator in a cluster gets conflicting results from different shards, which should never happen.
        /// </summary>			
        ERROR_CLUSTER_GOT_CONTRADICTING_ANSWERS = 1467,
        /// <summary>
        /// not all sharding attributes given: Will be raised if a Coordinator tries to find out which shard is responsible for a partial document, but cannot do this because not all sharding attributes are specified.
        /// </summary>			
        ERROR_CLUSTER_NOT_ALL_SHARDING_ATTRIBUTES_GIVEN = 1468,
        /// <summary>
        /// must not change the value of a shard key attribute: Will be raised if there is an attempt to update the value of a shard attribute.
        /// </summary>			
        ERROR_CLUSTER_MUST_NOT_CHANGE_SHARDING_ATTRIBUTES = 1469,
        /// <summary>
        /// unsupported operation or parameter for clusters: Will be raised when there is an attempt to carry out an operation that is not supported in the context of a sharded collection.
        /// </summary>			
        ERROR_CLUSTER_UNSUPPORTED = 1470,
        /// <summary>
        /// this operation is only valid on a coordinator in a cluster: Will be raised if there is an attempt to run a Coordinator-only operation on a different type of node.
        /// </summary>			
        ERROR_CLUSTER_ONLY_ON_COORDINATOR = 1471,
        /// <summary>
        /// error reading Plan in agency: Will be raised if a Coordinator or DB-Server cannot read the Plan in the Agency.
        /// </summary>			
        ERROR_CLUSTER_READING_PLAN_AGENCY = 1472,
        /// <summary>
        /// could not truncate collection: Will be raised if a Coordinator cannot truncate all shards of a cluster collection.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_TRUNCATE_COLLECTION = 1473,
        /// <summary>
        /// error in cluster internal communication for AQL: Will be raised if the internal communication of the cluster for AQL produces an error.
        /// </summary>			
        ERROR_CLUSTER_AQL_COMMUNICATION = 1474,
        /// <summary>
        /// this operation is only valid on a DBserver in a cluster: Will be raised if there is an attempt to run a DB-Server-only operation on a different type of node.
        /// </summary>			
        ERROR_CLUSTER_ONLY_ON_DBSERVER = 1477,
        /// <summary>
        /// A cluster backend which was required for the operation could not be reached: Will be raised if a required DB-Server can't be reached.
        /// </summary>			
        ERROR_CLUSTER_BACKEND_UNAVAILABLE = 1478,
        /// <summary>
        /// collection/view is out of sync: Will be raised if a collection/view needed during query execution is out of sync. This currently can happen when using SatelliteCollections, Arangosearch links or inverted indexes.
        /// </summary>			
        ERROR_CLUSTER_AQL_COLLECTION_OUT_OF_SYNC = 1481,
        /// <summary>
        /// could not create index in plan: Will be raised when a Coordinator in a cluster cannot create an entry for a new index in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_INDEX_IN_PLAN = 1482,
        /// <summary>
        /// could not drop index in plan: Will be raised when a Coordinator in a cluster cannot remove an index from the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_DROP_INDEX_IN_PLAN = 1483,
        /// <summary>
        /// chain of distributeShardsLike references: Will be raised if one tries to create a collection with a distributeShardsLike attribute which points to another collection that also has one.
        /// </summary>			
        ERROR_CLUSTER_CHAIN_OF_DISTRIBUTESHARDSLIKE = 1484,
        /// <summary>
        /// must not drop collection while another has a distributeShardsLike attribute pointing to it: Will be raised if one tries to drop a collection to which another collection points with its distributeShardsLike attribute.
        /// </summary>			
        ERROR_CLUSTER_MUST_NOT_DROP_COLL_OTHER_DISTRIBUTESHARDSLIKE = 1485,
        /// <summary>
        /// must not have a distributeShardsLike attribute pointing to an unknown collection: Will be raised if one tries to create a collection which points to an unknown collection in its distributeShardsLike attribute.
        /// </summary>			
        ERROR_CLUSTER_UNKNOWN_DISTRIBUTESHARDSLIKE = 1486,
        /// <summary>
        /// the number of current dbservers is lower than the requested replicationFactor: Will be raised if one tries to create a collection with a replicationFactor greater than the available number of DB-Servers.
        /// </summary>			
        ERROR_CLUSTER_INSUFFICIENT_DBSERVERS = 1487,
        /// <summary>
        /// a follower could not be dropped in agency: Will be raised if a follower that ought to be dropped could not be dropped in the Agency (under Current).
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_DROP_FOLLOWER = 1488,
        /// <summary>
        /// a shard leader refuses to perform a replication operation: Will be raised if a replication operation is refused by a shard leader.
        /// </summary>			
        ERROR_CLUSTER_SHARD_LEADER_REFUSES_REPLICATION = 1489,
        /// <summary>
        /// a shard follower refuses to perform an operation: Will be raised if a replication operation is refused by a shard follower because it is coming from the wrong leader.
        /// </summary>			
        ERROR_CLUSTER_SHARD_FOLLOWER_REFUSES_OPERATION = 1490,
        /// <summary>
        /// a (former) shard leader refuses to perform an operation, because it has resigned in the meantime: Will be raised if a non-replication operation is refused by a former shard leader that has found out that it is no longer the leader.
        /// </summary>			
        ERROR_CLUSTER_SHARD_LEADER_RESIGNED = 1491,
        /// <summary>
        /// some agency operation failed: Will be raised if after various retries an Agency operation could not be performed successfully.
        /// </summary>			
        ERROR_CLUSTER_AGENCY_COMMUNICATION_FAILED = 1492,
        /// <summary>
        /// leadership challenge is ongoing: Will be raised when servers are currently competing for leadership, and the result is still unknown.
        /// </summary>			
        ERROR_CLUSTER_LEADERSHIP_CHALLENGE_ONGOING = 1495,
        /// <summary>
        /// not a leader: Will be raised when an operation is sent to a non-leading server.
        /// </summary>			
        ERROR_CLUSTER_NOT_LEADER = 1496,
        /// <summary>
        /// could not create view in plan: Will be raised when a Coordinator in a cluster cannot create an entry for a new View in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_CREATE_VIEW_IN_PLAN = 1497,
        /// <summary>
        /// view ID already exists: Will be raised when a Coordinator in a cluster tries to create a View and the View ID already exists.
        /// </summary>			
        ERROR_CLUSTER_VIEW_ID_EXISTS = 1498,
        /// <summary>
        /// could not drop collection in plan: Will be raised when a Coordinator in a cluster cannot drop a collection entry in the Plan hierarchy in the Agency.
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_DROP_COLLECTION = 1499,
        /// <summary>
        /// query killed: Will be raised when a running query is killed by an explicit admin command.
        /// </summary>			
        ERROR_QUERY_KILLED = 1500,
        /// <summary>
        /// %s: Will be raised when query is parsed and is found to be syntactically invalid.
        /// </summary>			
        ERROR_QUERY_PARSE = 1501,
        /// <summary>
        /// query is empty: Will be raised when an empty query is specified.
        /// </summary>			
        ERROR_QUERY_EMPTY = 1502,
        /// <summary>
        /// runtime error '%s': Will be raised when a runtime error is caused by the query.
        /// </summary>			
        ERROR_QUERY_SCRIPT = 1503,
        /// <summary>
        /// number out of range: Will be raised when a number is outside the expected range.
        /// </summary>			
        ERROR_QUERY_NUMBER_OUT_OF_RANGE = 1504,
        /// <summary>
        /// invalid geo coordinate value: Will be raised when a geo index coordinate is invalid or out of range.
        /// </summary>			
        ERROR_QUERY_INVALID_GEO_VALUE = 1505,
        /// <summary>
        /// variable name '%s' has an invalid format: Will be raised when an invalid variable name is used.
        /// </summary>			
        ERROR_QUERY_VARIABLE_NAME_INVALID = 1510,
        /// <summary>
        /// variable '%s' is assigned multiple times: Will be raised when a variable gets re-assigned in a query.
        /// </summary>			
        ERROR_QUERY_VARIABLE_REDECLARED = 1511,
        /// <summary>
        /// unknown variable '%s': Will be raised when an unknown variable is used or the variable is undefined the context it is used.
        /// </summary>			
        ERROR_QUERY_VARIABLE_NAME_UNKNOWN = 1512,
        /// <summary>
        /// unable to read-lock collection %s: Will be raised when a read lock on the collection cannot be acquired.
        /// </summary>			
        ERROR_QUERY_COLLECTION_LOCK_FAILED = 1521,
        /// <summary>
        /// too many collections/shards: Will be raised when the number of collections or shards in a query is beyond the allowed value.
        /// </summary>			
        ERROR_QUERY_TOO_MANY_COLLECTIONS = 1522,
        /// <summary>
        /// too much nesting or too many objects: Will be raised when a query contains expressions or other constructs with too many objects or that are too deeply nested.
        /// </summary>			
        ERROR_QUERY_TOO_MUCH_NESTING = 1524,
        /// <summary>
        /// unknown OPTIONS attribute used: Will be raised when an unknown attribute is used inside an OPTIONS clause.
        /// </summary>			
        ERROR_QUERY_INVALID_OPTIONS_ATTRIBUTE = 1539,
        /// <summary>
        /// usage of unknown function '%s()': Will be raised when an undefined function is called.
        /// </summary>			
        ERROR_QUERY_FUNCTION_NAME_UNKNOWN = 1540,
        /// <summary>
        /// invalid number of arguments for function '%s()', expected number of arguments: minimum: %d, maximum: %d: Will be raised when the number of arguments used in a function call does not match the expected number of arguments for the function.
        /// </summary>			
        ERROR_QUERY_FUNCTION_ARGUMENT_NUMBER_MISMATCH = 1541,
        /// <summary>
        /// invalid argument type in call to function '%s()': Will be raised when the type of an argument used in a function call does not match the expected argument type.
        /// </summary>			
        ERROR_QUERY_FUNCTION_ARGUMENT_TYPE_MISMATCH = 1542,
        /// <summary>
        /// invalid regex value: Will be raised when an invalid regex argument value is used in a call to a function that expects a regex.
        /// </summary>			
        ERROR_QUERY_INVALID_REGEX = 1543,
        /// <summary>
        /// invalid structure of bind parameters: Will be raised when the structure of bind parameters passed has an unexpected format.
        /// </summary>			
        ERROR_QUERY_BIND_PARAMETERS_INVALID = 1550,
        /// <summary>
        /// no value specified for declared bind parameter '%s': Will be raised when a bind parameter was declared in the query but the query is being executed with no value for that parameter.
        /// </summary>			
        ERROR_QUERY_BIND_PARAMETER_MISSING = 1551,
        /// <summary>
        /// bind parameter '%s' was not declared in the query: Will be raised when a value gets specified for an undeclared bind parameter.
        /// </summary>			
        ERROR_QUERY_BIND_PARAMETER_UNDECLARED = 1552,
        /// <summary>
        /// bind parameter '%s' has an invalid value or type: Will be raised when a bind parameter has an invalid value or type.
        /// </summary>			
        ERROR_QUERY_BIND_PARAMETER_TYPE = 1553,
        /// <summary>
        /// invalid arithmetic value: Will be raised when a non-numeric value is used in an arithmetic operation.
        /// </summary>			
        ERROR_QUERY_INVALID_ARITHMETIC_VALUE = 1561,
        /// <summary>
        /// division by zero: Will be raised when there is an attempt to divide by zero.
        /// </summary>			
        ERROR_QUERY_DIVISION_BY_ZERO = 1562,
        /// <summary>
        /// array expected: Will be raised when a non-array operand is used for an operation that expects an array argument operand.
        /// </summary>			
        ERROR_QUERY_ARRAY_EXPECTED = 1563,
        /// <summary>
        /// collection '%s' used as expression operand: Will be raised when a collection is used as an operand in an AQL expression.
        /// </summary>			
        ERROR_QUERY_COLLECTION_USED_IN_EXPRESSION = 1568,
        /// <summary>
        /// FAIL(%s) called: Will be raised when the function FAIL() is called from inside a query.
        /// </summary>			
        ERROR_QUERY_FAIL_CALLED = 1569,
        /// <summary>
        /// no suitable geo index found for geo restriction on '%s': Will be raised when a geo restriction was specified but no suitable geo index is found to resolve it.
        /// </summary>			
        ERROR_QUERY_GEO_INDEX_MISSING = 1570,
        /// <summary>
        /// no suitable fulltext index found for fulltext query on '%s': Will be raised when a fulltext query is performed on a collection without a suitable fulltext index.
        /// </summary>			
        ERROR_QUERY_FULLTEXT_INDEX_MISSING = 1571,
        /// <summary>
        /// invalid date value: Will be raised when a value cannot be converted to a date.
        /// </summary>			
        ERROR_QUERY_INVALID_DATE_VALUE = 1572,
        /// <summary>
        /// multi-modify query: Will be raised when an AQL query contains more than one data-modifying operation.
        /// </summary>			
        ERROR_QUERY_MULTI_MODIFY = 1573,
        /// <summary>
        /// invalid aggregate expression: Will be raised when an AQL query contains an invalid aggregate expression.
        /// </summary>			
        ERROR_QUERY_INVALID_AGGREGATE_EXPRESSION = 1574,
        /// <summary>
        /// query options must be readable at query compile time: Will be raised when an AQL query contains OPTIONS that cannot be figured out at query compile time.
        /// </summary>			
        ERROR_QUERY_COMPILE_TIME_OPTIONS = 1575,
        /// <summary>
        /// could not use forced index hint: Will be raised when forceIndexHint is specified, and the hint cannot be used to serve the query.
        /// </summary>			
        ERROR_QUERY_FORCED_INDEX_HINT_UNUSABLE = 1577,
        /// <summary>
        /// disallowed dynamic call to '%s': Will be raised when a dynamic function call is made to a function that cannot be called dynamically.
        /// </summary>			
        ERROR_QUERY_DISALLOWED_DYNAMIC_CALL = 1578,
        /// <summary>
        /// access after data-modification by %s: Will be raised when collection data are accessed after a data-modification operation.
        /// </summary>			
        ERROR_QUERY_ACCESS_AFTER_MODIFICATION = 1579,
        /// <summary>
        /// invalid user function name: Will be raised when a user function with an invalid name is registered.
        /// </summary>			
        ERROR_QUERY_FUNCTION_INVALID_NAME = 1580,
        /// <summary>
        /// invalid user function code: Will be raised when a user function is registered with invalid code.
        /// </summary>			
        ERROR_QUERY_FUNCTION_INVALID_CODE = 1581,
        /// <summary>
        /// user function '%s()' not found: Will be raised when a user function is accessed but not found.
        /// </summary>			
        ERROR_QUERY_FUNCTION_NOT_FOUND = 1582,
        /// <summary>
        /// user function runtime error: %s: Will be raised when a user function throws a runtime exception.
        /// </summary>			
        ERROR_QUERY_FUNCTION_RUNTIME_ERROR = 1583,
        /// <summary>
        /// bad execution plan JSON: Will be raised when an HTTP API for a query got an invalid JSON object.
        /// </summary>			
        ERROR_QUERY_BAD_JSON_PLAN = 1590,
        /// <summary>
        /// query ID not found: Will be raised when an Id of a query is not found by the HTTP API.
        /// </summary>			
        ERROR_QUERY_NOT_FOUND = 1591,
        /// <summary>
        /// %s: Will be raised if and user provided expression fails to evaluate to true
        /// </summary>			
        ERROR_QUERY_USER_ASSERT = 1593,
        /// <summary>
        /// %s: Will be raised if and user provided expression fails to evaluate to true
        /// </summary>			
        ERROR_QUERY_USER_WARN = 1594,
        /// <summary>
        /// window operation after data-modification: Will be raised when a window node is created after a data-modification operation.
        /// </summary>			
        ERROR_QUERY_WINDOW_AFTER_MODIFICATION = 1595,
        /// <summary>
        /// cursor not found: Will be raised when a cursor is requested via its id but a cursor with that id cannot be found.
        /// </summary>			
        ERROR_CURSOR_NOT_FOUND = 1600,
        /// <summary>
        /// cursor is busy: Will be raised when a cursor is requested via its id but a concurrent request is still using the cursor.
        /// </summary>			
        ERROR_CURSOR_BUSY = 1601,
        /// <summary>
        /// schema validation failed: Will be raised when a document does not pass schema validation.
        /// </summary>			
        ERROR_VALIDATION_FAILED = 1620,
        /// <summary>
        /// invalid schema validation parameter: Will be raised when the schema description is invalid.
        /// </summary>			
        ERROR_VALIDATION_BAD_PARAMETER = 1621,
        /// <summary>
        /// internal transaction error: Will be raised when a wrong usage of transactions is detected. this is an internal error and indicates a bug in ArangoDB.
        /// </summary>			
        ERROR_TRANSACTION_INTERNAL = 1650,
        /// <summary>
        /// nested transactions detected: Will be raised when transactions are nested.
        /// </summary>			
        ERROR_TRANSACTION_NESTED = 1651,
        /// <summary>
        /// unregistered collection used in transaction: Will be raised when a collection is used in the middle of a transaction but was not registered at transaction start.
        /// </summary>			
        ERROR_TRANSACTION_UNREGISTERED_COLLECTION = 1652,
        /// <summary>
        /// disallowed operation inside transaction: Will be raised when a disallowed operation is carried out in a transaction.
        /// </summary>			
        ERROR_TRANSACTION_DISALLOWED_OPERATION = 1653,
        /// <summary>
        /// transaction aborted: Will be raised when a transaction was aborted.
        /// </summary>			
        ERROR_TRANSACTION_ABORTED = 1654,
        /// <summary>
        /// transaction not found: Will be raised when a transaction was not found.
        /// </summary>			
        ERROR_TRANSACTION_NOT_FOUND = 1655,
        /// <summary>
        /// invalid user name: Will be raised when an invalid user name is used.
        /// </summary>			
        ERROR_USER_INVALID_NAME = 1700,
        /// <summary>
        /// duplicate user: Will be raised when a user name already exists.
        /// </summary>			
        ERROR_USER_DUPLICATE = 1702,
        /// <summary>
        /// user not found: Will be raised when a user name is updated that does not exist.
        /// </summary>			
        ERROR_USER_NOT_FOUND = 1703,
        /// <summary>
        /// user is external: Will be raised when the user is authenticated by an external server.
        /// </summary>			
        ERROR_USER_EXTERNAL = 1705,
        /// <summary>
        /// service download failed: Will be raised when a service download from the central repository failed.
        /// </summary>			
        ERROR_SERVICE_DOWNLOAD_FAILED = 1752,
        /// <summary>
        /// service upload failed: Will be raised when a service upload from the client to the ArangoDB server failed.
        /// </summary>			
        ERROR_SERVICE_UPLOAD_FAILED = 1753,
        /// <summary>
        /// cannot init a LDAP connection: can not init a LDAP connection
        /// </summary>			
        ERROR_LDAP_CANNOT_INIT = 1800,
        /// <summary>
        /// cannot set a LDAP option: can not set a LDAP option
        /// </summary>			
        ERROR_LDAP_CANNOT_SET_OPTION = 1801,
        /// <summary>
        /// cannot bind to a LDAP server: can not bind to a LDAP server
        /// </summary>			
        ERROR_LDAP_CANNOT_BIND = 1802,
        /// <summary>
        /// cannot unbind from a LDAP server: can not unbind from a LDAP server
        /// </summary>			
        ERROR_LDAP_CANNOT_UNBIND = 1803,
        /// <summary>
        /// cannot issue a LDAP search: can not search the LDAP server
        /// </summary>			
        ERROR_LDAP_CANNOT_SEARCH = 1804,
        /// <summary>
        /// cannot start a TLS LDAP session: can not star a TLS LDAP session
        /// </summary>			
        ERROR_LDAP_CANNOT_START_TLS = 1805,
        /// <summary>
        /// LDAP didn't found any objects: LDAP didn't found any objects with the specified search query
        /// </summary>			
        ERROR_LDAP_FOUND_NO_OBJECTS = 1806,
        /// <summary>
        /// LDAP found zero ore more than one user: LDAP found zero ore more than one user
        /// </summary>			
        ERROR_LDAP_NOT_ONE_USER_FOUND = 1807,
        /// <summary>
        /// LDAP found a user, but its not the desired one: LDAP found a user, but its not the desired one
        /// </summary>			
        ERROR_LDAP_USER_NOT_IDENTIFIED = 1808,
        /// <summary>
        /// LDAP returned an operations error: LDAP returned an operations error
        /// </summary>			
        ERROR_LDAP_OPERATIONS_ERROR = 1809,
        /// <summary>
        /// invalid ldap mode: cant distinguish a valid mode for provided LDAP configuration
        /// </summary>			
        ERROR_LDAP_INVALID_MODE = 1820,
        /// <summary>
        /// invalid task id: Will be raised when a task is created with an invalid id.
        /// </summary>			
        ERROR_TASK_INVALID_ID = 1850,
        /// <summary>
        /// duplicate task id: Will be raised when a task id is created with a duplicate id.
        /// </summary>			
        ERROR_TASK_DUPLICATE_ID = 1851,
        /// <summary>
        /// task not found: Will be raised when a task with the specified id could not be found.
        /// </summary>			
        ERROR_TASK_NOT_FOUND = 1852,
        /// <summary>
        /// invalid graph: Will be raised when an invalid name is passed to the server.
        /// </summary>			
        ERROR_GRAPH_INVALID_GRAPH = 1901,
        /// <summary>
        /// invalid edge: Will be raised when an invalid edge id is passed to the server.
        /// </summary>			
        ERROR_GRAPH_INVALID_EDGE = 1906,
        /// <summary>
        /// too many iterations - try increasing the value of 'maxIterations': Will be raised when too many iterations are done in a graph traversal.
        /// </summary>			
        ERROR_GRAPH_TOO_MANY_ITERATIONS = 1909,
        /// <summary>
        /// invalid filter result: Will be raised when an invalid filter result is returned in a graph traversal.
        /// </summary>			
        ERROR_GRAPH_INVALID_FILTER_RESULT = 1910,
        /// <summary>
        /// multi use of edge collection in edge def: an edge collection may only be used once in one edge definition of a graph.
        /// </summary>			
        ERROR_GRAPH_COLLECTION_MULTI_USE = 1920,
        /// <summary>
        /// edge collection already used in edge def:  is already used by another graph in a different edge definition.
        /// </summary>			
        ERROR_GRAPH_COLLECTION_USE_IN_MULTI_GRAPHS = 1921,
        /// <summary>
        /// missing graph name: a graph name is required to create or drop a graph.
        /// </summary>			
        ERROR_GRAPH_CREATE_MISSING_NAME = 1922,
        /// <summary>
        /// malformed edge definition: the edge definition is malformed. It has to be an array of objects.
        /// </summary>			
        ERROR_GRAPH_CREATE_MALFORMED_EDGE_DEFINITION = 1923,
        /// <summary>
        /// graph '%s' not found: a graph with this name could not be found.
        /// </summary>			
        ERROR_GRAPH_NOT_FOUND = 1924,
        /// <summary>
        /// graph already exists: a graph with this name already exists.
        /// </summary>			
        ERROR_GRAPH_DUPLICATE = 1925,
        /// <summary>
        /// vertex collection does not exist or is not part of the graph: the specified vertex collection does not exist or is not part of the graph.
        /// </summary>			
        ERROR_GRAPH_VERTEX_COL_DOES_NOT_EXIST = 1926,
        /// <summary>
        /// collection not a vertex collection: the collection is not a vertex collection.
        /// </summary>			
        ERROR_GRAPH_WRONG_COLLECTION_TYPE_VERTEX = 1927,
        /// <summary>
        /// collection is not in list of orphan collections: Vertex collection not in list of orphan collections of the graph.
        /// </summary>			
        ERROR_GRAPH_NOT_IN_ORPHAN_COLLECTION = 1928,
        /// <summary>
        /// collection already used in edge def: The collection is already used in an edge definition of the graph.
        /// </summary>			
        ERROR_GRAPH_COLLECTION_USED_IN_EDGE_DEF = 1929,
        /// <summary>
        /// edge collection not used in graph: The edge collection is not used in any edge definition of the graph.
        /// </summary>			
        ERROR_GRAPH_EDGE_COLLECTION_NOT_USED = 1930,
        /// <summary>
        /// collection _graphs does not exist: collection _graphs does not exist.
        /// </summary>			
        ERROR_GRAPH_NO_GRAPH_COLLECTION = 1932,
        /// <summary>
        /// Invalid number of arguments. Expected: : Invalid number of arguments. Expected: 
        /// </summary>			
        ERROR_GRAPH_INVALID_NUMBER_OF_ARGUMENTS = 1935,
        /// <summary>
        /// Invalid parameter type.: Invalid parameter type.
        /// </summary>			
        ERROR_GRAPH_INVALID_PARAMETER = 1936,
        /// <summary>
        /// collection used in orphans: The collection is already used in the orphans of the graph.
        /// </summary>			
        ERROR_GRAPH_COLLECTION_USED_IN_ORPHANS = 1938,
        /// <summary>
        /// edge collection does not exist or is not part of the graph: the specified edge collection does not exist or is not part of the graph.
        /// </summary>			
        ERROR_GRAPH_EDGE_COL_DOES_NOT_EXIST = 1939,
        /// <summary>
        /// empty graph: The requested graph has no edge collections.
        /// </summary>			
        ERROR_GRAPH_EMPTY = 1940,
        /// <summary>
        /// internal graph data corrupt: The _graphs collection contains invalid data.
        /// </summary>			
        ERROR_GRAPH_INTERNAL_DATA_CORRUPT = 1941,
        /// <summary>
        /// malformed orphan list: the orphan list argument is malformed. It has to be an array of strings.
        /// </summary>			
        ERROR_GRAPH_CREATE_MALFORMED_ORPHAN_LIST = 1943,
        /// <summary>
        /// edge definition collection is a document collection: the collection used as a relation is existing, but is a document collection, it cannot be used here.
        /// </summary>			
        ERROR_GRAPH_EDGE_DEFINITION_IS_DOCUMENT = 1944,
        /// <summary>
        /// initial collection is not allowed to be removed manually: the collection is used as the initial collection of this graph and is not allowed to be removed manually.
        /// </summary>			
        ERROR_GRAPH_COLLECTION_IS_INITIAL = 1945,
        /// <summary>
        /// no valid initial collection found: during the graph creation process no collection could be selected as the needed initial collection. Happens if a distributeShardsLike or replicationFactor mismatch was found.
        /// </summary>			
        ERROR_GRAPH_NO_INITIAL_COLLECTION = 1946,
        /// <summary>
        /// referenced vertex collection is not part of the graph: the _from or _to collection specified for the edge refers to a vertex collection which is not used in any edge definition of the graph.
        /// </summary>			
        ERROR_GRAPH_REFERENCED_VERTEX_COLLECTION_NOT_USED = 1947,
        /// <summary>
        /// negative edge weight found: a negative edge weight was found during a weighted graph traversal or shortest path query
        /// </summary>			
        ERROR_GRAPH_NEGATIVE_EDGE_WEIGHT = 1948,
        /// <summary>
        /// unknown session: Will be raised when an invalid/unknown session id is passed to the server.
        /// </summary>			
        ERROR_SESSION_UNKNOWN = 1950,
        /// <summary>
        /// session expired: Will be raised when a session is expired.
        /// </summary>			
        ERROR_SESSION_EXPIRED = 1951,
        /// <summary>
        /// unknown client error: This error should not happen.
        /// </summary>			
        ERROR_SIMPLE_CLIENT_UNKNOWN_ERROR = 2000,
        /// <summary>
        /// could not connect to server: Will be raised when the client could not connect to the server.
        /// </summary>			
        ERROR_SIMPLE_CLIENT_COULD_NOT_CONNECT = 2001,
        /// <summary>
        /// could not write to server: Will be raised when the client could not write data.
        /// </summary>			
        ERROR_SIMPLE_CLIENT_COULD_NOT_WRITE = 2002,
        /// <summary>
        /// could not read from server: Will be raised when the client could not read data.
        /// </summary>			
        ERROR_SIMPLE_CLIENT_COULD_NOT_READ = 2003,
        /// <summary>
        /// was erlaube?!: Will be raised if was erlaube?!
        /// </summary>			
        ERROR_WAS_ERLAUBE = 2019,
        /// <summary>
        /// General internal AQL error: Internal error during AQL execution
        /// </summary>			
        ERROR_INTERNAL_AQL = 2200,
        /// <summary>
        /// An AQL block wrote too few output registers: An AQL block wrote too few output registers
        /// </summary>			
        ERROR_WROTE_TOO_FEW_OUTPUT_REGISTERS = 2201,
        /// <summary>
        /// An AQL block wrote too many output registers: An AQL block wrote too many output registers
        /// </summary>			
        ERROR_WROTE_TOO_MANY_OUTPUT_REGISTERS = 2202,
        /// <summary>
        /// An AQL block wrote an output register twice: An AQL block wrote an output register twice
        /// </summary>			
        ERROR_WROTE_OUTPUT_REGISTER_TWICE = 2203,
        /// <summary>
        /// An AQL block wrote in a register that is not its output: An AQL block wrote in a register that is not its output
        /// </summary>			
        ERROR_WROTE_IN_WRONG_REGISTER = 2204,
        /// <summary>
        /// An AQL block did not copy its input registers: An AQL block did not copy its input registers
        /// </summary>			
        ERROR_INPUT_REGISTERS_NOT_COPIED = 2205,
        /// <summary>
        /// failed to parse manifest file: The service manifest file is not well-formed JSON.
        /// </summary>			
        ERROR_MALFORMED_MANIFEST_FILE = 3000,
        /// <summary>
        /// manifest file is invalid: The service manifest contains invalid values.
        /// </summary>			
        ERROR_INVALID_SERVICE_MANIFEST = 3001,
        /// <summary>
        /// service files missing: The service folder or bundle does not exist on this server.
        /// </summary>			
        ERROR_SERVICE_FILES_MISSING = 3002,
        /// <summary>
        /// service files outdated: The local service bundle does not match the checksum in the database.
        /// </summary>			
        ERROR_SERVICE_FILES_OUTDATED = 3003,
        /// <summary>
        /// service options are invalid: The service options contain invalid values.
        /// </summary>			
        ERROR_INVALID_FOXX_OPTIONS = 3004,
        /// <summary>
        /// invalid mountpath: The service mountpath contains invalid characters.
        /// </summary>			
        ERROR_INVALID_MOUNTPOINT = 3007,
        /// <summary>
        /// service not found: No service found at the given mountpath.
        /// </summary>			
        ERROR_SERVICE_NOT_FOUND = 3009,
        /// <summary>
        /// service needs configuration: The service is missing configuration or dependencies.
        /// </summary>			
        ERROR_SERVICE_NEEDS_CONFIGURATION = 3010,
        /// <summary>
        /// service already exists: A service already exists at the given mountpath.
        /// </summary>			
        ERROR_SERVICE_MOUNTPOINT_CONFLICT = 3011,
        /// <summary>
        /// missing manifest file: The service directory does not contain a manifest file.
        /// </summary>			
        ERROR_SERVICE_MANIFEST_NOT_FOUND = 3012,
        /// <summary>
        /// failed to parse service options: The service options are not well-formed JSON.
        /// </summary>			
        ERROR_SERVICE_OPTIONS_MALFORMED = 3013,
        /// <summary>
        /// source path not found: The source path does not match a file or directory.
        /// </summary>			
        ERROR_SERVICE_SOURCE_NOT_FOUND = 3014,
        /// <summary>
        /// error resolving source: The source path could not be resolved.
        /// </summary>			
        ERROR_SERVICE_SOURCE_ERROR = 3015,
        /// <summary>
        /// unknown script: The service does not have a script with this name.
        /// </summary>			
        ERROR_SERVICE_UNKNOWN_SCRIPT = 3016,
        /// <summary>
        /// service api disabled: The API for managing Foxx services has been disabled on this server.
        /// </summary>			
        ERROR_SERVICE_API_DISABLED = 3099,
        /// <summary>
        /// cannot locate module: The module path could not be resolved.
        /// </summary>			
        ERROR_MODULE_NOT_FOUND = 3100,
        /// <summary>
        /// syntax error in module: The module could not be parsed because of a syntax error.
        /// </summary>			
        ERROR_MODULE_SYNTAX_ERROR = 3101,
        /// <summary>
        /// failed to invoke module: Failed to invoke the module in its context.
        /// </summary>			
        ERROR_MODULE_FAILURE = 3103,
        /// <summary>
        /// collection is not smart: The requested collection needs to be smart, but it isn't.
        /// </summary>			
        ERROR_NO_SMART_COLLECTION = 4000,
        /// <summary>
        /// smart graph attribute not given: The given document does not have the SmartGraph attribute set.
        /// </summary>			
        ERROR_NO_SMART_GRAPH_ATTRIBUTE = 4001,
        /// <summary>
        /// cannot drop this smart collection: This smart collection cannot be dropped, it dictates sharding in the graph.
        /// </summary>			
        ERROR_CANNOT_DROP_SMART_COLLECTION = 4002,
        /// <summary>
        /// in smart vertex collections _key must be a string and prefixed with the value of the smart graph attribute: In a smart vertex collection _key must be prefixed with the value of the SmartGraph attribute.
        /// </summary>			
        ERROR_KEY_MUST_BE_PREFIXED_WITH_SMART_GRAPH_ATTRIBUTE = 4003,
        /// <summary>
        /// attribute cannot be used as smart graph attribute: The given smartGraph attribute is illegal and cannot be used for sharding. All system attributes are forbidden.
        /// </summary>			
        ERROR_ILLEGAL_SMART_GRAPH_ATTRIBUTE = 4004,
        /// <summary>
        /// smart graph attribute mismatch: The SmartGraph attribute of the given collection does not match the SmartGraph attribute of the graph.
        /// </summary>			
        ERROR_SMART_GRAPH_ATTRIBUTE_MISMATCH = 4005,
        /// <summary>
        /// invalid smart join attribute declaration: Will be raised when the smartJoinAttribute declaration is invalid.
        /// </summary>			
        ERROR_INVALID_SMART_JOIN_ATTRIBUTE = 4006,
        /// <summary>
        /// shard key value must be prefixed with the value of the smart join attribute: when using smartJoinAttribute for a collection, the shard key value must be prefixed with the value of the SmartJoin attribute.
        /// </summary>			
        ERROR_KEY_MUST_BE_PREFIXED_WITH_SMART_JOIN_ATTRIBUTE = 4007,
        /// <summary>
        /// smart join attribute not given or invalid: The given document does not have the required SmartJoin attribute set or it has an invalid value.
        /// </summary>			
        ERROR_NO_SMART_JOIN_ATTRIBUTE = 4008,
        /// <summary>
        /// must not change the value of the smartJoinAttribute: Will be raised if there is an attempt to update the value of the smartJoinAttribute.
        /// </summary>			
        ERROR_CLUSTER_MUST_NOT_CHANGE_SMART_JOIN_ATTRIBUTE = 4009,
        /// <summary>
        /// non disjoint edge found: Will be raised if there is an attempt to create an edge between separated graph components.
        /// </summary>			
        ERROR_INVALID_DISJOINT_SMART_EDGE = 4010,
        /// <summary>
        /// Unsupported alternating Smart and Satellite in Disjoint SmartGraph.: Switching back and forth between Satellite and Smart in Disjoint SmartGraph is not supported within a single AQL statement. Split into multiple statements.
        /// </summary>			
        ERROR_UNSUPPORTED_CHANGE_IN_SMART_TO_SATELLITE_DISJOINT_EDGE_DIRECTION = 4011,
        /// <summary>
        /// malformed gossip message: Malformed gossip message.
        /// </summary>			
        ERROR_AGENCY_MALFORMED_GOSSIP_MESSAGE = 20001,
        /// <summary>
        /// malformed inquire request: Malformed inquire request.
        /// </summary>			
        ERROR_AGENCY_MALFORMED_INQUIRE_REQUEST = 20002,
        /// <summary>
        /// Inform message must be an object.: The inform message in the Agency must be an object.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_BE_OBJECT = 20011,
        /// <summary>
        /// Inform message must contain uint parameter 'term': The inform message in the Agency must contain a uint parameter 'term'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_TERM = 20012,
        /// <summary>
        /// Inform message must contain string parameter 'id': The inform message in the Agency must contain a string parameter 'id'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_ID = 20013,
        /// <summary>
        /// Inform message must contain array 'active': The inform message in the Agency must contain an array 'active'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_ACTIVE = 20014,
        /// <summary>
        /// Inform message must contain object 'pool': The inform message in the Agency must contain an object 'pool'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_POOL = 20015,
        /// <summary>
        /// Inform message must contain object 'min ping': The inform message in the Agency must contain an object 'min ping'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_MIN_PING = 20016,
        /// <summary>
        /// Inform message must contain object 'max ping': The inform message in the Agency must contain an object 'max ping'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_MAX_PING = 20017,
        /// <summary>
        /// Inform message must contain object 'timeoutMult': The inform message in the Agency must contain an object 'timeoutMult'.
        /// </summary>			
        ERROR_AGENCY_INFORM_MUST_CONTAIN_TIMEOUT_MULT = 20018,
        /// <summary>
        /// Cannot rebuild readDB and spearHead: Will be raised if the readDB or the spearHead cannot be rebuilt from the replicated log.
        /// </summary>			
        ERROR_AGENCY_CANNOT_REBUILD_DBS = 20021,
        /// <summary>
        /// malformed agency transaction: Malformed agency transaction.
        /// </summary>			
        ERROR_AGENCY_MALFORMED_TRANSACTION = 20030,
        /// <summary>
        /// general supervision failure: General supervision failure.
        /// </summary>			
        ERROR_SUPERVISION_GENERAL_FAILURE = 20501,
        /// <summary>
        /// queue is full: Will be returned if the scheduler queue is full.
        /// </summary>			
        ERROR_QUEUE_FULL = 21003,
        /// <summary>
        /// queue time violated: Will be returned if a request with a queue time requirement is set and it cannot be fulfilled.
        /// </summary>			
        ERROR_QUEUE_TIME_REQUIREMENT_VIOLATED = 21004,
        /// <summary>
        /// this maintenance action cannot be stopped: This maintenance action cannot be stopped once it is started
        /// </summary>			
        ERROR_ACTION_OPERATION_UNABORTABLE = 6002,
        /// <summary>
        /// maintenance action still processing: This maintenance action is still processing
        /// </summary>			
        ERROR_ACTION_UNFINISHED = 6003,
        /// <summary>
        /// internal hot backup error: Failed to create hot backup set
        /// </summary>			
        ERROR_HOT_BACKUP_INTERNAL = 7001,
        /// <summary>
        /// internal hot restore error: Failed to restore to hot backup set
        /// </summary>			
        ERROR_HOT_RESTORE_INTERNAL = 7002,
        /// <summary>
        /// backup does not match this topology: The hot backup set cannot be restored on non matching cluster topology
        /// </summary>			
        ERROR_BACKUP_TOPOLOGY = 7003,
        /// <summary>
        /// no space left on device: No space left on device
        /// </summary>			
        ERROR_NO_SPACE_LEFT_ON_DEVICE = 7004,
        /// <summary>
        /// failed to upload hot backup set to remote target: Failed to upload hot backup set to remote target
        /// </summary>			
        ERROR_FAILED_TO_UPLOAD_BACKUP = 7005,
        /// <summary>
        /// failed to download hot backup set from remote source: Failed to download hot backup set from remote source
        /// </summary>			
        ERROR_FAILED_TO_DOWNLOAD_BACKUP = 7006,
        /// <summary>
        /// no such hot backup set can be found: Cannot find a hot backup set with this Id
        /// </summary>			
        ERROR_NO_SUCH_HOT_BACKUP = 7007,
        /// <summary>
        /// remote hotback repository configuration error: The configuration given for upload or download operation to/from remote hot backup repositories is wrong.
        /// </summary>			
        ERROR_REMOTE_REPOSITORY_CONFIG_BAD = 7008,
        /// <summary>
        /// some db servers cannot be reached for transaction locks: Some of the DB-Servers cannot be reached for transaction locks.
        /// </summary>			
        ERROR_LOCAL_LOCK_FAILED = 7009,
        /// <summary>
        /// some db servers cannot be reached for transaction locks: Some of the DB-Servers cannot be reached for transaction locks.
        /// </summary>			
        ERROR_LOCAL_LOCK_RETRY = 7010,
        /// <summary>
        /// hot backup conflict: Conflict of multiple hot backup processes.
        /// </summary>			
        ERROR_HOT_BACKUP_CONFLICT = 7011,
        /// <summary>
        /// hot backup not all db servers reachable: One or more DB-Servers could not be reached for hot backup inquiry
        /// </summary>			
        ERROR_HOT_BACKUP_DBSERVERS_AWOL = 7012,
        /// <summary>
        /// analyzers in plan could not be modified: Plan could not be modified while creating or deleting Analyzers revision
        /// </summary>			
        ERROR_CLUSTER_COULD_NOT_MODIFY_ANALYZERS_IN_PLAN = 7021,
        /// <summary>
        /// license has expired or is invalid: The license has expired or is invalid.
        /// </summary>			
        ERROR_LICENSE_EXPIRED_OR_INVALID = 9001,
        /// <summary>
        /// license verification failed: Verification of license failed.
        /// </summary>			
        ERROR_LICENSE_SIGNATURE_VERIFICATION = 9002,
        /// <summary>
        /// non-matching license id: The ID of the license does not match the ID of this instance.
        /// </summary>			
        ERROR_LICENSE_NON_MATCHING_ID = 9003,
        /// <summary>
        /// feature is not enabled by the license: The installed license does not cover this feature.
        /// </summary>			
        ERROR_LICENSE_FEATURE_NOT_ENABLED = 9004,
        /// <summary>
        /// the resource is exhausted: The installed license does not cover a higher number of this resource.
        /// </summary>			
        ERROR_LICENSE_RESOURCE_EXHAUSTED = 9005,
        /// <summary>
        /// invalid license: The license does not hold features of an ArangoDB license.
        /// </summary>			
        ERROR_LICENSE_INVALID = 9006,
        /// <summary>
        /// conflicting license: The license has one or more inferior features.
        /// </summary>			
        ERROR_LICENSE_CONFLICT = 9007,
        /// <summary>
        /// failed to validate license signature: Could not verify the license's signature.
        /// </summary>			
        ERROR_LICENSE_VALIDATION_FAILED = 9008,
    }
}
