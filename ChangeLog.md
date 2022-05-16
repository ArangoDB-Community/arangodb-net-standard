# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## 2022-05-02
### Added
- Added support for connecting to ArangoDB Oasis Cloud Service (#364)
- Operations defaults to _system database when a database is not specified.
- Added support for Oasis SSL certificates.

## 2022-04-29
### Added
- added support for ArangoDB Views API features (#358)
- `GET : /_api/view` : reads all views
- `POST : /_api/view#iresearch` : Create iresearch view
- `DELETE : /_api/view/{view-name}` : Drops a view
- `GET : /_api/view/{view-name}` : Return information about a view
- `GET : /_api/view/{view-name}/properties` : Read properties of a view
- `PATCH : /_api/view/{view-name}/properties#iresearch` : Partially changes properties of an iresearch view
- `PUT : /_api/view/{view-name}/properties#iresearch` : Change properties of an iresearch view
- `PUT : /_api/view/{view-name}/rename` : Rename view

## 2022-04-28
### Added
- added support for ArangoDB Analyzers API features (#357)
- `GET /_api/analyzer` List all Analyzers
- `POST /_api/analyzer` Create an Analyzer with the supplied definition
- `DELETE /_api/analyzer/{analyzer-name}` Remove an Analyzer
- `GET /_api/analyzer/{analyzer-name}` Return the Analyzer definition

## 2022-04-12
### Added
- added support for additional ArangoDB Collections API features (#356)
- `GET/_api/collection/{collection-name}/checksum`: Returns checksum for the collection
- `PUT/_api/collection/{collection-name}/loadIndexesIntoMemory`: Loads a collection's indexes into memory
- `PUT/_api/collection/{collection-name}/recalculateCount`: Recalculate count of a collection
- `PUT​/_api​/collection​/{collection-name}​/responsibleShard`: Returns the responsible shard for a document
- `GET/_api/collection/{collection-name}/shards`: Returns the shard ids of a collection
- `PUT/_api/collection/{collection-name}/compact`: Compacts a collection

## 2022-03-24
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

## 2022-03-04
### Added
- added support for ArangoDB Indexes API features (#350)
- `GET /_api/index` Read all indexes of a collection
- `POST /_api/index#fulltext` Create fulltext index 
- `POST /_api/index#geo` Create geo-spatial index 
- `POST /_api/index#persistent` Create a persistent index
- `POST /_api/index#ttl` Create TTL index
- `DELETE /_api/index/{index-id}` Delete index
- `GET /_api/index/{index-id}` Read index

## 2022-05-16
### Added
- added support for basic Admin API features (#370)
- `GET/_admin/log/entries` Read global logs from the server
- `POST/_admin/routing/reload` Reloads the routing information
- `GET/_admin/server/id` Return id of a server in a cluster
- `GET/_admin/server/role` Return role of a server in a cluster
- `GET/_api/engine` Return server database engine type
- `GET/_api/version` Return server version
