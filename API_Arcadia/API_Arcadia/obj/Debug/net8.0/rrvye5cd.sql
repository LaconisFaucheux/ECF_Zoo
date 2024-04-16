CREATE TABLE [AnimalImages] (
    [Id] int NOT NULL IDENTITY,
    [Slug] nvarchar(150) NOT NULL,
    [IdAnimal] int NOT NULL,
    CONSTRAINT [PK_AnimalImages] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Animals] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [IsMale] bit NOT NULL,
    [IdSpecies] int NOT NULL,
    [IdHealth] tinyint NOT NULL,
    CONSTRAINT [PK_Animals] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Diets] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Diets] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [HabitatImages] (
    [Id] int NOT NULL IDENTITY,
    [Slug] nvarchar(150) NOT NULL,
    [IdHabitat] int NOT NULL,
    CONSTRAINT [PK_HabitatImages] PRIMARY KEY ([Id])
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
    [MorningOpening] time NOT NULL,
    [MorningClosing] time NOT NULL,
    [AfternoonOpening] time NOT NULL,
    [AfternoonClosing] time NOT NULL,
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
    [Id] tinyint NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [Abbr] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_SizeUnits] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [SpeciesHabitats] (
    [IdSpecies] int NOT NULL,
    [IdHabitat] int NOT NULL,
    CONSTRAINT [PK_SpeciesHabitats] PRIMARY KEY ([IdHabitat], [IdSpecies])
);
GO


CREATE TABLE [Speciess] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [ScientificName] nvarchar(150) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MaleMaxSize] real NOT NULL,
    [FemaleMaxSize] real NOT NULL,
    [MaleMaxWeight] real NOT NULL,
    [FemaleMaxWeight] real NOT NULL,
    [IdSizeUnit] tinyint NOT NULL,
    [IdWeightUnit] tinyint NOT NULL,
    [Lifespan] tinyint NOT NULL,
    [IdDiet] tinyint NOT NULL,
    CONSTRAINT [PK_Speciess] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [VetVisits] (
    [Id] int NOT NULL IDENTITY,
    [Food] nvarchar(200) NOT NULL,
    [FoodWeight] real NOT NULL,
    [IdWeightUnit] tinyint NOT NULL,
    [VisitDate] datetime2 NOT NULL,
    [Observations] nvarchar(max) NOT NULL,
    [IdAnimal] int NOT NULL,
    [IdVet] int NULL,
    CONSTRAINT [PK_VetVisits] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [WeightUnits] (
    [Id] tinyint NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [Abbr] nvarchar(2) NOT NULL,
    CONSTRAINT [PK_WeightUnits] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [ZooServices] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [FullPrice] real NOT NULL,
    [ChildPrice] real NOT NULL,
    CONSTRAINT [PK_ZooServices] PRIMARY KEY ([Id])
);
GO


