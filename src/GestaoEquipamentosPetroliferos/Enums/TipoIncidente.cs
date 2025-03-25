namespace GestaoEquipamentosPetroliferos.Enums;

public enum TipoIncidente
{
    // Incidentes de Segurança
    VazamentoGas,               // Vazamento de gás inflamável/tóxico
    Incendio,                   // Fogo não controlado
    Explosao,                   // Detonação não planejada
    AcessoNaoAutorizado,        // Violação de área restrita

    // Incidentes Ambientais
    VazamentoOleo,              // Liberação de hidrocarbonetos
    VazamentoQuimico,           // Produtos químicos perigosos
    ContaminacaoSolo,           // Poluição terrestre
    ContaminacaoAquatica,        // Poluição de corpos d'água

    // Falhas Operacionais
    FalhaEquipamentoCritico,    // Parada de equipamento essencial
    RompimentoDuto,             // Ruptura de pipeline
    FalhaSistemaControle,       // Erro em sistemas automatizados
    PerdaPressao,               // Queda anormal de pressão

    // Incidentes de Processo
    Blowout,                    // Perda de controle de poço
    Overpressurizacao,          // Excesso de pressão em vasos
    CorrosaoAcelerada,          // Degradação estrutural crítica

    // Segurança Cibernética
    AtaqueCibernetico,          // Comprometimento de sistemas SCADA
    FalhaComunicacao,           // Perda de monitoramento remoto

    // Incidentes Humanos
    ErroOperacional,            // Falha humana no processo
    ViolacaoProcedimento,       // Desvio de protocolos

    // Outros
    NearMiss,                   // Quase acidente (sem consequências)
    EmergenciaMedica,           // Acidentes com pessoal
    NaoIdentificado,            // Causa inicial desconhecida
    Outro                       // Casos não categorizados
}