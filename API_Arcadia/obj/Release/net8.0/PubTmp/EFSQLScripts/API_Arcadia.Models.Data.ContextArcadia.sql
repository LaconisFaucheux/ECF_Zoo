IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Diets] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Diets] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Habitats] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Habitats] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Healths] (
        [Id] int NOT NULL IDENTITY,
        [State] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_Healths] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [OpeningHours] (
        [Id] int NOT NULL IDENTITY,
        [DayOfWeek] nvarchar(10) NOT NULL,
        [MorningOpening] time NULL,
        [MorningClosing] time NULL,
        [AfternoonOpening] time NULL,
        [AfternoonClosing] time NULL,
        CONSTRAINT [PK_OpeningHours] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Reviews] (
        [Id] int NOT NULL IDENTITY,
        [Pseudo] nvarchar(100) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [IsValidated] bit NOT NULL,
        [Note] tinyint NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [SizeUnits] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        [Abbr] nvarchar(2) NOT NULL,
        CONSTRAINT [PK_SizeUnits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [WeightUnits] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        [Abbr] nvarchar(2) NOT NULL,
        CONSTRAINT [PK_WeightUnits] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [ZooServices] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [FullPrice] real NULL,
        [ChildPrice] real NULL,
        CONSTRAINT [PK_ZooServices] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [HabitatImages] (
        [Id] int NOT NULL IDENTITY,
        [Slug] nvarchar(150) NOT NULL,
        [IdHabitat] int NOT NULL,
        CONSTRAINT [PK_HabitatImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HabitatImages_Habitats_IdHabitat] FOREIGN KEY ([IdHabitat]) REFERENCES [Habitats] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Speciess] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(150) NOT NULL,
        [ScientificName] nvarchar(150) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [MaleMaxSize] real NOT NULL,
        [FemaleMaxSize] real NULL,
        [MaleMaxWeight] real NOT NULL,
        [FemaleMaxWeight] real NULL,
        [IdSizeUnit] int NOT NULL,
        [IdWeightUnit] int NOT NULL,
        [Lifespan] tinyint NOT NULL,
        [IdDiet] int NOT NULL,
        CONSTRAINT [PK_Speciess] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Speciess_Diets_IdDiet] FOREIGN KEY ([IdDiet]) REFERENCES [Diets] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Speciess_SizeUnits_IdSizeUnit] FOREIGN KEY ([IdSizeUnit]) REFERENCES [SizeUnits] ([Id]),
        CONSTRAINT [FK_Speciess_WeightUnits_IdWeightUnit] FOREIGN KEY ([IdWeightUnit]) REFERENCES [WeightUnits] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [Animals] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [IsMale] bit NOT NULL,
        [IdSpecies] int NOT NULL,
        [IdHealth] int NOT NULL,
        CONSTRAINT [PK_Animals] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Animals_Healths_IdHealth] FOREIGN KEY ([IdHealth]) REFERENCES [Healths] ([Id]),
        CONSTRAINT [FK_Animals_Speciess_IdSpecies] FOREIGN KEY ([IdSpecies]) REFERENCES [Speciess] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [SpeciesHabitats] (
        [IdSpecies] int NOT NULL,
        [IdHabitat] int NOT NULL,
        CONSTRAINT [PK_SpeciesHabitats] PRIMARY KEY ([IdHabitat], [IdSpecies]),
        CONSTRAINT [FK_SpeciesHabitats_Habitats_IdHabitat] FOREIGN KEY ([IdHabitat]) REFERENCES [Habitats] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_SpeciesHabitats_Speciess_IdSpecies] FOREIGN KEY ([IdSpecies]) REFERENCES [Speciess] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [AnimalImages] (
        [Id] int NOT NULL IDENTITY,
        [Slug] nvarchar(150) NOT NULL,
        [IdAnimal] int NOT NULL,
        CONSTRAINT [PK_AnimalImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AnimalImages_Animals_IdAnimal] FOREIGN KEY ([IdAnimal]) REFERENCES [Animals] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE TABLE [VetVisits] (
        [Id] int NOT NULL IDENTITY,
        [Food] nvarchar(200) NOT NULL,
        [FoodWeight] real NOT NULL,
        [IdWeightUnit] int NOT NULL,
        [VisitDate] datetime2 NOT NULL,
        [Observations] nvarchar(max) NOT NULL,
        [IdAnimal] int NOT NULL,
        [IdVet] int NULL,
        CONSTRAINT [PK_VetVisits] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_VetVisits_Animals_IdAnimal] FOREIGN KEY ([IdAnimal]) REFERENCES [Animals] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_VetVisits_WeightUnits_IdWeightUnit] FOREIGN KEY ([IdWeightUnit]) REFERENCES [WeightUnits] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_AnimalImages_IdAnimal] ON [AnimalImages] ([IdAnimal]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_Animals_IdHealth] ON [Animals] ([IdHealth]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_Animals_IdSpecies] ON [Animals] ([IdSpecies]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_HabitatImages_IdHabitat] ON [HabitatImages] ([IdHabitat]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_SpeciesHabitats_IdSpecies] ON [SpeciesHabitats] ([IdSpecies]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_Speciess_IdDiet] ON [Speciess] ([IdDiet]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_Speciess_IdSizeUnit] ON [Speciess] ([IdSizeUnit]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_Speciess_IdWeightUnit] ON [Speciess] ([IdWeightUnit]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_VetVisits_IdAnimal] ON [VetVisits] ([IdAnimal]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    CREATE INDEX [IX_VetVisits_IdWeightUnit] ON [VetVisits] ([IdWeightUnit]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240417114152_DbCreation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240417114152_DbCreation', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240423203332_AjoutMiniatureImage'
)
BEGIN
    ALTER TABLE [HabitatImages] ADD [MiniSlug] nvarchar(150) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240423203332_AjoutMiniatureImage'
)
BEGIN
    ALTER TABLE [AnimalImages] ADD [MiniSlug] nvarchar(150) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240423203332_AjoutMiniatureImage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240423203332_AjoutMiniatureImage', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240703202629_Ajout_Images_ZooServices'
)
BEGIN
    ALTER TABLE [ZooServices] ADD [Pic] nvarchar(150) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240703202629_Ajout_Images_ZooServices'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240703202629_Ajout_Images_ZooServices', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240825080916_ModifTypeIdVetForVetVisit'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[VetVisits]') AND [c].[name] = N'IdVet');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [VetVisits] DROP CONSTRAINT [' + @var0 + '];');
    EXEC(N'UPDATE [VetVisits] SET [IdVet] = N'''' WHERE [IdVet] IS NULL');
    ALTER TABLE [VetVisits] ALTER COLUMN [IdVet] nvarchar(max) NOT NULL;
    ALTER TABLE [VetVisits] ADD DEFAULT N'' FOR [IdVet];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240825080916_ModifTypeIdVetForVetVisit'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240825080916_ModifTypeIdVetForVetVisit', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240916192918_Adding_EmployeeFeeding'
)
BEGIN
    CREATE TABLE [EmployeeFeedings] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeEmail] nvarchar(max) NOT NULL,
        [IdAnimal] int NOT NULL,
        [Food] nvarchar(max) NOT NULL,
        [Date] datetime2 NOT NULL,
        [Weight] float NOT NULL,
        [IdWeightUnit] int NOT NULL,
        CONSTRAINT [PK_EmployeeFeedings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EmployeeFeedings_Animals_IdAnimal] FOREIGN KEY ([IdAnimal]) REFERENCES [Animals] ([Id]),
        CONSTRAINT [FK_EmployeeFeedings_WeightUnits_IdWeightUnit] FOREIGN KEY ([IdWeightUnit]) REFERENCES [WeightUnits] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240916192918_Adding_EmployeeFeeding'
)
BEGIN
    CREATE INDEX [IX_EmployeeFeedings_IdAnimal] ON [EmployeeFeedings] ([IdAnimal]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240916192918_Adding_EmployeeFeeding'
)
BEGIN
    CREATE INDEX [IX_EmployeeFeedings_IdWeightUnit] ON [EmployeeFeedings] ([IdWeightUnit]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240916192918_Adding_EmployeeFeeding'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240916192918_Adding_EmployeeFeeding', N'8.0.8');
END;
GO

COMMIT;
GO

