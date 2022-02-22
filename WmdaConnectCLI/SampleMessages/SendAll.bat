@pushd %~dp0..
cls
@ECHO [1. Remember to update the env, clientId and clientSecret]
@ECHO [2. Check that the recipient key in the sample messages refers to a registry set up in your environment]
@ECHO [3. run wm listen with clientId of recipient registry to simulate a recipient registry that will acknowledge the messages]
SET clientId=<clientId (Guid)>
SET clientSecret=<clientSecret (Guid or complex text)>
SET environment=<environment number (integer value)>
SET targetRegistry=<registry ion to send to e.g. 1234>

wm connect --clientId %clientId% --clientSecret %clientSecret% --environment %environment%
@pause
wm message --messageType TextMessage --file "./SampleMessages/TextMessage.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType TypingRequest --file "./SampleMessages/TypingRequest.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType TypingResponse --file "./SampleMessages/TypingResponse.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType SampleRequest --file "./SampleMessages/SampleRequest.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType SampleArrival --file "./SampleMessages/SampleArrival.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType SampleResponse --file "./SampleMessages/SampleResponse.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType CordBloodUnitReportRequest --file "./SampleMessages/CordBloodUnitReportRequest.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType InfectiousDiseaseMarkerRequest --file "./SampleMessages/InfectiousDiseaseMarkerRequest.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType InfectiousDiseaseMarkerResult --file "./SampleMessages/InfectiousDiseaseMarkerResult.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType MessageAcknowledgement --file "./SampleMessages/MessageAcknowledgement.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType MessageDenial --file "./SampleMessages/MessageDenial.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType NoResult --file "./SampleMessages/NoResult.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType RequestCancellation --file "./SampleMessages/RequestCancellation.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType ReservationRequest --file "./SampleMessages/ReservationRequest.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType ReservationResult --file "./SampleMessages/ReservationResult.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType ResultReminder --file "./SampleMessages/ResultReminder.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType SampleInfo --file "./SampleMessages/SampleInfo.json" --targetRegistry %targetRegistry%
@pause
wm message --messageType Warning --file "./SampleMessages/Warning.json" --targetRegistry %targetRegistry%
@popd
@pause