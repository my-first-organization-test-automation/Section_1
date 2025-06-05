# Development Roadmap for Simple ORM Implementation

This roadmap provides a structured approach to implementing the Simple ORM framework. Follow these steps in order to build a complete, working ORM solution.

## Phase 1: Understanding the Architecture

Before writing any code, ensure you understand:
- The role of each class and interface in the project
- How data flows from C# objects to the database and back
- The mapping between C# types and PostgreSQL data types

## Phase 2: SQL Generation Implementation

### Step 1: Table Name Generation
Implement the `GetTableName<T>()` method in `SqlGenerator.cs`:
- Convert class names to table names (typically pluralized)
- Consider naming conventions that work for your database

### Step 2: SQL Type Mapping
Implement the `GetPostgreSqlType(Type type)` method:
- Map C# types to PostgreSQL data types
- Handle nullable types appropriately
- Include support for common types like DateTime, Guid, etc.

### Step 3: CREATE TABLE Statements
Implement the `GenerateCreateTableSql<T>()` method:
- Use reflection to examine entity properties
- Generate column definitions from property types
- Add primary key constraints for the Id property

### Step 4: CRUD SQL Statements
Implement these methods in order:
1. `GenerateInsertSql<T>()` - Create records with proper parameter placeholders
2. `GenerateSelectByIdSql<T>()` - Retrieve a single record by ID
3. `GenerateSelectAllSql<T>()` - Retrieve all records of a type
4. `GenerateUpdateSql<T>()` - Update existing records with parameter placeholders
5. `GenerateDeleteSql<T>()` - Delete records by ID

## Phase 3: Database Context Implementation

### Step 1: Setup and Connection
Implement the `EnsureDatabaseCreated()` method:
- Establish a database connection
- Create tables if they don't exist using your SQL generator

### Step 2: Parameter Management
Implement the `AddParametersFromEntity<T>()` method:
- Extract values from entity properties using reflection
- Create and configure NpgsqlParameter objects
- Handle null values appropriately

### Step 3: Entity Mapping
Implement the `MapEntity<T>()` method:
- Convert database results to C# objects
- Map column values to properties using reflection
- Handle type conversions and null values

### Step 4: CRUD Operations
Implement these methods in order:
1. `Add<T>()` - Insert a new entity and retrieve its generated ID
2. `GetById<T>()` - Retrieve a single entity by ID
3. `GetAll<T>()` - Retrieve all entities of a specific type
4. `Update<T>()` - Update an existing entity
5. `Remove<T>()` - Delete an entity from the database

## Phase 4: Testing

1. Run the demonstration code in `Program.cs`
2. Test each CRUD operation individually
3. Handle edge cases (null values, type conversions, etc.)
4. Implement proper error handling and transaction management
