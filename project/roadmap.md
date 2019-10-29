# ArangoDB-net-standard Roadmap
## Version 1.0

### Miscellaneous

- [X]	Readme
- [ ]	Usage guide
- [X]	License
- [ ]	Automated CI build
- [ ]	Nuget package

### API Implementations

A tick indicates an item is implemented and has automated tests in place.

#### Authentication

- [X]	Basic authentication
- [ ]	JWT authentication

#### Collections API

- [ ]	GET/_api/collectionreads all collections
- [ ]	POST/_api/collectionCreate collection
- [ ]	DELETE/_api/collection/{collection-name}Drops a collection
- [ ]	GET/_api/collection/{collection-name}Return information about a collection
- [ ]	GET/_api/collection/{collection-name}/countReturn number of documents in a collection
- [ ]	GET/_api/collection/{collection-name}/propertiesRead properties of a collection
- [ ]	PUT/_api/collection/{collection-name}/propertiesChange properties of a collection
- [ ]	PUT/_api/collection/{collection-name}/renameRename collection
- [ ]	GET/_api/collection/{collection-name}/revisionReturn collection revision id
- [ ]	PUT/_api/collection/{collection-name}/truncateTruncate collection

#### Cursor API

- [X]	POST/_api/cursorCreate cursor
- [X]	DELETE/_api/cursor/{cursor-identifier}Delete cursor
- [X]	PUT/_api/cursor/{cursor-identifier}Read next batch from cursor

#### Database API

- [ ]	GET/_api/databaseList of databases
- [ ]	POST/_api/databaseCreate database
- [ ]	GET/_api/database/currentInformation of the database
- [ ]	GET/_api/database/userList of accessible databases
- [ ]	DELETE/_api/database/{database-name}Drop database

#### Document API

- [X]	DELETE/_api/document/{collection}Removes multiple documents
- [ ]	PATCH/_api/document/{collection}Update documents
- [X]	POST/_api/document/{collection}Create document
- [X]	PUT/_api/document/{collection}Replace documents
- [X]	DELETE/_api/document/{document-handle}Removes a document
- [X]	GET/_api/document/{document-handle}Read document
- [ ]	HEAD/_api/document/{document-handle}Read document header
- [ ]	PATCH/_api/document/{document-handle}Update document
- [X]	PUT/_api/document/{document-handle}Replace document
- [ ]	PUT/_api/simple/all-keysRead all documents

#### Graph API
- [ ]	GET/_api/gharialList all graphs
- [ ]	POST/_api/gharialCreate a graph
- [ ]	DELETE/_api/gharial/{graph}Drop a graph
- [ ]	GET/_api/gharial/{graph}Get a graph
- [ ]	GET/_api/gharial/{graph}/edgeList edge definitions
- [ ]	POST/_api/gharial/{graph}/edgeAdd edge definition
- [ ]	POST/_api/gharial/{graph}/edge/{collection}Create an edge
- [ ]	DELETE/_api/gharial/{graph}/edge/{collection}/{edge}Remove an edge
- [ ]	GET/_api/gharial/{graph}/edge/{collection}/{edge}Get an edge
- [ ]	PATCH/_api/gharial/{graph}/edge/{collection}/{edge}Modify an edge
- [ ]	PUT/_api/gharial/{graph}/edge/{collection}/{edge}Replace an edge
- [ ]	DELETE/_api/gharial/{graph}/edge/{definition}Remove an edge definition from the graph
- [ ]	PUT/_api/gharial/{graph}/edge/{definition}Replace an edge definition
- [ ]	GET/_api/gharial/{graph}/vertexList vertex collections
- [ ]	POST/_api/gharial/{graph}/vertexAdd vertex collection
- [ ]	DELETE/_api/gharial/{graph}/vertex/{collection}Remove vertex collection
- [ ]	POST/_api/gharial/{graph}/vertex/{collection}Create a vertex
- [ ]	DELETE/_api/gharial/{graph}/vertex/{collection}/{vertex}Remove a vertex
- [ ]	GET/_api/gharial/{graph}/vertex/{collection}/{vertex}Get a vertex
- [ ]	PATCH/_api/gharial/{graph}/vertex/{collection}/{vertex}Update a vertex
- [ ]	PUT/_api/gharial/{graph}/vertex/{collection}/{vertex}Replace a vertex

#### Transactions API

- [X]	POST/_api/transactionExecute transaction

## Versions 1.1+

### Other API implementations

#### Collections API
- [ ]	PUT/_api/collection/{collection-name}/unloadUnload collection
- [ ]	PUT/_api/collection/{collection-name}/loadLoad collection
- [ ]	GET/_api/collection/{collection-name}/checksumReturn checksum for the collection
- [ ]	GET/_api/collection/{collection-name}/figuresReturn statistics for a collection
- [ ]	PUT/_api/collection/{collection-name}/loadIndexesIntoMemoryLoad Indexes into Memory
- [ ]	PUT/_api/collection/{collection-name}/recalculateCountRecalculate count of a collection
- [ ]	PUT/_api/collection/{collection-name}/rotateRotate journal of a collection

#### Simple Queries API

- [ ]	TODO

#### Job API

- [ ]	TODO

#### Graph Edges API

- [ ]	GET/_api/edges/{collection-id}Read in- or outbound edges

#### Graph Traversal API

- [ ]	POST/_api/traversalexecutes a traversal

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
