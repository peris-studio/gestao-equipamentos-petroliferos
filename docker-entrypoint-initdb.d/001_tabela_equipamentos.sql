CREATE TABLE public."Equipamentos" (
    "Id" UUID PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "TipoEquipamento" INTEGER NOT NULL,               
    "FabricanteEquipamento" INTEGER NOT NULL,         
    "NumeroSerie" VARCHAR(50) NOT NULL,               
    "DataInstalacao" DATE NOT NULL,                   
    "DataUltimaManutencao" DATE NOT NULL,             
    "LocalizacaoEquipamento" INTEGER NOT NULL,        
    "StatusOperacionalEquipamento" INTEGER NOT NULL,  
    "CapacidadeMaxima" DECIMAL(18,2),
    "Especificacoes" TEXT NOT NULL,                  
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL
);

INSERT INTO public."Equipamentos" ("Id",
                                   "Nome",
                                   "TipoEquipamento",
                                   "FabricanteEquipamento",
                                   "NumeroSerie",
                                   "DataInstalacao",
                                   "DataUltimaManutencao",
                                   "LocalizacaoEquipamento",
                                   "StatusOperacionalEquipamento",
                                   "CapacidadeMaxima",
                                   "Especificacoes",
                                   "DataCriacao",
                                   "Ativo") VALUES 
(
    'd622734e-f6e1-4f75-bff6-a8e3f9c5e9aa',
    'Bomba Centrífuga XPTO-3000',
    0,  -- Valor correspondente a TipoEquipamento.BombaCentrifuga
    2,  -- Valor correspondente a FabricanteEquipamento.BakerHughes
    'SN-ACME-2024-001',
    '2024-01-15',
    '2024-06-01',
    0,  -- Valor correspondente a LocalizacaoEquipamento.PlataformaMaritima
    0,  -- Valor correspondente a StatusOperacionalEquipamento.Operacional
    1500.50,
    'Bomba de alta pressão com revestimento anticorrosivo',
    CURRENT_TIMESTAMP,
    TRUE
),
(
    '247ef7c3-f5b3-4b7f-81d7-117047e27bd4',
    'Válvula de Controle VMAX',
    1,  -- Valor correspondente a TipoEquipamento.ValvulaControle
    8,  -- Valor correspondente a FabricanteEquipamento.Petrobras
    'SN-PT-2023-045',
    '2023-11-20',
    '2024-05-15',
    1,  -- Valor correspondente a LocalizacaoEquipamento.Refinaria
    1,  -- Valor correspondente a StatusOperacionalEquipamento.EmManutencaoPreventiva
    500.00,
    'Válvula reguladora de fluxo para alta vazão',
    CURRENT_TIMESTAMP,
    TRUE
);