USE BMManager

SELECT e.Codigo_Etapa, e.Imagem, e.Numero, e.Proxima_Etapa, e.Movel, m.Numero, m.Nome, m.Quantidade, m.Imagem, em.Quantidade
	FROM Etapa e
		LEFT JOIN Etapa_Precisa_Material em ON e.Codigo_Etapa = em.Etapa
			LEFT JOIN Material m ON m.Numero = em.Material
				WHERE e.Codigo_Etapa = 1;

SELECT e.Codigo_Etapa, e.Imagem, e.Numero, e.Proxima_Etapa, e.Movel, m.Numero AS NumeroMaterial, m.Nome, m.Quantidade, m.Imagem, em.Quantidade AS QuantidadeNecessaria
	                        FROM Etapa e
		                        LEFT JOIN Etapa_Precisa_Material em ON e.Codigo_Etapa = em.Etapa
			                        LEFT JOIN Material m ON m.Numero = em.Material;
