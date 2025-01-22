USE BMManager
GO

-- Funcion�rios
INSERT INTO Funcionario
           (Nome,
			Email,
			Telefone,
			Senha,
			Equipa,
			Conta_Ativa)
     VALUES
           ('Anabela Silva',
			'1@bmmanager.pt',
			'930000000',
			'senha1',
			'Administracao',
			1),
           ('Bela Rodrigues',
			'2@bmmanager.pt',
			'920000000',
			'senha2',
			'Encomendas',
			1),
           ('Isabela Costa',
			'3@bmmanager.pt',
			'910000000',
			'senha3',
			'Modelacao',
			1),
           ('Jo�o Belo',
			'4@bmmanager.pt',
			'940000000',
			'senha4',
			'Montagem',
			1),
           ('Florbela Lopes',
			'5@bmmanager.pt',
			'950000000',
			'senha5',
			'Armazem',
			1),
           ('Jo�o Feio',
			'nome6@bmmanager.pt',
			'960000000',
			'senha6',
			'Recursos_Humanos',
			1);

-- M�veis
INSERT INTO Movel (Nome, Quantidade, Imagem) -- As imagens s�o configuradas como NULL
	VALUES
	('Estante Branco 42X41', 5, NULL),
	('Estante Branco 77X41', 5, NULL);

-- Materiais
INSERT INTO Material (Nome, Quantidade, Imagem) -- As imagens s�o configuradas como NULL
	VALUES
	('Parafuso 10cm Metal', 10, NULL),
	('Pino 5cm Metal', 10, NULL);

-- Encomendas
INSERT INTO Encomenda (Cliente, Data_Prevista, Data_Real, Concluida)
	VALUES
	('Jo�o Silva', '2025-03-01', NULL, 0),
	('Maria Costa', '2025-04-15', NULL, 0),
	('Empresa Macedo', '2025-05-01', NULL, 0);

-- Etapas
INSERT INTO Etapa (Imagem, Numero, Proxima_Etapa, Movel) -- As imagens s�o configuradas como NULL
VALUES
    -- Etapas para o Movel 1
    (NULL, 1, 2, 1),
    (NULL, 2, 3, 1),
    (NULL, 3, NULL, 1),

    -- Etapas para o Movel 2
    (NULL, 1, 5, 2),
    (NULL, 2, 6, 2),
    (NULL, 3, 7, 2),
    (NULL, 4, 8, 2),
    (NULL, 5, NULL, 2);

-- M�veis encomendados
INSERT INTO Encomenda_Precisa_Movel (Encomenda, Movel, Quantidade)
	VALUES
	(1, 1, 3), -- 3 Estantes �nicas para Jo�o Silva
	(2, 2, 5), -- 5 Estantes duplas para Maria Costa
	(3, 1, 2); -- 2 Estantes �nicas para Empresa Macedo

-- Material das etapas
INSERT INTO Etapa_Precisa_Material (Etapa, Material, Quantidade)
	VALUES
	(1, 1, 2), -- Etapa 1 da Estante �nica precisa de 2 unidades de Parafuso 10cm
	(2, 1, 2), -- Etapa 2 de Estante �nica precisa de 2 unidades de Parafuso 10cm
	(3, 1, 4), -- Etapa 3 de Estante �nica precisa de 4 unidades de Parafuso 10cm

	(4, 1, 2), -- Etapa 1 da Estante dupla precisa de 2 unidades de Parafuso 10cm
	(5, 2, 2), -- Etapa 2 da Estante dupla precisa de 2 unidades de Pino5cm
	(6, 1, 2),
	(7, 2, 2),
	(8, 1, 4);

-- Montagens
INSERT INTO Montagem (Data_Inicial, Data_Final, Estado, Movel, Etapa, Encomenda)
VALUES
	('2025-01-21 08:00:00', NULL, 'Em_Pausa', 1, 1, NULL),
	('2025-01-21 10:00:00', NULL, 'Em_Pausa', 1, 1, NULL),
	('2025-01-21 08:00:00', NULL, 'Em_Pausa', 2, 1, NULL);

-- Funcion�rios participando nas montagens
INSERT INTO Funcionario_Participa_Montagem (Montagem, Funcionario)
VALUES
	(1, 4), -- Jo�o Belo participa na montagem 1
	(2, 4), -- Jo�o Belo participa na montagem 2
	(3, 4); -- Jo�o Belo participa na montagem 3
