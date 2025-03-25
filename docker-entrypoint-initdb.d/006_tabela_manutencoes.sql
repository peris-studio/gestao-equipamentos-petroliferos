CREATE TABLE public."Manutencoes" (
    "Id" UUID PRIMARY KEY,
    "TipoManutencao" INTEGER NOT NULL,
    "Descricao" TEXT NOT NULL,               
    "DataAgendada" DATE NOT NULL,         
    "DataExecucao" DATE NOT NULL,               
    "CustoManutencao" DECIMAL NOT NULL,                   
    "StatusManutencao" INTEGER NOT NULL,  
    "ProximaManutencao" DATE,                         
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL,
    "EquipamentoId" UUID NOT NULL,
    CONSTRAINT "FK_Equipamentos" FOREIGN KEY ("EquipamentoId") REFERENCES public."Equipamentos"("Id")
);

INSERT INTO public."Manutencoes" ( "Id",
                                   "TipoManutencao",
                                   "Descricao",
                                   "DataAgendada",
                                   "DataExecucao",
                                   "CustoManutencao",
                                   "StatusManutencao",
                                   "ProximaManutencao",
                                   "DataCriacao",
                                   "Ativo",
                                   "EquipamentoId")

VALUES 
(
    'bf5d59f0-7fa7-497e-b3ac-42c8f74dcd4a',
    5,
    'pipipopo',
    '2025-02-02',
    '2025-02-02',
    300.000,
    1,  
    '2025-03-02',
    CURRENT_TIMESTAMP,
    TRUE,
    'd622734e-f6e1-4f75-bff6-a8e3f9c5e9aa'
),
(
    'c27dc614-824c-46f2-85b3-4039132c0f96',
    2,
    'pipipipopopo',
    '2024-12-12',
    '2025-02-12',
    4560.000,
    3,  
    '2025-04-04',
    CURRENT_TIMESTAMP,
    TRUE,
    '247ef7c3-f5b3-4b7f-81d7-117047e27bd4'
);