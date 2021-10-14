namespace WmdaConnect.Models.MessageBases
{
    /// <summary>
    /// Message types supported by WmdaConnect
    /// </summary>
    /// <remarks>Note that the Swagger Example text will</remarks>
    public enum MessageTypes
    {
        Ping = 1,
        Ack,
        TypingRequest,
        TypingResponse,
        SampleRequest,
        SampleArrival,
        SampleResponse,
        SampleInfo,
        TextMessage,
        Warning,
        RequestCancellation,
        NoResult,
        MessageAcknowledgement,
        MessageDenial,
        ResultReminder,
        CordBloodUnitReportRequest,
        InfectiousDiseaseMarkerRequest,
        InfectiousDiseaseMarkerResult,
        ReservationRequest,
        ReservationResult


    }
}