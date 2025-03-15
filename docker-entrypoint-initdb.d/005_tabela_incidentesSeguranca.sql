CREATE TABLE public."IncidentesSeguranca" (
    "Id" UUID PRIMARY KEY,
    "DataFalha" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "Descricao" TEXT NOT NULL,
    "GravidadeIncidente" INTEGER NOT NULL,               
    "CausaRaiz" TEXT NOT NULL,         
    "AcaoCorretiva" TEXT NOT NULL,    
    "Responsavel" VARCHAR(225) NOT NULL,
    "DataInvestigacao" DATE,                 
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL
    "InvestigacaoConcluida" BOOLEAN NOT NULL,
    "EquipamentoId",
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
);

INSERT INTO public."IncidentesSeguranca" (  "DataFalha",
                                            "Descricao",
                                            "GravidadeIncidente",
                                            "CausaRaiz",
                                            "AcaoCorretiva",
                                            "Responsavel",
                                            "DataInvestigacao",
                                            "DataCriacao",
                                            "DataAtualizacao",
                                            "DataDelecao",
                                            "Ativo",
                                            "InvestigacaoConcluida",
                                            "EquipamentoId") 
VALUES 
(
    '2024-07-10 08:45:00',
    'Vazamento de óleo na área de operação',
    2,
    'Falha na vedação da bomba',
    'Isolamento da área e substituição da junta',
    'Eng. Safety Oliveira',
    '2024-08-10',



    TRUE,
    TRUE,
    '',  -- ID de equipamento existente
);