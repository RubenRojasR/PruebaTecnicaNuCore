CREATE DATABASE IncidenciasDB;

USE IncidenciasDB;
CREATE TABLE Category (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE Responsible (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE Incident (
	Id INT AUTO_INCREMENT PRIMARY KEY,
    Code VARCHAR(20) NOT NULL,
    Title VARCHAR(100) NOT NULL,
    Description VARCHAR(500),
    Priority INT NOT NULL,
    Status INT NOT NULL,
    SolutionComment VARCHAR(500),
    CreatedDate DATETIME,
    UpdatedDate DATETIME,
    CategoryId INT,
    ResponsibleId INT,
    CONSTRAINT UQ_Incident_Code UNIQUE (Code),

    CONSTRAINT FK_Incident_Category
        FOREIGN KEY (CategoryId)
        REFERENCES Category(Id)
        ON DELETE RESTRICT,

    CONSTRAINT FK_Incident_Responsible
        FOREIGN KEY (ResponsibleId)
        REFERENCES Responsible(Id)
        ON DELETE RESTRICT,

    CONSTRAINT CHK_Incident_Priority
        CHECK (Priority BETWEEN 1 AND 4),

    CONSTRAINT CHK_Incident_Status
        CHECK (Status BETWEEN 1 AND 4)
);

CREATE TABLE IncidentHistory(
	Id INT AUTO_INCREMENT PRIMARY KEY,
    IncidentId INT,
    PreviousStatus INT,
    CurrentStatus INT NOT NULL,
    UpdatedDate DATETIME,
    Comment VARCHAR(500),
    
	CONSTRAINT FK_IncidentHistory_Incident
	FOREIGN KEY (IncidentId)
	REFERENCES Incident(Id)
	ON DELETE RESTRICT
);


-- valores de ejemplo
INSERT INTO category (Name, Description) VALUES ('Hardware','Problemas en el equipo fisico');
INSERT INTO category (Name, Description) VALUES ('Software','Problemas en software del equipo');
INSERT INTO Responsible (Name, Email) VALUES ('Ruben Rojas','rubenrojasroman96@gmail.com');

