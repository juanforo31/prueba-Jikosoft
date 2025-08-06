-- Se realiza la creación de la base de datos
CREATE DATABASE Blogs;

-- Se dice la base de datos a utilizar
USE Blogs;

-- Creación de la tabla de los usuarios
CREATE TABLE Users (
    UserId int PRIMARY KEY NOT NULL IDENTITY(1,1),
    FirstName varchar(30) NOT NULL,
    LastName varchar(30) NOT NULL,
    Username varchar(30) NOT NULL,
	Password varchar(20) NOT NULL,
);

-- Creación de la tabla de las etiquetas
CREATE TABLE Tags (
    TagId int PRIMARY KEY NOT NULL IDENTITY(1,1),
    Nametag varchar(30) NOT NULL,
    DescriptionTag varchar(150) NOT NULL,
    ColorTag varchar(8) NOT NULL,
);

-- Creación de la tabla de las etiquetas
CREATE TABLE PublishStatus (
    PublishStatusId int PRIMARY KEY NOT NULL IDENTITY(1,1),
    PublishStatusName varchar(30) NOT NULL,
    PublishStatusDescription varchar(150) NOT NULL,
);

-- Creación de la tabla de las publicaciones
CREATE TABLE Publishing (
    PublishingId int PRIMARY KEY NOT NULL IDENTITY(1,1),
    PublishingName varchar(50) NOT NULL,
    Description text NOT NULL,
	PublishingDate dateTime not null,
	PublishStatusId int not null,
    TagId int NULL,
    UserId int NOT NULL,
	
	FOREIGN KEY (PublishStatusId) REFERENCES PublishStatus(PublishStatusId),
	FOREIGN KEY (TagId) REFERENCES Tags(TagId),
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Creación de la tabla de los comentarios
CREATE TABLE Comments (
    CommentId int PRIMARY KEY NOT NULL IDENTITY(1,1),
    Comment text NOT NULL,
    UserId int NOT NULL,
	PublishingId int NOT NULL,

	FOREIGN KEY (UserId) REFERENCES Users(UserId),
	FOREIGN KEY (PublishingId) REFERENCES Publishing(PublishingId)
);