USE BMManager;

-- 1. Inserir Movel
INSERT INTO [dbo].[Movel] ([Nome], [Quantidade], [Imagem]) 
VALUES 
    ('Sofá', 10, NULL),
    ('Cadeira', 20, NULL),
    ('Mesa', 15, NULL),
    ('Cama', 25, NULL),
    ('Poltrona', 12, NULL),
    ('Banco', 30, NULL),
    ('Estante', 8, NULL),
    ('Armário', 18, NULL),
    ('Rack', 22, NULL),
    ('Cômoda', 5, NULL),
    ('Sofá-cama', 10, NULL),
    ('Mesa de Jantar', 6, NULL),
    ('Mesa de Escritório', 10, NULL),
    ('Cadeira de Escritório', 20, NULL),
    ('Cadeira de Rodas', 7, NULL),
    ('Mesa de Centro', 30, NULL),
    ('Cabeceira de Cama', 20, NULL),
    ('Estante de TV', 17, NULL),
    ('Luminária', 25, NULL),
    ('Cadeira de Amamentação', 6, NULL),
    ('Prateleira', 18, NULL),
    ('Sofá de 2 Lugares', 10, NULL),
    ('Sofá de 3 Lugares', 14, NULL),
    ('Cadeira de Jantar', 30, NULL),
    ('Pufe', 12, NULL),
    ('Rack para TV', 15, NULL),
    ('Guarda-Roupa', 20, NULL),
    ('Mesa Auxiliar', 9, NULL),
    ('Mesinha de Cabeceira', 18, NULL),
    ('Poltrona Reclinável', 10, NULL),
    ('Mesa de Café', 11, NULL),
    ('Cadeira de Cozinha', 25, NULL),
    ('Sofá de Couro', 13, NULL),
    ('Sofá de Tecido', 17, NULL),
    ('Mesa de Bar', 14, NULL),
    ('Cama de Casal', 15, NULL),
    ('Cama de Solteiro', 12, NULL),
    ('Banco de Madeira', 30, NULL),
    ('Sofá Modular', 8, NULL),
    ('Mesa de Luz', 9, NULL),
    ('Poltrona de Veludo', 10, NULL),
    ('Estante de Livros', 14, NULL),
    ('Mesa de Trabalho', 16, NULL),
    ('Prateleira de Vidro', 7, NULL),
    ('Cadeira Dobrável', 13, NULL),
    ('Sofá de Jardim', 9, NULL),
    ('Cadeira de Bar', 11, NULL),
    ('Mesa de Escritório Moderno', 5, NULL),
    ('Sofá de Lona', 18, NULL);

-- 2. Inserir Material
INSERT INTO [dbo].[Material] ([Nome], [Quantidade], [Imagem]) 
VALUES 
    ('Parafuso', 500, NULL),
    ('Prego', 300, NULL),
    ('Madeira', 100, NULL),
    ('Tinta', 50, NULL),
    ('Martelo', 10, NULL),
    ('Chave Inglesa', 120, NULL),
    ('Pregador', 400, NULL),
    ('Papelão', 200, NULL),
    ('Lixa', 75, NULL),
    ('Cola', 150, NULL),
    ('Fita Métrica', 180, NULL),
    ('Broca', 50, NULL),
    ('Serra', 30, NULL),
    ('Parafuso 3x8', 250, NULL),
    ('Cimento', 500, NULL),
    ('Areia', 1000, NULL),
    ('Cal', 800, NULL),
    ('Tinta Acrílica', 200, NULL),
    ('Silicone', 120, NULL),
    ('Arame', 250, NULL),
    ('Ferramentas Diversas', 100, NULL),
    ('Trena', 150, NULL),
    ('Escada', 30, NULL),
    ('Alicate', 80, NULL),
    ('Corda', 200, NULL),
    ('Saco de Areia', 50, NULL),
    ('Massa Corrida', 60, NULL),
    ('Móveis Prontos', 70, NULL),
    ('Cimento Rápido', 200, NULL),
    ('Plástico Bolha', 300, NULL),
    ('Espuma', 150, NULL),
    ('Chave de Fenda', 100, NULL),
    ('Dobradiça', 200, NULL),
    ('Travesseiro', 50, NULL),
    ('Carro de Mão', 25, NULL),
    ('Papel de Lixa', 80, NULL),
    ('Óleo para Madeira', 50, NULL),
    ('Peças de Reposição', 120, NULL),
    ('Cordas de Nylon', 70, NULL),
    ('Rodízio', 200, NULL),
    ('Parafuso Auto-atarraxante', 400, NULL),
    ('Cinta para Movel', 250, NULL),
    ('Manta Acrílica', 60, NULL),
    ('Espelho', 80, NULL),
    ('Placa de MDF', 150, NULL),
    ('Cabo de Aço', 50, NULL),
    ('Reboco', 300, NULL),
    ('Argamassa', 500, NULL);

-- 3. Inserir Etapas para cada Movel (4 etapas por movel)
DECLARE @Codigo_Etapa INT;
DECLARE @Movel INT;
DECLARE @NumeroEtapa INT;

-- Loop para cada móvel
SET @Movel = 1;
WHILE @Movel <= 50
BEGIN
    SET @NumeroEtapa = 4;
    
    -- Inserir 4 etapas para cada móvel
    WHILE @NumeroEtapa > 0
    BEGIN
        INSERT INTO [dbo].[Etapa] ([Imagem], [Numero], [Proxima_Etapa], [Movel]) 
        VALUES (NULL, @NumeroEtapa, NULL, @Movel);
        SET @Codigo_Etapa = SCOPE_IDENTITY();
        
        -- Atualiza a próxima etapa
        IF @NumeroEtapa < 4
        BEGIN
            UPDATE [dbo].[Etapa] 
            SET [Proxima_Etapa] = @Codigo_Etapa 
            WHERE [Codigo_Etapa] = @Codigo_Etapa - 1;
        END
        
        SET @NumeroEtapa = @NumeroEtapa - 1;
    END
    
    SET @Movel = @Movel + 1;
END;

-- 4. Inserir Etapa_Precisa_Material (com 2 materiais distintos por etapa)
DECLARE @EtapaAtual INT = 1;  -- Variável para controle da etapa
DECLARE @Material1 INT, @Material2 INT;  -- Variáveis para os materiais

WHILE @EtapaAtual <= 50
BEGIN
    -- Gerar dois materiais distintos aleatoriamente
    SET @Material1 = (ABS(CHECKSUM(NEWID())) % 50) + 1;
    SET @Material2 = (ABS(CHECKSUM(NEWID())) % 50) + 1;

    -- Garantir que os dois materiais sejam diferentes
    WHILE @Material1 = @Material2
    BEGIN
        SET @Material2 = (ABS(CHECKSUM(NEWID())) % 50) + 1;
    END

    -- Inserir dois materiais distintos para a etapa
    INSERT INTO [dbo].[Etapa_Precisa_Material] ([Etapa], [Material], [Quantidade]) 
    VALUES 
        (@EtapaAtual, @Material1, (ABS(CHECKSUM(NEWID())) % 100) + 1),  -- Material 1
        (@EtapaAtual, @Material2, (ABS(CHECKSUM(NEWID())) % 100) + 1);  -- Material 2

    -- Incrementar a etapa
    SET @EtapaAtual = @EtapaAtual + 1;
END;

-- 5. Inserir Encomenda
DECLARE @Cliente INT = 1;
DECLARE @Data_Prevista DATE = '2025-01-01';
WHILE @Cliente <= 50
BEGIN
    INSERT INTO [dbo].[Encomenda] ([Cliente], [Data_Prevista], [Data_Real], [Concluida]) 
    VALUES 
        (CONCAT('Cliente ', @Cliente), @Data_Prevista, NULL, 0);
    SET @Cliente = @Cliente + 1;
    SET @Data_Prevista = DATEADD(DAY, 10, @Data_Prevista);
END;

-- 6. Inserir Encomenda_Precisa_Movel
DECLARE @Encomenda INT = 1;
DECLARE @MovelReferencia INT = 1;
WHILE @Encomenda <= 50
BEGIN
    INSERT INTO [dbo].[Encomenda_Precisa_Movel] ([Encomenda], [Movel], [Quantidade]) 
    VALUES 
        (@Encomenda, (@MovelReferencia % 10) + 1, ABS(CHECKSUM(NEWID())) % 50 + 1);
    SET @Encomenda = @Encomenda + 1;
    SET @MovelReferencia = @MovelReferencia + 1;
END;

-- 7. Inserir Montagem
DECLARE @Montagem INT = 1;
WHILE @Montagem <= 50
BEGIN
    INSERT INTO [dbo].[Montagem] ([Data_Inicial], [Duracao], [Estado], [Etapa_Concluida], [Movel], [Etapa], [Encomenda]) 
    VALUES 
        ('2025-01-01', '00:00:00', 'Em_Progresso', 0, 1, @Montagem, @Montagem);
    SET @Montagem = @Montagem + 1;
END;

-- 8. Inserir Funcionario
DECLARE @Funcionario INT = 1;
WHILE @Funcionario <= 50
BEGIN
    INSERT INTO [dbo].[Funcionario] (Nome, Email, Telefone, Senha, Equipa, Conta_Ativa) 
    VALUES 
        (CONCAT('Funcionario ', @Funcionario), CONCAT('email', @Funcionario, '@bmmanager.pt'), '930000000', 'senha', 'Encomendas', 1);
    SET @Funcionario = @Funcionario + 1;
END;

-- 9. Inserir Funcionario_Participa_Montagem
DECLARE @Funcionario_Montagem INT = 1;
DECLARE @Montagem_Participacao INT = 1;
WHILE @Funcionario_Montagem <= 50
BEGIN
    INSERT INTO [dbo].[Funcionario_Participa_Montagem] ([Montagem], [Funcionario]) 
    VALUES 
        (@Montagem_Participacao, @Funcionario_Montagem);
    SET @Funcionario_Montagem = @Funcionario_Montagem + 1;
    SET @Montagem_Participacao = @Montagem_Participacao + 1;
END;
