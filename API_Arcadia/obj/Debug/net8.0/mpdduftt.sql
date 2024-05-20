CREATE TABLE [Diets] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Diets] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Habitats] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Habitats] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Healths] (
    [Id] int NOT NULL IDENTITY,
    [State] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Healths] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [OpeningHours] (
    [Id] int NOT NULL IDENTITY,
    [DayOfWeek] nvarchar(10) NOT NULL,
    [MorningOpening] time NULL,
    [MorningClosing] time NULL,
    [AfternoonOpening] time NULL,
    [AfternoonClosing] time NULL,
    CONSTRAINT [PK_OpeningHours] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [Pseudo] nvarchar(100) NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [IsValidated] bit NOT NULL,
    [Note] tinyint NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [SizeUnits] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    [Abbr] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_SizeUnits] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [WeightUnits] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    [Abbr] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_WeightUnits] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [ZooServices] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [FullPrice] real NULL,
    [ChildPrice] real NULL,
    CONSTRAINT [PK_ZooServices] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [HabitatImages] (
    [Id] int NOT NULL IDENTITY,
    [Slug] nvarchar(150) NOT NULL,
    [IdHabitat] int NOT NULL,
    CONSTRAINT [PK_HabitatImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HabitatImages_Habitats_IdHabitat] FOREIGN KEY ([IdHabitat]) REFERENCES [Habitats] ([Id]) ON DELETE CASCADE
);
GO


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
GO


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
GO


CREATE TABLE [SpeciesHabitats] (
    [IdSpecies] int NOT NULL,
    [IdHabitat] int NOT NULL,
    CONSTRAINT [PK_SpeciesHabitats] PRIMARY KEY ([IdHabitat], [IdSpecies]),
    CONSTRAINT [FK_SpeciesHabitats_Habitats_IdHabitat] FOREIGN KEY ([IdHabitat]) REFERENCES [Habitats] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SpeciesHabitats_Speciess_IdSpecies] FOREIGN KEY ([IdSpecies]) REFERENCES [Speciess] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AnimalImages] (
    [Id] int NOT NULL IDENTITY,
    [Slug] nvarchar(150) NOT NULL,
    [IdAnimal] int NOT NULL,
    CONSTRAINT [PK_AnimalImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnimalImages_Animals_IdAnimal] FOREIGN KEY ([IdAnimal]) REFERENCES [Animals] ([Id]) ON DELETE CASCADE
);
GO


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
GO


CREATE INDEX [IX_AnimalImages_IdAnimal] ON [AnimalImages] ([IdAnimal]);
GO


CREATE INDEX [IX_Animals_IdHealth] ON [Animals] ([IdHealth]);
GO


CREATE INDEX [IX_Animals_IdSpecies] ON [Animals] ([IdSpecies]);
GO


CREATE INDEX [IX_HabitatImages_IdHabitat] ON [HabitatImages] ([IdHabitat]);
GO


CREATE INDEX [IX_SpeciesHabitats_IdSpecies] ON [SpeciesHabitats] ([IdSpecies]);
GO


CREATE INDEX [IX_Speciess_IdDiet] ON [Speciess] ([IdDiet]);
GO


CREATE INDEX [IX_Speciess_IdSizeUnit] ON [Speciess] ([IdSizeUnit]);
GO


CREATE INDEX [IX_Speciess_IdWeightUnit] ON [Speciess] ([IdWeightUnit]);
GO


CREATE INDEX [IX_VetVisits_IdAnimal] ON [VetVisits] ([IdAnimal]);
GO


CREATE INDEX [IX_VetVisits_IdWeightUnit] ON [VetVisits] ([IdWeightUnit]);
GO


