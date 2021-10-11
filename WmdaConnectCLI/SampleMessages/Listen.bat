@pushd %~dp0..
cls
@ECHO [1. Remember to update the env, clientId and clientSecret]
@ECHO [2. that recipient registry (i.e. the listener) and the registry doing the sending correspond in the sample messages json]
REM Status: { "DisplayName": "", "ClientId": "bdec1779-8a0a-4f86-a85d-b52d14de9403", "ClientSecret": "d283fa9d-ba55-42ef-9ced-5882b31686e0", "QueueName": "9999" }
wm listen --clientId bdec1779-8a0a-4f86-a85d-b52d14de9403 --ClientSecret d283fa9d-ba55-42ef-9ced-5882b31686e0 --env stn7
@popd
@pause
