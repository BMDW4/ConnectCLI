@pushd %~dp0..
cls
@ECHO [1. Remember to update the env, clientId and clientSecret]
@ECHO [2. that recipient registry (i.e. the listener) and the registry doing the sending correspond in the sample messages json]
SET clientId=<clientId (Guid)>
SET clientSecret=<clientSecret (Guid or complex text)>
SET environment=<environment number (integer value)>

wm listen --clientId %clientId% --clientSecret %clientSecret% --env %environment%

@popd
@pause
