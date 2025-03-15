CREATE TABLE public."Alertas" (
    "Id" UUID PRIMARY KEY,
    "TipoAlerta" INTEGER NOT NULL,
    "Mensagem" TEXT NOT NULL,
    "StatusAlerta" INTEGER NOT NULL,  
    "PrioridadeAlerta" INTEGER NOT NULL, 
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL,
    "EquipamentoId" UUID NOT NULL,
    "PecaId" UUID NOT NULL,
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
    CONSTRAINT "FK_Pecas" FOREIGN KEY ("PecaId") REFERENCES public."Pecas"("Id")
);

INSERT INTO public."Alertas" ("Id",
                              "TipoAlerta",
                              "Mensagem",
                              "StatusAlerta",
                              "PrioridadeAlerta",
                              "DataCriacao",
                              "Ativo",
                              "EquipamentoId",
                              "PecaId") 
VALUES
(
    '',
    1,  -- Valor correspondente a TipoAlerta.Critico
    'Vazamento no tanque principal',
    2,  -- Valor correspondente a StatusAlerta.EmAndamento
    0,  -- Valor correspondente a PrioridadeAlerta.Urgente
    CURRENT_TIMESTAMP,
    TRUE,
    '',  -- Equipamento existente
    '',  -- Peça existente
),

(
    '',
    2,  -- Valor correspondente a TipoAlerta.Preventivo
    'Troca de filtro atrasada',
    1,  -- Valor correspondente a StatusAlerta.Aberto
    2,  -- Valor correspondente a PrioridadeAlerta.Baixa
    CURRENT_TIMESTAMP,
    TRUE,
    '',  -- Equipamento existente
    '',  -- Peça existente
);