CREATE TABLE public."Pecas" (
    "Id" UUID PRIMARY KEY,
    "Nome" VARCHAR(225) NOT NULL,
    "Descricao" TEXT NOT NULL,               
    "Numeracao" VARCHAR(50) NOT NULL,                  
    "FornecedorPecas" INTEGER NOT NULL,        
    "QuantidadeEstoque" INTEGER NOT NULL,  
    "PrecoUnitario" DECIMAL(18,2),
    "EquipamentoCompativel" TEXT NOT NULL,                  
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DataAtualizacao" TIMESTAMP,
    "DataDelecao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL
);

INSERT INTO public."Pecas" ("Id",
                            "Nome",
                            "Descricao",
                            "Numeracao",
                            "FornecedorPecas",
                            "QuantidadeEstoque",
                            "PrecoUnitario",
                            "EquipamentoCompativel",
                            "DataCriacao",
                            "Ativo") 
VALUES 
(
    '80c73dfb-f2d6-4e74-b9cd-e2c3465dc5c8',
    'Junta Tórica 150mm',
    'Junta para vedação de alta pressão',
    'JT-150-2024',
    4,  -- Valor correspondente a FornecedorPecas.GEOilGas
    200,
    89.90,
    'Bomba XPTO-3000; Válvula VMAX',
    CURRENT_TIMESTAMP,
    TRUE
),
(
    'c9274614-7305-4913-a8cc-39eb790e9d4c',
    'Válvula de Segurança 200mm',
    'Válvula de segurança de alta pressão para sistemas de válvulas industriais.',
    'Vs-200-2025',
    7,  -- Valor correspondente a FornecedorPecas.Petrobras
    5000,
    89.90,
    'Bomba XPTO-3000; Válvula VMAX; Sistema de Redução de Pressão PRS-4000',
    CURRENT_TIMESTAMP,
    TRUE
);