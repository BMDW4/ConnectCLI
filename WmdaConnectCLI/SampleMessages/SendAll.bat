@pushd %~dp0..
cls
@ECHO [1. Remember to update the env, clientId and clientSecret]
@ECHO [2. Check that the recipient key in the sample messages refers to a registry set up in your environment]
@ECHO [3. run wm listen with clientId of recipient registry to simulate a recipient registry that will acknowledge the messages]
REM Status: { "DisplayName": "", "ClientId": "9e940c2c-c5f2-455a-80bb-9efe9fc73c81", "ClientSecret": "c40bf7bb-a77c-450f-bd8e-1be0f9d279fa", "QueueName": "9998" }
wm connect --clientId 9e940c2c-c5f2-455a-80bb-9efe9fc73c81 --ClientSecret c40bf7bb-a77c-450f-bd8e-1be0f9d279fa --env stn7
@pause
wm message --messageType TextMessage --file "./SampleMessages/TextMessage.json"
@pause
wm message --messageType TypingRequest --file "./SampleMessages/TypingRequest.json"
@pause
wm message --messageType TypingResponse --file "./SampleMessages/TypingResponse.json"
@pause
wm message --messageType SampleRequest --file "./SampleMessages/SampleRequest.json"
@pause
wm message --messageType SampleArrival --file "./SampleMessages/SampleArrival.json"
@pause
wm message --messageType SampleResponse --file "./SampleMessages/SampleResponse.json"
@pause
wm message --messageType CordBloodUnitReportRequest --file "./SampleMessages/CordBloodUnitReportRequest.json"
@pause
wm message --messageType InfectiousDiseaseMarkerRequest --file "./SampleMessages/InfectiousDiseaseMarkerRequest.json"
@pause
wm message --messageType InfectiousDiseaseMarkerResult --file "./SampleMessages/InfectiousDiseaseMarkerResult.json"
@pause
wm message --messageType MessageAcknowledgement --file "./SampleMessages/MessageAcknowledgement.json"
@pause
wm message --messageType MessageDenial --file "./SampleMessages/MessageDenial.json"
@pause
wm message --messageType NoResult --file "./SampleMessages/NoResult.json"
@pause
wm message --messageType RequestCancellation --file "./SampleMessages/RequestCancellation.json"
@pause
wm message --messageType ReservationRequest --file "./SampleMessages/ReservationRequest.json"
@pause
wm message --messageType ReservationResult --file "./SampleMessages/ReservationResult.json"
@pause
wm message --messageType ResultReminder --file "./SampleMessages/ResultReminder.json"
@pause
wm message --messageType SampleInfo --file "./SampleMessages/SampleInfo.json"
@pause
wm message --messageType Warning --file "./SampleMessages/Warning.json"
@popd
@pause