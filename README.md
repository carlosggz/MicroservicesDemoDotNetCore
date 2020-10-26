# Microservices Demo using c#

Example of a microservices application using best practices:
* Api gateway re-routing, aggregates, handler delegation and secured entrypoints  (using Ocelot)
* Service Discovery (using Consul)
* Quality of Service (using Polly)
* Asynchronous communication (using Rabbitmq)

## Instructions to run on docker

Clone the repository and run composer: 

docker-compose up --build

The api gateway will be available at http://localhost:8000.

Import the postman collection to test the api.

## Instructions to run on kubernetes

Clone the repository and build the docker images:

docker-compose build

Move to the k8s folder and add the deploys and services to the kubernete cluster.

For windows there are 3 scripts but are the same for other operating systems due they use the kubectl command:
* add.ps1 to add the deployments and services. It adds 2 replicas of movies and actors services, declare the api gateway (and consul and rabbit just as examples) as a load balancer service.
* remove.ps1 to remove all items
* list.ps1 to list the pods, services and deployments

