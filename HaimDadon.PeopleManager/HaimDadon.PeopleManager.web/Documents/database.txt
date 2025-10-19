CREATE TABLE [dbo].[Person]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    [FullName] NVARCHAR(30) NOT NULL
        CHECK (LEN([FullName]) BETWEEN 2 AND 30),

    [Phone] NVARCHAR(10) NOT NULL UNIQUE
        CHECK ([Phone] LIKE '05%' AND LEN([Phone]) = 10),

    [Email] NVARCHAR(100) NOT NULL UNIQUE
        CHECK ([Email] LIKE '_%@_%._%'),

    [ImagePath] NVARCHAR(255) NULL
        CHECK ([ImagePath] LIKE '%.png' OR [ImagePath] LIKE '%.jpg')
);
