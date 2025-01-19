-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BMManager')
BEGIN
    CREATE DATABASE BMManager;
END
GO

-- Switch to the created database
USE BMManager;

-- Create table Funcionario
IF OBJECT_ID('Funcionario', 'U') IS NOT NULL 
  DROP TABLE Funcionario;

CREATE TABLE Funcionario (
    Codigo_Utilizador INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(75) NOT NULL,
    Email VARCHAR(30) NOT NULL,
    Telefone CHAR(9),
    Senha VARCHAR(45) NOT NULL,
    Equipa VARCHAR(16) NOT NULL CHECK (Equipa IN ('Administracao', 'Modelacao', 'Armazem', 'Recursos_humanos', 'Montagem', 'Encomendas')),
    Conta_Ativa BIT NOT NULL
);

-- Create table Encomenda
IF OBJECT_ID('Encomenda', 'U') IS NOT NULL 
  DROP TABLE Encomenda;

CREATE TABLE Encomenda (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    Cliente VARCHAR(75) NOT NULL,
    Data_Prevista DATE NOT NULL,
    Data_Real DATE,
    Concluida BIT NOT NULL
);

-- Create table Movel
IF OBJECT_ID('Movel', 'U') IS NOT NULL 
  DROP TABLE Movel;

CREATE TABLE Movel (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(75) NOT NULL,
    Quantidade INT NOT NULL DEFAULT 0,
    Imagem VARBINARY(MAX)
);

-- Create table Etapa
IF OBJECT_ID('Etapa', 'U') IS NOT NULL 
  DROP TABLE Etapa;

CREATE TABLE Etapa (
    Codigo_Etapa INT IDENTITY(1,1) PRIMARY KEY,
    Imagem VARBINARY(MAX),
    Numero INT NOT NULL,
    Proxima_Etapa INT,
    Movel INT NOT NULL,
    FOREIGN KEY (Movel) REFERENCES Movel(Numero),
    FOREIGN KEY (Proxima_Etapa) REFERENCES Etapa(Codigo_Etapa)
);

-- Create table Material
IF OBJECT_ID('Material', 'U') IS NOT NULL 
  DROP TABLE Material;

CREATE TABLE Material (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(75) NOT NULL,
    Quantidade INT NOT NULL,
    Imagem VARBINARY(MAX)
);

-- Create table Encomenda_Precisa_Movel
IF OBJECT_ID('Encomenda_Precisa_Movel', 'U') IS NOT NULL 
  DROP TABLE Encomenda_Precisa_Movel;

CREATE TABLE Encomenda_Precisa_Movel (
    Encomenda INT NOT NULL,
    Movel INT NOT NULL,
    Quantidade INT NOT NULL,
    PRIMARY KEY (Encomenda, Movel),
    FOREIGN KEY (Encomenda) REFERENCES Encomenda(Numero),
    FOREIGN KEY (Movel) REFERENCES Movel(Numero)
);

-- Create table Etapa_Precisa_Material
IF OBJECT_ID('Etapa_Precisa_Material', 'U') IS NOT NULL 
  DROP TABLE Etapa_Precisa_Material;

CREATE TABLE Etapa_Precisa_Material (
    Etapa INT NOT NULL,
    Material INT NOT NULL,
    Quantidade INT NOT NULL,
    PRIMARY KEY (Etapa, Material),
    FOREIGN KEY (Etapa) REFERENCES Etapa(Codigo_Etapa),
    FOREIGN KEY (Material) REFERENCES Material(Numero)
);

-- Create table Montagem
IF OBJECT_ID('Montagem', 'U') IS NOT NULL 
  DROP TABLE Montagem;

CREATE TABLE Montagem (
    Numero INT IDENTITY(1,1) PRIMARY KEY,
    Data_Inicial DATETIME NOT NULL,
    Data_Final DATETIME,
    Duracao TIME,
    Estado VARCHAR(15) NOT NULL CHECK (Estado IN ('Pendente', 'Em_Progresso', 'Concluida')),
    Etapa_Concluida BIT NOT NULL,
    Movel INT NOT NULL,
    Etapa INT NOT NULL,
    Encomenda INT NOT NULL,
    FOREIGN KEY (Movel) REFERENCES Movel(Numero),
    FOREIGN KEY (Etapa) REFERENCES Etapa(Codigo_Etapa),
    FOREIGN KEY (Encomenda) REFERENCES Encomenda(Numero)
);

-- Create table Funcionario_Participa_Montagem
IF OBJECT_ID('Funcionario_Participa_Montagem', 'U') IS NOT NULL 
  DROP TABLE Funcionario_Participa_Montagem;

CREATE TABLE Funcionario_Participa_Montagem (
    Montagem INT NOT NULL,
    Funcionario INT NOT NULL,
    PRIMARY KEY (Montagem, Funcionario),
    FOREIGN KEY (Montagem) REFERENCES Montagem(Numero),
    FOREIGN KEY (Funcionario) REFERENCES Funcionario(Codigo_Utilizador)
);