# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## [2.00.2] - 2022-04-12
### Added
- added support for additional ArangoDB Collections API features (#356)
- `GET/_api/collection/{collection-name}/checksum`: Returns checksum for the collection
- `PUT/_api/collection/{collection-name}/loadIndexesIntoMemory`: Loads a collection's indexes into memory
- `PUT/_api/collection/{collection-name}/recalculateCount`: Recalculate count of a collection
- `PUT​/_api​/collection​/{collection-name}​/responsibleShard`: Returns the responsible shard for a document
- `GET/_api/collection/{collection-name}/shards`: Returns the shard ids of a collection
- `PUT/_api/collection/{collection-name}/compact`: Compacts a collection

## [2.00.1] - 2022-03-24
### Added
- added support for additional ArangoDB AQL/Query API features (#352)
- `POST: /_api/explain`: Explain an AQL query
- `POST: /_api/query`: Parse an AQL query
- `DELETE: /_api/query/slow`: Clears the list of slow AQL queries
- `GET: /_api/query/slow`: Returns the list of slow AQL queries
- `DELETE: /_api/query/{query-id}`: Kills a running AQL query
- `DELETE: /_api/query-cache`: Clears any results in the AQL query cache
- `GET: /_api/query-cache/entries`: Returns the currently cached query results
- `GET: /_api/query-cache/properties`: Returns the global properties for the AQL query cache
- `PUT: /_api/query-cache/properties`: Globally adjusts the AQL query result cache properties
- `GET: /_api/query/current`: Returns the currently running AQL queries
- `GET: /_api/query/properties`: Returns the properties for the AQL query tracking
- `PUT: /_api/query/properties`: Changes the properties for the AQL query tracking

## [2.00.0] - 2022-03-04
### Added
- added support for ArangoDB Indexes API features (#350)
- `GET /_api/index` Read all indexes of a collection
- `POST /_api/index#fulltext` Create fulltext index 
- `POST /_api/index#geo` Create geo-spatial index 
- `POST /_api/index#persistent` Create a persistent index
- `POST /_api/index#ttl` Create TTL index
- `DELETE /_api/index/{index-id}` Delete index
- `GET /_api/index/{index-id}` Read index