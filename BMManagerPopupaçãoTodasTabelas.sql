USE BMManager

-- 1. Inserir Movel
INSERT INTO [dbo].[Movel] ([Nome], [Quantidade], [Imagem]) 
VALUES 
    ('Sofá', 10, NULL),
    ('Cadeira', 20, NULL);

-- 2. Inserir Material
INSERT INTO [dbo].[Material] ([Nome], [Quantidade], [Imagem]) 
VALUES 
    ('Parafuso', 500, NULL),
    ('Prego', 300, NULL),
    ('Madeira', 100, NULL),
    ('Tinta', 50, NULL),
    ('Martelo', 10, NULL);

-- 3. Inserir Etapas
DECLARE @Codigo_Etapa INT;

-- Inserir Etapa 1
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 4, NULL, 1);
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 1

-- Inserir Etapa 2
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 3, @Codigo_Etapa, 1);  -- Proxima_Etapa = Codigo da Etapa 1
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 2

-- Inserir Etapa 3
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 2, @Codigo_Etapa, 1);  -- Proxima_Etapa = Codigo da Etapa 2
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 3

-- Inserir Etapa 4
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 1, @Codigo_Etapa, 1);  -- Proxima_Etapa = Codigo da Etapa 3
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 4

-- Inserir Etapa 5
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 4, NULL, 2);  -- Etapa 5, sem próxima etapa, associada ao Movel 2 (Cadeira)
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 5

-- Inserir Etapa 6
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 3, @Codigo_Etapa, 2);  -- Proxima_Etapa = Codigo da Etapa 5
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 6

-- Inserir Etapa 7
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 2, @Codigo_Etapa, 2);  -- Proxima_Etapa = Codigo da Etapa 6
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 7

-- Inserir Etapa 8
INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
VALUES (NULL, 1, @Codigo_Etapa, 2);  -- Proxima_Etapa = Codigo da Etapa 7
SET @Codigo_Etapa = SCOPE_IDENTITY();  -- Captura o Código da Etapa 8

-- 4. Inserir Etapa_Precisa_Material
INSERT INTO [dbo].[Etapa_Precisa_Material] ([Etapa], [Material], [Quantidade]) 
VALUES 
    (1, 1, 200),  -- Etapa 1 precisa de 200 Parafusos
    (1, 2, 100),  -- Etapa 1 precisa de 100 Pregos
    (2, 5, 10),   -- Etapa 2 precisa de 10 Martelos
    (3, 3, 150),  -- Etapa 3 precisa de 150 Madeiras
    (4, 2, 400),  -- Etapa 4 precisa de 400 Pregos
    (5, 4, 50),   -- Etapa 5 precisa de 50 Litros de Tinta
    (6, 1, 100),  -- Etapa 6 precisa de 100 Parafusos
    (7, 3, 50),   -- Etapa 7 precisa de 50 Madeiras
    (8, 5, 10);   -- Etapa 8 precisa de 50 Martelos

-- 5. Inserir Encomenda
INSERT INTO [dbo].[Encomenda] ([Cliente], [Data_Prevista], [Data_Real], [Concluida]) 
VALUES 
    ('Cliente A', '2025-02-01', NULL, 0),  -- Encomenda 1 (não concluída)
    ('Cliente B', '2025-03-01', '2025-02-20', 1);  -- Encomenda 2 (concluída)

-- 6. Inserir Encomenda_Precisa_Movel
INSERT INTO [dbo].[Encomenda_Precisa_Movel] ([Encomenda], [Movel], [Quantidade]) 
VALUES 
    (1, 1, 5),  -- Encomenda 1 precisa de 5 Sofás
    (1, 2, 10), -- Encomenda 1 precisa de 10 Cadeiras
    (2, 2, 15); -- Encomenda 2 precisa de 15 Cadeiras

-- 7. Inserir Montagem
INSERT INTO [dbo].[Montagem] ([Data_Inicial], [Estado], [Etapa_Concluida], [Movel], [Etapa], [Encomenda]) 
VALUES 
    ('2025-01-10', 'Em_Progresso', 0, 1, 1, 1),  -- Montagem 1, Sofá, Etapa 1, Encomenda 1
    ('2025-01-12', 'Pendente', 0, 2, 8, 2);      -- Montagem 2, Cadeira, Etapa 2, Encomenda 2

INSERT INTO Funcionario (Nome,Email,Telefone,Senha,Equipa,Conta_Ativa)
VALUES
	('Nome1','nome1@bmmanager.pt','930000000','senha1','Encomendas',1),
	('Nome2','nome2@bmmanager.pt','920000000','senha2','Modelacao',1);

-- 8. Inserir Funcionario_Participa_Montagem
INSERT INTO [dbo].[Funcionario_Participa_Montagem] ([Montagem], [Funcionario]) 
VALUES 
    (1, 1),  -- Funcionario 1 participa da Montagem 1
    (2, 2);  -- Funcionario 2 participa da Montagem 2
