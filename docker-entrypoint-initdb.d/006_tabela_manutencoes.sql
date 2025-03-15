CREATE TABLE public."Manutencoes" (
    "Id" UUID PRIMARY KEY,
    "TipoManutencao" INTEGER NOT NULL,
    "Descricao" TEXT NOT NULL,               
    "DatAgendada" DATE NOT NULL,         
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