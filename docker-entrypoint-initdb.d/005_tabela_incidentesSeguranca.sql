CREATE TABLE public."IncidentesSeguranca" (
    "Id" UUID PRIMARY KEY,
    "DataIncidente" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "TipoIncidente" INTEGER NOT NULL,
    "Descricao" TEXT NOT NULL,
    "GravidadeIncidente" INTEGER NOT NULL,               
    "CausaRaiz" TEXT NOT NULL,         
    "AcaoCorretiva" TEXT NOT NULL,    
    "Responsavel" VARCHAR(225) NOT NULL,
    "DataInvestigacao" DATE NOT NULL,
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL,
    "InvestigacaoConcluida" BOOLEAN,
    "EquipamentoId" UUID NOT NULL,
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
);

INSERT INTO public."IncidentesSeguranca" (  "Id",
											"DataIncidente",
                                            "TipoIncidente",
                                            "Descricao",
                                            "GravidadeIncidente",
                                            "CausaRaiz",
                                            "AcaoCorretiva",
                                            "Responsavel",
                                            "DataInvestigacao",
                                            "DataCriacao",
                                            "Ativo",
                                            "EquipamentoId") 
VALUES 
(
    '16dddf11-13c2-4386-bc9e-962b826c596b',
	CURRENT_TIMESTAMP,	
    4,
    'Vazamento de óleo na área de operação',
    3,
    'Falha na vedação da bomba',
    'Isolamento da área e substituição da junta',
    'Eng. Safety Oliveira',
    '2024-08-11',
    CURRENT_TIMESTAMP,
    TRUE,
    '247ef7c3-f5b3-4b7f-81d7-117047e27bd4'  -- ID de equipamento existente
);