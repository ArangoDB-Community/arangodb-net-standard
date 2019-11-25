# ArangoDB-net-standard Roadmap
## Version 1.0

### Miscellaneous

- [X]	Readme
- [ ]	Usage guide
- [X]	License
- [X]	Automated CI build
- [ ]	Nuget package

### API Implementations

A tick indicates an item is implemented and has automated tests in place.

#### Authentication

- [X]	Basic authentication
- [ ]	JWT authentication

#### Collections API

- [X]	GET/_api/collection reads all collections
- [X]	POST/_api/collection Create collection
- [X]	DELETE/_api/collection/{collection-name} Drops a collection
- [X]	GET/_api/collection/{collection-name} Return information about a collection
- [X]	GET/_api/collection/{collection-name}/count Return number of documents in a collection
- [X]	GET/_api/collection/{collection-name}/properties Read properties of a collection
- [X]	PUT/_api/collection/{collection-name}/properties Change properties of a collection
- [X]	PUT/_api/collection/{collection-name}/rename Rename collection
- [X]	GET/_api/collection/{collection-name}/revision Return collection revision id
- [X]	PUT/_api/collection/{collection-name}/truncate Truncate collection
- [X]	GET/_api/collection/{collection-name}/figures Return statistics for a collection

#### Cursor API

- [X]	POST/_api/cursor Create cursor
- [X]	DELETE/_api/cursor/{cursor-identifier} Delete cursor
- [X]	PUT/_api/cursor/{cursor-identifier} Read next batch from cursor

#### Database API

- [X]	GET/_api/database List of databases
- [X]	POST/_api/database Create database
- [X]	GET/_api/database/current Information of the database
- [X]	GET/_api/database/user List of accessible databases
- [X]	DELETE/_api/database/{database-name} Drop database

#### Document API

- [X]	DELETE/_api/document/{collection} Removes multiple documents
- [ ]	PATCH/_api/document/{collection} Update documents
- [X]	POST/_api/document/{collection} Create document
- [X]	PUT/_api/document/{collection} Replace documents
- [X]	DELETE/_api/document/{document-handle} Removes a document
- [X]	GET/_api/document/{document-handle} Read document
- [ ]	HEAD/_api/document/{document-handle} Read document header
- [ ]	PATCH/_api/document/{document-handle} Update document
- [X]	PUT/_api/document/{document-handle} Replace document

#### Graph API
- [X]	GET/_api/gharial List all graphs
- [X]	POST/_api/gharial Create a graph
- [X]	DELETE/_api/gharial/{graph} Drop a graph
- [X]	GET/_api/gharial/{graph} Get a graph
- [X]	GET/_api/gharial/{graph}/edge List edge definitions
- [X]	POST/_api/gharial/{graph}/edge Add edge definition
- [ ]	POST/_api/gharial/{graph}/edge/{collection} Create an edge
- [ ]	DELETE/_api/gharial/{graph}/edge/{collection}/{edge} Remove an edge
- [ ]	GET/_api/gharial/{graph}/edge/{collection}/{edge} Get an edge
- [ ]	PATCH/_api/gharial/{graph}/edge/{collection}/{edge} Modify an edge
- [ ]	PUT/_api/gharial/{graph}/edge/{collection}/{edge} Replace an edge
- [X]	DELETE/_api/gharial/{graph}/edge/{definition} Remove an edge definition from the graph
- [ ]	PUT/_api/gharial/{graph}/edge/{definition} Replace an edge definition
- [X]	GET/_api/gharial/{graph}/vertex List vertex collections
- [X]	POST/_api/gharial/{graph}/vertex Add vertex collection
- [ ]	DELETE/_api/gharial/{graph}/vertex/{collection} Remove vertex collection
- [ ]	POST/_api/gharial/{graph}/vertex/{collection} Create a vertex
- [ ]	DELETE/_api/gharial/{graph}/vertex/{collection}/{vertex} Remove a vertex
- [ ]	GET/_api/gharial/{graph}/vertex/{collection}/{vertex} Get a vertex
- [ ]	PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex} Update a vertex
- [ ]	PUT/_api/gharial/{graph}/vertex/{collection}/{vertex} Replace a vertex

#### Transactions API

- [X]	POST/_api/transaction Execute transaction

## Versions 1.1+

### Other API implementations

#### Collections API
- [ ]	PUT/_api/collection/{collection-name}/unload Unload collection
- [ ]	PUT/_api/collection/{collection-name}/load Load collection
- [ ]	GET/_api/collection/{collection-name}/checksum Return checksum for the collection
- [ ]	GET/_api/collection/{collection-name}/figures Return statistics for a collection
- [ ]	PUT/_api/collection/{collection-name}/loadIndexesIntoMemory Load Indexes into Memory
- [ ]	PUT/_api/collection/{collection-name}/recalculateCount Recalculate count of a collection
- [ ]	PUT/_api/collection/{collection-name}/rotate Rotate journal of a collection

#### Simple Queries API

- [ ]	TODO

#### Job API

- [ ]	TODO

#### Graph Edges API

- [ ]	GET/_api/edges/{collection-id} Read in- or outbound edges

#### Graph Traversal API

- [ ]	POST/_api/traversal executes a traversal

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

#### User Management API

- [ ]	TODO

## Version 2.0

#### New transport implementations

- [ ]	VelocyStream support
- [ ]	VelocyPack-over-HTTP support
