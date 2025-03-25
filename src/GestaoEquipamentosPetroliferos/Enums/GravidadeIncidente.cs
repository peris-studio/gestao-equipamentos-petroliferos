namespace GestaoEquipamentosPetroliferos.Enums;

public enum GravidadeIncidente
{
    NenhumImpacto,          // Registro informativo sem consequências
    Leve,                   // Impacto localizado, sem parada operacional
    Moderado,               // Parada parcial (< 24h), pequeno impacto ambiental
    Grave,                  // Parada total (> 24h), vazamento controlado
    Catastrofico,           // Danos permanentes, vazamento incontrolado
    NearMiss,               // Quase acidente (requer investigação)
    EmInvestigacao,         // Severidade ainda não determinada
    NaoClassificado,        // Para incidentes antigos sem classificação
    RequerNotificacaoANP,   // Incidentes que exigem comunicação à Agência Nacional do Petróleo
    EmergenciaAmbiental
}