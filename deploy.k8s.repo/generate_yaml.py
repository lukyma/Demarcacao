import yaml
import io
import sys
import os
import base64
from yaml.resolver import BaseResolver

for arg in sys.argv:
    if arg.startswith("--deployfile"):
        deployfile = arg.split("=")[1]
        break

try:
    deployfile
except NameError:
    deployfile = None

if deployfile is None:
    sys.exit("O argumento --deployfile não foi informado. Exemplo: --deployfile=deploy.yaml")

print("Iniciando montagem dos arquivos .yaml para deploy")

if os.path.exists(deployfile) is False:
    sys.exit("O arquivo " + deployfile + " não foi encontrado. "
             "Verifique se o arquivo \"deploy_<--deployfile=value>.yaml\" está no seu projeto. ")


deploy_yaml_file = open(deployfile)
obj_deploy = yaml.load(deploy_yaml_file, Loader=yaml.FullLoader)
k8s_service_yaml_file = open("configk8s_service.yaml")
k8s_deployment_yaml_file = open("configk8s_deployment.yaml")
k8s_ingress_yaml_file = open("configk8s_ingress.yaml")
k8s_secret_yaml_file = open("configk8s_secret.yaml")
k8s_service = yaml.load(k8s_service_yaml_file, Loader=yaml.FullLoader)
k8s_deployment = yaml.load(k8s_deployment_yaml_file, Loader=yaml.FullLoader)
k8s_ingress = yaml.load(k8s_ingress_yaml_file, Loader=yaml.FullLoader)
k8s_secret = yaml.load(k8s_secret_yaml_file, Loader=yaml.FullLoader)
deployServiceConfig = obj_deploy["deploy"]["config"]
deploySeriviceEnv = obj_deploy["deploy"]["env"]

print("Montando arquivo service.yaml")
k8s_service["metadata"]["name"] = deployServiceConfig["alias"] + "-service"
k8s_service["spec"]["selector"]["app"] = deployServiceConfig["alias"]
k8s_service["metadata"]["labels"]["app"] = deployServiceConfig["alias"]


#Configurando arquivo deployment.yaml
print("Montando arquivo deployment.yaml")
k8s_deployment["metadata"]["name"] = deployServiceConfig["alias"] + "-deployment"
k8s_deployment["metadata"]["labels"]["app"] = deployServiceConfig["alias"]
k8s_deployment["spec"]["replicas"] = deployServiceConfig["replicas"]
k8s_deployment["spec"]["selector"]["matchLabels"]["app"] = deployServiceConfig["alias"]
k8s_deployment["spec"]["template"]["metadata"]["labels"]["app"] = deployServiceConfig["alias"]
for element in k8s_deployment["spec"]["template"]["spec"]["affinity"]["podAntiAffinity"]["preferredDuringSchedulingIgnoredDuringExecution"]:
    for element2 in element["podAffinityTerm"]["labelSelector"]["matchExpressions"]:
        element2["values"] = [deployServiceConfig["alias"]]

class SecretKeyRef:
    def __init__(self, name, key):
        self.name = name
        self.key = key
    name: str
    key: str
class ValueFrom:
    secretKeyRef: SecretKeyRef
class Env:
    name: str
    valueFrom: ValueFrom

yaml.add_representer(SecretKeyRef, lambda dumper, data: dumper.represent_mapping(
    BaseResolver.DEFAULT_MAPPING_TAG, data.__dict__))

yaml.add_representer(ValueFrom, lambda dumper, data: dumper.represent_mapping(
    BaseResolver.DEFAULT_MAPPING_TAG, data.__dict__))

for element in k8s_deployment["spec"]["template"]["spec"]["containers"]:
    element["name"] = deployServiceConfig["alias"]
    element["image"] = deployServiceConfig["regisgry"] + "/" + deployServiceConfig["image"] + ":latest"
    try:
     element["readinessProbe"]["httpGet"]["path"] = deployServiceConfig["health"]["urn_ready"]
     element["livenessProbe"]["httpGet"]["path"] = deployServiceConfig["health"]["urn_live"]
    except KeyError as e:
     sys.exit("Urn's de healthcheck não foram informadas: " + ';'.join(map(str, e.args)))
    element["readinessProbe"]["httpGet"]["path"] = deployServiceConfig["health"]["urn_ready"]
    element["livenessProbe"]["httpGet"]["path"] = deployServiceConfig["health"]["urn_live"]
    if len(deploySeriviceEnv) > 0:
        element["env"] = []
        for env in deploySeriviceEnv:
            envObject = Env()
            envObject.name = env
            envObject.valueFrom = ValueFrom()
            name = deployServiceConfig["alias"] + "-secret"
            envObject.valueFrom.secretKeyRef = SecretKeyRef(name, env)
            element["env"].append(envObject.__dict__)
        

#------------------------------

# Configurando ingress
print("Preenchendo itens do ingress.yaml")
k8s_ingress["metadata"]["name"] = deployServiceConfig["alias"] + "-ingress"
for element in k8s_ingress["spec"]["tls"]:
    element["hosts"] = [deployServiceConfig["host"]]

for element in k8s_ingress["spec"]["rules"]:
    element["host"] = deployServiceConfig["host"]
    for element2 in element["http"]["paths"]:
        try:
            element2["path"] = deployServiceConfig["basePath"]
        except Exception as e:
            element2["path"] = "/"
            print("A chave \"basePath\" não foi encontrada. O valor default é /")
        element2["backend"]["service"]["name"] = deployServiceConfig["alias"] + "-service"
#------------------------------

# Configurando secret
print("Preenchendo itens do secret.yaml")
print("Preenchendo itens do secret.yaml")
k8s_secret["metadata"]["name"] = deployServiceConfig["alias"] + "-secret"
k8s_secret["data"] = {}
if len(deploySeriviceEnv) > 0:
    for env in deploySeriviceEnv:
        if deploySeriviceEnv[env] is not None:
            value = str(deploySeriviceEnv[env])
            deploySeriviceEnv[env] = base64.b64encode(value.encode()).decode()
            k8s_secret["data"][env] = deploySeriviceEnv[env]
#------------------------------

deploy_yaml_file.close()
k8s_service_yaml_file.close()
k8s_deployment_yaml_file.close()
k8s_secret_yaml_file.close()

with io.open('k8s_service.yaml', 'w', encoding='utf8') as outfile:
    yaml.dump(k8s_service, outfile, default_flow_style=False, allow_unicode=True)

with io.open('k8s_deployment.yaml', 'w', encoding='utf8') as outfile:
    yaml.dump(k8s_deployment, outfile, default_flow_style=False, allow_unicode=True)

with io.open('k8s_ingress.yaml', 'w', encoding='utf8') as outfile:
    yaml.dump(k8s_ingress, outfile, default_flow_style=False, allow_unicode=True)

with io.open('k8s_secret.yaml', 'w', encoding='utf8') as outfile:
    yaml.dump(k8s_secret, outfile, default_flow_style=False, allow_unicode=True)