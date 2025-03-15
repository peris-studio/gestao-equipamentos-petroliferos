CREATE TABLE public."HistoricoFalhas" (
    "Id" UUID PRIMARY KEY,  
    "DataFalha" TIMESTAMP NOT NULL,                  
    "Descricao" TEXT NOT NULL,
    "CausaProvavel" TEXT NOT NULL,
    "AcaoCorretiva" TEXT NOT NULL,
    "TempoParado" INTERVAL NOT NULL,
    "Responsavel" VARCHAR(100) NOT NULL,              
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL DEFAULT TRUE,
    "EquipamentoId" UUID NOT NULL, 
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
);

-- Exemplo de inserção
INSERT INTO public."HistoricoFalhas" (  "Id",
                                        "DataFalha",
                                        "Descricao",
                                        "CausaProvavel",
                                        "AcaoCorretiva",
                                        "TempoParado",
                                        "Responsavel",
                                        "EquipamentoId",
                                        "DataCriacao",
                                        "Ativo",
                                        "EquipamentoId") 
VALUES 
(
    '',
    '2024-05-15 14:30:00',
    'Vazamento no selo mecânico',
    'Desgaste por fadiga do material',
    'Substituição do selo e lubrificação',
    '2 horas 30 minutos',  
    'Eng. João Silva',
    '',  -- ID de um equipamento existente
    CURRENT_TIMESTAMP,
    TRUE,
    ''
),
(
    '',
    '2024-06-01 09:15:00',
    'Superaquecimento do motor',
    'Falha no sistema de refrigeração',
    'Limpeza das aletas e troca do fluido',
    '8:00:00',  -- Formato ISO (8 horas)
    'Técnica Maria Oliveira',
    '',  -- ID de outro equipamento
    CURRENT_TIMESTAMP,
    TRUE,
    ''
);