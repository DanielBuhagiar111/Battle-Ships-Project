CREATE DATABASE BattleShips;
GO

USE BattleShips;
GO

CREATE TABLE Players (
    ID INT IDENTITY(1,1)
	CONSTRAINT pl_plid_pk PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL 
	CONSTRAINT pl_us_un UNIQUE,
    Password NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Ships (
    ID INT IDENTITY(1,1)
	CONSTRAINT sh_shid_pk PRIMARY KEY,
    Title NVARCHAR(50) NOT NULL 
	CONSTRAINT sh_us_un UNIQUE,
    Size INT NOT NULL
);
GO

CREATE TABLE Games (
    ID INT IDENTITY(1,1)
	CONSTRAINT ga_gaid_pk PRIMARY KEY,
    Title NVARCHAR(50) NOT NULL,
	CreatorFK INT NOT NULL
	CONSTRAINT ga_crid_fk 
	REFERENCES Players (ID),
	OpponentFK INT NOT NULL
	CONSTRAINT ga_apid_fk 
	REFERENCES Players (ID),
    Complete BIT NOT NULL
);
GO

CREATE TABLE Attacks (
    ID INT IDENTITY(1,1)
	CONSTRAINT at_atid_pk PRIMARY KEY,
    Coordinate NVARCHAR(50) NOT NULL,
    Hit BIT NOT NULL,
	GameFK INT NOT NULL
	CONSTRAINT at_gaid_fk 
	REFERENCES Games (ID),
	PlayerFK INT NOT NULL
	CONSTRAINT at_plid_fk 
	REFERENCES Players (ID)
);
GO

CREATE TABLE GameShipConfigurations (
    ID INT IDENTITY(1,1)
	CONSTRAINT gsc_gscid_pk PRIMARY KEY,
    PlayerFK INT NOT NULL
	CONSTRAINT gsc_plid_fk 
	REFERENCES Players (ID),
    GameFK INT NOT NULL
	CONSTRAINT gsc_gaid_fk 
	REFERENCES Games (ID),
	Coordinate NVARCHAR(50) NOT NULL,
);
GO