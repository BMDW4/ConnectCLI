
import argparse
import msal
import json
import sys
import logging
import requests
import os
import uuid
from azure.servicebus import ServiceBusClient, ServiceBusMessage
from azure.identity import EnvironmentCredential, ClientSecretCredential
from azure.servicebus.exceptions import ServiceBusError
from azure.core.exceptions import ClientAuthenticationError


def ack(client_id, client_secret, message): 
    with open('appsettings.'+str(env)+'.json', 'r') as f:
        config = json.load(f)

    tenant_id = config["tenant_id"]

    app = msal.ConfidentialClientApplication(
        client_id=client_id,
        client_credential=client_secret,
        authority=config["authority"]
        )
    
    result = None

    result = app.acquire_token_silent(config["scope"], account=None)

    if not result:
        logging.info("No suitable token exists in cache. Let's get a new one from AAD.")
        result = app.acquire_token_for_client(scopes=config["scope"])


    if "access_token" in result:
        # Call a protected API with the access token.
        endpoint = config["endpoint"]+"/Ack"
        http_headers = {'Authorization': 'Bearer ' + result['access_token'],
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'}

        body = {"TargetRegistry":message["FromRegistry"], "CorrelationGuid": message["CorrelationGuid"]}
        body = json.dumps(body)
        data = requests.post(endpoint, data=body, headers=http_headers, stream=False)

        if data.status_code == 200:
            pass
        else:
            print('\x1b[0;37;41m')
            print("ACK SENDING ERROR:")
            print("Status code:", data.status_code)
            print("Body:\n" + data.text)
            print('\x1b[0m')
            sys.exit()
    
    else:
        print("Access token not found.")
        print(result.get("error"))
        print(result.get("error_description"))



def listen(client_id, client_secret, guid=None, timeout=None):
    
    with open('appsettings.'+str(env)+'.json', 'r') as f:
    #with open('../WmdaConnectCLIpy_unzipped/appsettings.'+str(env)+'.json', 'r') as f:
        config = json.load(f)

    tenant_id = config["tenant_id"]
    #client_id = config["client_id"]
    #client_secret = config["client_secret"]
    SERVICE_BUS_NAMESPACE = config["service_bus_namespace"]
    QUEUE_NAME = client_id

    credential = ClientSecretCredential(tenant_id=tenant_id, client_id=client_id, client_secret=client_secret)

    servicebus_client = ServiceBusClient(SERVICE_BUS_NAMESPACE, credential)


    with servicebus_client:
        receiver = servicebus_client.get_queue_receiver(queue_name=QUEUE_NAME, max_wait_time=timeout)
        try:
            with receiver:
                print('Listening...')
                print('Press CTRL-C to stop listening') 
                msg = False
                for msg in receiver:
                    if not msg.application_properties["MessageType".encode()]:
                        print("NO MESSAGE TYPE")

                    else:
                        message_type = msg.application_properties["MessageType".encode()]     # dictionary from application_properties is encoded in bytes, hence encode here.
                        message_type = message_type.decode()
                        message = json.loads(str(msg))

                        if message_type == "PingMessage":
                            print('\x1b[0;37;44m' + f"Received:\n{message}" + '\x1b[0m') # Blue back, white text
                            ack(client_id, client_secret, message)
                            receiver.complete_message(msg)

                        elif message_type == "AckMessage":
                            pass # listen command should ignore Ack messages

                        else:
                            print("UNHANDLED MESSAGE TYPE: " + message_type)
                            receiver.complete_message(msg)

        except ServiceBusError as ex:
            print('\x1b[0;37;41m')
            print("SERVICE BUS ERROR:")
            print(ex)
            print('\x1b[0m')
        except KeyboardInterrupt:
            print('Stopped receiving messages')
            sys.exit()

def listen_for_ack(client_id, client_secret, guid, timeout):
    ack_received=False
    with open('appsettings.'+str(env)+'.json', 'r') as f:
        config = json.load(f)

    tenant_id = config["tenant_id"]

    SERVICE_BUS_NAMESPACE = config["service_bus_namespace"]
    QUEUE_NAME = client_id

    credential = ClientSecretCredential(tenant_id=tenant_id, client_id=client_id, client_secret=client_secret)

    servicebus_client = ServiceBusClient(SERVICE_BUS_NAMESPACE, credential)

    with servicebus_client:
        receiver = servicebus_client.get_queue_receiver(queue_name=QUEUE_NAME, max_wait_time=timeout)
        try:
            with receiver:
                msg = False
                message_type = False
                for msg in receiver:
                    if not msg.application_properties["MessageType".encode()]:
                        print("NO MESSAGE TYPE")

                    else:
                        message_type = msg.application_properties["MessageType".encode()]  # dictionary from application_properties is encoded in bytes, hence encode here.
                        message_type = message_type.decode()
                        message = json.loads(str(msg))

                        if message_type == "PingMessage":
                            print('\x1b[0;37;44m' + f"Received:\n{message}" + '\x1b[0m') # Blue back, white text.
                            ack(client_id, client_secret, message)

                        elif message_type == "AckMessage":

                            if guid != message["CorrelationGuid"]:
                                print("Stale Message: " + message["CorrelationGuid"])
                            else:
                                print('\x1b[0;37;45m' + "Message Acknowledged: " + message["CorrelationGuid"]+ '\x1b[0m') # Pink back, white text.
                                ack_received=True
                                receiver.complete_message(msg)
                                break
                                # done
                        else:
                            print("UNHANDLED MESSAGE TYPE: " + message_type)
                    receiver.complete_message(msg)

                if not ack_received:
                    print('\x1b[0;31;40m' + "Acknowledgement not received." + '\x1b[0m') # Red text.

        except ServiceBusError as ex:
            print('\x1b[0;37;41m') # Red back, white text.
            print("SERVICE BUS ERROR:")
            print(ex)
            print('\x1b[0m')
            
def ping(client_id, client_secret, target_registry):

    with open('appsettings.'+str(env)+'.json', 'r') as f:
        config = json.load(f)

    tenant_id = config["tenant_id"]

    app = msal.ConfidentialClientApplication(
        client_id=client_id,
        client_credential=client_secret,
        authority=config["authority"]
        )

    result = None

    result = app.acquire_token_silent(config["scope"], account=None)

    if not result:
        logging.info("No suitable token exists in cache. Let's get a new one from AAD.")
        result = app.acquire_token_for_client(scopes=config["scope"])
    if "access_token" in result:
        # Call a protected API with the access token.

        endpoint = config["endpoint"]+"/Ping"
        http_headers = {'Authorization': 'Bearer ' + result['access_token'],
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'}
        guid = str(uuid.uuid1())
        body = {"TargetRegistry":target_registry, "CorrelationGuid": guid}
        body = json.dumps(body)
        data = requests.post(endpoint, data=body, headers=http_headers, stream=False)
        if data.status_code == 200:
            print('\x1b[0;37;42m' + "Message sent.") # Green back, white text.
            print(body + '\x1b[0m')
        else:
            print('\x1b[0;37;41m') # Red back, white text.
            print("PING SENDING ERROR:")
            print("Status code:", data.status_code)
            print("Body:\n" + data.text)
            print('\x1b[0m')
            sys.exit()
    
    else:
        print(result.get("error"))
        print(result.get("error_description"))

    timeout = 10 # waiting for ack timeout in seconds.

    listen_for_ack(client_id, client_secret, guid, timeout)


def main(command_line=None):
    os.system('color')

    parser = argparse.ArgumentParser('Ping or Listen to another registry.')
   
    subparsers = parser.add_subparsers(dest='command')
    ping_parser = subparsers.add_parser('ping', help='ping another registry')
    ping_parser.add_argument('-c', '--clientId', dest='client_id', action='store', type=str, required=True, help='From Registry/Client Id')
    ping_parser.add_argument('-s', '--clientSecret', dest='client_secret', action='store', type=str, required=True, help='Client Secret')
    ping_parser.add_argument('-t', '--targetRegistry', dest='target_registry', action='store', type=str, required=True, help='Target Registry')
    ping_parser.add_argument('-e', '--env', dest='env', action='store', type=str, required=True, help='Environment')

    listen_parser = subparsers.add_parser('listen', help='listen for pings')
    listen_parser.add_argument('-c', '--clientId', dest='client_id', action='store', type=str, required=True, help='Client Id')
    listen_parser.add_argument('-s', '--clientSecret', dest='client_secret', action='store', type=str, required=True, help='Client Secret')    
    listen_parser.add_argument('-e', '--env', dest='env', action='store', type=str, required=True, help='Environment')

    args = parser.parse_args(command_line)
    global env 
    env = args.env
    if args.command == 'ping':
        ping(args.client_id, args.client_secret, args.target_registry)
    elif args.command == 'listen':
        listen(args.client_id, args.client_secret)
    else:
        print("'"+args.command+"'", "command not valid")


if __name__ == '__main__':
    main()

