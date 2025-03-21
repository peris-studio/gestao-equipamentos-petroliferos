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
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id"),
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
    'b4d87531-4fbc-44ae-99ed-5918a3c441ba',
    2,  -- Valor correspondente a TipoAlerta.Vazamento
    'Vazamento no tanque principal por desgaste de peça',
    2,  -- Valor correspondente a StatusAlerta.EmAndamento
    4,  -- Valor correspondente a PrioridadeAlerta.Critica
    CURRENT_TIMESTAMP,
    TRUE,
    'd622734e-f6e1-4f75-bff6-a8e3f9c5e9aa',  -- Equipamento existente
    '80c73dfb-f2d6-4e74-b9cd-e2c3465dc5c8'  -- Peça existente
),
(
    '30c64b73-041f-4cb3-933c-8a0390b79830',
    5,  -- Valor correspondente a TipoAlerta.TemperaturaExcedida
    'Troca de filtro atrasada',
    0,  -- Valor correspondente a StatusAlerta.Aberto
    3,  -- Valor correspondente a PrioridadeAlerta.Alta
    CURRENT_TIMESTAMP,
    TRUE,
    '247ef7c3-f5b3-4b7f-81d7-117047e27bd4',  -- Equipamento existente
    'c9274614-7305-4913-a8cc-39eb790e9d4c'  -- Peça existente
);