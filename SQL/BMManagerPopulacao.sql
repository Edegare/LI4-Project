USE BMManager
GO

-- Funcionários
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
           ('João Belo',
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
           ('João Feio',
			'6@bmmanager.pt',
			'960000000',
			'senha6',
			'Recursos_Humanos',
			1)

-- Móveis
INSERT INTO Movel (Nome, Quantidade, Imagem) -- Na imagem colocar o path correto
	VALUES
	('Estante Branca 42x41x39', 0, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKallaxBranco42x41Modelo.png', SINGLE_BLOB) AS Imagem)),
	('Estante Branca 77x41x39', 0, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Modelo.png', SINGLE_BLOB) AS Imagem));

-- Materiais
INSERT INTO Material (Nome, Quantidade, Imagem)
	VALUES
	('Parafuso 10cm Metal', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\Parafuso10cm.png', SINGLE_BLOB) AS Imagem)),
	('Pino 5cm Metal', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\Pino5cm.png', SINGLE_BLOB) AS Imagem)),
	('Placa de Madeira 34x39x4 Branca', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\vermelho.png', SINGLE_BLOB) AS Imagem)),
	('Placa de Madeira 34x39x2 Branca', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\verde.png', SINGLE_BLOB) AS Imagem)),
	('Placa de Madeira 77x39x4 Branca', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\azul.png', SINGLE_BLOB) AS Imagem)),
	('Placa de Madeira 42x39x4 branca', 10, (SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\rosa.png', SINGLE_BLOB) AS Imagem));

-- Encomendas
INSERT INTO Encomenda (Cliente, Data_Prevista, Data_Real, Concluida)
	VALUES
	('João Silva', '2025-03-01', NULL, 0),
	('Maria Costa', '2025-04-15', NULL, 0),
	('Empresa Macedo', '2025-05-01', NULL, 0);

-- Etapas
INSERT INTO Etapa (Imagem, Numero, Proxima_Etapa, Movel)
VALUES
    -- Etapas para o Movel 1
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKallaxBranco42x41Etapa1.png', SINGLE_BLOB) AS Imagem), 1, 2, 1),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKallaxBranco42x41Etapa2.png', SINGLE_BLOB) AS Imagem), 2, 3, 1),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKallaxBranco42x41Etapa3.png', SINGLE_BLOB) AS Imagem), 3, NULL, 1),

    -- Etapas para o Movel 2
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Etapa1.png', SINGLE_BLOB) AS Imagem), 1, 5, 2),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Etapa2.png', SINGLE_BLOB) AS Imagem), 2, 6, 2),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Etapa3.png', SINGLE_BLOB) AS Imagem), 3, 7, 2),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Etapa4.png', SINGLE_BLOB) AS Imagem), 4, 8, 2),
    ((SELECT * FROM OPENROWSET(BULK N'C:\Users\Ana Sá Oliveira\Desktop\LI4-Project\Imagens\EstanteKALLAXBranco77x41Etapa5.png', SINGLE_BLOB) AS Imagem), 5, NULL, 2);

-- Móveis encomendados
INSERT INTO Encomenda_Precisa_Movel (Encomenda, Movel, Quantidade)
	VALUES
	(1, 1, 3), -- 3 Estantes únicas para João Silva
	(2, 2, 5), -- 5 Estantes duplas para Maria Costa
	(3, 1, 2); -- 2 Estantes únicas para Empresa Macedo

-- Material das etapas
INSERT INTO Etapa_Precisa_Material (Etapa, Material, Quantidade)
	VALUES
	(1, 1, 2), -- Etapa 1 da Estante única precisa de 2 unidades de Parafuso 10cm
	(1, 6, 1),
	(1, 3, 1),
	(2, 1, 2), -- Etapa 2 de Estante única precisa de 2 unidades de Parafuso 10cm
	(2, 3, 1),
	(3, 1, 4), -- Etapa 3 de Estante única precisa de 4 unidades de Parafuso 10cm
	(3, 6, 1),

	(4, 1, 2),-- Etapa 1 da Estante dupla precisa de 2 unidades de Parafuso 10cm
	(4, 5, 1),
	(4, 3, 1),
	(5, 2, 2), -- Etapa 2 da Estante dupla precisa de 2 unidades de Pino5cm
	(5, 4, 1),
	(6, 1, 2),
	(6, 3, 1),
	(7, 2, 2),
	(8, 1, 4),
	(8, 5, 1);

-- Montagens
INSERT INTO Montagem (Data_Inicial, Data_Final, Estado, Movel, Etapa, Encomenda)
VALUES
	('2025-01-21 08:00:00', NULL, 'Em_Pausa', 1, 1, NULL),
	('2025-01-21 10:00:00', NULL, 'Em_Pausa', 1, 1, NULL),
	('2025-01-21 08:00:00', NULL, 'Em_Pausa', 2, 4, NULL);

INSERT INTO Funcionario_Participa_Montagem (Montagem, Funcionario)
VALUES
	(1, 4), -- João Belo participa na montagem 1
	(2, 4), -- João Belo participa na montagem 2
	(3, 4); -- João Belo participa na montagem 3

