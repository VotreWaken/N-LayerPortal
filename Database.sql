use [master];
go

if db_id('PortalMusic') is not null
begin
 drop database PortalMusic;
end
go

create database PortalMusic;
go

use PortalMusic;
go

-- Image
CREATE TABLE [Image] (
    Id INT PRIMARY KEY Identity(1, 1),
    Path VARCHAR(255)
);

-- User
CREATE TABLE [User] (
    Id INT PRIMARY KEY Identity(1, 1),
    Login VARCHAR(255),
    Password VARCHAR(255),
    Salt VARCHAR(255),
    ImageId INT FOREIGN KEY REFERENCES [Image](Id),
    IsAdmin BIT,
    IsAuth BIT
);

-- Audio
CREATE TABLE Audio (
    Id INT PRIMARY KEY Identity(1, 1),
    Path VARCHAR(255),
    Title VARCHAR(255),
    Author VARCHAR(255),
    ImageId INT FOREIGN KEY REFERENCES [Image](Id)
);

-- Genre
CREATE TABLE Genre (
    Id INT PRIMARY KEY Identity(1, 1),
    Name VARCHAR(255)
);

-- AudioGenre ( Many To Many Audio & Genre )
CREATE TABLE AudioGenre (
    AudioId INT FOREIGN KEY REFERENCES Audio(Id),
    GenreId INT FOREIGN KEY REFERENCES Genre(Id),
    PRIMARY KEY (AudioId, GenreId)
);