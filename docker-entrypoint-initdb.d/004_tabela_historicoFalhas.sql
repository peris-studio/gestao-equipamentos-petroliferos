CREATE TABLE public."HistoricoFalhas" (
    "Id" UUID PRIMARY KEY,  
    "DataFalha" TIMESTAMP NOT NULL,                  
    "Descricao" TEXT NOT NULL,
    "CausaProvavel" TEXT NOT NULL,
    "AcaoCorretiva" TEXT NOT NULL,
    "TempoParado" TIMESTAMP NOT NULL,
    "Responsavel" VARCHAR(100) NOT NULL,              
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL DEFAULT TRUE,
    "EquipamentoId" UUID NOT NULL, 
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
);

INSERT INTO public."HistoricoFalhas" (  "Id",
                                        "DataFalha",
                                        "Descricao",
                                        "CausaProvavel",
                                        "AcaoCorretiva",
                                        "TempoParado",
                                        "Responsavel",
                                        "DataCriacao",
                                        "Ativo",
                                        "EquipamentoId") 
VALUES 
(
    'd37298b7-ccf7-4589-928a-1a84842ab2c4',
    CURRENT_TIMESTAMP,
    'Vazamento no selo mecânico',
    'Desgaste por fadiga do material',
    'Substituição do selo e lubrificação',
    CURRENT_TIMESTAMP,
    'Eng. João Silva',
    CURRENT_TIMESTAMP,
    TRUE,
    'd622734e-f6e1-4f75-bff6-a8e3f9c5e9aa' -- ID de um equipamento
),
(
    '5998810b-0807-4266-8867-016ad2e685d5',
    CURRENT_TIMESTAMP,
    'Superaquecimento da válvula',
    'Falha no sistema de refrigeração',
    'Limpeza das aletas e troca do fluido',
    CURRENT_TIMESTAMP,
    'Técnica Maria Oliveira',
    CURRENT_TIMESTAMP,
    TRUE,
    '247ef7c3-f5b3-4b7f-81d7-117047e27bd4' -- ID Equipamento
);