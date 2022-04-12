# ArangoDB-net-standard Roadmap
## Version 1.0

### Miscellaneous

- [X]	Readme
- [X]	Usage guide
- [X]	License
- [X]	Automated CI build
- [X]	Nuget package

### API Implementations

A tick indicates an item is implemented and has automated tests in place.

#### Authentication

- [X]	Basic authentication
- [X]	JWT authentication

#### Collections API

- [X]	`GET/_api/collection` reads all collections
- [X]	`POST/_api/collection` Create collection
- [X]	`DELETE/_api/collection/{collection-name}` Drops a collection
- [X]	`GET/_api/collection/{collection-name}` Return information about a collection
- [X]	`GET/_api/collection/{collection-name}/count` Return number of documents in a collection
- [X]	`GET/_api/collection/{collection-name}/properties` Read properties of a collection
- [X]	`PUT/_api/collection/{collection-name}/properties` Change properties of a collection
- [X]	`PUT/_api/collection/{collection-name}/rename` Rename collection
- [X]	`GET/_api/collection/{collection-name}/revision` Return collection revision id
- [X]	`PUT/_api/collection/{collection-name}/truncate` Truncate collection
- [X]	`GET/_api/collection/{collection-name}/figures` Return statistics for a collection

#### Cursor API

- [X]	`POST/_api/cursor` Create cursor
- [X]	`DELETE/_api/cursor/{cursor-identifier}` Delete cursor
- [X]	`PUT/_api/cursor/{cursor-identifier}` Read next batch from cursor

#### Database API

- [X]	`GET/_api/database` List of databases
- [X]	`POST/_api/database` Create database
- [X]	`GET/_api/database/current` Information of the database
- [X]	`GET/_api/database/user` List of accessible databases
- [X]	`DELETE/_api/database/{database-name}` Drop database

#### Document API

- [X]	`DELETE/_api/document/{collection}` Removes multiple documents
- [X]	`PATCH/_api/document/{collection}` Update documents
- [X]	`POST/_api/document/{collection}` Create document
- [X]	`PUT/_api/document/{collection}` Replace documents
- [X]	`DELETE/_api/document/{document-handle}` Removes a document
- [X]	`GET/_api/document/{document-handle}` Read document
- [X]	`HEAD/_api/document/{document-handle}` Read document header
- [X]	`PATCH/_api/document/{document-handle}` Update document
- [X]	`PUT/_api/document/{document-handle}` Replace document

#### Graph API
- [X]	`GET/_api/gharial` List all graphs
- [X]	`POST/_api/gharial` Create a graph
- [X]	`DELETE/_api/gharial/{graph}` Drop a graph
- [X]	`GET/_api/gharial/{graph}` Get a graph
- [X]	`GET/_api/gharial/{graph}/edge` List edge definitions
- [X]	`POST/_api/gharial/{graph}/edge` Add edge definition
- [X]	`POST/_api/gharial/{graph}/edge/{collection}` Create an edge
- [X]	`DELETE/_api/gharial/{graph}/edge/{collection}/{edge}` Remove an edge
- [X]	`GET/_api/gharial/{graph}/edge/{collection}/{edge}` Get an edge
- [X]	`PATCH/_api/gharial/{graph}/edge/{collection}/{edge}` Modify an edge
- [X]	`PUT/_api/gharial/{graph}/edge/{collection}/{edge}` Replace an edge
- [X]	`DELETE/_api/gharial/{graph}/edge/{definition}` Remove an edge definition from the graph
- [X]	`PUT/_api/gharial/{graph}/edge/{definition}` Replace an edge definition
- [X]	`GET/_api/gharial/{graph}/vertex` List vertex collections
- [X]	`POST/_api/gharial/{graph}/vertex` Add vertex collection
- [X]	`DELETE/_api/gharial/{graph}/vertex/{collection}` Remove vertex collection
- [X]	`POST/_api/gharial/{graph}/vertex/{collection}` Create a vertex
- [X]	`DELETE/_api/gharial/{graph}/vertex/{collection}/{vertex}` Remove a vertex
- [X]	`GET/_api/gharial/{graph}/vertex/{collection}/{vertex}` Get a vertex
- [X]	`PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex}` Update a vertex
- [X]	`PUT/_api/gharial/{graph}/vertex/{collection}/{vertex}` Replace a vertex

#### Javascript Transaction API

- [X]	`POST/_api/transaction` Execute transaction

#### Stream Transaction API

- [X]   `POST /_api/transaction/begin` Begin transaction
- [X]   `GET /_api/transaction/{transaction-id}` Get transaction status
- [X]   `PUT /_api/transaction/{transaction-id}` Commit transaction
- [X]   `DELETE /_api/transaction/{transaction-id}` Abort transaction
- [X]   `GET /_api/transaction` Get currently running transactions

#### AQL User Functions Management API

- [X]   `POST /_api/aqlfunction` Create AQL user function
- [X]   `DELETE /_api/aqlfunction/{name}` Remove existing AQL user function
- [X]   `GET /_api/aqlfunction` Return registered AQL user functions

#### User Management API

- [X]	`POST /_api/user` Create User
- [X]	`PUT /_api/user/{user}/database/{dbname}` Set the database access level
- [X]	`PUT /_api/user/{user}/database/{dbname}/{collection}` Set the collection access level
- [X]	`DELETE /_api/user/{user}/database/{dbname}` Clear the database access level
- [X]	`DELETE /_api/user/{user}/database/{dbname}/{collection}` Clear the collection access level
- [X]	`GET /_api/user/{user}/database/` List the accessible databases for a user
- [X]	`GET /_api/user/{user}/database/{dbname}` Get the database access level
- [X]	`GET /_api/user/{user}/database/{dbname}/{collection}` Get the specific collection access level
- [X]	`PUT /_api/user/{user}` Replace User
- [X]	`PATCH /_api/user/{user}` Modify User
- [X]	`DELETE /_api/user/{user}` Remove User
- [X]	`GET /_api/user/{user}` Fetch User
- [X]	`GET /_api/user/` List available Users

## Versions 1.1+

### Other API implementations

#### Collections API

- [ ]	`PUT/_api/collection/{collection-name}/unload` Unload collection
- [ ]	`PUT/_api/collection/{collection-name}/load` Load collection
- [ ]	`GET/_api/collection/{collection-name}/checksum` Return checksum for the collection
- [X]	`GET/_api/collection/{collection-name}/figures` Return statistics for a collection
- [ ]	`PUT/_api/collection/{collection-name}/loadIndexesIntoMemory` Load Indexes into Memory
- [ ]	`PUT/_api/collection/{collection-name}/recalculateCount` Recalculate count of a collection
- [ ]	`PUT/_api/collection/{collection-name}/rotate` Rotate journal of a collection

#### Simple Queries API

- [ ]	TODO

#### Job API

- [ ]	TODO

#### Graph Edges API

- [ ]	`GET/_api/edges/{collection-id}` Read in- or outbound edges

#### Graph Traversal API

- [ ]	`POST/_api/traversal` executes a traversal

### Database Management API implementations

#### Administration API

- [ ]	TODO

#### Cluster API

- [ ]	TODO

#### FOXX API

- [ ]	TODO

#### Indexes API

- [ ]	TODO

#### Replication API

- [ ]	TODO

## Version 2.0

#### New transport implementations

- [ ]	VelocyStream support
- [ ]	VelocyPack-over-HTTP support

#### Indexes API

- [X]	`GET/_api/index` Read all indexes of a collection
- [X]	`POST/_api/index#fulltext` Create fulltext index
- [X]	`POST/_api/index#geo` Create geo-spatial index
- [X]	`POST/_api/index#persistent` Create a persistent index
- [X]	`POST/_api/index#ttl` Create TTL index
- [X]	`DELETE/_api/index/{index-id}` Delete index
- [X]	`GET/_api/index/{index-id}` Read index

#### AQL API

- [X]	`POST/_api/explain` Explain an AQL query
- [X]	`POST/_api/query` Parse an AQL query
- [X]	`DELETE/_api/query/slow` Clears the list of slow AQL queries
- [X]	`GET/_api/query/slow` Returns the list of slow AQL queries
- [X]	`DELETE/_api/query/{query-id}` Kills a running AQL query
- [X]	`DELETE/_api/query-cache` Clears any results in the AQL query cache
- [X]	`GET/_api/query-cache/entries` Returns the currently cached query results
- [X]	`GET/_api/query-cache/properties` Returns the global properties for the AQL query cache
- [X]	`PUT/_api/query-cache/properties` Globally adjusts the AQL query result cache properties
- [X]	`GET/_api/query/current` Returns the currently running AQL queries
- [X]	`GET/_api/query/properties` Returns the properties for the AQL query tracking
- [X]	`PUT/_api/query/properties` Changes the properties for the AQL query tracking

#### Collections API

- [X]	`GET/_api/collection/{collection-name}/checksum` Return checksum for the collection
- [X]	`PUT/_api/collection/{collection-name}/loadIndexesIntoMemory` Load Indexes into Memory
- [X]	`PUT/_api/collection/{collection-name}/recalculateCount` Recalculate count of a collection
- [X]	`PUT​/_api​/collection​/{collection-name}​/responsibleShard` Return responsible shard for a document
- [X]	`GET/_api/collection/{collection-name}/shards` Return the shard ids of a collection
- [X]	`PUT/_api/collection/{collection-name}/compact` Compact a collection

#### Views API

- [X]	`GET/_api/view` Reads all views
- [X]	`POST/_api/view#iresearch` Create iresearch view
- [X]	`DELETE/_api/view/{view-name}` Drops a view
- [X]	`GET/_api/view/{view-name}` Return information about a view
- [X]	`GET/_api/view/{view-name}/properties` Read properties of a view
- [X]	`PATCH/_api/view/{view-name}/properties#iresearch` Partially changes properties of an iresearch view
- [X]	`PUT/_api/view/{view-name}/properties#iresearch` Change properties of an iresearch view
- [X]	`PUT/_api/view/{view-name}/rename` Rename view